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
	public class DojoClassCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoClass[] DojoClassArray ;

		public DojoClassCollection() : base()
		{
			DojoClassArray = new DojoClass[15];
		}

		public DojoClassCollection(int capacity) : base()
		{
			DojoClassArray = new DojoClass[capacity];
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
				DojoClassArray[index] = (DojoClass) value;
			}
		}

		public DojoClass this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoClassArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoClassArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoClass) value);
		}

		public int Add(DojoClass value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoClassArray.GetUpperBound(0) + 1)
				{
					DojoClass[] tempDojoClassArray = new DojoClass[count * 2];
					Array.Copy(DojoClassArray, tempDojoClassArray, count - 1);
					DojoClassArray = tempDojoClassArray;
				}
				DojoClassArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoClassArray = new DojoClass[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoClass) value);
		}

		public bool Contains(DojoClass value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoClass) value);
		}

		public int IndexOf(DojoClass value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoClassArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoClass) value);
		}

		public void Insert(int index, DojoClass value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoClassArray.GetUpperBound(0) + 1)
				{
					DojoClass[] tempDojoClassArray = new DojoClass[count * 2];
					Array.Copy(DojoClassArray, tempDojoClassArray, count - 1);
					DojoClassArray = tempDojoClassArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoClassArray[x] = DojoClassArray[x - 1];
				DojoClassArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoClass) value);
		}

		public void Remove(DojoClass value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoClass not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoClassArray[x-1] = DojoClassArray[x];
				DojoClassArray[count - 1] = null;
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
				return DojoClassArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoClassArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoClassArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoClassArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoClass[] DojoClassArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoClass[] DojoClassArray, int virtualCount)
			{
				this.DojoClassArray = DojoClassArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoClassArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoClass Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoClassArray[cursor];
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
		/// Makes a shallow copy of the current DojoClassCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoClassCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoClassCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoClassCollection</returns>
		public DojoClassCollection Clone()
		{
			DojoClassCollection clonedDojoClass = new DojoClassCollection(count);
			lock(this)
			{
				foreach(DojoClass item in this)
					clonedDojoClass.Add(item);
			}
			return clonedDojoClass;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoClass.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoClassCollection from their children.</param>
		public DojoClassCollection Copy(bool isolated)
		{
			DojoClassCollection isolatedCollection = new DojoClassCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoClassArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoClassArray[i].Copy());
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
				Array.Sort(DojoClassArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoClass Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoClassArray[x].ID == id)
						return DojoClassArray[x];
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
				s.Append(DojoClassArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
