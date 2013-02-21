using System;
using System.Diagnostics;
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
        private Skype _skype;
        private ChatCollection _chats;
        private Chat _currentChat;
        private Color _color1 = ColorTranslator.FromHtml("#234177");
        private Color _color2 = ColorTranslator.FromHtml("#514124");
        private Timer _skypeReconnectTime;

        #endregion

        #region Constructor

        public SkypePopDialog(Form poOwner, float pfStep)
            : base(poOwner, pfStep)
        {
            try
            {
                InitializeComponent();

                SlideDirection = (SkypePopSettings.Default.Side == Constants.SideRight) ? SLIDE_DIRECTION.LEFT : SLIDE_DIRECTION.RIGHT;
                HookupMouseEnterLeaveEvents(this);
                Updater.CheckUpdates();

                _skypeReconnectTime = new Timer {Interval = 30000};
                _skypeReconnectTime.Tick += _skypeReconnectTime_Tick;
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
            }
        }
        
        #endregion
        
        #region Event handlers

        void SkypePopDialog_AttachmentStatus(TAttachmentStatus Status)
        {
            if (Status == TAttachmentStatus.apiAttachNotAvailable)
            {
                InitSkype();
            }
        }

        private void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            try
            {
                if (status == TChatMessageStatus.cmsReceived)
                {
                    var chatName = string.Empty;
                    if (msg.Chat.Members.Count > 2)
                        chatName = "[" + GetChatName(msg.Chat.FriendlyName) + "]";
                    AppendMessage(msg.FromDisplayName, chatName, msg.Body);
                    if (_skype.CurrentUserStatus != TUserStatus.cusDoNotDisturb)
                    {
                        Slide(true);
                    }
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
                context.Show(MousePosition.X, MousePosition.Y);
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
                Slide(false);
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
                txtSend.Focus();
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

        private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void miSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var settings = new Settings();
                settings.ShowDialog(this);
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
            txtReceived.SelectionColor = _color1;
            txtReceived.SelectionFont = _bold;
            txtReceived.AppendText(from + " : ");
            txtReceived.SelectionFont = _regular;
            txtReceived.SelectionColor = _color2;
            txtReceived.AppendText(message + Environment.NewLine);
            txtReceived.ScrollToCaret();
        }

        private void AppendMessage(string from, string chatName, string message)
        {
            txtReceived.SelectionColor = _color1;
            txtReceived.SelectionFont = _bold;
            txtReceived.AppendText(from + chatName + Environment.NewLine);
            txtReceived.SelectionFont = _regular;
            txtReceived.SelectionColor = _color2;
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

            if (found)
                CueProvider.SetCue(txtSend, "Type a message to " + GetChatName(_currentChat.FriendlyName) + " here");
            else
                CueProvider.SetCue(txtSend, "No active chats are running");

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
        
        #endregion

        #region Handling focus

        protected override bool IsInFocus()
        {
            return txtSend.Focused || base.IsInFocus();
        }

        private void HookupMouseEnterLeaveEvents(Control control)
        {
            context.MouseEnter += new EventHandler(childControl_MouseEnter);
            context.MouseLeave += new EventHandler(childControl_MouseLeave);
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

        public bool InitSkype()
        {
            try
            {
                if (IsProcessOpen("Skype"))
                {
                    _skype = new Skype();
                    // Use skype protocol version 7 
                    _skype.Attach(7, false);
                    // Listen 
                    _skype.MessageStatus += skype_MessageStatus;
                    ((_ISkypeEvents_Event)_skype).AttachmentStatus += new _ISkypeEvents_AttachmentStatusEventHandler(SkypePopDialog_AttachmentStatus);
                    _chats = _skype.ActiveChats;
                    RefreshActiveChats();
                    if (_chats.Count > 0)
                    {
                        cmbActiveChats.SelectedIndex = 0;
                    }
                    return true;
                }
                else
                {
                    CueProvider.SetCue(txtSend, "Waiting untill Skype starts... ");
                    _skypeReconnectTime.Enabled = true;
                    return false;
                }
            }
            catch(Exception ex)
            {
                CueProvider.SetCue(txtSend, "Waiting untill Skype starts... ");
                _skypeReconnectTime.Enabled = true;
                return false;
            }
        }

        void _skypeReconnectTime_Tick(object sender, EventArgs e)
        {
            if(InitSkype())
                _skypeReconnectTime.Enabled = false;
        }

        public bool IsProcessOpen(string name)
        {
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running.
                //Remember, if you have the process running more than once,
                //say IE open 4 times the loop thr way it is now will close all 4,
                //if you want it to just close the first one it finds
                //then add a return; after the Kill
                if (clsProcess.ProcessName== name)
                {
                    //if the process is found to be running then we
                    //return a true
                    return true;
                }
            }
            //otherwise we return a false
            return false;
        }
        
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