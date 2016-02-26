MERGE INTO NodeType AS Target 
USING (
	VALUES 
	  (1, 'Solution'), 
	  (2, 'Project'),
	  (3, 'Assembly'),
	  (4, 'GAC'),
	  (5, 'NuGet Package')
) 
AS Source (NodeTypeID, Name)
ON Target.NodeTypeID = Source.NodeTypeID 
WHEN MATCHED THEN 
	UPDATE SET Name = Source.Name
WHEN NOT MATCHED BY TARGET THEN 
	INSERT (NodeTypeID, Name) VALUES (NodeTypeID, Name) 
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;