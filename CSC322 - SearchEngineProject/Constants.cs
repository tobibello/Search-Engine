using System.Collections.Generic;

namespace CSC322_SearchEngineProject
{
    internal static class Constants
    {
        public static readonly Dictionary<string, bool> TextFileExtensions = new Dictionary<string, bool>
        {
            {".pdf",true},
            {".doc", true},
            {".docx",true},
            {".pptx",true},
            {".xls",true},
            {"xlsx",true},
            {".txt",true},
            {".html",true},
            {".xml",true},
            { ".ppt",true},
        };

        public static readonly char[] Delimiters =
        {
            ' ',
            ',',
            ';',
            '.',
            '\n',
            '\r',
            '?',
            '\"',
            '\\','/',
            '(',')',
            ':',
            '@'
        };

        public static readonly Dictionary<string, bool> StopWords = new Dictionary<string, bool>();
        public const string IndexFile = "01.inx";
        public const string DocCollectionFile = "02.inx";
    }
}