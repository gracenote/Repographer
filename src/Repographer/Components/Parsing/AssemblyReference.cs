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