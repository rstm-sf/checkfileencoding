using System;
using System.Runtime.InteropServices;

namespace UCharDetSharp
{
    public class UCharDet
    {
        public UCharDet()
        {
            IntPtr handle = uchardet_new();
            _handle = new HandleRef(this, handle);
        }

        ~UCharDet()
        {
            uchardet_delete(_handle);
        }

        public int HandleData(byte[] data, uint len)
        {
            return uchardet_handle_data(_handle, data, len);
        }

        public void DataEnd()
        {
            uchardet_data_end(_handle);
        }

        public void Reset()
        {
            uchardet_reset(_handle);
        }

        public string GetCharset()
        {
            IntPtr ptr = uchardet_get_charset(_handle);
            return Marshal.PtrToStringAnsi(ptr);
        }

        private HandleRef _handle;

        [DllImport("uchardet.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr uchardet_new();

        [DllImport("uchardet.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void uchardet_delete(HandleRef ud);

        [DllImport("uchardet.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int uchardet_handle_data(HandleRef ud, byte[] data, uint len);

        [DllImport("uchardet.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void uchardet_data_end(HandleRef ud);

        [DllImport("uchardet.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void uchardet_reset(HandleRef ud);

        [DllImport("uchardet.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr uchardet_get_charset(HandleRef ud);
    }
}
