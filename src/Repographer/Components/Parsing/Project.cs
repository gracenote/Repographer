#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Repographer.DataAccess;
using Repographer.DataAccess.Graphing;

namespace Repographer.Components.Parsing
{
    public class Project : INode
    {
        private readonly List<IReference> _references;

        public string Name { get; private set; }

        public string FilePath { get; private set; }

        public NodeTypes NodeType { get { return NodeTypes.Project; } }

        public IEnumerable<IReference> References
        {
            get { return _references; }
        }

        private Project(string rootDirectoryPath, string projFilePath)
        {
            Name = Path.GetFileNameWithoutExtension(projFilePath);
            FilePath = projFilePath.Replace(rootDirectoryPath, "");

            _references = new List<IReference>();
        }

        public static bool TryParse(string rootDirectoryPath, string projFilePath, out Project project)
        {
            project = null;

            if (string.IsNullOrEmpty(projFilePath))
                return false;

            project = new Project(rootDirectoryPath, projFilePath);

            var projDoc = XDocument.Load(projFilePath);

            var projElement = projDoc.Element(Constants.MsBuild + "Project");

            if (projElement == null)
                return false;

            var itemGroupElements = projElement.Elements(Constants.MsBuild + "ItemGroup").ToList();
            var assemblyReferenceElements = itemGroupElements.Elements(Constants.MsBuild + "Reference").ToList();
            var projectReferenceElements = itemGroupElements.Elements(Constants.MsBuild + "ProjectReference").ToList();

            var asmRefs = ParseAssemblyReferences(rootDirectoryPath, projFilePath, assemblyReferenceElements);
            var projRefs = ParseProjectReferences(rootDirectoryPath, projFilePath, projectReferenceElements);

            project._references.AddRange(asmRefs);
            project._references.AddRange(projRefs);

            return project.References.Any();
        }

        private static IEnumerable<IReference> ParseProjectReferences(string rootDirectoryPath, string projFilePath, List<XElement> projectReferenceElements)
        {
            return projectReferenceElements.Select(proj =>
            {
                IReference reference;
                return ProjectReference.TryParse(rootDirectoryPath, projFilePath, proj, out reference)
                    ? reference
                    : null;
            })
            .Where(proj => proj != null);
        }

        private static IEnumerable<IReference> ParseAssemblyReferences(string rootDirectoryPath, string projFilePath, List<XElement> assemblyReferenceElements)
        {
            return assemblyReferenceElements.Select(asm =>
            {
                IReference reference;
                return AssemblyReference.TryParse(rootDirectoryPath, projFilePath, asm, out reference)
                    ? reference
                    : null;
            })
            .Where(asm => asm != null);
        }
    }
}