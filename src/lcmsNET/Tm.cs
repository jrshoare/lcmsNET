using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Tm
    {
        [MarshalAs(UnmanagedType.I4)]
        public int sec;
        [MarshalAs(UnmanagedType.I4)]
        public int min;
        [MarshalAs(UnmanagedType.I4)]
        public int hour;
        [MarshalAs(UnmanagedType.I4)]
        public int mday;
        [MarshalAs(UnmanagedType.I4)]
        public int mon;
        [MarshalAs(UnmanagedType.I4)]
        public int year;
        [MarshalAs(UnmanagedType.I4)]
        public int wday;
        [MarshalAs(UnmanagedType.I4)]
        public int yday;
        [MarshalAs(UnmanagedType.I4)]
        public int isdst;
        // The glibc version of struct tm has additional fields
        //   long tm_gmtoff;           /* Seconds east of UTC */
        //   const char *tm_zone;      /* Timezone abbreviation */
        // defined when _BSD_SOURCE was set before including <time.h>.
        //
        // Use filler to ensure sufficient size when marshaling this type in this runtime.
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = FILL_SIZE)]
        private readonly byte[] fill;

        public static Tm FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<Tm>(handle);
        }

        public static implicit operator DateTime(in Tm tm)
        {
            return new DateTime(tm.year + 1900, tm.mon + 1, tm.mday, tm.hour, tm.min, tm.sec);
        }

        public Tm(DateTime date)
        {
            sec = date.Second;
            min = date.Minute;
            hour = date.Hour;
            mday = date.Day;
            mon = date.Month - 1;
            year = (date.Year >= 1900) ? date.Year - 1900 : 0;
            wday = (int)date.DayOfWeek;
            yday = date.DayOfYear - 1;
            isdst = -1;
            fill = new byte[FILL_SIZE];
        }

        private const int FILL_SIZE = 16;
    }
}
