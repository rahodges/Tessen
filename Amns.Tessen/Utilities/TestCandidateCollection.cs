/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using System.Collections;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// <summary>
	/// TestCandidate
	/// </summary>
	/// </summary>
	public class TestCandidateCollection : IList, ICloneable 
	{
		private int _count = 0;
		private TestCandidate[] _candidates ;

		public TestCandidateCollection() : base()
		{
			_candidates = new TestCandidate[15];
		}

		public TestCandidateCollection(int capacity) : base()
		{
			_candidates = new TestCandidate[capacity];
		}

		#region IList Implemenation

		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				_candidates[index] = (TestCandidate) value;
			}
		}

		public TestCandidate this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > _count - 1)
						throw(new Exception("Index out of bounds."));
					return _candidates[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				_candidates[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((TestCandidate) value);
		}

		public int Add(TestCandidate value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				_count++;
				// Resize the array if the _count is greater than the length 
				// of the array.
				if(_count > _candidates.GetUpperBound(0) + 1)
				{
					TestCandidate[] temp_candidates = new TestCandidate[_count * 2];
					Array.Copy(_candidates, temp_candidates, _count - 1);
					_candidates = temp_candidates;
				}
				_candidates[_count - 1] = value;
			}
			return _count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				_count = 0;
				_candidates = new TestCandidate[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((TestCandidate) value);
		}

		public bool Contains(TestCandidate value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((TestCandidate) value);
		}

		public int IndexOf(TestCandidate value)
		{
			lock(this)
			{
				for(int x = 0; x < _count; x++)
					if(_candidates[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (TestCandidate) value);
		}

		public void Insert(int index, TestCandidate value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				_count++;
				// Resize the array if the _count is greater than the length 
				// of the array.
				if(_count > _candidates.GetUpperBound(0) + 1)
				{
					TestCandidate[] temp_candidates = new TestCandidate[_count * 2];
					Array.Copy(_candidates, temp_candidates, _count - 1);
					_candidates = temp_candidates;
				}
				for(int x = index + 1; x == _count - 2; x ++)
					_candidates[x] = _candidates[x - 1];
				_candidates[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((TestCandidate) value);
		}

		public void Remove(TestCandidate value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("TestCandidate not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= _count - 1; x++)
					_candidates[x-1] = _candidates[x];
				_candidates[_count - 1] = null;
				_count--;
			}
		}

		#endregion

		#region ICollection Implementation

		public int Count
		{
			get
			{
				return _count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return _candidates.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return _candidates;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				_candidates.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(_candidates, _count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private TestCandidate[] _candidates;
			private int cursor;
			private int virtual_count;

			public Enumerator(TestCandidate[] _candidates, int virtual_count)
			{
				this._candidates = _candidates;
				this.virtual_count = virtual_count;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < _candidates.Length)
					cursor++;
				return(!(cursor == virtual_count));
			}

			public TestCandidate Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtual_count))
						throw(new InvalidOperationException());
					return _candidates[cursor];
				}
			}

			Object IEnumerator.Current
			{
				get
				{
					return Current;
				}
			}
		}

		#endregion

		/// <summary>
		/// Makes a shallow copy of the current TestCandidateCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>TestCandidateCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current TestCandidateCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>TestCandidateCollection</returns>
		public TestCandidateCollection Clone()
		{
			TestCandidateCollection clonedTestCandidate = new TestCandidateCollection(_count);
			lock(this)
			{
				foreach(TestCandidate item in this)
					clonedTestCandidate.Add(item);
			}
			return clonedTestCandidate;
		}

		#endregion

		#region Events

		public event EventHandler CollectionChanged;

		protected virtual void OnCollectionChanged(EventArgs e)
		{
			if(CollectionChanged != null)
				CollectionChanged(this, e);
		}

		#endregion

		#region Sort Methods

		/// <summary>
		/// Sorts the collection by id.
		/// </summary>
		public void Sort()
		{
			lock(this)
			{
				Array.Sort(_candidates, 0, _count);
			}
		}

		#endregion

		#region ToString() Override Method

		public override string ToString()
		{
			string lineBreak;

			if(System.Web.HttpContext.Current != null)
			lineBreak = "<br />";
			else
			lineBreak = "\r\n";

			System.Text.StringBuilder s = new System.Text.StringBuilder();
			for(int x = 0; x < _count; x++)
			{
				if(x != 0)
					s.Append(lineBreak);
				s.Append(_candidates[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
