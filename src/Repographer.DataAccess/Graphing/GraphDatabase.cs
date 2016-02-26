using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Repographer.DataAccess.Graphing
{
    public class GraphDatabase : IGraphDatabase
    {
        public void Save(Graph graph)
        {
            using (var dbContext = new RepographerEntities())
            {
                try
                {
                    dbContext.Nodes.AddRange(graph.Nodes);
                    dbContext.Relations.AddRange(graph.Relations);
                    dbContext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    ex.EntityValidationErrors
                        .SelectMany(err => err.ValidationErrors)
                        .Select(err => err.ErrorMessage)
                        .ToList().ForEach(Console.WriteLine);
                }
            }
        }

        public Graph Load()
        {
            IEnumerable<Node> nodes;

            using (var dbContext = new RepographerEntities())
            {
                nodes = dbContext
                    .Nodes
                    .Include(x => x.Out)
                    .ToList();
            }

            return new Graph(nodes);
        }
    }
}