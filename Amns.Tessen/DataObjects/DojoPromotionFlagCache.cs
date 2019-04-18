using System;
using System.Collections;
using System.Threading;

namespace Amns.GreyFox.Tessen
{
	/// <summary>
	/// <summary>
	/// Promotion Flags
	/// </summary>
	/// </summary>
	public class DojoPromotionFlagCache : IEnumerable
	{
		private bool monitorRun = false;
		private Thread monitorThread;
		private int hits = 0;
		private int misses = 0;
		private int sorts = 0;

		private int count = 0;
		private DateTime[] timeStamps;
		private DateTime[] absoluteExpirations;
		private TimeSpan[] slidingExpirations;
		private DojoPromotionFlag[] dojoPromotionFlagArray ;

		public int Hits
		{
			get { return hits; }
		}

		public int Misses
		{
			get { return misses; }
		}

		public int Sorts
		{
			get { return sorts; }
		}

		public DojoPromotionFlagCache()
		{
			dojoPromotionFlagArray = new DojoPromotionFlag[15];
			timeStamps = new DateTime[15];
			absoluteExpirations = new DateTime[15];
			slidingExpirations = new TimeSpan[15];

			// Start the cache monitor
			monitorThread = new Thread(new ThreadStart(monitor));
			monitorThread.IsBackground = true;
			monitorThread.Start();
		}

		public DojoPromotionFlag this[int index]
		{
			get
			{
				lock(this)
				{
					int i = binarySearch(index);
					if(i == -1)
					{
						misses++;
						return null;
					}
					hits++;
					if(slidingExpirations[i] != TimeSpan.Zero)
						absoluteExpirations[i].Add(slidingExpirations[i]);
					return dojoPromotionFlagArray[i];
				}
			}
		}

		public void CheckedAdd(DojoPromotionFlag dojoPromotionFlag, DateTime expiration)
		{
			lock(this)
			{
				int i = binarySearch(dojoPromotionFlag.iD);
				if(i != -1)
				{
					dojoPromotionFlagArray[i] = dojoPromotionFlag;
					absoluteExpirations[i] = expiration; // Expires
					slidingExpirations[i] = TimeSpan.Zero; // Never slides
					return;
				}
				count++;
				ensureArrays();
				dojoPromotionFlagArray[count - 1] = dojoPromotionFlag;
				timeStamps[count -1] = DateTime.Now;
				absoluteExpirations[count -1] = expiration; // Expires
				slidingExpirations[count-1] = TimeSpan.Zero; // Never slides
				quickSort(0, count - 1);
			}
		}

		public void CheckedAdd(DojoPromotionFlag dojoPromotionFlag, TimeSpan slidingExpiration)
		{
			lock(this)
			{
				int i = binarySearch(dojoPromotionFlag.iD);
				if(i != -1)
				{
					dojoPromotionFlagArray[i] = dojoPromotionFlag;
					absoluteExpirations[i] = DateTime.Now.Add(slidingExpiration); // Expires
					slidingExpirations[i] = slidingExpiration; // Never slides
					return;
				}
				count++;
				ensureArrays();
				dojoPromotionFlagArray[count - 1] = dojoPromotionFlag;
				timeStamps[count -1] = DateTime.Now;
				absoluteExpirations[count -1] = DateTime.Now.Add(slidingExpiration); // Expires
				slidingExpirations[count-1] = slidingExpiration; // Never slides
				quickSort(0, count - 1);
			}
		}

		public void Add(DojoPromotionFlag dojoPromotionFlag, DateTime expiration)
		{
			lock(this)
			{
				count++;
				ensureArrays();
				dojoPromotionFlagArray[count - 1] = dojoPromotionFlag;
				timeStamps[count -1] = DateTime.Now;
				absoluteExpirations[count -1] = expiration; // Expires
				slidingExpirations[count-1] = TimeSpan.Zero; // Never slides
				quickSort(0, count - 1);
			}
		}

		public void Add(DojoPromotionFlag dojoPromotionFlag, TimeSpan slidingExpiration)
		{
			lock(this)
			{
				count++;
				ensureArrays();
				dojoPromotionFlagArray[count - 1] = dojoPromotionFlag;
				timeStamps[count -1] = DateTime.Now;
				absoluteExpirations[count -1] = DateTime.Now.Add(slidingExpiration); // Never Expires
				slidingExpirations[count-1] = slidingExpiration; // Never slides
				quickSort(0, count - 1);
			}
		}

		public void Clear()
		{
			lock(this)
			{
				count = 0;
				dojoPromotionFlagArray = new DojoPromotionFlag[15];
				timeStamps = new DateTime[15];
				absoluteExpirations = new DateTime[15];
				slidingExpirations = new TimeSpan[15];
			}
		}

		public int IndexOf(int id)
		{
			lock(this)
			{
				return binarySearch(id);
			}
		}

