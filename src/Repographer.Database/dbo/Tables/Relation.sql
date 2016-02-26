CREATE TABLE [dbo].[Relation] (
    [RelationID]     INT IDENTITY (1, 1) NOT NULL,
    [RelationTypeID] INT NOT NULL,
    [FromNodeID]     INT NOT NULL,
    [ToNodeID]       INT NOT NULL,
    [Weight] FLOAT NULL, 
    CONSTRAINT [PK_Relation] PRIMARY KEY CLUSTERED ([RelationID] ASC),
    CONSTRAINT [FK_Relation_Node] FOREIGN KEY ([FromNodeID]) REFERENCES [dbo].[Node] ([NodeID]),
    CONSTRAINT [FK_Relation_Node1] FOREIGN KEY ([ToNodeID]) REFERENCES [dbo].[Node] ([NodeID]),
    CONSTRAINT [FK_Relation_RelationType] FOREIGN KEY ([RelationTypeID]) REFERENCES [dbo].[RelationType] ([RelationTypeID])
);

