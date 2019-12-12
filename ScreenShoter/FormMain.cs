using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShoter
{
    public partial class FormMain : MaterialForm
    {
        KeyboardHook hook = new KeyboardHook();           

        public FormMain()
        {
            InitializeComponent();
            //copied from designer.cs
            this.tabPage2.Controls.Add(this.materialSingleLineTextField1);

            this.FormBorderStyle = FormBorderStyle.None;
            this.Sizable = false;

            this.ShowInTaskbar = false;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800,
                Primary.BlueGrey900,
                Primary.BlueGrey500,
                Accent.LightBlue200,
                TextShade.WHITE);

            BindEvents();

            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>((s, e) => {
                ShowShotFrm();
            });
            hook.RegisterHotKey(ScreenShoter.ModifierKeys.Control | ScreenShoter.ModifierKeys.Alt, Keys.Z);
        }

        private void BindEvents()
        {
            this.BtnScreenShot.Click += async (s, e) => await BtnScreenShot_Click(s, e);
            this.NtfIco.Click += (s, e) =>
            {
                var me = e as MouseEventArgs;
                if (me.Button == MouseButtons.Left)
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                    Activate();
                }
            };
        }

        private async Task BtnScreenShot_Click(object sender, EventArgs e)
        {
            await Task.Delay(300);
            ShowShotFrm();
        }

        private void ShowShotFrm()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is FormShot)
                {
                    return;
                }
            }

            this.Hide();            
            var frm1 = new FormShot();
            frm1.TopMost = true;
            frm1.Show(this);
        }
       
    }
}
