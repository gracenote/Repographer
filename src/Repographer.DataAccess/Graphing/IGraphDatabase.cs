namespace Repographer.DataAccess.Graphing
{
    public interface IGraphDatabase
    {
        void Save(Graph graph);
        Graph Load();
    }
}