using CGRedSoft.Classes;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CGRedSoft {
    public partial class MainForm : Form {
        // Color config
        // TODO: Change this config from MainForm
        const string CLOSEST_RANGE_ENEMY_COLOR = "Red";
        const string MEDIUM_RANGE_ENEMY_COLOR = "Green";
        const string FAR_AWAY_ENEMY_COLOR = "Beige";
        const string INACTIVE_ENEMY_COLOR = "Purple";

        // Distance label position offset
        int DISTANCE_LABEL_X_OFFSET = 0;
        int DISTANCE_LABEL_Y_OFFSET = 0;

        // TODO: Change this config from MainForm
        List<string> fakeNames = new List<string>() { "notepad", "calc", "chrome", "firefox", "notepad++" };

        Client Client;
        Player Player;
        Thread threadHud;
        HUD hud;

        bool IsRunning = false;

        TextFormat textFormat = new TextFormat(new SharpDX.DirectWrite.Factory(),
            "Bahnschrift SemiBold", FontWeight.Medium, SharpDX.DirectWrite.FontStyle.Normal, 17f);

        Dictionary<string, SolidColorBrush> textBruchs;

        public MainForm() {
            InitializeComponent();
            this.Text = fakeNames[new Random(DateTime.Now.Millisecond).Next(0, fakeNames.Count)];
        }

        private void Form1_Load(object sender, EventArgs e) {
            var processes = Process.GetProcessesByName("csgo");
            if (processes.Length == 0) {
                MessageBox.Show("You must open CS:GO first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }

            Client = new Client(processes[0]);
            Player = new Player(Client);
            hud = new HUD(Client);

            textBruchs = new Dictionary<string, SolidColorBrush>();
            textBruchs.Add(CLOSEST_RANGE_ENEMY_COLOR, new SolidColorBrush(hud.Device, System.Drawing.Color.FromName(CLOSEST_RANGE_ENEMY_COLOR).ToRaw()));
            textBruchs.Add(MEDIUM_RANGE_ENEMY_COLOR, new SolidColorBrush(hud.Device, System.Drawing.Color.FromName(MEDIUM_RANGE_ENEMY_COLOR).ToRaw()));
            textBruchs.Add(FAR_AWAY_ENEMY_COLOR, new SolidColorBrush(hud.Device, System.Drawing.Color.FromName(FAR_AWAY_ENEMY_COLOR).ToRaw()));
            textBruchs.Add(INACTIVE_ENEMY_COLOR, new SolidColorBrush(hud.Device, System.Drawing.Color.FromName(INACTIVE_ENEMY_COLOR).ToRaw()));

        }

        void ShowEnemies() {
            while (IsRunning) {
                if (Player == null)
                    Player = new Player(Client);

                hud.CheckFocus();

                hud.Device.BeginDraw();

                hud.Device.Clear(Color.Transparent.ToRaw());

                hud.Device.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Aliased;
                hud.Device.AntialiasMode = AntialiasMode.Aliased;

                var pTeam = Player.GetTeam();
                for (int i = 0; i < 64; i++) {
                    if (Client.EntityList[i].GetTeam() != pTeam) {

                        if (Client.EntityList[i].GetHealth() == 0) {
                            continue;
                        }

                        if (IsOnScreen(Player.GetWorldToScreen(), Client.EntityList[i])) {

                            double dist = Client.Get3dDistance(Player.GetLocation(), Client.EntityList[i].GetLocation());

                            string color = FAR_AWAY_ENEMY_COLOR;

                            if (Client.EntityList[i].IsDormant())
                                color = INACTIVE_ENEMY_COLOR;
                            else if (dist > 0 && dist <= 18)
                                color = CLOSEST_RANGE_ENEMY_COLOR;
                            else if (dist > 18 && dist <= 30)
                                color = MEDIUM_RANGE_ENEMY_COLOR;

                            DrawTextOnHUD(dist.ToString(".0#"), (int)ScreenPos[0], (int)ScreenPos[1], color);

                        }
                    }
                }

                hud.Device.EndDraw();

                Thread.Sleep(30);
            }
        }

        void DrawTextOnHUD(string text, int posX, int posY, string color) {
            var txtColor = textBruchs[color];
            var realPosX = posX + DISTANCE_LABEL_X_OFFSET;
            var realPosY = posY + DISTANCE_LABEL_Y_OFFSET;
            var labelSize = 100; // the label rectangle size, just to fit the text

            hud.Device.DrawText(text, textFormat,
                new RawRectangleF(realPosX, realPosY, realPosX + labelSize, realPosY + labelSize), txtColor);
        }

        float[] ScreenPos = new float[2];
        private bool IsOnScreen(float[,] w2s, Entity from) {
            Location loc = from.GetLocation();
            ScreenPos[0] = w2s[0, 0] * loc.X + w2s[0, 1] * loc.Y + w2s[0, 2] * loc.Z + w2s[0, 3];
            ScreenPos[1] = w2s[1, 0] * loc.X + w2s[1, 1] * loc.Y + w2s[1, 2] * loc.Z + w2s[1, 3];

            float w = w2s[3, 0] * loc.X + w2s[3, 1] * loc.Y + w2s[3, 2] * loc.Z + w2s[3, 3];

            if (w < 0.01F)
                return false;

            float invw = 1.0F / w;
            ScreenPos[0] *= invw;
            ScreenPos[1] *= invw;

            float x = Client.GameScreen.width / 2;
            float y = Client.GameScreen.height / 2;

            x += 0.5F * ScreenPos[0] * Client.GameScreen.width + 0.5F;
            y -= 0.5F * ScreenPos[1] * Client.GameScreen.height + 0.5F;

            ScreenPos[0] = x + Client.GameScreen.left;
            ScreenPos[1] = y + Client.GameScreen.top;

            return true;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (threadHud == null
                || threadHud.ThreadState == System.Threading.ThreadState.Stopped
                || threadHud.ThreadState == System.Threading.ThreadState.Unstarted) {
                IsRunning = true;
                threadHud = new Thread(new ThreadStart(ShowEnemies));
                threadHud.Start();

                btnEnableWall.Text = "Enabled";
            } else {
                IsRunning = false;
                btnEnableWall.Text = "Disabled";
            }
        }

        private void numDistanceLblXOffset_ValueChanged(object sender, EventArgs e) {
            var numericObj = sender as NumericUpDown;
            DISTANCE_LABEL_X_OFFSET = (int)numericObj.Value;
        }

        private void numDistanceLblYOffset_ValueChanged(object sender, EventArgs e) {
            var numericObj = sender as NumericUpDown;
            DISTANCE_LABEL_Y_OFFSET = (int)numericObj.Value;
        }
    }
}
