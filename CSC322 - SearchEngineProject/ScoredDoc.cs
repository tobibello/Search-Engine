using System;
using System.Collections.Generic;

namespace CSC322_SearchEngineProject
{
	/// <summary>
	/// ScoredDoc Class.
	/// Key-Value Pair of document id to search score
	/// </summary>
	public class ScoredDoc : IComparable<ScoredDoc>, IComparer<ScoredDoc>
	{
		/// <summary>
		/// The Document id
		/// </summary>
		public decimal Docid { get; }
		private KeyValuePair<decimal, double> _docScores;

		internal ScoredDoc(decimal docid, double score)
		{
			Docid = docid;
			_docScores = new KeyValuePair<decimal, double>(docid, score);
		}
		/// <summary>
		/// Compares two ScoredDoc instances
		/// </summary>
		/// <param name="x">first ScoredDoc instance</param>
		/// <param name="y">second ScoredDoc instance</param>
		/// <returns>a positive number if x is greater than y, zero if x = y, and a negative number if x is less than y</returns>
		public int Compare(ScoredDoc x, ScoredDoc y)
		{
			return x.CompareTo(y);
		}
		/// <summary>
		/// Compares a ScoredDoc instance with this instance
		/// </summary>
		/// <param name="other">the ScoredDoc instance</param>
		/// <returns>a positive number if x is greater than y, zero if x = y, and a negative number if x is less than y</returns>
		public int CompareTo(ScoredDoc other)
		{
			return _docScores.Value.CompareTo(other._docScores.Value) != 0 ?
				_docScores.Value.CompareTo(other._docScores.Value) :
				Docid.CompareTo(other.Docid);
		}
	}
}
