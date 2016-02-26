namespace Repographer.DataAccess.Graphing
{
    public interface IReference
    {
        string TypeGuid { get; }
        string Guid { get; }
        string Name { get; }
        string Path { get; }

        RelationTypes RelationType { get; }
    }
}