		public void Remove(int id)
		{
			lock(this)
			{
				int i = binarySearch(id);
				if(i == -1)
					return;
				remove(i);
			}
		}

		private void remove(int index)
		{
			for(int x = index + 1; x < count; x++)
			{
				dojoPromotionFlagArray[x-1] = dojoPromotionFlagArray[x];
				timeStamps[x-1] = timeStamps[x]; // Copy TimeStamp
				absoluteExpirations[x-1] = absoluteExpirations[x]; // Copy AbsoluteExpiration
				slidingExpirations[x-1] = slidingExpirations[x]; // Copy SlidingExpiration
			}
			dojoPromotionFlagArray[count - 1] = null;
			timeStamps[count - 1] = DateTime.MinValue;
			absoluteExpirations[count - 1] = DateTime.MaxValue;
			slidingExpirations[count - 1] = TimeSpan.Zero;
			count--;
		}

		public int Count
		{
			get
			{
				return count;
			}
		}

		#region Enumerator

		public Enumerator GetEnumerator()
		{
			return new Enumerator(dojoPromotionFlagArray, count);
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

		#region EnsureArrays

		/// <summary>
		/// Ensures that the index and object array are sized correctly 
		/// for additions. This method should be protected by locks 
		/// issued by calling methods.
		/// </summary>
		private void ensureArrays()
		{
			if(count > dojoPromotionFlagArray.GetUpperBound(0) + 1)
			{
				DojoPromotionFlag[] tempDojoPromotionFlagArray = new DojoPromotionFlag[count * 2];
				DateTime[] tempTimeStamps = new DateTime[count * 2];
				DateTime[] tempAbsoluteExpirations = new DateTime[count * 2];
				TimeSpan[] tempSlidingExpirations = new TimeSpan[count *2];
				Array.Copy(dojoPromotionFlagArray, tempDojoPromotionFlagArray, count - 1);
				Array.Copy(timeStamps, tempTimeStamps, count - 1);
				Array.Copy(absoluteExpirations, tempAbsoluteExpirations, count - 1);
				Array.Copy(slidingExpirations, tempSlidingExpirations, count - 1);
				dojoPromotionFlagArray = tempDojoPromotionFlagArray;
				timeStamps = tempTimeStamps;
				absoluteExpirations = tempAbsoluteExpirations;
				slidingExpirations = tempSlidingExpirations;
			}
		}

		#endregion

		#region QuickSort and BinarySearch Methods

		/// <summary>
		/// Ensures that the index and object array are sized correctly 
		/// for additions.
		/// </summary>
		private void quickSort(int left, int right)
		{
			int i, j, x;
			DojoPromotionFlag ya;
			DateTime yb;
			DateTime yc;
			TimeSpan yd;
			i = left;
			j = right;
			x = dojoPromotionFlagArray[(left + right) / 2].ID;
			while(i <= j)
			{
				while(dojoPromotionFlagArray[i].ID < x & i < right)
					i++;
				while(x < dojoPromotionFlagArray[j].ID & j > left)
					j--;
				if(i <= j)
				{
					ya = dojoPromotionFlagArray[i];
					yb = timeStamps[i];
					yc = absoluteExpirations[i];
					yd = slidingExpirations[i];
					dojoPromotionFlagArray[i] = dojoPromotionFlagArray[j];
					timeStamps[i] = timeStamps[j];
					absoluteExpirations[i] = absoluteExpirations[j];
					slidingExpirations[i] = slidingExpirations[j];
					dojoPromotionFlagArray[j] = ya;
					timeStamps[j] = yb;
					absoluteExpirations[j] = yc;
					slidingExpirations[j] = yd;
					i++;
					j--;
				}
			}
			if(left < j) quickSort(left, j);
			if(i < right) quickSort(i, right);
		}

		/// <summary>
		/// Finds the location of the DbModel Object ID in the index. 
		/// Returns the index of the entry.
		/// </summary>
		private int binarySearch(int id)
		{
			int left = 0;
			int right = count - 1;
			while (left <= right)
			{
				int middle = (left + right) / 2;
				if(id > dojoPromotionFlagArray[middle].ID)
					left = middle + 1;
				else if(id < dojoPromotionFlagArray[middle].ID)
					right = middle - 1;
				else
					return middle;
			}
			return -1;
		}

		#endregion

		private void monitor()
		{
			monitorRun = true;
			while(monitorRun)
			{
				Thread.Sleep(TimeSpan.FromSeconds(10));
				monitorScan();	// Scan until no items have been removed.
			}
		}

		private void monitorScan()
		{
			lock(this)
			{
				for(int x = 0; x < count; x++)
				{
					if(DateTime.Now > absoluteExpirations[x])
					{
						remove(x);
						x--;
					}
				}
			}
		}

	}
}
