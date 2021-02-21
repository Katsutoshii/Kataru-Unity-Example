using System.Runtime.InteropServices;
using System;

namespace Kataru
{
    struct FFIArray
    {
        public IntPtr vecptr;
        public UIntPtr length;

        public int[] ToArray()
        {
            var buffer = new int[(int)length];
            Marshal.Copy(vecptr, buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
