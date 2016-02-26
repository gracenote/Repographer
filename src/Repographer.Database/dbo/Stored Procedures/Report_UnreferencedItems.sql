CREATE PROCEDURE [dbo].[Report#UnreferencedItems]
	@ItemName varchar(1000) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @ProjectTypeID int = 2;
	DECLARE @AssemblyTypeID int = 3;

	SELECT
		o.Name as ItemName,
		o.Path as ItemPath
	FROM
		Node o
	WHERE 
		(@ItemName IS NULL OR o.Name = @ItemName)
		AND o.NodeTypeID IN (@ProjectTypeID, @AssemblyTypeID)
		AND NOT EXISTS (
			SELECT TOP 1 1 FROM Relation r WHERE r.ToNodeID = o.NodeID
		)
	ORDER BY
		ItemPath, ItemName
END
