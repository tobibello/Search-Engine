using System.Diagnostics.Contracts;
using System.IO;

namespace CSC322_SearchEngineProject
{
    /// <summary>
    /// Index Reader Class
    /// Reads Saved Index
    /// </summary>
    public class IndexReader
    {
        internal readonly InvertedIndex InvertedIndex;
        internal readonly DocCollection Collection;

        /// <summary>
        /// Creates the instance
        /// </summary>
        /// <param name="indexDir">the directory. Must be non-null</param>
        public IndexReader(DirectoryInfo indexDir)
        {
            Contract.Requires(indexDir != null);
            Contract.Ensures(Collection != null);
            Contract.Ensures(InvertedIndex != null);
            InvertedIndex = SerializationHelper.Deserialize<InvertedIndex>(Path.Combine(indexDir.FullName, Constants.IndexFile));
            Collection = SerializationHelper.Deserialize<DocCollection>(Path.Combine(indexDir.FullName, Constants.DocCollectionFile));
        }
    }
}