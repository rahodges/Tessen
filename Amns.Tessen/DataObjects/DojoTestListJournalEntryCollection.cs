/* ********************************************************** *
 * AMNS NitroCast v1.0 Class Collection Object Business Tier    *
 * Autogenerated by NitroCast © 2007 Roy A.E Hodges             *
 * All Rights Reserved                                        *
 * ---------------------------------------------------------- *
 * Source code may not be reproduced or redistributed without *
 * written expressed permission from the author.              *
 * Permission is granted to modify source code by licencee.   *
 * These permissions do not extend to third parties.          *
 * ********************************************************** */

using System;
using System.Collections;

namespace Amns.Tessen
{
	/// <summary>
	/// <summary>
	/// A wrapper for DojoMember class for collection placement in 
	/// DojoTestList class.
	/// </summary>
	/// </summary>
	public class DojoTestListJournalEntryCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoTestListJournalEntry[] DojoTestListJournalEntryArray ;

		public DojoTestListJournalEntryCollection() : base()
		{
			DojoTestListJournalEntryArray = new DojoTestListJournalEntry[15];
		}

		public DojoTestListJournalEntryCollection(int capacity) : base()
		{
			DojoTestListJournalEntryArray = new DojoTestListJournalEntry[capacity];
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
				DojoTestListJournalEntryArray[index] = (DojoTestListJournalEntry) value;
			}
		}

		public DojoTestListJournalEntry this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoTestListJournalEntryArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoTestListJournalEntryArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoTestListJournalEntry) value);
		}

		public int Add(DojoTestListJournalEntry value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoTestListJournalEntryArray.GetUpperBound(0) + 1)
				{
					DojoTestListJournalEntry[] tempDojoTestListJournalEntryArray = new DojoTestListJournalEntry[count * 2];
					Array.Copy(DojoTestListJournalEntryArray, tempDojoTestListJournalEntryArray, count - 1);
					DojoTestListJournalEntryArray = tempDojoTestListJournalEntryArray;
				}
				DojoTestListJournalEntryArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoTestListJournalEntryArray = new DojoTestListJournalEntry[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoTestListJournalEntry) value);
		}

		public bool Contains(DojoTestListJournalEntry value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoTestListJournalEntry) value);
		}

		public int IndexOf(DojoTestListJournalEntry value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoTestListJournalEntryArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoTestListJournalEntry) value);
		}

		public void Insert(int index, DojoTestListJournalEntry value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoTestListJournalEntryArray.GetUpperBound(0) + 1)
				{
					DojoTestListJournalEntry[] tempDojoTestListJournalEntryArray = new DojoTestListJournalEntry[count * 2];
					Array.Copy(DojoTestListJournalEntryArray, tempDojoTestListJournalEntryArray, count - 1);
					DojoTestListJournalEntryArray = tempDojoTestListJournalEntryArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoTestListJournalEntryArray[x] = DojoTestListJournalEntryArray[x - 1];
				DojoTestListJournalEntryArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoTestListJournalEntry) value);
		}

		public void Remove(DojoTestListJournalEntry value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoTestListJournalEntry not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoTestListJournalEntryArray[x-1] = DojoTestListJournalEntryArray[x];
				DojoTestListJournalEntryArray[count - 1] = null;
				count--;
			}
		}

		#endregion

		#region ICollection Implementation

		public int Count
		{
			get
			{
				return count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return DojoTestListJournalEntryArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoTestListJournalEntryArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoTestListJournalEntryArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoTestListJournalEntryArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoTestListJournalEntry[] DojoTestListJournalEntryArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoTestListJournalEntry[] DojoTestListJournalEntryArray, int virtualCount)
			{
				this.DojoTestListJournalEntryArray = DojoTestListJournalEntryArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoTestListJournalEntryArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoTestListJournalEntry Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoTestListJournalEntryArray[cursor];
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
		/// Makes a shallow copy of the current DojoTestListJournalEntryCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoTestListJournalEntryCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoTestListJournalEntryCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoTestListJournalEntryCollection</returns>
		public DojoTestListJournalEntryCollection Clone()
		{
			DojoTestListJournalEntryCollection clonedDojoTestListJournalEntry = new DojoTestListJournalEntryCollection(count);
			lock(this)
			{
				foreach(DojoTestListJournalEntry item in this)
					clonedDojoTestListJournalEntry.Add(item);
			}
			return clonedDojoTestListJournalEntry;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoTestListJournalEntry.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoTestListJournalEntryCollection from their children.</param>
		public DojoTestListJournalEntryCollection Copy(bool isolated)
		{
			DojoTestListJournalEntryCollection isolatedCollection = new DojoTestListJournalEntryCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoTestListJournalEntryArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoTestListJournalEntryArray[i].Copy());
					}
				}
			}
			return isolatedCollection;
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
				Array.Sort(DojoTestListJournalEntryArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoTestListJournalEntry Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoTestListJournalEntryArray[x].ID == id)
						return DojoTestListJournalEntryArray[x];
			}
			return null;
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
			for(int x = 0; x < count; x++)
			{
				if(x != 0)
					s.Append(lineBreak);
				s.Append(DojoTestListJournalEntryArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
