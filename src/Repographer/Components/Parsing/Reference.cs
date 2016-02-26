using Repographer.DataAccess;
using Repographer.DataAccess.Graphing;

namespace Repographer.Components.Parsing
{
    public class Reference<TLineSource> : IReference
    {
        public string TypeGuid { get; private set; }

        public string Guid { get; private set; }

        public string Name { get; private set; }

        public string Path { get; private set; }

        public TLineSource Source { get; private set; }

        public RelationTypes RelationType { get; private set; }

        public Reference(string rootDirectory, string typeGuid, string name, string path, string guid, TLineSource source, RelationTypes relationType)
        {
            TypeGuid = typeGuid;
            Guid = guid;
            Name = name;
            RelationType = relationType;
            Source = source;
            Path = path == null ? null : path.Replace(rootDirectory, "");
        }

        public override string ToString()
        {
            return string.Format("Guid: {0}\tName: {1}\tPath: {2}", TypeGuid, Name, Path);
        }
    }
}