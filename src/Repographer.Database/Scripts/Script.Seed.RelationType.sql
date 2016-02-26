MERGE INTO RelationType AS Target 
USING (
	VALUES 
	  (1, 'Member'), 
	  (2, 'Reference')
) 
AS Source (RelationTypeID, Name)
ON Target.RelationTypeID = Source.RelationTypeID 
WHEN MATCHED THEN 
	UPDATE SET Name = Source.Name
WHEN NOT MATCHED BY TARGET THEN 
	INSERT (RelationTypeID, Name) VALUES (RelationTypeID, Name) 
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;