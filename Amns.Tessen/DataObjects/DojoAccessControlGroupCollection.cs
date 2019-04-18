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
	/// An Access Control Group

	/// </summary>
	/// </summary>
	public class DojoAccessControlGroupCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoAccessControlGroup[] DojoAccessControlGroupArray ;

		public DojoAccessControlGroupCollection() : base()
		{
			DojoAccessControlGroupArray = new DojoAccessControlGroup[15];
		}

		public DojoAccessControlGroupCollection(int capacity) : base()
		{
			DojoAccessControlGroupArray = new DojoAccessControlGroup[capacity];
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
				DojoAccessControlGroupArray[index] = (DojoAccessControlGroup) value;
			}
		}

		public DojoAccessControlGroup this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoAccessControlGroupArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoAccessControlGroupArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoAccessControlGroup) value);
		}

		public int Add(DojoAccessControlGroup value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoAccessControlGroupArray.GetUpperBound(0) + 1)
				{
					DojoAccessControlGroup[] tempDojoAccessControlGroupArray = new DojoAccessControlGroup[count * 2];
					Array.Copy(DojoAccessControlGroupArray, tempDojoAccessControlGroupArray, count - 1);
					DojoAccessControlGroupArray = tempDojoAccessControlGroupArray;
				}
				DojoAccessControlGroupArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoAccessControlGroupArray = new DojoAccessControlGroup[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoAccessControlGroup) value);
		}

		public bool Contains(DojoAccessControlGroup value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoAccessControlGroup) value);
		}

		public int IndexOf(DojoAccessControlGroup value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoAccessControlGroupArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoAccessControlGroup) value);
		}

		public void Insert(int index, DojoAccessControlGroup value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoAccessControlGroupArray.GetUpperBound(0) + 1)
				{
					DojoAccessControlGroup[] tempDojoAccessControlGroupArray = new DojoAccessControlGroup[count * 2];
					Array.Copy(DojoAccessControlGroupArray, tempDojoAccessControlGroupArray, count - 1);
					DojoAccessControlGroupArray = tempDojoAccessControlGroupArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoAccessControlGroupArray[x] = DojoAccessControlGroupArray[x - 1];
				DojoAccessControlGroupArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoAccessControlGroup) value);
		}

		public void Remove(DojoAccessControlGroup value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoAccessControlGroup not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoAccessControlGroupArray[x-1] = DojoAccessControlGroupArray[x];
				DojoAccessControlGroupArray[count - 1] = null;
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
				return DojoAccessControlGroupArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoAccessControlGroupArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoAccessControlGroupArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoAccessControlGroupArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoAccessControlGroup[] DojoAccessControlGroupArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoAccessControlGroup[] DojoAccessControlGroupArray, int virtualCount)
			{
				this.DojoAccessControlGroupArray = DojoAccessControlGroupArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoAccessControlGroupArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoAccessControlGroup Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoAccessControlGroupArray[cursor];
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
		/// Makes a shallow copy of the current DojoAccessControlGroupCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoAccessControlGroupCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoAccessControlGroupCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoAccessControlGroupCollection</returns>
		public DojoAccessControlGroupCollection Clone()
		{
			DojoAccessControlGroupCollection clonedDojoAccessControlGroup = new DojoAccessControlGroupCollection(count);
			lock(this)
			{
				foreach(DojoAccessControlGroup item in this)
					clonedDojoAccessControlGroup.Add(item);
			}
			return clonedDojoAccessControlGroup;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoAccessControlGroup.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoAccessControlGroupCollection from their children.</param>
		public DojoAccessControlGroupCollection Copy(bool isolated)
		{
			DojoAccessControlGroupCollection isolatedCollection = new DojoAccessControlGroupCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoAccessControlGroupArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoAccessControlGroupArray[i].Copy());
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
				Array.Sort(DojoAccessControlGroupArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoAccessControlGroup Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoAccessControlGroupArray[x].ID == id)
						return DojoAccessControlGroupArray[x];
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
				s.Append(DojoAccessControlGroupArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}