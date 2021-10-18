using CSGOMemoryDumper.Helpers;
using System;
using System.Diagnostics;

namespace CSGOMemoryDumper.MemoryHelpers {
    public class MemoryReader {
        public Process Process;
        public IntPtr Handle;
        public MemoryReader(Process _process) {
            Process = _process;
            Handle = _process.Handle;

        }
        public byte[] ReadBytes(long address, int bytesToRead) {
            byte[] buffer = new byte[bytesToRead];

            WinApi.ReadProcessMemory(Handle, new IntPtr(address), buffer, bytesToRead, out IntPtr _);

            return buffer;
        }

        public int ReadInt32(long address) {
            return BitConverter.ToInt32(ReadBytes(address, 4));
        }
    }
}
