using System;
using System.Collections;

namespace Amns.GreyFox.Tessen
{
	/// <summary>
	/// <summary>
	/// Summary of MyClass
	/// </summary>
	/// </summary>
	public class DojoBulkAttendanceEntryIndexedList : IList, ICloneable 
	{
		private int count = 0;
		private int[,] index;
		private DojoBulkAttendanceEntry[] DojoBulkAttendanceEntryArray ;

		public DojoBulkAttendanceEntryIndexedList() : base()
		{
			index = new int[15,2];
			DojoBulkAttendanceEntryArray = new DojoBulkAttendanceEntry[15];
		}

		public DojoBulkAttendanceEntryIndexedList(int capacity) : base()
		{
			index = new int[capacity,2];
			DojoBulkAttendanceEntryArray = new DojoBulkAttendanceEntry[capacity];
		}

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
				DojoBulkAttendanceEntryArray[index] = (DojoBulkAttendanceEntry) value;
			}
		}

		public DojoBulkAttendanceEntry this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoBulkAttendanceEntryArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				lock(this)
				{
					DojoBulkAttendanceEntryArray[index] = value;
				}
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoBulkAttendanceEntry) value);
		}

		public int Add(DojoBulkAttendanceEntry value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				ensureArrays();
				addIndexKey(value.ID);
				DojoBulkAttendanceEntryArray[count - 1] = value;
				return count -1;
			}
		}

		public void Clear()
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count = 0;
				index = new int[15,2];
				DojoBulkAttendanceEntryArray = new DojoBulkAttendanceEntry[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoBulkAttendanceEntry) value);
		}

		public bool Contains(DojoBulkAttendanceEntry value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoBulkAttendanceEntry) value);
		}

		public int IndexOf(DojoBulkAttendanceEntry value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoBulkAttendanceEntryArray[x].Equals(value))
						return x;
			}
			return -1;
		}

		public int IndexOf(int id)
		{
			lock(this)
			{
				int i = binarySearch(id);
				if(i == -1)
					return -1;
				return index[i, 1];
			}
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (DojoBulkAttendanceEntry) value);
		}

		public void Insert(int index, DojoBulkAttendanceEntry value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				ensureArrays();
				addIndexKey(value.ID);
				for(int x = index + 1; x == count - 2; x ++)
					DojoBulkAttendanceEntryArray[x] = DojoBulkAttendanceEntryArray[x - 1];
				DojoBulkAttendanceEntryArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoBulkAttendanceEntry) value);
		}

		public void Remove(DojoBulkAttendanceEntry value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoBulkAttendanceEntry not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				removeIndexKey(DojoBulkAttendanceEntryArray[index].ID);
				for(int x = index + 1; x <= count - 1; x++)
				{
					DojoBulkAttendanceEntryArray[x-1] = DojoBulkAttendanceEntryArray[x];
				}
				DojoBulkAttendanceEntryArray[count - 1] = null;
				count--;
			}
		}

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
				return DojoBulkAttendanceEntryArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoBulkAttendanceEntryArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoBulkAttendanceEntryArray.CopyTo(array, index);
			}
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoBulkAttendanceEntryArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoBulkAttendanceEntry[] DojoBulkAttendanceEntryArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoBulkAttendanceEntry[] DojoBulkAttendanceEntryArray, int virtualCount)
			{
				this.DojoBulkAttendanceEntryArray = DojoBulkAttendanceEntryArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoBulkAttendanceEntryArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoBulkAttendanceEntry Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoBulkAttendanceEntryArray[cursor];
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

		object ICloneable.Clone()
		{
			return Clone();
		}

		public DojoBulkAttendanceEntryCollection Clone()
		{
			DojoBulkAttendanceEntryCollection clonedDojoBulkAttendanceEntry = new DojoBulkAttendanceEntryCollection(count);
			lock(this)
			{
				foreach(DojoBulkAttendanceEntry item in this)
					clonedDojoBulkAttendanceEntry.Add(item);
			}
			return clonedDojoBulkAttendanceEntry;
		}

		public event EventHandler CollectionChanged;

		protected virtual void OnCollectionChanged(EventArgs e)
		{
			if(CollectionChanged != null)
				CollectionChanged(this, e);
		}

		/// <summary>
		/// Ensures that the index and object array are sized correctly 
		/// for additions. This method should be protected by locks 
		/// issued by calling methods.
		/// </summary>
		private void ensureArrays()
		{
			if(count > DojoBulkAttendanceEntryArray.GetUpperBound(0) + 1)
			{
				int[,] tempIndex = new int[count * 2, 2];
				DojoBulkAttendanceEntry[] tempDojoBulkAttendanceEntryArray = new DojoBulkAttendanceEntry[count * 2];
				for(int x = 0; x <= DojoBulkAttendanceEntryArray.GetUpperBound(0); x++)
				{
					tempIndex[x,0] = index[x,0];	 // Copy ID
					tempIndex[x,1] = index[x,1];	 // Copy Location
					tempDojoBulkAttendanceEntryArray[x] = DojoBulkAttendanceEntryArray[x]; // Copy Object
				}
				index = tempIndex;
				DojoBulkAttendanceEntryArray = tempDojoBulkAttendanceEntryArray;
			}
		}

		/// <summary>
		/// Ensures that the index and object array are sized correctly 
		/// for additions.
		/// </summary>
		private void addIndexKey(int id)
		{
			index[count - 1, 0] = id;
			index[count - 1, 1] = count - 1;
			quickSort(0, count -1);
		}

		/// <summary>
		/// Ensures that the index and object array are sized correctly 
		/// for additions.
		/// </summary>
		private void removeIndexKey(int id)
		{
			int i = binarySearch(id);
			if(count > 1)
			{
				for(int x = i; x < count; x++)
				{
					index[x, 0] = index[x + 1, 0];
					index[x, 1] = index[x + 1, 1];
				}
			}
		}

		/// <summary>
		/// Ensures that the index and object array are sized correctly 
		/// for additions.
		/// </summary>
		private void quickSort(int left, int right)
		{
			int i, j, x;
			int ya, yb;
			i = left;
			j = right;
			x = index[(left + right) / 2, 0];
			while(i <= j)
			{
				while(index[i, 0] < x & i < right)
					i++;
				while(x < index[j, 0] & j > left)
					j--;
				if(i <= j)
				{
					ya = index[i, 0];
					yb = index[i, 1];
					index[i, 0] = index[j, 0];
					index[i, 1] = index[j, 1];
					index[j, 0] = ya;
					index[j, 1] = yb;
					i++;
					j--;
				}
			}
			if(left < j) quickSort(left, j);
			if(i < right) quickSort(i, right);
		}

		/// <summary>
		/// Finds the location of the id.
		/// </summary>
		private int binarySearch(int id)
		{
			int left = 0;
			int right = count - 1;
			while (left <= right)
			{
				int middle = (left + right) / 2;
				if(id > index[middle, 0])
					left = middle + 1;
				else if(id < index[middle, 0])
					right = middle - 1;
				else
					return middle;
			}
			return -1;
		}

	}
}
