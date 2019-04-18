using System;
using System.Collections;

namespace Amns.Tessen
{
	/// <summary>
	/// Summary description for DojoAttendanceEntryComparer.
	/// </summary>
	public class DojoAttendanceEntryComparer : IComparer
	{
		private DojoAttendanceEntryCompareKey[] _keys;

		public DojoAttendanceEntryComparer(params DojoAttendanceEntryCompareKey[] keys)
		{
			_keys = keys;
		}

		int IComparer.Compare(object a, object b)
		{
			return Compare((DojoAttendanceEntry) a, (DojoAttendanceEntry) b);
		}

		public int Compare(DojoAttendanceEntry a, DojoAttendanceEntry b)
		{
			int result = 0;

			for(int i = 0; i <= _keys.GetUpperBound(0); i++)
			{
				switch(_keys[i])
				{
					case DojoAttendanceEntryCompareKey.ClassStart:
						result = DateTime.Compare(a.Class.ClassStart, b.Class.ClassStart);
						break;
					case DojoAttendanceEntryCompareKey.Instructor:
						result = a.Class.Instructor.ID - b.Class.Instructor.ID;
						break;
					default:
						result = a.ID - b.ID;
						break;
				}

				if(result != 0)
					return result;
			}

			return result;
			
		}
	}
}
