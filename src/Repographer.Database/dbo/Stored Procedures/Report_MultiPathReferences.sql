CREATE PROCEDURE [dbo].[Report#MultiPathReferences]
	@ItemName varchar(1000) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @ProjectTypeID int = 2;
	DECLARE @AssemblyTypeID int = 3;

	WITH MultiPathReferenceCTE AS
	(
		SELECT
			Name
		FROM
			Node
		WHERE
			NodeTypeID IN (@ProjectTypeID, @AssemblyTypeID)
		GROUP BY
			Name
		HAVING
			COUNT(Path) > 1
	)

	SELECT
		o.Name as ItemName,
		o.Path as ReferencePath,
		i.Name as ReferenceBy,
		i.Path as ReferenceByPath
	FROM
		MultiPathReferenceCTE cte
		JOIN Node o ON o.Name = cte.Name
		JOIN Relation r ON r.ToNodeID = o.NodeID
		JOIN Node i ON i.NodeID = r.FromNodeID
	WHERE
		@ItemName IS NULL OR o.Name = @ItemName
	ORDER BY
		ItemName, ReferencePath, ReferenceByPath
END