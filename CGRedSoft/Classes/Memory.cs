using System;
using System.Text;

namespace CGRedSoft.Classes {
    public class Memory {
        private Client Client { get; set; }

        public Memory(Client c) { this.Client = c; }

        #region Read Memory Methods

        public byte[] ReadBytes(IntPtr handle, long address, uint bytesToRead) {
            IntPtr ptrBytesRead;
            byte[] buffer = new byte[bytesToRead];

            WinAPI.ReadProcessMemory(handle, new IntPtr(address), buffer, bytesToRead, out ptrBytesRead);

            return buffer;
        }

        public byte ReadByte(long address) {
            return ReadBytes(this.Client.CGHandle, address, 1)[0];
        }

        public int ReadInt32(long address) {
            return BitConverter.ToInt32(ReadBytes(this.Client.CGHandle, address, 4), 0);
        }

        public float ReadFloat(long address) {
            return BitConverter.ToSingle(ReadBytes(this.Client.CGHandle, address, 4), 0);
        }

        #endregion
    }
}
