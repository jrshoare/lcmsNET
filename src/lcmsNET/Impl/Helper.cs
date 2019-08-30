using System;

namespace lcmsNET.Impl
{
    internal static class Helper
    {
        public static void CheckCreated<T>(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                throw new LcmsNETException($"Failed to create {typeof(T)}.");
            }
        }
    }
}
