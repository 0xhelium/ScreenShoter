#define SHOW_TEST_MSG
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _toolBtn = new ScreenShoter.ToolButtons();
            _toolBtn.Parent = this;            

            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.KeyPreview = true;
            //this.TopMost = true;

            this.KeyDown += Form1_KeyDown;
            this.Paint += Form1_Paint;

            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;

            this.MouseDoubleClick += Form1_MouseDoubleClick;

            this.Load += Form1_Load;

            Init();
            
        }
        
        private ToolButtons _toolBtn;
        private Image _toolBtnImg;
        
        private Rectangle _rect;
        private Dictionary<string,Rectangle> _dicRects
        {
            get
            {
                return new Dictionary<string, Rectangle> {
                    {"NW", new Rectangle(_rect.X-2,_rect.Y-2,5,5)                           },
                    {"N", new Rectangle(_rect.X+_rect.Width/2-2,_rect.Y-2,5,5)              },
                    {"NE", new Rectangle(_rect.X+_rect.Width-2,_rect.Y-2,5,5)               },
                    {"W", new Rectangle(_rect.X-2,_rect.Y+_rect.Height/2-2,5,5)             },
                    {"E", new Rectangle(_rect.X+_rect.Width-2,_rect.Y+_rect.Height/2-2,5,5) },
                    {"SW", new Rectangle(_rect.X-2,_rect.Y+_rect.Height-2,5,5)              },
                    {"S", new Rectangle(_rect.X+_rect.Width/2-2,_rect.Y+_rect.Height-2,5,5) },
                    {"SE", new Rectangle(_rect.X+_rect.Width-2,_rect.Y+_rect.Height-2,5,5)  },
                };
            }
        }
        private bool _mouseDown = false;
        private Point? _startPos = null;
        private Rectangle? _rectBeforeMove = null;
        private string _corner = "";

        private void Init()
        {
            var img = PrintScreen();
            this.BackgroundImage = img;
            Cursor = Cursors.Arrow;
        }

        private Image PrintScreen()
        {
            Image img = new Bitmap(this.Size.Width, this.Size.Height);
            var g = Graphics.FromImage(img);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), this.Size);
            g.Dispose();
            return img;
        }

        private bool RectContainsPt(Rectangle rect, Point point)
        {
            return point.X >= rect.X && point.Y >= rect.Y && point.X <= (rect.X + rect.Width) && point.Y <= (rect.Y + rect.Height);
        }

        private void Draw(Graphics g)
        {
#if SHOW_TEST_MSG
            g.DrawString(string.Format("{5}.width:{0},height:{1},loc:{2}-{3},toolBarVisible:{4}", 
                _rect.Width, _rect.Height, _rect.X, _rect.Y,_toolBtn.Visible,DateTime.Now.ToString("ss.fff")),
                new Font("微软雅黑", 13), new SolidBrush(Color.Black), new Point(0, 0));
#endif
            Region reg = new Region(new RectangleF(new Point(0, 0), this.Size));         
            if (_rect.Width > 0 || _rect.Height > 0)
            {
                g.DrawRectangle(new Pen(Color.Red), _rect);
                reg.Xor(_rect);
                if (_toolBtn.Visible)
                    reg.Xor(new Rectangle(_toolBtn.Location, _toolBtn.Size));
                
                g.FillRegion(new SolidBrush(Color.FromArgb(100, 200, 200, 200)), reg);
                g.FillRectangles(new SolidBrush(Color.Red), _dicRects.Values.Select(x => x).ToArray());
                
                DrawToolBtn(g);
            }
            else
            {
                _toolBtn.Hide();
                g.FillRegion(new SolidBrush(Color.FromArgb(100, 200, 200, 200)), reg);
            }
        }

        private void DrawToolBtn(Graphics g)
        {
            _toolBtn.Hide();
            if (((_rect.Width > 0 || _rect.Height > 0)))
            {
                var margin = 5;
                var tw = _toolBtn.Width;
                var th = _toolBtn.Height;
                if (th + margin + _rect.Location.Y + _rect.Height <= this.Height)
                {
                    //toolbar 在底部
                    var p = new Point(_rect.X, _rect.Y + _rect.Height + margin);
                    if (tw + _rect.X > this.Width)
                    {
                        p = new Point(_rect.X - (tw + _rect.X - this.Width), _rect.Y + _rect.Height + margin);
                    }
                    if (_mouseDown)
                        g.DrawImage(_toolBtnImg, p);
                    else
                    {
                        _toolBtn.Show();
                        _toolBtn.Location = p;
                    }
                }
                else
                {
                    var p = new Point(_rect.X + _rect.Width - tw, th + margin > _rect.Y ? 0 : _rect.Y - margin - th);
                    if (p.X < 0)
                    {
                        p = new Point(0, p.Y);
                    }
                    if (_mouseDown)
                        g.DrawImage(_toolBtnImg, p);
                    else
                    {
                        _toolBtn.Show();
                        _toolBtn.Location = p;
                    }
                }
            }
        }

        private void DrawTip(Graphics g)
        {
            //70*20
            //if (_rect.X>)
            //{
            //
            //}
        }

        #region - EVENTS -

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_toolBtnImg == null)
            {
                _toolBtnImg = new Bitmap(_toolBtn.Width, _toolBtn.Height);
                _toolBtn.DrawToBitmap((Bitmap)_toolBtnImg, new Rectangle(_toolBtn.Location, _toolBtn.Size));
                _toolBtn.Hide();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.Invalidate();
            if (_toolBtn.Visible)
            {
                _toolBtn.Refresh();
            }
            Draw(e.Graphics);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
            _startPos = null;
            _rectBeforeMove = null;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDown = true;
                _startPos = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((_rect.Width > 0 || _rect.Height > 0) && e.Button == MouseButtons.Left)
            {
                SaveToClipboard();
                this.Close();
            }
        }

        public void SaveToClipboard()
        {
            var rect = new Rectangle(_rect.X, _rect.Y, _rect.Width + 1, _rect.Height + 1);
            using (var img = new Bitmap(rect.Width, rect.Height))
            {
                using (var g = Graphics.FromImage(img))
                {
                    g.DrawImage(this.BackgroundImage,
                        new Rectangle(0, 0, rect.Width, rect.Height),
                        rect,
                        GraphicsUnit.Pixel);
                    Clipboard.SetImage(img);
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                var setRect = new Action(() =>
                {
                    _rect = new Rectangle(
                        _startPos.Value.X > e.X ? e.X : _startPos.Value.X,
                        _startPos.Value.Y > e.Y ? e.Y : _startPos.Value.Y,
                        Math.Abs(_startPos.Value.X - e.X),
                        Math.Abs(_startPos.Value.Y - e.Y));
                });

                if (Cursor == Cursors.SizeAll)
                {
                    if (_rectBeforeMove == null)
                        _rectBeforeMove = _rect;

                    var x = _rectBeforeMove.Value.X + (e.X - _startPos.Value.X);
                    var y = _rectBeforeMove.Value.Y + (e.Y - _startPos.Value.Y);
                    if (x < 0)
                        x = 0;
                    if (y < 0)
                        y = 0;
                    if (x > this.Size.Width - _rect.Width)
                        x = this.Size.Width - _rect.Width;
                    if (y > this.Size.Height - _rect.Height)
                        y = this.Size.Height - _rect.Height;
                    _rect.Location = new Point(x, y);
                }
                else if (Cursor == Cursors.SizeNESW)
                {
                    if (_rectBeforeMove == null)
                    {
                        _rectBeforeMove = _rect;
                        _startPos = null;
                    }
                    if (_startPos == null)
                    {
                        var cornerRects = _dicRects.Where(x => RectContainsPt(x.Value, e.Location));
                        if (cornerRects.Any() && cornerRects.First().Key == "NE")
                            _startPos = new Point(_rectBeforeMove.Value.X, _rectBeforeMove.Value.Y + _rectBeforeMove.Value.Height);
                        else
                            _startPos = new Point(_rectBeforeMove.Value.X + _rectBeforeMove.Value.Width, _rectBeforeMove.Value.Y);
                    }
                    setRect();
                }
                else if (Cursor == Cursors.SizeNWSE)
                {
                    if (_rectBeforeMove == null)
                    {
                        _rectBeforeMove = _rect;
                        _startPos = null;
                    }
                    if (_startPos == null)
                    {
                        var cornerRects = _dicRects.Where(x => RectContainsPt(x.Value, e.Location));
                        if (cornerRects.Any() && cornerRects.First().Key == "NW")
                            _startPos = new Point(_rectBeforeMove.Value.X + _rectBeforeMove.Value.Width, _rectBeforeMove.Value.Y + _rectBeforeMove.Value.Height);
                        else
                            _startPos = new Point(_rectBeforeMove.Value.X, _rectBeforeMove.Value.Y);
                    }
                    setRect();
                }
                else if (Cursor == Cursors.SizeNS)
                {
                    if (_rectBeforeMove == null)
                    {
                        _rectBeforeMove = _rect;
                        _startPos = null;
                    }
                    if (_startPos == null)
                    {
                        _startPos = e.Location;
                    }
                    if (_corner == "N")
                    {
                        _rect = new Rectangle(
                            _rectBeforeMove.Value.X,
                            e.Y < _rectBeforeMove.Value.Y + _rectBeforeMove.Value.Height ?
                                e.Y :
                                _rectBeforeMove.Value.Y + _rectBeforeMove.Value.Height,
                            _rectBeforeMove.Value.Width,
                            e.Y < _rectBeforeMove.Value.Y + _rectBeforeMove.Value.Height ?
                                _rectBeforeMove.Value.Height + _rectBeforeMove.Value.Y - e.Y :
                                e.Y - _rectBeforeMove.Value.Y - _rectBeforeMove.Value.Height);
                    }
                    else
                    {
                        _rect = new Rectangle(
                            _rectBeforeMove.Value.X,
                            e.Y < _rectBeforeMove.Value.Y ? e.Y : _rectBeforeMove.Value.Y,
                            _rectBeforeMove.Value.Width,
                            Math.Abs(e.Y - _rectBeforeMove.Value.Y));
                    }
                }
                else if (Cursor == Cursors.SizeWE)
                {
                    if (_rectBeforeMove == null)
                    {
                        _rectBeforeMove = _rect;
                        _startPos = null;
                    }
                    if (_startPos == null)
                    {
                        _startPos = e.Location;
                    }
                    if (_corner == "W")
                    {
                        _rect = new Rectangle(
                           e.X < _rectBeforeMove.Value.X + _rectBeforeMove.Value.Width ?
                               e.X :
                               _rectBeforeMove.Value.X + _rectBeforeMove.Value.Width,
                           _rectBeforeMove.Value.Y,
                           e.X < _rectBeforeMove.Value.X + _rectBeforeMove.Value.Width ?
                               _rectBeforeMove.Value.Width + _rectBeforeMove.Value.X - e.X :
                               e.X - _rectBeforeMove.Value.X - _rectBeforeMove.Value.Width,
                           _rectBeforeMove.Value.Height);
                    }
                    else
                    {
                        _rect = new Rectangle(
                            e.X < _rectBeforeMove.Value.X ? e.X : _rectBeforeMove.Value.X,
                            _rectBeforeMove.Value.Y,
                            Math.Abs(e.X - _rectBeforeMove.Value.X),
                            _rectBeforeMove.Value.Height);
                    }
                }
                else
                {
                    Cursor = Cursors.Cross;
                    _rect = new Rectangle(
                        _startPos.Value.X > e.X ? e.X : _startPos.Value.X,
                        _startPos.Value.Y > e.Y ? e.Y : _startPos.Value.Y,
                        Math.Abs(_startPos.Value.X - e.X),
                        Math.Abs(_startPos.Value.Y - e.Y));
                }
            }
            else
            {
                if (_rect.Width > 0 && _rect.Height > 0)
                {
                    Cursor = Cursors.Arrow;
                    if (RectContainsPt(_rect,e.Location))
                    {
                        Cursor = Cursors.SizeAll;
                    }
                    foreach (KeyValuePair<string, Rectangle> kv in _dicRects)
                    {
                        if (RectContainsPt(kv.Value, e.Location))
                        {
                            _corner = kv.Key;
                            if (kv.Key == "N" || kv.Key == "S")
                                Cursor = Cursors.SizeNS;
                            if (kv.Key == "W" || kv.Key == "E")
                                Cursor = Cursors.SizeWE;
                            if (kv.Key == "NW" || kv.Key == "SE")
                                Cursor = Cursors.SizeNWSE;
                            if (kv.Key == "NE" || kv.Key == "SW")
                                Cursor = Cursors.SizeNESW;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
