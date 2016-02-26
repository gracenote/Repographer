#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.IO;
using System.Text.RegularExpressions;
using Repographer.DataAccess;
using Repographer.DataAccess.Graphing;

namespace Repographer.Components.Parsing
{
    public static class SolutionReference
    {
        const string SOLUTION_ITEM_GUID = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";
        const string PROJECTEX = "^Project\\(\"(?<PROJECTTYPEGUID>.*)\"\\)\\s*=\\s* \"(?<PROJECTNAME>.*)\"\\s*,\\s*\"(?<PROJECTRELATIVEPATH>.*)\"\\s*,\\s*\"(?<PROJECTGUID>.*)\"$";

        public static bool TryParse(string rootDirectory, string slnFilePath, string lineText, out IReference reference)
        {
            reference = null;

            var slnDirectory = Path.GetDirectoryName(slnFilePath);

            if (slnDirectory == null || string.IsNullOrEmpty(lineText) || !lineText.StartsWith("Project"))
                return false;

            var match = Regex.Match(lineText, PROJECTEX);

            if (!match.Success)
                return false;

            var typeGuid = match.Groups["PROJECTTYPEGUID"].Value;
            var projectName = match.Groups["PROJECTNAME"].Value;
            var matchedPath = match.Groups["PROJECTRELATIVEPATH"].Value;
            var guid = match.Groups["PROJECTGUID"].Value;
            var refPath = Path.GetFullPath(Path.Combine(slnDirectory, matchedPath));

            if (typeGuid == SOLUTION_ITEM_GUID)
                return false;

            reference = new Reference<string>(
                rootDirectory,
                typeGuid,
                projectName,
                refPath,
                guid,
                lineText,
                RelationTypes.Member
                );

            return true;
        }
    }
}