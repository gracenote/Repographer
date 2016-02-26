using System.Collections.Generic;

namespace Repographer.DataAccess.Graphing
{
    public interface INode
    {
        string Name { get; }

        string FilePath { get; }

        NodeTypes NodeType { get; }

        IEnumerable<IReference> References { get; } 
    }
}