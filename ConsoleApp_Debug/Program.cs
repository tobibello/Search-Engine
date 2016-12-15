using CSC322_SearchEngineProject;
using System;
using System.IO;

namespace ConsoleApp_Debug
{
    internal class Program
    {
        public static readonly string Documents = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");

        private static void Main()
        {
            string input = "computer science";
            DocDirectory dir = new DocDirectory(new DirectoryInfo(Documents));
            DirectoryInfo dirIndexSave =
                new DirectoryInfo(@"C:\Users\Tobi\Documents\Visual Studio 2015\Projects\SearchEngine\Test\bin\Debug\AppIndexFiles");
            Console.WriteLine("Indexing...");
            DateTime indexStart = DateTime.Now;
            IndexWriter indexer = new IndexWriter(dirIndexSave);
            indexer.AddDirectory(dir);
            TimeSpan indexTimeTaken = DateTime.Now.Subtract(indexStart);
            Console.WriteLine($"Time Taken To Index: {indexTimeTaken.Milliseconds} millisecs");

            var reader = new IndexReader(dirIndexSave);
            var searcher = new IndexSearcher(reader);
            DateTime searchStart = DateTime.Now;
            var query = Query.Create(input);
            var collector = Collector.Create(10);
            searcher.Search(query, collector);
            var hits = collector.GetTopDocs();
            Console.WriteLine($"Results for \"{input}\":");
            for (int i = 0; i < hits.Length; i++)
            {
                var docId = hits[i].Docid;
                var document = searcher.Doc(docId);
                Console.WriteLine($"{i + 1}. {document.Name}");
            }
            TimeSpan searcTimeTaken = DateTime.Now.Subtract(searchStart);
            Console.WriteLine($"Time Taken To Search: {searcTimeTaken.Milliseconds} millisecs");
            Console.ReadLine();
        }
    }
}