using System;
using System.Collections;

namespace Amns.GreyFox.Tessen
{
	/// <summary>
	/// <summary>
	/// Registration option wrapper.
	/// </summary>
	/// </summary>
	public class DojoSeminarRegistrationOptionIndexedList : IList, ICloneable 
	{
		private int count = 0;
		private int[,] index;
		private DojoSeminarRegistrationOption[] DojoSeminarRegistrationOptionArray ;

		public DojoSeminarRegistrationOptionIndexedList() : base()
		{
			index = new int[15,2];
			DojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[15];
		}

		public DojoSeminarRegistrationOptionIndexedList(int capacity) : base()
		{
			index = new int[capacity,2];
			DojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[capacity];
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
				lock(this)
				{
					DojoSeminarRegistrationOptionArray[index] = value;
				}
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
				ensureArrays();
				addIndexKey(value.ID);
				DojoSeminarRegistrationOptionArray[count - 1] = value;
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
			Insert(index, (DojoSeminarRegistrationOption) value);
		}

		public void Insert(int index, DojoSeminarRegistrationOption value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				ensureArrays();
				addIndexKey(value.ID);
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
				removeIndexKey(DojoSeminarRegistrationOptionArray[index].ID);
				for(int x = index + 1; x <= count - 1; x++)
				{
					DojoSeminarRegistrationOptionArray[x-1] = DojoSeminarRegistrationOptionArray[x];
				}
				DojoSeminarRegistrationOptionArray[count - 1] = null;
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

		object ICloneable.Clone()
		{
			return Clone();
		}

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
			if(count > DojoSeminarRegistrationOptionArray.GetUpperBound(0) + 1)
			{
				int[,] tempIndex = new int[count * 2, 2];
				DojoSeminarRegistrationOption[] tempDojoSeminarRegistrationOptionArray = new DojoSeminarRegistrationOption[count * 2];
				for(int x = 0; x <= DojoSeminarRegistrationOptionArray.GetUpperBound(0); x++)
				{
					tempIndex[x,0] = index[x,0];	 // Copy ID
					tempIndex[x,1] = index[x,1];	 // Copy Location
					tempDojoSeminarRegistrationOptionArray[x] = DojoSeminarRegistrationOptionArray[x]; // Copy Object
				}
				index = tempIndex;
				DojoSeminarRegistrationOptionArray = tempDojoSeminarRegistrationOptionArray;
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
