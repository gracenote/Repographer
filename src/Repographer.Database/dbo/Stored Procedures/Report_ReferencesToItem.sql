CREATE PROCEDURE [dbo].[Report#ReferencesToItem]
	@ItemName varchar(1000) = NULL,
	@ShowProjects bit = 1,
	@ShowAssemblies bit = 1
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @ProjectTypeID int = 2;
	DECLARE @AssemblyTypeID int = 3;

	SELECT
		o.Name as ItemName,
		o.Path as ItemPath,
		i.Name as ReferenceName,
		i.Path as ReferencePath
	FROM
		Relation r
		JOIN Node i ON
			i.NodeID = r.FromNodeID
			AND i.NodeTypeID = @ProjectTypeID
		JOIN Node o ON
			o.NodeID = r.ToNodeID
			AND o.NodeTypeID IN (@ProjectTypeID, @AssemblyTypeID)
			AND
			(
				(@ShowProjects = 1 AND o.NodeTypeID = @ProjectTypeID) OR
				(@ShowAssemblies = 1 AND o.NodeTypeID = @AssemblyTypeID)
			)
	WHERE
		@ItemName IS NULL OR o.Name = @ItemName
	ORDER BY
		ItemName, ReferencePath
END