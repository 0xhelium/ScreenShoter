using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShooter
{
    public partial class ToolButtons : UserControl
    {
        public ToolButtons()
        {
            InitializeComponent();
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            this.BackColor = Color.FromArgb(255, 220, 220, 220);
            
            this.picOk.Click += (s, e) => 
            {
                (this.ParentForm as Form1).SaveToClipboard();
                (this.ParentForm as Form1).Close();
            };
            this.picClose.Click += (s, e) =>
            {
                (this.ParentForm as Form1).Close();
            };
        }
    }
}
