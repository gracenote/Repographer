#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Repographer.Components;
using Repographer.DataAccess;
using Repographer.DataAccess.Graphing;
using Repographer.Modules.Main;

namespace Repographer.Modules.Graph
{
    public class GraphExportAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IGraphDatabase _graphDatabase;
        private readonly string _creator;
        private readonly string _description;

        public GraphExportAction(
            ITraceWriter traceWriter,
            IGraphDatabase graphDatabase,
            string creator,
            string description
            )
        {
            _traceWriter = traceWriter;
            _graphDatabase = graphDatabase;
            _creator = creator;
            _description = description;
        }

        public void Execute()
        {
            _traceWriter.Output("Loading graph from database...");

            var graph = _graphDatabase.Load();

            _traceWriter.Output("Transforming nodes...");

            var nodes = graph.Nodes.Where(x => x.NodeTypeID == NodeTypes.Solution || x.NodeTypeID == NodeTypes.Project).ToList();

            var gexfNodes = nodes
                .Select(x =>
                    new nodecontent
                    {
                        id = x.NodeID.ToString(),
                        label = x.Name,
                        Items = new object[]
                        {
                            new attvaluescontent
                            {
                                attvalue = new[]
                                {
                                    new attvalue
                                    {
                                        @for = "0",
                                        value = Enum.GetName(typeof(NodeTypes), x.NodeTypeID)
                                    }
                                }
                            }
                        }
                    }).ToArray();

            _traceWriter.Output("Transforming edges...");

            var gexfEdges =
                nodes.SelectMany(
                    x => x.Out
                        .Where(y => y.To.NodeTypeID == NodeTypes.Solution || y.To.NodeTypeID == NodeTypes.Project)
                        .Select(
                            r =>
                                new edgecontent
                                {
                                    id = r.RelationID.ToString(),
                                    source = r.FromNodeID.ToString(),
                                    target = r.ToNodeID.ToString(),
                                    weight = (float)r.Weight,
                                    weightSpecified = true
                                })).ToArray();

            _traceWriter.Output("Constructing gexf format");

            var gexf = new gexfcontent
            {
                meta = new metacontent
                {
                    lastmodifieddate = DateTime.UtcNow,
                    Items = new[]
                    {
                        _creator,
                        _description
                    },
                    ItemsElementName = new[]
                    {
                        ItemsChoiceType1.creator, 
                        ItemsChoiceType1.description,
                    }
                },
                graph = new graphcontent
                {
                    mode = modetype.@static,
                    defaultedgetype = defaultedgetypetype.directed,
                    Items = new object[]
                    {
                        new attributescontent
                        {
                            @class = classtype.node,
                            attribute = new[]
                            {
                                new attributecontent()
                                {
                                    id = "0",
                                    title = "NodeType",
                                    type = attrtypetype.@string
                                }
                            }
                        },
                        new nodescontent { node = gexfNodes },
                        new edgescontent { edge = gexfEdges }
                    }
                }
            };

            _traceWriter.Output("Writing to gexf file...");

            var writer = new XmlSerializer(typeof(gexfcontent));

            using (var file = File.Create("repository.gexf"))
            {
                writer.Serialize(file, gexf);
                file.Close();
            }
        }
    }
}