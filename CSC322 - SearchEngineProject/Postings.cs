using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSC322_SearchEngineProject
{
    [Serializable]
    internal class Postings : IEnumerable, IComparable<Postings>
    {
        private readonly Dictionary<decimal, HashSet<decimal>> _postingDictionary;

        internal Postings(decimal docid, decimal firstPos)
        {
            Docid = docid;
            _postingDictionary = new Dictionary<decimal, HashSet<decimal>>
            {
                {
                    docid, new HashSet<decimal>()
                    {
                        firstPos
                    }
                }
            };
        }

        private decimal Docid { get; }

        public HashSet<decimal> this[decimal id]
        {
            get
            {
                return _postingDictionary[id];
            }

            set
            {
                _postingDictionary[id] = value;
            }
        }

        public int Count => _postingDictionary.Count;

        public bool IsReadOnly => ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).IsReadOnly;

        public ICollection<decimal> Keys => ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).Keys;

        public ICollection<HashSet<decimal>> Values => ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).Values;

        public void Add(KeyValuePair<decimal, HashSet<decimal>> item)
        {
            ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).Add(item);
        }

        public void Add(decimal key, HashSet<decimal> value)
        {
            _postingDictionary.Add(key, value);
        }

        public void Clear()
        {
            _postingDictionary.Clear();
        }

        public int CompareTo(Postings other)
        {
            return Docid.CompareTo(other.Docid);
        }

        public bool Contains(KeyValuePair<decimal, HashSet<decimal>> item)
        {
            return ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).Contains(item);
        }

        public bool ContainsKey(decimal key)
        {
            return _postingDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<decimal, HashSet<decimal>>[] array, int arrayIndex)
        {
            ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<decimal, HashSet<decimal>>> GetEnumerator()
        {
            return ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).GetEnumerator();
        }

        public bool Remove(KeyValuePair<decimal, HashSet<decimal>> item)
        {
            return ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).Remove(item);
        }

        public bool Remove(decimal key)
        {
            return _postingDictionary.Remove(key);
        }

        public bool TryGetValue(decimal key, out HashSet<decimal> value)
        {
            return _postingDictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<decimal, HashSet<decimal>>)_postingDictionary).GetEnumerator();
        }

        public override string ToString()
        {
            string str = "";
            foreach (var posting in _postingDictionary)
            {
                str +=
                    $"  |doc{posting.Key},{posting.Value.Count}: <{posting.Value.Aggregate("", (current, i) => current + (i + " "))}>|  ";
            }
            return str;
        }
    }
}