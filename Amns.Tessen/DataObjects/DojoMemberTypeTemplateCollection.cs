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
	/// A template group for organizing and applying templates to 
	/// members.
	/// </summary>
	/// </summary>
	public class DojoMemberTypeTemplateCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoMemberTypeTemplate[] DojoMemberTypeTemplateArray ;

		public DojoMemberTypeTemplateCollection() : base()
		{
			DojoMemberTypeTemplateArray = new DojoMemberTypeTemplate[15];
		}

		public DojoMemberTypeTemplateCollection(int capacity) : base()
		{
			DojoMemberTypeTemplateArray = new DojoMemberTypeTemplate[capacity];
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
				DojoMemberTypeTemplateArray[index] = (DojoMemberTypeTemplate) value;
			}
		}

		public DojoMemberTypeTemplate this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoMemberTypeTemplateArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoMemberTypeTemplateArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoMemberTypeTemplate) value);
		}

		public int Add(DojoMemberTypeTemplate value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoMemberTypeTemplateArray.GetUpperBound(0) + 1)
				{
					DojoMemberTypeTemplate[] tempDojoMemberTypeTemplateArray = new DojoMemberTypeTemplate[count * 2];
					Array.Copy(DojoMemberTypeTemplateArray, tempDojoMemberTypeTemplateArray, count - 1);
					DojoMemberTypeTemplateArray = tempDojoMemberTypeTemplateArray;
				}
				DojoMemberTypeTemplateArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoMemberTypeTemplateArray = new DojoMemberTypeTemplate[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoMemberTypeTemplate) value);
		}

		public bool Contains(DojoMemberTypeTemplate value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoMemberTypeTemplate) value);
		}

		public int IndexOf(DojoMemberTypeTemplate value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoMemberTypeTemplateArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoMemberTypeTemplate) value);
		}

		public void Insert(int index, DojoMemberTypeTemplate value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoMemberTypeTemplateArray.GetUpperBound(0) + 1)
				{
					DojoMemberTypeTemplate[] tempDojoMemberTypeTemplateArray = new DojoMemberTypeTemplate[count * 2];
					Array.Copy(DojoMemberTypeTemplateArray, tempDojoMemberTypeTemplateArray, count - 1);
					DojoMemberTypeTemplateArray = tempDojoMemberTypeTemplateArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoMemberTypeTemplateArray[x] = DojoMemberTypeTemplateArray[x - 1];
				DojoMemberTypeTemplateArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoMemberTypeTemplate) value);
		}

		public void Remove(DojoMemberTypeTemplate value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoMemberTypeTemplate not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoMemberTypeTemplateArray[x-1] = DojoMemberTypeTemplateArray[x];
				DojoMemberTypeTemplateArray[count - 1] = null;
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
				return DojoMemberTypeTemplateArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoMemberTypeTemplateArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoMemberTypeTemplateArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoMemberTypeTemplateArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoMemberTypeTemplate[] DojoMemberTypeTemplateArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoMemberTypeTemplate[] DojoMemberTypeTemplateArray, int virtualCount)
			{
				this.DojoMemberTypeTemplateArray = DojoMemberTypeTemplateArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoMemberTypeTemplateArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoMemberTypeTemplate Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoMemberTypeTemplateArray[cursor];
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
		/// Makes a shallow copy of the current DojoMemberTypeTemplateCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoMemberTypeTemplateCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoMemberTypeTemplateCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoMemberTypeTemplateCollection</returns>
		public DojoMemberTypeTemplateCollection Clone()
		{
			DojoMemberTypeTemplateCollection clonedDojoMemberTypeTemplate = new DojoMemberTypeTemplateCollection(count);
			lock(this)
			{
				foreach(DojoMemberTypeTemplate item in this)
					clonedDojoMemberTypeTemplate.Add(item);
			}
			return clonedDojoMemberTypeTemplate;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoMemberTypeTemplate.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoMemberTypeTemplateCollection from their children.</param>
		public DojoMemberTypeTemplateCollection Copy(bool isolated)
		{
			DojoMemberTypeTemplateCollection isolatedCollection = new DojoMemberTypeTemplateCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoMemberTypeTemplateArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoMemberTypeTemplateArray[i].Copy());
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
				Array.Sort(DojoMemberTypeTemplateArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoMemberTypeTemplate Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoMemberTypeTemplateArray[x].ID == id)
						return DojoMemberTypeTemplateArray[x];
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
				s.Append(DojoMemberTypeTemplateArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

		//--- Begin Custom Code ---

		//--- End Custom Code ---
	}
}
