CREATE PROCEDURE [dbo].[Report#ProjectHomeSummary]
	@SolutionName varchar(1000) = NULL,
	@ProjectName varchar(1000) = NULL,
	@HomingOption bit = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @SolutionTypeID int = 1;
	DECLARE @ProjectTypeID int = 2;

	WITH ProjectHomeCTE AS
	(
		SELECT
			o.NodeID,
			o.Name,
			o.Path,
			COUNT(o.NodeID) as TotalCount
		FROM
			Relation r
			JOIN Node i ON 
				i.NodeID = r.FromNodeID
				AND i.NodeTypeID = @SolutionTypeID
			JOIN Node o ON 
				o.NodeID = r.ToNodeID
				AND o.NodeTypeID = @ProjectTypeID
		GROUP BY
			o.NodeID, o.Name, o.Path
		HAVING
			@HomingOption IS NULL
			OR (@HomingOption = 0 AND COUNT(r.ToNodeID) = 1)
			OR (@HomingOption = 1 AND COUNT(r.ToNodeID) > 1)
	)

	SELECT
		cte.Name as ProjectName,
		cte.Path as ProjectPath,
		cte.TotalCount,
		i.Name as SolutionName,
		i.Path as SolutionPath
	FROM
		Relation r
		JOIN ProjectHomeCTE cte ON
			cte.NodeID = r.ToNodeID
			AND (@ProjectName IS NULL OR cte.Name = @ProjectName)
		JOIN Node i ON
			i.NodeID = r.FromNodeID
			AND i.NodeTypeID = @SolutionTypeID
			AND (@SolutionName IS NULL OR i.Name = @SolutionName)
	ORDER BY
		cte.TotalCount DESC, ProjectName, SolutionName
END