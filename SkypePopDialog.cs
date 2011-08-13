using System;
using System.Drawing;
using System.Windows.Forms;
using SKYPE4COMLib;
using Application = System.Windows.Forms.Application;

namespace SkypePop
{
    public partial class SkypePopDialog : SlideDialog
    {
        private readonly Font _bold = new Font("Consolas", 11f, FontStyle.Bold);
        private readonly Font _regular = new Font("Consolas", 11f, FontStyle.Regular);
        private readonly Skype _skype;
        private ChatCollection _chats;
        private bool _colourToggle;
        private Chat _currentChat;
        private Color _color1 = ColorTranslator.FromHtml("#514124");
        private Color _color2 = ColorTranslator.FromHtml("#234177");

        public SkypePopDialog(Form poOwner, float pfStep)
            : base(poOwner, pfStep)
        {
            InitializeComponent();

            _skype = new Skype();
            // Use skype protocol version 7 
            _skype.Attach(7, false);
            // Listen 
            _skype.MessageStatus += skype_MessageStatus;
            _chats = _skype.ActiveChats;
            for (int i = 1; i < _chats.Count + 1; i++)
            {
                string chatName = _chats[i].FriendlyName;
                if (chatName.IndexOf("|") > 0)
                    chatName = chatName.Substring(0, chatName.IndexOf("|") - 1);
                cmbActiveChats.Items.Add(chatName);
            }
            if (_chats.Count > 0)
            {
                cmbActiveChats.SelectedIndex = 0;
                _currentChat = _chats[1];
            }
        }
        
        public void FocusOut()
        {
            //cmbActiveChats.Focus();
            btnHide.Focus();
        }

        private void skype_MessageStatus(ChatMessage msg, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsReceived)
            {
                AppendMessage(msg.FromDisplayName, msg.Body);
                Slide(true);
                int index = IndexOf(msg.Chat);
                if (index == -1)
                {
                    string chatName = msg.Chat.FriendlyName;
                    if (chatName.IndexOf("|") > 0)
                        chatName = chatName.Substring(0, chatName.IndexOf("|") - 1);
                    cmbActiveChats.Items.Add(chatName);
                    //cmbActiveChats.SelectedIndex = cmbActiveChats.Items.Count - 1;
                }
                //else
                //{
                //    cmbActiveChats.SelectedIndex = index;
                //}
            }
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

        private void cmbActiveChats_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 1; i < _chats.Count + 1; i++)
            {
                string chatName = _chats[i].FriendlyName;
                if (chatName.IndexOf("|") > 0)
                    chatName = chatName.Substring(0, chatName.IndexOf("|") - 1);
                if (chatName == cmbActiveChats.Text)
                {
                    //if (!txtSend.Focused)
                    //{
                    _currentChat = _chats[i];
                    break;
                    //}
                }
            }
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
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

        private void AppendMessage(string from, string message)
        {
            txtReceived.SelectionColor = GetColor();
            txtReceived.SelectionFont = _bold;
            txtReceived.AppendText(from + " : ");
            txtReceived.SelectionFont = _regular;
            txtReceived.AppendText(message + Environment.NewLine);
            txtReceived.ScrollToCaret();
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

        private void btnHide_Click(object sender, EventArgs e)
        {
            Slide(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}