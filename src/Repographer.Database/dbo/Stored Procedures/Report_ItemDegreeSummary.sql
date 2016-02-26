CREATE PROCEDURE [dbo].[Report#ItemDegreeSummary]
	@ItemName varchar(1000) = NULL
AS
BEGIN
	DECLARE @ProjectTypeID int = 2;
	DECLARE @AssemblyTypeID int = 3;

	WITH InCountCTE AS
	(
		SELECT
			o.NodeID,
			o.Name,
			o.Path,
			ISNULL(COUNT(i.NodeID), 0) as InCount
		FROM
			Node o
			LEFT JOIN Relation r ON r.ToNodeID = o.NodeID
			LEFT JOIN Node i ON i.NodeID = r.FromNodeID
		WHERE
			(@ItemName IS NULL OR o.Name = @ItemName)
			AND o.NodeTypeID IN (@ProjectTypeID, @AssemblyTypeID)
			AND (i.NodeTypeID IS NULL OR i.NodeTypeID = @ProjectTypeID)
		GROUP BY
			o.NodeID, o.Name, o.Path
	),
	OutCountCTE AS
	(
		SELECT
			i.NodeID,
			i.Name,
			i.Path,
			ISNULL(COUNT(o.NodeID), 0) as OutCount
		FROM
			Node i
			LEFT JOIN Relation r ON r.FromNodeID = i.NodeID
			LEFT JOIN Node o ON o.NodeID = r.ToNodeID
		WHERE
			(@ItemName IS NULL OR i.Name = @ItemName)
			AND i.NodeTypeID = @ProjectTypeID
			AND (o.NodeTypeID IS NULL OR o.NodeTypeID IN (@ProjectTypeID, @AssemblyTypeID))
		GROUP BY
			i.NodeID, i.Name, i.Path
	)

	SELECT
		ISNULL(i.Name, o.Name) as ItemName,
		ISNULL(i.Path, o.Path) as ItemPath,
		ISNULL(o.OutCount,0) as OutCount,
		ISNULL(i.InCount, 0) as InCount
	FROM
		InCountCTE i
		FULL JOIN OutCountCTE o ON o.NodeID = i.NodeID
	WHERE
		i.InCount > 0 OR o.OutCount > 0
	ORDER BY
		o.OutCount ASC, i.InCount DESC
END
