using System;
using System.Runtime.InteropServices;

namespace CSGOMemoryDumper.Helpers {
    public class WinApi {
        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(
             IntPtr processHandle,
             IntPtr address,
             [In, Out] byte[] buffer,
             int size,
             out IntPtr numberOfBytesRead
         );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
