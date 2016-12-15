using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace CSC322_SearchEngineProject
{
    /// <summary>
    /// Index Writer Class.
    /// Creates and adds to the index.
    /// </summary>
    public sealed class IndexWriter
    {
        private readonly InvertedIndex _invertedIndex;
        private readonly DocCollection _collection;
        private readonly DirectoryInfo _saveTo;

        /// <summary>
        /// Number of documents indexed
        /// </summary>
        internal decimal NumberOfDocsIndexed { get; private set; }

        /// <summary>
        /// Creates the instance
        /// </summary>
        /// <param name="saveTo">Directory to save index to. Must be non-null</param>
        public IndexWriter(DirectoryInfo saveTo)
        {
            Contract.Requires(saveTo != null);
            Contract.Ensures(_invertedIndex != null);
            Contract.Ensures(_collection != null);

            _saveTo = saveTo;
            _invertedIndex = new InvertedIndex();
            _collection = new DocCollection();
            NumberOfDocsIndexed = 0;
        }

        private void IndexFile(Document doc)
        {
            doc.Docid = ++NumberOfDocsIndexed;
            _collection.Add(doc.Docid, doc.File);
            int pos = 0;
            foreach (string term in doc.Terms)
            {
                pos++;
                Postings postings;
                if (!_invertedIndex.TryGetValue(term, out postings))
                {
                    _invertedIndex.Add(term, new Postings(doc.Docid, pos));
                }
                else
                {
                    HashSet<decimal> positions;
                    if (!postings.TryGetValue(doc.Docid, out positions))
                    {
                        postings.Add(doc.Docid, new HashSet<decimal> { pos });
                    }
                    else
                    {
                        positions.Add(pos);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a document
        /// </summary>
        /// <param name="doc">the document. Must be non-null</param>
        public void AddDocument(Document doc)
        {
            Contract.Requires(doc != null);
            IndexFile(doc);
            SerializationHelper.Serialize(Path.Combine(_saveTo.FullName, Constants.IndexFile), _invertedIndex);
            SerializationHelper.Serialize(Path.Combine(_saveTo.FullName, Constants.DocCollectionFile), _collection);
        }

        /// <summary>
        /// Adds a directory
        /// </summary>
        /// <param name="docDirectory">the directory. Must be non-null</param>
        public void AddDirectory(DocDirectory docDirectory)
        {
            Contract.Requires(docDirectory != null);
            foreach (var doc in docDirectory.Documents)
            {
                IndexFile(doc);
            }
            SerializationHelper.Serialize(Path.Combine(_saveTo.FullName, Constants.IndexFile), _invertedIndex);
            SerializationHelper.Serialize(Path.Combine(_saveTo.FullName, Constants.DocCollectionFile), _collection);
        }

        //public void Close()
        //{
        //    _invertedIndexStream.Close();
        //    _docCollectionStream.Close();
        //    IsDisposed = true;
        //}
    }
}