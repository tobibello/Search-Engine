using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace CSC322_SearchEngineProject
{
    /// <summary>
    /// Index Searcher Class.
    /// Performs generation of search results
    /// </summary>
    public class IndexSearcher
    {
        private readonly IndexReader _reader;
        internal int DocumentCount { get; }

        /// <summary>
        /// Creates the instance
        /// </summary>
        /// <param name="reader">The index reader. Must be non-null</param>
        public IndexSearcher(IndexReader reader)
        {
            Contract.Requires(reader != null);
            _reader = reader;
            DocumentCount = _reader.Collection.Count;
        }

        /// <summary>
        /// Searches for query in index
        /// </summary>
        /// <param name="query">The query. Must be non-null</param>
        /// <param name="collector">The collector. Must be non-null</param>
        public void Search(Query query, Collector collector)
        {
            Contract.Requires(query != null);
            Contract.Requires(collector != null);

            string[] tokens = query.Terms.ToArray();
            InvertedIndex subInvertedIndex = new InvertedIndex();
            foreach (string token in tokens)
            {
                if (_reader.InvertedIndex.ContainsKey(token))
                {
                    subInvertedIndex.Add(token, _reader.InvertedIndex[token]);
                }
            }
            collector.NumberOfDocumentsIndexed = DocumentCount;
            collector.SubInvertedIndex = subInvertedIndex;
            collector.CalculateRanks();
        }

        /// <summary>
        /// Gets the Document with specified id
        /// </summary>
        /// <param name="docid">the doc id</param>
        /// <returns>the Document</returns>
        public FileInfo Doc(decimal docid)
        {
            Contract.Requires(docid > 0 && docid <= DocumentCount);
            return _reader.Collection[docid];
        }
    }
}