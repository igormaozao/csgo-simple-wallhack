using CSGOMemoryDumper.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSGOMemoryDumper.MemoryHelpers {
    public class MemoryScanner {

        byte[] Buffer;
        int MemoryStarts;

        public MemoryScanner(Process csGoProcess, ProcessModule module) {
            Buffer = new byte[module.ModuleMemorySize];
            MemoryStarts = module.BaseAddress.ToInt32();
            
            WinApi.ReadProcessMemory(csGoProcess.Handle, module.BaseAddress, Buffer, module.ModuleMemorySize, out IntPtr _);
        }

        public List<long> ScanBytes(int[] value, int ignoreByteValue) {
            List<long> result = new List<long>();
            int len = value.Length;
            int end = Buffer.Length - len;

            for (int i = 0; i < end; ++i) {
                int j = 0;

                for (; j < len; ++j) {
                    if(value[j] == ignoreByteValue) continue;
                    if (Buffer[i + j] != (byte)value[j]) break;
                }
                if (j == len) {
                    result.Add(MemoryStarts + i);
                }
            }

            return result;
        }
    }
}
