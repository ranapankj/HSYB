
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
            this.comboBoxTeamPVP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Concedeline = new System.Windows.Forms.TextBox();
            this.checkBoxautoConcede = new System.Windows.Forms.CheckBox();
            this.checkBoxonlypc = new System.Windows.Forms.CheckBox();
            this.groupBoxPVE = new System.Windows.Forms.GroupBox();
            this.comboBoxS = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMap = new System.Windows.Forms.ComboBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTeamPVE = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxStrategy = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.groupBoxPVP.Controls.Add(this.comboBoxTeamPVP);
            this.groupBoxPVP.Controls.Add(this.label1);
            this.groupBoxPVP.Controls.Add(this.Concedeline);
            this.groupBoxPVP.Controls.Add(this.checkBoxautoConcede);
            this.groupBoxPVP.Controls.Add(this.checkBoxonlypc);
            this.groupBoxPVP.Location = new System.Drawing.Point(12, 65);
            this.groupBoxPVP.Name = "groupBoxPVP";
            this.groupBoxPVP.Size = new System.Drawing.Size(289, 176);
            this.groupBoxPVP.TabIndex = 2;
            this.groupBoxPVP.TabStop = false;
            this.groupBoxPVP.Text = "PVP";
            // 
            // comboBoxTeamPVP
            // 
            this.comboBoxTeamPVP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTeamPVP.FormattingEnabled = true;
            this.comboBoxTeamPVP.Location = new System.Drawing.Point(109, 25);
            this.comboBoxTeamPVP.Name = "comboBoxTeamPVP";
            this.comboBoxTeamPVP.Size = new System.Drawing.Size(142, 26);
            this.comboBoxTeamPVP.TabIndex = 0;
            //this.comboBoxTeamPVP.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeamPVP_SelectedIndexChanged);
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
            // Concedeline
            // 
            this.Concedeline.Location = new System.Drawing.Point(222, 96);
            this.Concedeline.MaxLength = 5;
            this.Concedeline.Name = "Concedeline";
            this.Concedeline.Size = new System.Drawing.Size(52, 28);
            this.Concedeline.TabIndex = 1;
            this.Concedeline.Text = "6000";
            this.Concedeline.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Concedeline.TextChanged += new System.EventHandler(this.Concedeline_TextChanged);
            //this.Concedeline.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Concedeline_KeyPress);
            // 
            // checkBoxautoConcede
            // 
            this.checkBoxautoConcede.AutoSize = true;
            this.checkBoxautoConcede.Location = new System.Drawing.Point(16, 96);
            this.checkBoxautoConcede.Name = "checkBoxautoConcede";
            this.checkBoxautoConcede.Size = new System.Drawing.Size(214, 22);
            this.checkBoxautoConcede.TabIndex = 0;
            this.checkBoxautoConcede.Text = "自动投降：当分数高于";
            this.checkBoxautoConcede.UseVisualStyleBackColor = true;
            //this.checkBoxautoConcede.CheckedChanged += new System.EventHandler(this.checkBoxautoConcede_CheckedChanged);
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
            //this.checkBoxonlypc.CheckedChanged += new System.EventHandler(this.checkBoxonlypc_CheckedChanged);
            // 
            // groupBoxPVE
            // 
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
            this.groupBoxPVE.Size = new System.Drawing.Size(289, 176);
            this.groupBoxPVE.TabIndex = 3;
            this.groupBoxPVE.TabStop = false;
            this.groupBoxPVE.Text = "PVE";
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
            //this.comboBoxS.SelectedIndexChanged += new System.EventHandler(this.comboBoxS_SelectedIndexChanged);
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
            //this.comboBoxMap.SelectedIndexChanged += new System.EventHandler(this.comboBoxMap_SelectedIndexChanged);
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
            //this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
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
            //this.comboBoxTeamPVE.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeamPVE_SelectedIndexChanged);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "战斗策略";
            // 
            // comboBoxStrategy
            // 
            this.comboBoxStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrategy.DropDownWidth = 242;
            this.comboBoxStrategy.FormattingEnabled = true;
            this.comboBoxStrategy.Location = new System.Drawing.Point(122, 247);
            this.comboBoxStrategy.Name = "comboBoxStrategy";
            this.comboBoxStrategy.Size = new System.Drawing.Size(164, 26);
            this.comboBoxStrategy.TabIndex = 13;
            //this.comboBoxStrategy.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrategy_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 53);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 293);
            this.Controls.Add(this.comboBoxStrategy);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxPVE);
            this.Controls.Add(this.groupBoxPVP);
            this.Controls.Add(this.groupBox1);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBoxPVP;
        private System.Windows.Forms.TextBox Concedeline;
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxStrategy;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}