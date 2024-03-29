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
	/// Promotion Flags
	/// </summary>
	/// </summary>
	public class DojoPromotionFlagCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoPromotionFlag[] DojoPromotionFlagArray ;

		public DojoPromotionFlagCollection() : base()
		{
			DojoPromotionFlagArray = new DojoPromotionFlag[15];
		}

		public DojoPromotionFlagCollection(int capacity) : base()
		{
			DojoPromotionFlagArray = new DojoPromotionFlag[capacity];
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
				DojoPromotionFlagArray[index] = (DojoPromotionFlag) value;
			}
		}

		public DojoPromotionFlag this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoPromotionFlagArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoPromotionFlagArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoPromotionFlag) value);
		}

		public int Add(DojoPromotionFlag value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoPromotionFlagArray.GetUpperBound(0) + 1)
				{
					DojoPromotionFlag[] tempDojoPromotionFlagArray = new DojoPromotionFlag[count * 2];
					Array.Copy(DojoPromotionFlagArray, tempDojoPromotionFlagArray, count - 1);
					DojoPromotionFlagArray = tempDojoPromotionFlagArray;
				}
				DojoPromotionFlagArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoPromotionFlagArray = new DojoPromotionFlag[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoPromotionFlag) value);
		}

		public bool Contains(DojoPromotionFlag value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoPromotionFlag) value);
		}

		public int IndexOf(DojoPromotionFlag value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoPromotionFlagArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoPromotionFlag) value);
		}

		public void Insert(int index, DojoPromotionFlag value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoPromotionFlagArray.GetUpperBound(0) + 1)
				{
					DojoPromotionFlag[] tempDojoPromotionFlagArray = new DojoPromotionFlag[count * 2];
					Array.Copy(DojoPromotionFlagArray, tempDojoPromotionFlagArray, count - 1);
					DojoPromotionFlagArray = tempDojoPromotionFlagArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoPromotionFlagArray[x] = DojoPromotionFlagArray[x - 1];
				DojoPromotionFlagArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoPromotionFlag) value);
		}

		public void Remove(DojoPromotionFlag value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoPromotionFlag not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoPromotionFlagArray[x-1] = DojoPromotionFlagArray[x];
				DojoPromotionFlagArray[count - 1] = null;
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
				return DojoPromotionFlagArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoPromotionFlagArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoPromotionFlagArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoPromotionFlagArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoPromotionFlag[] DojoPromotionFlagArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoPromotionFlag[] DojoPromotionFlagArray, int virtualCount)
			{
				this.DojoPromotionFlagArray = DojoPromotionFlagArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoPromotionFlagArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoPromotionFlag Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoPromotionFlagArray[cursor];
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
		/// Makes a shallow copy of the current DojoPromotionFlagCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoPromotionFlagCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoPromotionFlagCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoPromotionFlagCollection</returns>
		public DojoPromotionFlagCollection Clone()
		{
			DojoPromotionFlagCollection clonedDojoPromotionFlag = new DojoPromotionFlagCollection(count);
			lock(this)
			{
				foreach(DojoPromotionFlag item in this)
					clonedDojoPromotionFlag.Add(item);
			}
			return clonedDojoPromotionFlag;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoPromotionFlag.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoPromotionFlagCollection from their children.</param>
		public DojoPromotionFlagCollection Copy(bool isolated)
		{
			DojoPromotionFlagCollection isolatedCollection = new DojoPromotionFlagCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoPromotionFlagArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoPromotionFlagArray[i].Copy());
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
				Array.Sort(DojoPromotionFlagArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoPromotionFlag Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoPromotionFlagArray[x].ID == id)
						return DojoPromotionFlagArray[x];
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
				s.Append(DojoPromotionFlagArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
