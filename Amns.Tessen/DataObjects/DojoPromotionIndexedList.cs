using System;
using System.Collections;

namespace Amns.GreyFox.Tessen
{
	/// <summary>
	/// <summary>
	/// Holds information on a promotion.
	/// </summary>
	/// </summary>
	public class DojoPromotionIndexedList : IList, ICloneable 
	{
		private int count = 0;
		private int[,] index;
		private DojoPromotion[] DojoPromotionArray ;

		public DojoPromotionIndexedList() : base()
		{
			index = new int[15,2];
			DojoPromotionArray = new DojoPromotion[15];
		}

		public DojoPromotionIndexedList(int capacity) : base()
		{
			index = new int[capacity,2];
			DojoPromotionArray = new DojoPromotion[capacity];
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
				lock(this)
				{
					DojoPromotionArray[index] = value;
				}
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
				ensureArrays();
				addIndexKey(value.ID);
				DojoPromotionArray[count - 1] = value;
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
			Insert(index, (DojoPromotion) value);
		}

		public void Insert(int index, DojoPromotion value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				ensureArrays();
				addIndexKey(value.ID);
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
				removeIndexKey(DojoPromotionArray[index].ID);
				for(int x = index + 1; x <= count - 1; x++)
				{
					DojoPromotionArray[x-1] = DojoPromotionArray[x];
				}
				DojoPromotionArray[count - 1] = null;
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

		object ICloneable.Clone()
		{
			return Clone();
		}

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
			if(count > DojoPromotionArray.GetUpperBound(0) + 1)
			{
				int[,] tempIndex = new int[count * 2, 2];
				DojoPromotion[] tempDojoPromotionArray = new DojoPromotion[count * 2];
				for(int x = 0; x <= DojoPromotionArray.GetUpperBound(0); x++)
				{
					tempIndex[x,0] = index[x,0];	 // Copy ID
					tempIndex[x,1] = index[x,1];	 // Copy Location
					tempDojoPromotionArray[x] = DojoPromotionArray[x]; // Copy Object
				}
				index = tempIndex;
				DojoPromotionArray = tempDojoPromotionArray;
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
