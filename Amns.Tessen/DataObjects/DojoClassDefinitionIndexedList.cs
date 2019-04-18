using System;
using System.Collections;

namespace Amns.GreyFox.Tessen
{
	/// <summary>
	/// <summary>
	/// Creates and schedules classes for the dojo.
	/// </summary>
	/// </summary>
	public class DojoClassDefinitionIndexedList : IList, ICloneable 
	{
		private int count = 0;
		private int[,] index;
		private DojoClassDefinition[] DojoClassDefinitionArray ;

		public DojoClassDefinitionIndexedList() : base()
		{
			index = new int[15,2];
			DojoClassDefinitionArray = new DojoClassDefinition[15];
		}

		public DojoClassDefinitionIndexedList(int capacity) : base()
		{
			index = new int[capacity,2];
			DojoClassDefinitionArray = new DojoClassDefinition[capacity];
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
				DojoClassDefinitionArray[index] = (DojoClassDefinition) value;
			}
		}

		public DojoClassDefinition this[int index]
		{
			get
			{
				lock(this)
				{
					if(index > count - 1)
						throw(new Exception("Index out of bounds."));
					return DojoClassDefinitionArray[index];
				}
			}
			set
			{
				OnCollectionChanged(EventArgs.Empty);
				lock(this)
				{
					DojoClassDefinitionArray[index] = value;
				}
			}
		}

		int IList.Add(object value)
		{
			return Add((DojoClassDefinition) value);
		}

		public int Add(DojoClassDefinition value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				ensureArrays();
				addIndexKey(value.ID);
				DojoClassDefinitionArray[count - 1] = value;
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
				DojoClassDefinitionArray = new DojoClassDefinition[15];
			}
		}

		bool IList.Contains(object value)
		{
			return Contains((DojoClassDefinition) value);
		}

		public bool Contains(DojoClassDefinition value)
		{
			return IndexOf(value) != -1;
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((DojoClassDefinition) value);
		}

		public int IndexOf(DojoClassDefinition value)
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
					if(DojoClassDefinitionArray[x].Equals(value))
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
			Insert(index, (DojoClassDefinition) value);
		}

		public void Insert(int index, DojoClassDefinition value)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				count++;
				ensureArrays();
				addIndexKey(value.ID);
				for(int x = index + 1; x == count - 2; x ++)
					DojoClassDefinitionArray[x] = DojoClassDefinitionArray[x - 1];
				DojoClassDefinitionArray[index] = value;
			}
		}

		void IList.Remove(object value)
		{
			Remove((DojoClassDefinition) value);
		}

		public void Remove(DojoClassDefinition value)
		{
			OnCollectionChanged(EventArgs.Empty);
			int index = IndexOf(value);
			if(index == -1)
				throw(new Exception("DojoClassDefinition not found in collection."));
			RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			OnCollectionChanged(EventArgs.Empty);
			lock(this)
			{
				removeIndexKey(DojoClassDefinitionArray[index].ID);
				for(int x = index + 1; x <= count - 1; x++)
				{
					DojoClassDefinitionArray[x-1] = DojoClassDefinitionArray[x];
				}
				DojoClassDefinitionArray[count - 1] = null;
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
				return DojoClassDefinitionArray.IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return DojoClassDefinitionArray;
			}
		}

		public void CopyTo(Array array, int index)
		{
			lock(this)
			{
				DojoClassDefinitionArray.CopyTo(array, index);
			}
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(DojoClassDefinitionArray, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator
		{
			private DojoClassDefinition[] DojoClassDefinitionArray;
			private int cursor;
			private int virtualCount;

			public Enumerator(DojoClassDefinition[] DojoClassDefinitionArray, int virtualCount)
			{
				this.DojoClassDefinitionArray = DojoClassDefinitionArray;
				this.virtualCount = virtualCount;
				cursor = -1;
			}

			public void Reset()
			{
				cursor = -1;
			}

			public bool MoveNext()
			{
				if(cursor < DojoClassDefinitionArray.Length)
					cursor++;
				return(!(cursor == virtualCount));
			}

			public DojoClassDefinition Current
			{
				get
				{
					if((cursor < 0) || (cursor == virtualCount))
						throw(new InvalidOperationException());
					return DojoClassDefinitionArray[cursor];
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

		public DojoClassDefinitionCollection Clone()
		{
			DojoClassDefinitionCollection clonedDojoClassDefinition = new DojoClassDefinitionCollection(count);
			lock(this)
			{
				foreach(DojoClassDefinition item in this)
					clonedDojoClassDefinition.Add(item);
			}
			return clonedDojoClassDefinition;
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
			if(count > DojoClassDefinitionArray.GetUpperBound(0) + 1)
			{
				int[,] tempIndex = new int[count * 2, 2];
				DojoClassDefinition[] tempDojoClassDefinitionArray = new DojoClassDefinition[count * 2];
				for(int x = 0; x <= DojoClassDefinitionArray.GetUpperBound(0); x++)
				{
					tempIndex[x,0] = index[x,0];	 // Copy ID
					tempIndex[x,1] = index[x,1];	 // Copy Location
					tempDojoClassDefinitionArray[x] = DojoClassDefinitionArray[x]; // Copy Object
				}
				index = tempIndex;
				DojoClassDefinitionArray = tempDojoClassDefinitionArray;
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
