using System;
using System.Collections.Generic;
using System.Linq;

namespace Repographer.DataAccess.Graphing
{
    public class Graph
    {
        readonly List<Node> _nodes = new List<Node>();
        readonly List<Relation> _relations = new List<Relation>();

        public IEnumerable<Node> Nodes { get { return _nodes; } }

        public IEnumerable<Relation> Relations { get { return _relations; } }

        public Graph()
        {
        }

        internal Graph(IEnumerable<Node> nodes)
        {
            _nodes.AddRange(nodes);
        }

        public void AddNode(INode nodeImpl)
        {
            var node = _nodes.FirstOrDefault(n => n.Name == nodeImpl.Name && n.Path == nodeImpl.FilePath) ??
                new Node
                {
                    Name = nodeImpl.Name,
                    Path = nodeImpl.FilePath,
                    NodeTypeID = nodeImpl.NodeType,
                };

            if (!_nodes.Contains(node))
            {
                _nodes.Add(node);
            }

            foreach (var reference in nodeImpl.References)
            {
                var refNode = _nodes.FirstOrDefault(n => n.Name == reference.Name && n.Path == reference.Path) ??
                    new Node
                    {
                        Name = reference.Name,
                        Path = reference.Path,
                        NodeTypeID = 
                            !string.IsNullOrEmpty(reference.Guid)
                                ? NodeTypes.Project
                                : string.IsNullOrEmpty(reference.Path)
                                    ? NodeTypes.GAC
                                    : reference.Path.ToLower().Contains("packages")
                                        ? NodeTypes.NugetPackage
                                        : NodeTypes.Assembly
                    };

                if (!_nodes.Contains(refNode))
                {
                    _nodes.Add(refNode);
                }

                var relation = new Relation
                {
                    From = node,
                    To = refNode,
                    RelationTypeID = reference.RelationType
                };

                node.Out.Add(relation);
                refNode.In.Add(relation);
                _relations.Add(relation);
            }
        }

        public void ComputeWeights()
        {
            Relations.ToList().ForEach(r =>
            {
                r.Weight = 100d * (1 / Math.Sqrt(r.To.In.Count));
            });
        }
    }
}