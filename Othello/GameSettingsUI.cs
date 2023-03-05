using System;
using System.Windows.Forms;

namespace Ex05_Othello
{
    public partial class GameSettingsUI : Form
    {
        private readonly Button r_ButtonBoardSize;
        private readonly Button r_ButtonPlayAgainstPC;
        private readonly Button r_ButtonPlayAgainstYourFriend;
        private int m_BoardSize = 6;
        private eGameOption m_GameOption;

        public GameSettingsUI()
        {
            this.r_ButtonBoardSize = new System.Windows.Forms.Button();
            this.r_ButtonPlayAgainstPC = new System.Windows.Forms.Button();
            this.r_ButtonPlayAgainstYourFriend = new System.Windows.Forms.Button();
            InitializeComponent();
            initializeForm();
            setTextBoardSizeButton();
        }

        private void initializeForm()
        {
            initializeButtonBoardSize();
            initializeButtonPlayAgainstPC();
            initializeButtonPlayAgainstYourFriend();
            this.ClientSize = new System.Drawing.Size(365, 160);
            this.Controls.Add(this.r_ButtonPlayAgainstYourFriend);
            this.Controls.Add(this.r_ButtonPlayAgainstPC);
            this.Controls.Add(this.r_ButtonBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othelo - Game Settings";
        }

        private void initializeButtonBoardSize()
        {
            this.r_ButtonBoardSize.Location = new System.Drawing.Point(28, 15);
            this.r_ButtonBoardSize.Name = "buttonBoardSize";
            this.r_ButtonBoardSize.Size = new System.Drawing.Size(321, 43);
            this.r_ButtonBoardSize.TabIndex = 1;
            this.r_ButtonBoardSize.UseVisualStyleBackColor = true;
            this.r_ButtonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
        }

        private void initializeButtonPlayAgainstPC()
        {
            this.r_ButtonPlayAgainstPC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.r_ButtonPlayAgainstPC.Location = new System.Drawing.Point(28, 80);
            this.r_ButtonPlayAgainstPC.Name = "buttonPlayAgainstPC";
            this.r_ButtonPlayAgainstPC.Size = new System.Drawing.Size(150, 45);
            this.r_ButtonPlayAgainstPC.TabIndex = 0;
            this.r_ButtonPlayAgainstPC.Text = "Play against the\r\ncomputer";
            this.r_ButtonPlayAgainstPC.UseVisualStyleBackColor = true;
            this.r_ButtonPlayAgainstPC.Click += new System.EventHandler(this.buttonPlayAgainstPC_Click);
        }

        private void initializeButtonPlayAgainstYourFriend()
        {
            this.r_ButtonPlayAgainstYourFriend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.r_ButtonPlayAgainstYourFriend.Location = new System.Drawing.Point(199, 80);
            this.r_ButtonPlayAgainstYourFriend.Name = "buttonPlayAgainstYourFriend";
            this.r_ButtonPlayAgainstYourFriend.Size = new System.Drawing.Size(150, 45);
            this.r_ButtonPlayAgainstYourFriend.TabIndex = 2;
            this.r_ButtonPlayAgainstYourFriend.Text = "Play against your friend";
            this.r_ButtonPlayAgainstYourFriend.UseVisualStyleBackColor = true;
            this.r_ButtonPlayAgainstYourFriend.Click += new System.EventHandler(this.buttonPlayAgainstYourFriend_Click);
        }

        private void setTextBoardSizeButton()
        {
            this.r_ButtonBoardSize.Text = $"Board Size: {m_BoardSize}X{m_BoardSize} (click to increase)";
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            this.m_BoardSize = this.m_BoardSize == 12 ? 6 : this.m_BoardSize + 2;
            this.setTextBoardSizeButton();
        }

        private void buttonPlayAgainstPC_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.m_GameOption = eGameOption.PlayAgainstPC;
            this.DialogResult = DialogResult.OK;
        }

        private void buttonPlayAgainstYourFriend_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.m_GameOption = eGameOption.PlayAgainstUser;
            this.DialogResult = DialogResult.OK;
        }

        public int BoardSize
        {
            get { return this.m_BoardSize; }
        }

        public eGameOption GameOption
        {
            get { return this.m_GameOption; }
        }
    }
}
