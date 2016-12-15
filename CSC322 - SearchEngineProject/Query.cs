using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace CSC322_SearchEngineProject
{
    /// <summary>
    /// Query Class.
    /// Pre-processes string query
    /// </summary>
    public class Query
    {
        internal HashSet<string> Terms { get; }

        private Query(HashSet<string> terms)
        {
            Terms = terms;
        }

        /// <summary>
        /// Creates the Instance
        /// </summary>
        /// <param name="input">the input. Must be non-null</param>
        /// <returns>the QUery instance</returns>
        public static Query Create(string input)
        {
            Contract.Requires(input != null);
            HashSet<string> termsSet = new HashSet<string>();
            string[] terms = input.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries);
            PorterStemmer stemmer = new PorterStemmer();
            foreach (string term in terms)
            {
                string lower = term.ToLower();
                if (!Constants.StopWords.ContainsKey(lower))
                {
                    termsSet.Add(stemmer.StemWord(lower));
                }
            }
            return new Query(termsSet);
        }
    }
}