
namespace Hearthstone
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxPVP = new System.Windows.Forms.GroupBox();
            this.SwitchLine = new System.Windows.Forms.TextBox();
            this.checkBoxSwitchPVE = new System.Windows.Forms.CheckBox();
            this.comboBoxStrategyPVP = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxTeamPVP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConcedeLine = new System.Windows.Forms.TextBox();
            this.checkBoxautoConcede = new System.Windows.Forms.CheckBox();
            this.checkBoxonlypc = new System.Windows.Forms.CheckBox();
            this.groupBoxPVE = new System.Windows.Forms.GroupBox();
            this.comboBoxStrategyPVE = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxS = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMap = new System.Windows.Forms.ComboBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTeamPVE = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxPVP.SuspendLayout();
            this.groupBoxPVE.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "自动佣兵：开";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 47);
            this.button2.TabIndex = 1;
            this.button2.Text = "PVP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBoxPVP
            // 
            this.groupBoxPVP.Controls.Add(this.SwitchLine);
            this.groupBoxPVP.Controls.Add(this.checkBoxSwitchPVE);
            this.groupBoxPVP.Controls.Add(this.comboBoxStrategyPVP);
            this.groupBoxPVP.Controls.Add(this.label7);
            this.groupBoxPVP.Controls.Add(this.comboBoxTeamPVP);
            this.groupBoxPVP.Controls.Add(this.label1);
            this.groupBoxPVP.Controls.Add(this.ConcedeLine);
            this.groupBoxPVP.Controls.Add(this.checkBoxautoConcede);
            this.groupBoxPVP.Controls.Add(this.checkBoxonlypc);
            this.groupBoxPVP.Location = new System.Drawing.Point(12, 65);
            this.groupBoxPVP.Name = "groupBoxPVP";
            this.groupBoxPVP.Size = new System.Drawing.Size(289, 223);
            this.groupBoxPVP.TabIndex = 2;
            this.groupBoxPVP.TabStop = false;
            this.groupBoxPVP.Text = "PVP";
            // 
            // SwitchLine
            // 
            this.SwitchLine.Location = new System.Drawing.Point(222, 130);
            this.SwitchLine.MaxLength = 5;
            this.SwitchLine.Name = "SwitchLine";
            this.SwitchLine.Size = new System.Drawing.Size(52, 28);
            this.SwitchLine.TabIndex = 17;
            this.SwitchLine.Text = "14000";
            this.SwitchLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SwitchLine.TextChanged += new System.EventHandler(this.SwitchLine_TextChanged);
            this.SwitchLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SwitchLine_KeyPress);
            // 
            // checkBoxSwitchPVE
            // 
            this.checkBoxSwitchPVE.AutoSize = true;
            this.checkBoxSwitchPVE.Location = new System.Drawing.Point(16, 132);
            this.checkBoxSwitchPVE.Name = "checkBoxSwitchPVE";
            this.checkBoxSwitchPVE.Size = new System.Drawing.Size(205, 22);
            this.checkBoxSwitchPVE.TabIndex = 16;
            this.checkBoxSwitchPVE.Text = "分数达到时切换至PVE";
            this.checkBoxSwitchPVE.UseVisualStyleBackColor = true;
            this.checkBoxSwitchPVE.CheckedChanged += new System.EventHandler(this.checkBoxSwitchPVE_CheckedChanged);
            // 
            // comboBoxStrategyPVP
            // 
            this.comboBoxStrategyPVP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrategyPVP.DropDownWidth = 242;
            this.comboBoxStrategyPVP.FormattingEnabled = true;
            this.comboBoxStrategyPVP.Location = new System.Drawing.Point(110, 179);
            this.comboBoxStrategyPVP.Name = "comboBoxStrategyPVP";
            this.comboBoxStrategyPVP.Size = new System.Drawing.Size(164, 26);
            this.comboBoxStrategyPVP.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 14;
            this.label7.Text = "PVP策略:";
            // 
            // comboBoxTeamPVP
            // 
            this.comboBoxTeamPVP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTeamPVP.FormattingEnabled = true;
            this.comboBoxTeamPVP.Location = new System.Drawing.Point(109, 25);
            this.comboBoxTeamPVP.Name = "comboBoxTeamPVP";
            this.comboBoxTeamPVP.Size = new System.Drawing.Size(142, 26);
            this.comboBoxTeamPVP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择队伍:";
            // 
            // ConcedeLine
            // 
            this.ConcedeLine.Location = new System.Drawing.Point(222, 96);
            this.ConcedeLine.MaxLength = 5;
            this.ConcedeLine.Name = "ConcedeLine";
            this.ConcedeLine.Size = new System.Drawing.Size(52, 28);
            this.ConcedeLine.TabIndex = 1;
            this.ConcedeLine.Text = "6000";
            this.ConcedeLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ConcedeLine.TextChanged += new System.EventHandler(this.Concedeline_TextChanged);
            // 
            // checkBoxautoConcede
            // 
            this.checkBoxautoConcede.AutoSize = true;
            this.checkBoxautoConcede.Location = new System.Drawing.Point(16, 98);
            this.checkBoxautoConcede.Name = "checkBoxautoConcede";
            this.checkBoxautoConcede.Size = new System.Drawing.Size(214, 22);
            this.checkBoxautoConcede.TabIndex = 0;
            this.checkBoxautoConcede.Text = "自动投降：当分数高于";
            this.checkBoxautoConcede.UseVisualStyleBackColor = true;
            // 
            // checkBoxonlypc
            // 
            this.checkBoxonlypc.AutoSize = true;
            this.checkBoxonlypc.Location = new System.Drawing.Point(16, 64);
            this.checkBoxonlypc.Name = "checkBoxonlypc";
            this.checkBoxonlypc.Size = new System.Drawing.Size(106, 22);
            this.checkBoxonlypc.TabIndex = 0;
            this.checkBoxonlypc.Text = "只打电脑";
            this.checkBoxonlypc.UseVisualStyleBackColor = true;
            // 
            // groupBoxPVE
            // 
            this.groupBoxPVE.Controls.Add(this.comboBoxStrategyPVE);
            this.groupBoxPVE.Controls.Add(this.label8);
            this.groupBoxPVE.Controls.Add(this.comboBoxS);
            this.groupBoxPVE.Controls.Add(this.label5);
            this.groupBoxPVE.Controls.Add(this.comboBoxMap);
            this.groupBoxPVE.Controls.Add(this.comboBoxMode);
            this.groupBoxPVE.Controls.Add(this.label4);
            this.groupBoxPVE.Controls.Add(this.label3);
            this.groupBoxPVE.Controls.Add(this.comboBoxTeamPVE);
            this.groupBoxPVE.Controls.Add(this.label2);
            this.groupBoxPVE.Location = new System.Drawing.Point(12, 65);
            this.groupBoxPVE.Name = "groupBoxPVE";
            this.groupBoxPVE.Size = new System.Drawing.Size(289, 223);
            this.groupBoxPVE.TabIndex = 3;
            this.groupBoxPVE.TabStop = false;
            this.groupBoxPVE.Text = "PVE";
            // 
            // comboBoxStrategyPVE
            // 
            this.comboBoxStrategyPVE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrategyPVE.DropDownWidth = 242;
            this.comboBoxStrategyPVE.FormattingEnabled = true;
            this.comboBoxStrategyPVE.Location = new System.Drawing.Point(110, 179);
            this.comboBoxStrategyPVE.Name = "comboBoxStrategyPVE";
            this.comboBoxStrategyPVE.Size = new System.Drawing.Size(164, 26);
            this.comboBoxStrategyPVE.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "PVE策略:";
            // 
            // comboBoxS
            // 
            this.comboBoxS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxS.FormattingEnabled = true;
            this.comboBoxS.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxS.Location = new System.Drawing.Point(226, 103);
            this.comboBoxS.Name = "comboBoxS";
            this.comboBoxS.Size = new System.Drawing.Size(48, 26);
            this.comboBoxS.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "神秘选项怪物节点不超过：";
            // 
            // comboBoxMap
            // 
            this.comboBoxMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMap.DropDownWidth = 242;
            this.comboBoxMap.FormattingEnabled = true;
            this.comboBoxMap.Location = new System.Drawing.Point(110, 137);
            this.comboBoxMap.Name = "comboBoxMap";
            this.comboBoxMap.Size = new System.Drawing.Size(164, 26);
            this.comboBoxMap.TabIndex = 10;
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "任务模式",
            "刷图模式"});
            this.comboBoxMode.Location = new System.Drawing.Point(110, 69);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(164, 26);
            this.comboBoxMode.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "选择模式:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "选择地图:";
            // 
            // comboBoxTeamPVE
            // 
            this.comboBoxTeamPVE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTeamPVE.FormattingEnabled = true;
            this.comboBoxTeamPVE.Location = new System.Drawing.Point(110, 27);
            this.comboBoxTeamPVE.Name = "comboBoxTeamPVE";
            this.comboBoxTeamPVE.Size = new System.Drawing.Size(164, 26);
            this.comboBoxTeamPVE.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择队伍:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 293);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxPVP);
            this.Controls.Add(this.groupBoxPVE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "(bug反馈群:414051258)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxPVP.ResumeLayout(false);
            this.groupBoxPVP.PerformLayout();
            this.groupBoxPVE.ResumeLayout(false);
            this.groupBoxPVE.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public  System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBoxPVP;
        private System.Windows.Forms.TextBox ConcedeLine;
        private System.Windows.Forms.CheckBox checkBoxautoConcede;
        private System.Windows.Forms.CheckBox checkBoxonlypc;
        private System.Windows.Forms.GroupBox groupBoxPVE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTeamPVP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTeamPVE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.ComboBox comboBoxMap;
        private System.Windows.Forms.ComboBox comboBoxS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxStrategyPVP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxStrategyPVE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox SwitchLine;
        private System.Windows.Forms.CheckBox checkBoxSwitchPVE;
    }
}