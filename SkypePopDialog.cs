using System;
using System.Drawing;
using System.Windows.Forms;
using SKYPE4COMLib;
using SlidingHost;
using Application = System.Windows.Forms.Application;

namespace SkypePop
{
    public partial class SkypePopDialog : SlideDialog
    {
        #region Private members
        private readonly Font _bold = new Font("Consolas", 11f, FontStyle.Bold);
        private readonly Font _regular = new Font("Consolas", 11f, FontStyle.Regular);
        private readonly Skype _skype;
        private ChatCollection _chats;
        private bool _colourToggle;
        private Chat _currentChat;
        private Color _color1 = ColorTranslator.FromHtml("#514124");
        private Color _color2 = ColorTranslator.FromHtml("#234177");

        #endregion

        #region Constructor

        public SkypePopDialog(Form poOwner, float pfStep)
            : base(poOwner, pfStep)
        {
            try
            {
                InitializeComponent();

                SlideDirection = SLIDE_DIRECTION.LEFT;
                _skype = new Skype();
                // Use skype protocol version 7 
                _skype.Attach(7, false);
                // Listen 
                _skype.MessageStatus += skype_MessageStatus;
                _chats = _skype.ActiveChats;
                RefreshActiveChats();
                if (_chats.Count > 0)
                {
                    cmbActiveChats.SelectedIndex = 0;
                }

                HookupMouseEnterLeaveEvents(this);

                Updater.CheckUpdates();
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
            }
        }
        
        #endregion
        
        #region Event handlers

        private void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            try
            {
                if (status == TChatMessageStatus.cmsReceived)
                {
                    AppendMessage(msg.FromDisplayName, msg.Body);
                    Slide(true);
                    int index = IndexOf(msg.Chat);
                    if (index == -1)
                    {
                        RefreshActiveChats();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
            }
        }
        
        private void btnHide_Click(object sender, EventArgs e)
        {
            try
            {
                Slide(false);
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
            }
        }
        private void cmbActiveChats_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetSelected(cmbActiveChats.Text);
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
            }
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_currentChat != null &&
                    !string.IsNullOrEmpty(txtSend.Text) &&
                    e.KeyValue == 13)
                {
                    _currentChat.SendMessage(txtSend.Text);
                    AppendMessage("Me", txtSend.Text);
                    txtSend.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
            }
        }

        #endregion
        
        #region Private methods

        private void AppendMessage(string from, string message)
        {
            txtReceived.SelectionColor = GetColor();
            txtReceived.SelectionFont = _bold;
            txtReceived.AppendText(from + " : ");
            txtReceived.SelectionFont = _regular;
            txtReceived.AppendText(message + Environment.NewLine);
            txtReceived.ScrollToCaret();
        }

        private void SetSelected(string chatNameToSelect)
        {
            var found = false;
            for (int i = 1; i < _chats.Count + 1; i++)
            {
                string chatName = GetChatName(_chats[i].FriendlyName);
                if (chatName == chatNameToSelect)
                {
                    cmbActiveChats.SelectedIndex = i - 1;
                    _currentChat = _chats[i];
                    found = true;
                    break;
                }
            }
            if (!found && _chats.Count > 0)
            {
                cmbActiveChats.SelectedIndex = 0;
                _currentChat = _chats[1];
            }
            FocusOut();
        }

        public void FocusOut()
        {
            btnHide.Focus();
        }

        private void RefreshActiveChats()
        {
            var currentlySelected = cmbActiveChats.Text;
            cmbActiveChats.Items.Clear();
            for (int i = 1; i < _chats.Count + 1; i++)
            {
                string chatName = GetChatName(_chats[i].FriendlyName);
                cmbActiveChats.Items.Add(chatName);
                if (chatName == currentlySelected)
                {
                    cmbActiveChats.SelectedIndex = i - 1;
                }
            }
            SetSelected(currentlySelected);
        }

        private Color GetColor()
        {
            if (_colourToggle)
            {
                _colourToggle = false;
                return _color1;
            }
            else
            {
                _colourToggle = true;
                return _color2;
            }
        }

        #endregion

        #region Handling focus

        protected override bool IsInFocus()
        {
            return txtSend.Focused || base.IsInFocus();
        }

        private void HookupMouseEnterLeaveEvents(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                childControl.MouseEnter += new EventHandler(childControl_MouseEnter);
                childControl.MouseLeave += new EventHandler(childControl_MouseLeave);

                // Recurse on this child to get all of its descendents.
                HookupMouseEnterLeaveEvents(childControl);
            }
        }

        private void childControl_MouseLeave(object sender, EventArgs e)
        {
            IsMouseOver = false;
        }

        private void childControl_MouseEnter(object sender, EventArgs e)
        {
            IsMouseOver = true;
        }

        #endregion

        #region Skype utility methods

        private int IndexOf(Chat newChat)
        {
            int index = -1;
            for (int i = 1; i < _chats.Count + 1; i++)
            {
                if (_chats[i].Name == newChat.Name)
                    return i - 1;
            }
            _chats = _skype.ActiveChats;
            return index;
        }

        private string GetChatName(string chatName)
        {
            if (chatName.IndexOf("|") > 0)
                chatName = chatName.Substring(0, chatName.IndexOf("|") - 1);
            return chatName;
        }

        #endregion
        
    }
}