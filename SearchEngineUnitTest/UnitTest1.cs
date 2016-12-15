using CSC322_SearchEngineProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SearchEngineUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private DocDirectory _dir = new DocDirectory(new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents")));
        private DirectoryInfo _dirIndexSave = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IndexFiles"));

        [TestMethod]
        public void NumberOfResultsTest1()
        {
            string input = "schizophrenia";

            //Index Operation
            IndexWriter indexer = new IndexWriter(_dirIndexSave);
            indexer.AddDirectory(_dir);

            //Search Operation
            var reader = new IndexReader(_dirIndexSave);
            var searcher = new IndexSearcher(reader);
            var query = Query.Create(input);
            var collector = Collector.Create(10);
            searcher.Search(query, collector);
            var hits = collector.GetTopDocs();
            Assert.AreEqual(2, hits.Length);
        }

        [TestMethod]
        public void NumberOfResultsTest2()
        {
            string input = "july";

            //Index Operation
            IndexWriter indexer = new IndexWriter(_dirIndexSave);
            indexer.AddDirectory(_dir);

            //Search Operation
            var reader = new IndexReader(_dirIndexSave);
            var searcher = new IndexSearcher(reader);
            var query = Query.Create(input);
            var collector = Collector.Create(10);
            searcher.Search(query, collector);
            var hits = collector.GetTopDocs();
            Assert.AreEqual(4, hits.Length);
        }

        [TestMethod]
        public void MostRelevantResultTest1()
        {
            string input = "drug schizophrenia";

            //Index Operation
            IndexWriter indexer = new IndexWriter(_dirIndexSave);
            indexer.AddDirectory(_dir);

            //Search Operation
            var reader = new IndexReader(_dirIndexSave);
            var searcher = new IndexSearcher(reader);
            var query = Query.Create(input);
            var collector = Collector.Create(10);
            searcher.Search(query, collector);
            var hits = collector.GetTopDocs();
            var docid = hits[0].Docid;
            var document = searcher.Doc(docid);
            Assert.AreEqual(
                Path.Combine(@"C:\Users\Tobi\Documents\Visual Studio 2015\Projects\CSC322 - SearchEngineProject\SearchEngineUnitTest\bin\Debug\Documents",
                "increase in home sales in july.pdf"),
                document.FullName);
        }

        [TestMethod]
        public void MostRelevantResultTest2()
        {
            string input = "approach";

            //Index Operation
            IndexWriter indexer = new IndexWriter(_dirIndexSave);
            indexer.AddDirectory(_dir);

            //Search Operation
            var reader = new IndexReader(_dirIndexSave);
            var searcher = new IndexSearcher(reader);
            var query = Query.Create(input);
            var collector = Collector.Create(10);
            searcher.Search(query, collector);
            var hits = collector.GetTopDocs();
            var docid = hits[0].Docid;
            var document = searcher.Doc(docid);
            Assert.AreEqual(1, hits.Length);
            Assert.AreEqual(
                Path.Combine(@"C:\Users\Tobi\Documents\Visual Studio 2015\Projects\CSC322 - SearchEngineProject\SearchEngineUnitTest\bin\Debug\Documents",
                "textFile.txt"),
                document.FullName);
        }
    }
}