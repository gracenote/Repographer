#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.IO;
using System.Xml.Linq;
using Repographer.DataAccess;
using Repographer.DataAccess.Graphing;

namespace Repographer.Components.Parsing
{
    public static class AssemblyReference
    {
        public static bool TryParse(string rootDirectory, string projFilePath, XElement element, out IReference reference)
        {
            reference = null;

            var projDirectory = Path.GetDirectoryName(projFilePath);
            var includeAttr = element.Attribute("Include");

            if (projDirectory == null || includeAttr == null)
                return false;

            var hintPathElement = element.Element(Constants.MsBuild + "HintPath");
            var hintPath = hintPathElement == null
                ? null
                : Path.GetFullPath(Path.Combine(projDirectory, hintPathElement.Value));

            reference = new Reference<XElement>(rootDirectory, "", includeAttr.Value, hintPath, "", element, RelationTypes.Reference);

            return true;
        }
    }
}