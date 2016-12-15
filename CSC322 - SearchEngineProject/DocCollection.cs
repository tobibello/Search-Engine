using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CSC322_SearchEngineProject
{
    [Serializable]
    internal class DocCollection : IDictionary<decimal, FileInfo>//should be FileInfo
    {
        private readonly Dictionary<decimal, FileInfo> _collection;

        internal DocCollection()
        {
            _collection = new Dictionary<decimal, FileInfo>();
        }

        public FileInfo this[decimal key]
        {
            get
            {
                return _collection[key];
            }

            set
            {
                _collection[key] = value;
            }
        }

        public int Count => _collection.Count;

        public bool IsReadOnly => ((IDictionary<decimal, FileInfo>)_collection).IsReadOnly;

        public ICollection<decimal> Keys => ((IDictionary<decimal, FileInfo>)_collection).Keys;

        public ICollection<FileInfo> Values => ((IDictionary<decimal, FileInfo>)_collection).Values;

        public void Add(KeyValuePair<decimal, FileInfo> item)
        {
            ((IDictionary<decimal, FileInfo>)_collection).Add(item);
        }

        public void Add(decimal key, FileInfo value)
        {
            _collection.Add(key, value);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(KeyValuePair<decimal, FileInfo> item)
        {
            return ((IDictionary<decimal, FileInfo>)_collection).Contains(item);
        }

        public bool ContainsKey(decimal key)
        {
            return _collection.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<decimal, FileInfo>[] array, int arrayIndex)
        {
            ((IDictionary<decimal, FileInfo>)_collection).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<decimal, FileInfo>> GetEnumerator()
        {
            return ((IDictionary<decimal, FileInfo>)_collection).GetEnumerator();
        }

        public bool Remove(KeyValuePair<decimal, FileInfo> item)
        {
            return ((IDictionary<decimal, FileInfo>)_collection).Remove(item);
        }

        public bool Remove(decimal key)
        {
            return _collection.Remove(key);
        }

        public bool TryGetValue(decimal key, out FileInfo value)
        {
            return _collection.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<decimal, FileInfo>)_collection).GetEnumerator();
        }

        public override string ToString()
        {
            string str = "";
            foreach (var pair in _collection)
            {
                str +=
                    $"<doc{pair.Key} : {pair.Value.FullName}>";
            }
            return str;
        }
    }
}