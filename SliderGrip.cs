using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using SkypePop;

namespace MySlide
{
    public class SliderGrip : Form
    {
        private readonly SkypePopDialog _oSlideForm;
        private readonly Container components;

        public SliderGrip()
        {
            InitializeComponent();

            //_oSlideForm = new SlidingHost(this, 0.1f);
            _oSlideForm = new SkypePopDialog(this, 0.1f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void SliderGrip_MouseHover(object sender, EventArgs e)
        {
            _oSlideForm.Slide(false);
            _oSlideForm.FocusOut();
        }

        private void SliderGrip_MouseLeave(object sender, EventArgs e)
        {
            //_oSlideForm.SlideDirection = SlideDialog.SlideDialog.SLIDE_DIRECTION.RIGHT;
            //_oSlideForm.Slide();
        }

        [STAThread]
        public static void Main()
        {
            var grip = new SliderGrip();
            Size desktopSize = Screen.PrimaryScreen.WorkingArea.Size;
            grip.Location = new Point(desktopSize.Width - 2, desktopSize.Height - 210);
            Application.Run(grip);
        }

        private void SliderGrip_Click(object sender, EventArgs e)
        {
            _oSlideForm.SlideDirection = SlideDialog.SLIDE_DIRECTION.LEFT;
            _oSlideForm.Slide(false);
            _oSlideForm.Focus();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// M�thode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette m�thode avec l'�diteur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SliderGrip
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(2, 205);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 500);
            this.MaximumSize = new System.Drawing.Size(2, 205);
            this.MinimumSize = new System.Drawing.Size(2, 205);
            this.Name = "SliderGrip";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SlidingHost";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.Click += new System.EventHandler(this.SliderGrip_Click);
            this.MouseLeave += new System.EventHandler(this.SliderGrip_MouseLeave);
            this.MouseHover += new System.EventHandler(this.SliderGrip_MouseHover);
            this.ResumeLayout(false);
        }

        #endregion
    }
}