#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Repographer.DataAccess;
using Repographer.DataAccess.Graphing;

namespace Repographer.Components.Parsing
{
    public class Solution : INode
    {
        private readonly List<IReference> _references;

        public IEnumerable<IReference> References
        {
            get { return _references; }
        }

        public string Name { get; private set; }

        public string FilePath { get; private set; }

        public NodeTypes NodeType { get { return NodeTypes.Solution; } }

        private Solution(string rootDirectory, string slnFilePath)
        {
            Name = Path.GetFileNameWithoutExtension(slnFilePath);
            FilePath = slnFilePath.Replace(rootDirectory, "");

            _references = new List<IReference>();
        }

        public static bool TryParse(string rootDirectory, string slnFilePath, out Solution solution)
        {
            solution = null;

            if (string.IsNullOrEmpty(slnFilePath))
                return false;

            solution = new Solution(rootDirectory, slnFilePath);

            try
            {
                using (var reader = File.OpenText(slnFilePath))
                {
                    var lineText = reader.ReadLine();

                    while (lineText != null)
                    {
                        IReference reference;

                        if (SolutionReference.TryParse(rootDirectory, slnFilePath, lineText, out reference))
                        {
                            solution._references.Add(reference);
                        }

                        lineText = reader.ReadLine();
                    }

                    reader.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static void RemoveProject(string rootDirectory, string filePath, string projectName)
        {
            var builder = new StringBuilder();

            using (var reader = File.OpenText(filePath))
            {
                IReference reference = null;
                var nextLineIsEndProjectForReference = false;

                var lineText = reader.ReadLine();
                
                while (lineText != null)
                {
                    if (
                        reference == null
                        && SolutionReference.TryParse(rootDirectory, filePath, lineText, out reference))
                    {
                        if (reference.Name != projectName)
                        {
                            builder.AppendLine(lineText);
                            reference = null;
                        }
                        else
                        {
                            nextLineIsEndProjectForReference = true;
                        }

                        lineText = reader.ReadLine();
                        continue;
                    }

                    if (nextLineIsEndProjectForReference)
                    {
                        nextLineIsEndProjectForReference = false;
                    }
                    else if (reference == null || !lineText.Contains(reference.Guid))
                    {
                        builder.AppendLine(lineText);
                    }

                    lineText = reader.ReadLine();
                }

                reader.Close();
            }

            File.WriteAllText(filePath, builder.ToString());
        }
    }
}