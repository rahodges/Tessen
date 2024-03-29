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
	/// Summary of MyClass
	/// </summary>
	/// </summary>
	public class DojoTestListStatusCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoTestListStatus[] DojoTestListStatusArray ;

		public DojoTestListStatusCollection() : base()
		{
			DojoTestListStatusArray = new DojoTestListStatus[15];
		}

		public DojoTestListStatusCollection(int capacity) : base()
		{
			DojoTestListStatusArray = new DojoTestListStatus[capacity];
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
				DojoTestListStatusArray[index] = (DojoTestListStatus) value;
			}
		}

		public DojoTestListStatus this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoTestListStatusArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoTestListStatusArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoTestListStatus) value);
		}

		public int Add(DojoTestListStatus value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoTestListStatusArray.GetUpperBound(0) + 1)
				{
					DojoTestListStatus[] tempDojoTestListStatusArray = new DojoTestListStatus[count * 2];
					Array.Copy(DojoTestListStatusArray, tempDojoTestListStatusArray, count - 1);
					DojoTestListStatusArray = tempDojoTestListStatusArray;
				}
				DojoTestListStatusArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoTestListStatusArray = new DojoTestListStatus[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoTestListStatus) value);
		}

		public bool Contains(DojoTestListStatus value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoTestListStatus) value);
		}

		public int IndexOf(DojoTestListStatus value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoTestListStatusArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoTestListStatus) value);
		}

		public void Insert(int index, DojoTestListStatus value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoTestListStatusArray.GetUpperBound(0) + 1)
				{
					DojoTestListStatus[] tempDojoTestListStatusArray = new DojoTestListStatus[count * 2];
					Array.Copy(DojoTestListStatusArray, tempDojoTestListStatusArray, count - 1);
					DojoTestListStatusArray = tempDojoTestListStatusArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoTestListStatusArray[x] = DojoTestListStatusArray[x - 1];
				DojoTestListStatusArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoTestListStatus) value);
		}

		public void Remove(DojoTestListStatus value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoTestListStatus not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoTestListStatusArray[x-1] = DojoTestListStatusArray[x];
				DojoTestListStatusArray[count - 1] = null;
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
				return DojoTestListStatusArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoTestListStatusArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoTestListStatusArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoTestListStatusArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoTestListStatus[] DojoTestListStatusArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoTestListStatus[] DojoTestListStatusArray, int virtualCount)
			{
				this.DojoTestListStatusArray = DojoTestListStatusArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoTestListStatusArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoTestListStatus Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoTestListStatusArray[cursor];
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
		/// Makes a shallow copy of the current DojoTestListStatusCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoTestListStatusCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoTestListStatusCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoTestListStatusCollection</returns>
		public DojoTestListStatusCollection Clone()
		{
			DojoTestListStatusCollection clonedDojoTestListStatus = new DojoTestListStatusCollection(count);
			lock(this)
			{
				foreach(DojoTestListStatus item in this)
					clonedDojoTestListStatus.Add(item);
			}
			return clonedDojoTestListStatus;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoTestListStatus.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoTestListStatusCollection from their children.</param>
		public DojoTestListStatusCollection Copy(bool isolated)
		{
			DojoTestListStatusCollection isolatedCollection = new DojoTestListStatusCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoTestListStatusArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoTestListStatusArray[i].Copy());
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
				Array.Sort(DojoTestListStatusArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoTestListStatus Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoTestListStatusArray[x].ID == id)
						return DojoTestListStatusArray[x];
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
				s.Append(DojoTestListStatusArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
