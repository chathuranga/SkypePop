using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SkypePop
{
    public class SlideDialog : Form
    {
        #region SLIDE_DIRECTION enum

        public enum SLIDE_DIRECTION
        {
            TOP,
            LEFT,
            BOTTOM,
            RIGHT
        };

        #endregion

        private int _autoHideInterval = 5000;
        protected bool IsMouseOver;
        private bool _bExpand;

        protected SLIDE_DIRECTION _eSlideDirection;
        private float _fRatio;
        private float _fStep;
        private SizeF _oOffset;
        private Point _oOrigin;
        protected Form _oOwner;
        private SizeF _oStep;
        private IContainer components;
        private Timer timer1;
        private Timer _autoHideTimer = new Timer();

        /// <summary>
        /// Default constructor
        /// </summary>
        public SlideDialog() : this(null, 0)
        {
        }


        void _autoHideTimer_Tick(object sender, EventArgs e)
        {
            Slideout();
        }

        /// <summary>
        /// Constructor with parent window and step of sliding motion
        /// </summary>
        public SlideDialog(Form poOwner, float pfStep)
        {
            InitializeComponent();
            _oOwner = poOwner;
            _fRatio = 0.0f;
            SlideStep = pfStep;
            if (poOwner != null)
                Owner = poOwner.Owner;

            _autoHideTimer.Interval = _autoHideInterval;
            _autoHideTimer.Tick += new EventHandler(_autoHideTimer_Tick);
        }

        /// <summary>
        /// Return the state of the form (expanded or not)
        /// </summary>
        public bool IsExpanded
        {
            get { return _bExpand; }
        }

        /// <summary>
        /// Direction of sliding
        /// </summary>
        public SLIDE_DIRECTION SlideDirection
        {
            set { _eSlideDirection = value; }
        }

        /// <summary>
        /// Slide step of the motion
        /// </summary>
        public float SlideStep
        {
            set { _fStep = value; }
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

        /// <summary>
        /// Use this method to start the slide motion (in ou out) 
        /// according to the slide direction
        /// </summary>
        public void Slide(bool forceExpand)
        {
            if (forceExpand && _bExpand)
                return;
            if (!_bExpand)
                Show();
            _oOwner.BringToFront();
            _bExpand = !_bExpand;
            _autoHideTimer.Enabled = true;
            timer1.Start();
        }

        private void Slideout()
        {
            if (!IsInFocus())
            {
                _oOwner.BringToFront();
                _bExpand = false;
                timer1.Start();
                _autoHideTimer.Enabled = false;
            }
        }
        protected virtual bool IsInFocus()
        {
            if(IsMouseOver)
                return true;
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_bExpand)
            {
                _fRatio += _fStep;
                _oOffset += _oStep;
                if (_fRatio >= 1)
                    timer1.Stop();
            }
            else
            {
                _fRatio -= _fStep;
                _oOffset -= _oStep;
                if (_fRatio <= 0)
                    timer1.Stop();
            }
            SetLocation();
        }

        private void SetLocation()
        {
            Location = _oOrigin + _oOffset.ToSize();
        }

        private void SlideDialog_Move(object sender, EventArgs e)
        {
            SetSlideLocation();
            SetLocation();
        }

        private void SlideDialog_Resize(object sender, EventArgs e)
        {
            SetSlideLocation();
            SetLocation();
        }

        private void SlideDialog_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void SetSlideLocation()
        {
            if (_oOwner != null)
            {
                _oOrigin = new Point();
                switch (_eSlideDirection)
                {
                    case SLIDE_DIRECTION.BOTTOM:
                        _oOrigin.X = _oOwner.Location.X;
                        _oOrigin.Y = _oOwner.Location.Y + _oOwner.Height - Height;
                        Width = _oOwner.Width;
                        _oStep = new SizeF(0, Height*_fStep);
                        break;
                    case SLIDE_DIRECTION.LEFT:
                        _oOrigin = _oOwner.Location;
                        _oStep = new SizeF(- Width*_fStep, 0);
                        Height = _oOwner.Height;
                        break;
                    case SLIDE_DIRECTION.TOP:
                        _oOrigin = _oOwner.Location;
                        Width = _oOwner.Width;
                        _oStep = new SizeF(0, - Height*_fStep);
                        break;
                    case SLIDE_DIRECTION.RIGHT:
                        _oOrigin.X = _oOwner.Location.X + _oOwner.Width - Width;
                        _oOrigin.Y = _oOwner.Location.Y;
                        _oStep = new SizeF(Width*_fStep, 0);
                        Height = _oOwner.Height;
                        break;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            SetSlideLocation();
            SetLocation();
            if (_oOwner != null)
            {
                _oOwner.LocationChanged += SlideDialog_Move;
                _oOwner.Resize += SlideDialog_Resize;
                _oOwner.Closed += SlideDialog_Closed;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SlideDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(320, 192);
            this.ControlBox = false;
            this.Name = "SlideDialog";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion
    }
}