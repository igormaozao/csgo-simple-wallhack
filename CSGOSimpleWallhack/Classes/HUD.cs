using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CGRedSoft.Classes {
    public partial class HUD : Form {

        private Size lastSize;
        private Point lastLocation;
        private Client Client;

        public WindowRenderTarget Device = null;

        public HUD(Client c) {
            this.Client = c;

            InitializeComponent();

            var renderProp = new HwndRenderTargetProperties {
                Hwnd = this.Handle,
                PixelSize = new Size2(Client.GameScreen.width, Client.GameScreen.height),
                PresentOptions = PresentOptions.None
            };

            Device = new WindowRenderTarget(
                new SharpDX.Direct2D1.Factory(),
                new RenderTargetProperties(
                    new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied)
                ),
                renderProp);
        }

        public void CheckFocus() {
            this.Invoke(new MethodInvoker(delegate () {
                IntPtr thisHandle = this.Handle;

                if (this.Client.IsFocused() || WinAPI.GetForegroundWindow() == thisHandle) {
                    if (!this.Visible)
                        this.Invoke(new MethodInvoker(delegate () { this.Show(); }));
                    else
                        this.Invoke(new MethodInvoker(delegate () { this.BringToFront(); }));
                } else {
                    if (this.Visible)
                        this.Invoke(new MethodInvoker(delegate () { this.Hide(); }));
                }
            }));
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                // Set the form click-through
                cp.ExStyle |= 0x80000 /* WS_EX_LAYERED */ | 0x20 /* WS_EX_TRANSPARENT */;
                return cp;
            }
        }

        #region Initialize the window with the correct config, no border, etc
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Text = "";
            this.AutoSize = false;

            this.Size = new System.Drawing.Size(this.Client.GameScreen.width, this.Client.GameScreen.height);
            this.Location = new Point(this.Client.GameScreen.left, this.Client.GameScreen.top);
            this.ClientSize = new System.Drawing.Size(this.Client.GameScreen.width, this.Client.GameScreen.height);

            lastLocation = this.Location;
            lastSize = this.Size;

            this.UseWaitCursor = false;
            this.Cursor = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;

            this.ForeColor = Color.White;
            this.DoubleBuffered = true;

            WinAPI.MARGINS m = new WinAPI.MARGINS {
                topHeight = this.Client.GameScreen.width,
                bottomHeight = this.Client.GameScreen.height
            };

            WinAPI.DwmExtendFrameIntoClientArea(this.Handle, ref m);

            this.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
