using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace CSC322_SearchEngineProject
{
    /// <summary>
    /// Document Class.
    /// Acquires and manipulates file  information
    /// </summary>
    [Serializable]
    public class Document
    {
        /// <summary>
        /// The file
        /// </summary>
        public readonly FileInfo File;

        internal List<string> Terms { get; }
        internal decimal Docid { get; set; }

        private List<string> ExtractTerms(FileInfo file)
        {
            PorterStemmer stemmer = new PorterStemmer();
            List<string> terms = new List<string>();
            terms.AddRange(from word in file.FullName.Split(new[] { '\\', '.' }, StringSplitOptions.RemoveEmptyEntries)
                           select stemmer.StemWord(word.ToLower()));
            if (!Constants.TextFileExtensions.ContainsKey(file.Extension.ToLower())) return terms;
            string text = TextExtractor.ReadAllText(file);
            terms.AddRange(from word in text.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries)
                           where !Constants.StopWords.ContainsKey(word)
                           select stemmer.StemWord(word.ToLower().Trim()));
            return terms;
        }

        /// <summary>
        /// Creates the instance
        /// </summary>
        /// <param name="file">the file. Must be non-null</param>
        public Document(FileInfo file)
        {
            Contract.Requires(file != null);
            File = file;
            Terms = ExtractTerms(file);
        }
    }
}