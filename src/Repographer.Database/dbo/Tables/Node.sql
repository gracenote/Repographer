CREATE TABLE [dbo].[Node] (
    [NodeID]     INT             IDENTITY (1, 1) NOT NULL,
    [NodeTypeID] INT             NOT NULL,
    [Name]       NVARCHAR (4000) NOT NULL,
    [Path]       NVARCHAR (4000) NULL,
    CONSTRAINT [PK_Node] PRIMARY KEY CLUSTERED ([NodeID] ASC),
    CONSTRAINT [FK_Node_NodeType] FOREIGN KEY ([NodeTypeID]) REFERENCES [dbo].[NodeType] ([NodeTypeID])
);

