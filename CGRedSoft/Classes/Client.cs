using System;
using System.Diagnostics;

namespace CGRedSoft.Classes {
    public static class Addresses {
        public static uint EntitySizeOffSet = 0x10;
        public static uint PlayerTeamOffset = 0xF4;
        public static uint PlayerHpOffset = 0x100;
        public static uint VecOriginOffset = 0x138;
        public static uint LifeStateOffSet = 0x25F;
        public static uint EntityIsDormant = 0xED;

        public static uint EntityAddress = 0x4DBD5CC;
        public static uint PlayerAddress = 0xDA344C;
        public static uint DwVm = 0x4DAEEE4;
    }

    public class Client {
        public Process CGProcess { get; set; }
        public IntPtr CGHandle { get; set; }
        public IntPtr CGClient { get; set; }

        public Memory Memory { get; private set; }

        public Entity[] EntityList = new Entity[64];

        public Client(Process p) {
            this.CGProcess = p;
            this.CGHandle = p.Handle;
            this.CGClient = GetClientDll(p);

            this.Memory = new Memory(this);

            LoadEntities();
        }

        public WinAPI.RECT GameScreen {
            get {
                WinAPI.RECT _rect;
                WinAPI.GetClientRect(this.CGProcess.MainWindowHandle, out _rect);

                _rect.width = _rect.right - _rect.left;
                _rect.height = _rect.bottom - _rect.top;

                return _rect;
            }
        }

        public bool IsFocused() {
            return WinAPI.GetForegroundWindow() == this.CGProcess.MainWindowHandle;
        }

        private void LoadEntities() {
            for (int i = 0; i < 64; i++) {
                EntityList[i] = new Entity(this, i);
            }
        }

        private IntPtr GetClientDll(Process p) {
            foreach (ProcessModule module in p.Modules)
                if (module.ModuleName == "client.dll")
                    return module.BaseAddress;

            return IntPtr.Zero;
        }

        // 3D distance formula: (x2−x1)²+(y2−y1)²+(z2−z1)² squared
        public double Get3dDistance(Location myLoc, Location enemyLoc) {
            double dist = Math.Sqrt(Math.Pow(enemyLoc.X - myLoc.X, 2) +
                                    Math.Pow(enemyLoc.Y - myLoc.Y, 2) +
                                    Math.Pow(enemyLoc.Z - myLoc.Z, 2));

            // convert to meters
            dist *= 0.01905F;

            return dist;
        }
    }
}
