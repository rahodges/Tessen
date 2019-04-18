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
	/// Holds information on a promotion.
	/// </summary>
	/// </summary>
	public class DojoPromotionCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoPromotion[] DojoPromotionArray ;

		public DojoPromotionCollection() : base()
		{
			DojoPromotionArray = new DojoPromotion[15];
		}

		public DojoPromotionCollection(int capacity) : base()
		{
			DojoPromotionArray = new DojoPromotion[capacity];
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
				DojoPromotionArray[index] = (DojoPromotion) value;
			}
		}

		public DojoPromotion this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoPromotionArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoPromotionArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoPromotion) value);
		}

		public int Add(DojoPromotion value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoPromotionArray.GetUpperBound(0) + 1)
				{
					DojoPromotion[] tempDojoPromotionArray = new DojoPromotion[count * 2];
					Array.Copy(DojoPromotionArray, tempDojoPromotionArray, count - 1);
					DojoPromotionArray = tempDojoPromotionArray;
				}
				DojoPromotionArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoPromotionArray = new DojoPromotion[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoPromotion) value);
		}

		public bool Contains(DojoPromotion value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoPromotion) value);
		}

		public int IndexOf(DojoPromotion value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoPromotionArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoPromotion) value);
		}

		public void Insert(int index, DojoPromotion value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoPromotionArray.GetUpperBound(0) + 1)
				{
					DojoPromotion[] tempDojoPromotionArray = new DojoPromotion[count * 2];
					Array.Copy(DojoPromotionArray, tempDojoPromotionArray, count - 1);
					DojoPromotionArray = tempDojoPromotionArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoPromotionArray[x] = DojoPromotionArray[x - 1];
				DojoPromotionArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoPromotion) value);
		}

		public void Remove(DojoPromotion value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoPromotion not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoPromotionArray[x-1] = DojoPromotionArray[x];
				DojoPromotionArray[count - 1] = null;
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
				return DojoPromotionArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoPromotionArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoPromotionArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoPromotionArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoPromotion[] DojoPromotionArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoPromotion[] DojoPromotionArray, int virtualCount)
			{
				this.DojoPromotionArray = DojoPromotionArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoPromotionArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoPromotion Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoPromotionArray[cursor];
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
		/// Makes a shallow copy of the current DojoPromotionCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoPromotionCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoPromotionCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoPromotionCollection</returns>
		public DojoPromotionCollection Clone()
		{
			DojoPromotionCollection clonedDojoPromotion = new DojoPromotionCollection(count);
			lock(this)
			{
				foreach(DojoPromotion item in this)
					clonedDojoPromotion.Add(item);
			}
			return clonedDojoPromotion;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoPromotion.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoPromotionCollection from their children.</param>
		public DojoPromotionCollection Copy(bool isolated)
		{
			DojoPromotionCollection isolatedCollection = new DojoPromotionCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoPromotionArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoPromotionArray[i].Copy());
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
				Array.Sort(DojoPromotionArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoPromotion Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoPromotionArray[x].ID == id)
						return DojoPromotionArray[x];
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
				s.Append(DojoPromotionArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
