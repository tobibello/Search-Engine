using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace CSC322_SearchEngineProject
{
    /// <summary>
    /// Collector Class.
    /// Acquires and Manipulates the Search Result
    /// </summary>
    public class Collector
    {
        private readonly int _numberOfHits;
        internal InvertedIndex SubInvertedIndex;
        internal decimal NumberOfDocumentsIndexed;
        internal readonly Dictionary<decimal, double> DocScoresDict;
        private readonly SortedSet<ScoredDoc> _docScores;

        internal enum State
        {
            Ranked,
            NotRanked = ~Ranked
        }

        internal State ObjState { get; set; }

        private Collector(int numberOfHits)
        {
            Contract.Ensures(ObjState == State.NotRanked);

            _numberOfHits = numberOfHits;
            _docScores = new SortedSet<ScoredDoc>();
            DocScoresDict = new Dictionary<decimal, double>();
            ObjState = State.NotRanked;
        }

        /// <summary>
        /// Creates new Instance of a collector based on the provided query.
        /// </summary>
        /// <param name="numberOfHits">Number of hits to output. Must be a greater than 0</param>
        /// <returns>the Collector instance</returns>
        public static Collector Create(int numberOfHits)
        {
            Contract.Requires(numberOfHits > 0);

            return new Collector(numberOfHits);
        }

        internal void CalculateRanks()
        {
            Contract.Requires(DocScoresDict != null);
            Contract.Ensures(ObjState == State.Ranked);

            foreach (var termPostingslist in SubInvertedIndex)
            {
                foreach (var posting in termPostingslist.Value)
                {
                    if (!DocScoresDict.ContainsKey(posting.Key))
                    {
                        DocScoresDict.Add(posting.Key,
                            WeightCalculator.TfxIdfWeight(posting.Value.Count, NumberOfDocumentsIndexed,
                                termPostingslist.Value.Count));
                    }
                    else
                    {
                        DocScoresDict[posting.Key] +=
                            WeightCalculator.TfxIdfWeight(posting.Value.Count, NumberOfDocumentsIndexed,
                                termPostingslist.Value.Count);
                    }
                }
            }
            ObjState = State.Ranked;
        }

        /// <summary>
        /// returns the top search results
        /// </summary>
        /// <returns>the top search results</returns>
        public ScoredDoc[] GetTopDocs()
        {
            Contract.Requires(ObjState == State.Ranked);
            foreach (var d in DocScoresDict)
            {
                _docScores.Add(new ScoredDoc(d.Key, d.Value));
            }
            return _docScores.Take(_numberOfHits)
                             .Reverse()
                             .ToArray();
        }
    }
}