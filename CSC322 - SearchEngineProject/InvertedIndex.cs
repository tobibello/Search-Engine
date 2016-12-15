using System;
using System.Collections;
using System.Collections.Generic;

namespace CSC322_SearchEngineProject
{
    [Serializable]
    internal class InvertedIndex : IDictionary<string, Postings>
    {
        private readonly Dictionary<string, Postings> _postingsLists;

        internal InvertedIndex()
        {
            _postingsLists = new Dictionary<string, Postings>();
        }

        public Postings this[string key]
        {
            get
            {
                return _postingsLists[key];
            }

            set
            {
                _postingsLists[key] = value;
            }
        }

        public int Count => _postingsLists.Count;

        public bool IsReadOnly => ((IDictionary<string, Postings>)_postingsLists).IsReadOnly;

        public ICollection<string> Keys => ((IDictionary<string, Postings>)_postingsLists).Keys;

        public ICollection<Postings> Values => ((IDictionary<string, Postings>)_postingsLists).Values;

        public void Add(KeyValuePair<string, Postings> item)
        {
            ((IDictionary<string, Postings>)_postingsLists).Add(item);
        }

        public void Add(string key, Postings value)
        {
            _postingsLists.Add(key, value);
        }

        public void Clear()
        {
            _postingsLists.Clear();
        }

        public bool Contains(KeyValuePair<string, Postings> item)
        {
            return ((IDictionary<string, Postings>)_postingsLists).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _postingsLists.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Postings>[] array, int arrayIndex)
        {
            ((IDictionary<string, Postings>)_postingsLists).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, Postings>> GetEnumerator()
        {
            return ((IDictionary<string, Postings>)_postingsLists).GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, Postings> item)
        {
            return ((IDictionary<string, Postings>)_postingsLists).Remove(item);
        }

        public bool Remove(string key)
        {
            return _postingsLists.Remove(key);
        }

        public bool TryGetValue(string key, out Postings value)
        {
            return _postingsLists.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, Postings>)_postingsLists).GetEnumerator();
        }

        public override string ToString()
        {
            string str = "";
            foreach (var postingsList in _postingsLists)
            {
                str += $"{postingsList.Key},{postingsList.Value.Count}: <{postingsList.Value}>\t";
            }
            return str;
        }
    }
}