using System;

namespace Repographer.Components.IO
{
    public class FileTypeRule
    {
        public string Extension { get; private set; }

        public Action<string> Execute { get; private set; }

        public FileTypeRule(string extension, Action<string> execute)
        {
            Extension = extension;
            Execute = execute;
        }
    }
}