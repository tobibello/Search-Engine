using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace CSC322_SearchEngineProject
{
    /// <summary>
    /// Document Directory Class.
    /// Manipulates and pre-processes a directory.
    /// </summary>
    public class DocDirectory
    {
        internal List<Document> Documents { get; }

        private void ExtractTextFiles(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                Documents.Add(new Document(file));// use thread later
            }
            foreach (var dir in directory.GetDirectories())
            {
                ExtractTextFiles(dir);
            }
        }

        /// <summary>
        /// Creates the instance
        /// </summary>
        /// <param name="directory">the directory. Must be non-null</param>
        public DocDirectory(DirectoryInfo directory)
        {
            Contract.Requires(directory != null);
            Documents = new List<Document>();
            ExtractTextFiles(directory);
        }
    }
}