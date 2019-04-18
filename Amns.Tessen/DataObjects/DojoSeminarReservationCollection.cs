using System;
using System.Collections;

namespace Amns.Tessen
{
	/// <summary>
	/// <summary>
	/// Summary of MyClass
	/// </summary>
	/// </summary>
	public class DojoSeminarReservationCollection : IList, ICloneable 
	{
		private int count = 0;
		private DojoSeminarReservation[] DojoSeminarReservationArray ;

		public DojoSeminarReservationCollection() : base()
		{
			DojoSeminarReservationArray = new DojoSeminarReservation[15];
		}

		public DojoSeminarReservationCollection(int capacity) : base()
		{
			DojoSeminarReservationArray = new DojoSeminarReservation[capacity];
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
				DojoSeminarReservationArray[index] = (DojoSeminarReservation) value;
			}
		}

		public DojoSeminarReservation this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoSeminarReservationArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				DojoSeminarReservationArray[index] = value;
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoSeminarReservation) value);
		}

		public int Add(DojoSeminarReservation value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoSeminarReservationArray.GetUpperBound(0) + 1)
				{
					DojoSeminarReservation[] tempDojoSeminarReservationArray = new DojoSeminarReservation[count * 2];
					Array.Copy(DojoSeminarReservationArray, tempDojoSeminarReservationArray, count - 1);
					DojoSeminarReservationArray = tempDojoSeminarReservationArray;
				}
				DojoSeminarReservationArray[count - 1] = value;
			}
			return count -1;
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				DojoSeminarReservationArray = new DojoSeminarReservation[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoSeminarReservation) value);
		}

		public bool Contains(DojoSeminarReservation value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoSeminarReservation) value);
		}

		public int IndexOf(DojoSeminarReservation value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoSeminarReservationArray[x].Equals(value))
						return x;
				return -1;
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoSeminarReservation) value);
		}

		public void Insert(int index, DojoSeminarReservation value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				// Resize the array if the count is greater than the length 
				// of the array.
				if(count > DojoSeminarReservationArray.GetUpperBound(0) + 1)
				{
					DojoSeminarReservation[] tempDojoSeminarReservationArray = new DojoSeminarReservation[count * 2];
					Array.Copy(DojoSeminarReservationArray, tempDojoSeminarReservationArray, count - 1);
					DojoSeminarReservationArray = tempDojoSeminarReservationArray;
				}
				for(int x = index + 1; x == count - 2; x ++)
					DojoSeminarReservationArray[x] = DojoSeminarReservationArray[x - 1];
				DojoSeminarReservationArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoSeminarReservation) value);
		}

		public void Remove(DojoSeminarReservation value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoSeminarReservation not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				for(int x = index + 1; x <= count - 1; x++)
					DojoSeminarReservationArray[x-1] = DojoSeminarReservationArray[x];
				DojoSeminarReservationArray[count - 1] = null;
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
				return DojoSeminarReservationArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoSeminarReservationArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoSeminarReservationArray.CopyTo(array, index);
			}
		}

		#endregion

		#region IEnumerator Implementation

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoSeminarReservationArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoSeminarReservation[] DojoSeminarReservationArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoSeminarReservation[] DojoSeminarReservationArray, int virtualCount)
			{
				this.DojoSeminarReservationArray = DojoSeminarReservationArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoSeminarReservationArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoSeminarReservation Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoSeminarReservationArray[cursor];
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
		/// Makes a shallow copy of the current DojoSeminarReservationCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoSeminarReservationCollection</returns>
		#region ICloneable Implementation

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Makes a shallow copy of the current DojoSeminarReservationCollection.
		/// as the parent object.
		/// </summary>
		/// <returns>DojoSeminarReservationCollection</returns>
		public DojoSeminarReservationCollection Clone()
		{
			DojoSeminarReservationCollection clonedDojoSeminarReservation = new DojoSeminarReservationCollection(count);
			lock(this)
			{
				foreach(DojoSeminarReservation item in this)
					clonedDojoSeminarReservation.Add(item);
			}
			return clonedDojoSeminarReservation;
		}

		/// <summary>
		/// Makes a deep copy of the current DojoSeminarReservation.
		/// </summary>
		/// <param name="isolation">Placeholders are used to isolate the 
		/// items in the DojoSeminarReservationCollection from their children.</param>
		public DojoSeminarReservationCollection Copy(bool isolated)
		{
			DojoSeminarReservationCollection isolatedCollection = new DojoSeminarReservationCollection(count);

			lock(this)
			{
				if(isolated)
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoSeminarReservationArray[i].NewPlaceHolder());
					}
				}
				else
				{
					for(int i = 0; i < count; i++)
					{
						isolatedCollection.Add(DojoSeminarReservationArray[i].Copy());
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
				Array.Sort(DojoSeminarReservationArray, 0, count);
			}
		}

		#endregion

		#region Find Methods

		/// <summary>
		/// Finds a record by ID.
		/// </summary>
		public DojoSeminarReservation Find(int id)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoSeminarReservationArray[x].ID == id)
						return DojoSeminarReservationArray[x];
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
				s.Append(DojoSeminarReservationArray[x].ToString());
			}

			return s.ToString();
		}

		#endregion

	}
}
