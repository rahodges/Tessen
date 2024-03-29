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
	/// Registration option wrapper.
	/// </summary>
	/// </summary>
	public class DojoSeminarRegistrationOptionCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoSeminarRegistrationOption[] DojoSeminarRegistrationOptionArray ;

		public DojoSeminarRegistrationOptionCollection() : base()
		{
			DojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[15];
		}

		public DojoSeminarRegistrationOptionCollection(int capacity) : base()
		{
			DojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[capacity];
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
				DojoSeminarRegistrationOptionArray[index] = (DojoSeminarRegistrationOption) value;
			}
		}

		public DojoSeminarRegistrationOption this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoSeminarRegistrationOptionArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoSeminarRegistrationOptionArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoSeminarRegistrationOption) value);
		}

		public int Add(DojoSeminarRegistrationOption value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoSeminarRegistrationOptionArray.GetUpperBound(0) + 1)
				{
					DojoSeminarRegistrationOption[] tempDojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[count * 2];
					Array.Copy(DojoSeminarRegistrationOptionArray, tempDojoSeminarRegistrationOptionArray, count - 1);
					DojoSeminarRegistrationOptionArray = tempDojoSeminarRegistrationOptionArray;
				}
				DojoSeminarRegistrationOptionArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoSeminarRegistrationOption) value);
		}

		public bool Contains(DojoSeminarRegistrationOption value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoSeminarRegistrationOption) value);
		}

		public int IndexOf(DojoSeminarRegistrationOption value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoSeminarRegistrationOptionArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoSeminarRegistrationOption) value);
		}

		public void Insert(int index, DojoSeminarRegistrationOption value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoSeminarRegistrationOptionArray.GetUpperBound(0) + 1)
				{
					DojoSeminarRegistrationOption[] tempDojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[count * 2];
					Array.Copy(DojoSeminarRegistrationOptionArray, tempDojoSeminarRegistrationOptionArray, count - 1);
					DojoSeminarRegistrationOptionArray = tempDojoSeminarRegistrationOptionArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoSeminarRegistrationOptionArray[x] = DojoSeminarRegistrationOptionArray[x - 1];
				DojoSeminarRegistrationOptionArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoSeminarRegistrationOption) value);
		}

		public void Remove(DojoSeminarRegistrationOption value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoSeminarRegistrationOption not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoSeminarRegistrationOptionArray[x-1] = DojoSeminarRegistrationOptionArray[x];
				DojoSeminarRegistrationOptionArray[count - 1] = null;
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
				return DojoSeminarRegistrationOptionArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoSeminarRegistrationOptionArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoSeminarRegistrationOptionArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoSeminarRegistrationOptionArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoSeminarRegistrationOption[] DojoSeminarRegistrationOptionArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoSeminarRegistrationOption[] DojoSeminarRegistrationOptionArray, int virtualCount)
			{
				this.DojoSeminarRegistrationOptionArray = DojoSeminarRegistrationOptionArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoSeminarRegistrationOptionArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoSeminarRegistrationOption Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoSeminarRegistrationOptionArray[cursor];
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
		/// Makes a shallow copy of the current DojoSeminarRegistrationOptionCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoSeminarRegistrationOptionCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoSeminarRegistrationOptionCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoSeminarRegistrationOptionCollection</returns>
		public DojoSeminarRegistrationOptionCollection Clone()
		{
			DojoSeminarRegistrationOptionCollection clonedDojoSeminarRegistrationOption = new DojoSeminarRegistrationOptionCollection(count);
			lock(this)
			{
				foreach(DojoSeminarRegistrationOption item in this)
					clonedDojoSeminarRegistrationOption.Add(item);
			}
			return clonedDojoSeminarRegistrationOption;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoSeminarRegistrationOption.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoSeminarRegistrationOptionCollection from their children.</param>
		public DojoSeminarRegistrationOptionCollection Copy(bool isolated)
		{
			DojoSeminarRegistrationOptionCollection isolatedCollection = new DojoSeminarRegistrationOptionCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoSeminarRegistrationOptionArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoSeminarRegistrationOptionArray[i].Copy());
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
				Array.Sort(DojoSeminarRegistrationOptionArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoSeminarRegistrationOption Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoSeminarRegistrationOptionArray[x].ID == id)
						return DojoSeminarRegistrationOptionArray[x];
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
				s.Append(DojoSeminarRegistrationOptionArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
