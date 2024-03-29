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
	/// MemberType
	/// </summary>
	/// </summary>
	public class DojoMemberTypeCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoMemberType[] DojoMemberTypeArray ;

		public DojoMemberTypeCollection() : base()
		{
			DojoMemberTypeArray = new DojoMemberType[15];
		}

		public DojoMemberTypeCollection(int capacity) : base()
		{
			DojoMemberTypeArray = new DojoMemberType[capacity];
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
				DojoMemberTypeArray[index] = (DojoMemberType) value;
			}
		}

		public DojoMemberType this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoMemberTypeArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoMemberTypeArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoMemberType) value);
		}

		public int Add(DojoMemberType value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoMemberTypeArray.GetUpperBound(0) + 1)
				{
					DojoMemberType[] tempDojoMemberTypeArray = new DojoMemberType[count * 2];
					Array.Copy(DojoMemberTypeArray, tempDojoMemberTypeArray, count - 1);
					DojoMemberTypeArray = tempDojoMemberTypeArray;
				}
				DojoMemberTypeArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoMemberTypeArray = new DojoMemberType[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoMemberType) value);
		}

		public bool Contains(DojoMemberType value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoMemberType) value);
		}

		public int IndexOf(DojoMemberType value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoMemberTypeArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoMemberType) value);
		}

		public void Insert(int index, DojoMemberType value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoMemberTypeArray.GetUpperBound(0) + 1)
				{
					DojoMemberType[] tempDojoMemberTypeArray = new DojoMemberType[count * 2];
					Array.Copy(DojoMemberTypeArray, tempDojoMemberTypeArray, count - 1);
					DojoMemberTypeArray = tempDojoMemberTypeArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoMemberTypeArray[x] = DojoMemberTypeArray[x - 1];
				DojoMemberTypeArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoMemberType) value);
		}

		public void Remove(DojoMemberType value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoMemberType not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoMemberTypeArray[x-1] = DojoMemberTypeArray[x];
				DojoMemberTypeArray[count - 1] = null;
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
				return DojoMemberTypeArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoMemberTypeArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoMemberTypeArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoMemberTypeArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoMemberType[] DojoMemberTypeArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoMemberType[] DojoMemberTypeArray, int virtualCount)
			{
				this.DojoMemberTypeArray = DojoMemberTypeArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoMemberTypeArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoMemberType Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoMemberTypeArray[cursor];
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
		/// Makes a shallow copy of the current DojoMemberTypeCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoMemberTypeCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoMemberTypeCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoMemberTypeCollection</returns>
		public DojoMemberTypeCollection Clone()
		{
			DojoMemberTypeCollection clonedDojoMemberType = new DojoMemberTypeCollection(count);
			lock(this)
			{
				foreach(DojoMemberType item in this)
					clonedDojoMemberType.Add(item);
			}
			return clonedDojoMemberType;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoMemberType.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoMemberTypeCollection from their children.</param>
		public DojoMemberTypeCollection Copy(bool isolated)
		{
			DojoMemberTypeCollection isolatedCollection = new DojoMemberTypeCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoMemberTypeArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoMemberTypeArray[i].Copy());
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
				Array.Sort(DojoMemberTypeArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoMemberType Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoMemberTypeArray[x].ID == id)
						return DojoMemberTypeArray[x];
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
				s.Append(DojoMemberTypeArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
