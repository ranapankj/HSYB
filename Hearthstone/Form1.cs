using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Hearthstone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.enableAutoPlay = MyHsHelper.MyHsHelper.autorun.Value = !MyHsHelper.MyHsHelper.enableAutoPlay;
            button1.Text = "自动佣兵：" + (MyHsHelper.MyHsHelper.enableAutoPlay ? "开" : "关");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.isPVP = MyHsHelper.MyHsHelper.PVP.Value = !MyHsHelper.MyHsHelper.isPVP;
            button2.Text = MyHsHelper.MyHsHelper.isPVP ? "PVP" : "PVE";
            groupBoxPVP.Visible = MyHsHelper.MyHsHelper.isPVP;
            groupBoxPVE.Visible = !MyHsHelper.MyHsHelper.isPVP;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "自动佣兵：" + (MyHsHelper.MyHsHelper.enableAutoPlay ? "开" : "关");
            button2.Text = MyHsHelper.MyHsHelper.isPVP ? "PVP" : "PVE";
            groupBoxPVP.Visible = MyHsHelper.MyHsHelper.isPVP;
            groupBoxPVE.Visible = !MyHsHelper.MyHsHelper.isPVP;
            List<LettuceTeam> teams = CollectionManager.Get().GetTeams();
            if (teams.Count == 0) { SceneMgr.Get().SetNextMode(SceneMgr.Mode.LETTUCE_VILLAGE, SceneMgr.TransitionHandlerType.SCENEMGR, null); UIStatus.Get().AddInfo("请先进入佣兵场景或者创建佣兵队伍"); Form form = (Form)sender; form.Close(); }
            foreach (LettuceTeam team in teams)
            {
                comboBoxTeamPVE.Items.Add(team.Name);
                comboBoxTeamPVP.Items.Add(team.Name);
            }
            comboBoxTeamPVE.SelectedIndex = comboBoxTeamPVE.Items.IndexOf(MyHsHelper.MyHsHelper.PVEteamName);
            comboBoxTeamPVP.SelectedIndex = comboBoxTeamPVP.Items.IndexOf(MyHsHelper.MyHsHelper.PVPteamName);
            checkBoxonlypc.Checked = MyHsHelper.MyHsHelper.onlyPC;
            checkBoxautoConcede.Checked = MyHsHelper.MyHsHelper.认输;
            ConcedeLine.Text = MyHsHelper.MyHsHelper.分数线.ToString();
            checkBoxSwitchPVE.Checked = MyHsHelper.MyHsHelper.自动切换;
            SwitchLine.Text = MyHsHelper.MyHsHelper.切换线.ToString();
            comboBoxMode.SelectedIndex = comboBoxMode.Items.IndexOf(MyHsHelper.MyHsHelper.PVEMode ? "任务模式" : "刷图模式");
            label5.Visible = comboBoxS.Visible = MyHsHelper.MyHsHelper.PVEMode;
            comboBoxS.SelectedItem = MyHsHelper.MyHsHelper.PVEstep.ToString();
            for (int i = 57; i < 200; i++)
            {
                LettuceBountyDbfRecord record = GameDbf.LettuceBounty.GetRecord(i);
                if (record != null)
                {
                    //bool isLocked = false;
                    //int id = record.RequiredCompletedBounty;
                    //if (i == 67 || i == 85 ) { id = 65; }
                    //if (i == 73 || i == 94 ) { id = 72; }
                    //if (i == 79 || i == 100 ) { id = 78; }
                    //if (i == 85) { id = 57; }
                    //if (i == 94) { id = 67; }
                    //if (i == 100) { id = 73; }
                    //if (i == 106 ) { id = 79; }
                    //isLocked = !GameUtils.IsBountyComplete(id);
                    //if (i==57) { isLocked = false; }
                    //if (!isLocked)
                    //{
                    comboBoxMap.Items.Add(i.ToString() + (record.Heroic ? " H" : " ") + record.BountySetRecord.Name.GetString() + " " + record.FinalBossCardRecord.Name.GetString());
                    //}
                    if (i == MyHsHelper.MyHsHelper.mapID) { comboBoxMap.SelectedIndex = comboBoxMap.Items.Count - 1; }
                }
            }
            DirectoryInfo folder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            foreach (FileInfo file in folder.GetFiles("*.dll"))
            {
                if (Path.GetFileName(Assembly.GetExecutingAssembly().Location) == file.Name || Path.GetFileName(Assembly.GetExecutingAssembly().Location) + "1" == file.Name) { continue; }
                comboBoxStrategyPVP.Items.Add(file.Name);
                comboBoxStrategyPVE.Items.Add(file.Name);
                if (file.Name == MyHsHelper.MyHsHelper.StrategyPVP) { comboBoxStrategyPVP.SelectedIndex = comboBoxStrategyPVP.Items.Count - 1; }
                if (file.Name == MyHsHelper.MyHsHelper.StrategyPVE) { comboBoxStrategyPVE.SelectedIndex = comboBoxStrategyPVE.Items.Count - 1; }
            }
            comboBoxStrategyPVP.Items.Add("");
            comboBoxStrategyPVE.Items.Add("");
            if (comboBoxStrategyPVP.Items.Count == 1) { MyHsHelper.MyHsHelper.StrategyPVP = MyHsHelper.MyHsHelper.PVP策略.Value = MyHsHelper.MyHsHelper.StrategyPVE = MyHsHelper.MyHsHelper.PVE策略.Value = null; }

            Form1_LoadComplete();   //等待窗口显示后添加comboBox事件，避免自动触发。
        }

        private void Form1_LoadComplete()
        {
            this.comboBoxTeamPVP.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeamPVP_SelectedIndexChanged);
            this.ConcedeLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Concedeline_KeyPress);
            this.checkBoxautoConcede.CheckedChanged += new System.EventHandler(this.checkBoxautoConcede_CheckedChanged);
            this.checkBoxonlypc.CheckedChanged += new System.EventHandler(this.checkBoxonlypc_CheckedChanged);
            this.comboBoxS.SelectedIndexChanged += new System.EventHandler(this.comboBoxS_SelectedIndexChanged);
            this.comboBoxMap.SelectedIndexChanged += new System.EventHandler(this.comboBoxMap_SelectedIndexChanged);
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
            this.comboBoxTeamPVE.SelectedIndexChanged += new System.EventHandler(this.comboBoxTeamPVE_SelectedIndexChanged);
            this.comboBoxStrategyPVP.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrategyPVP_SelectedIndexChanged);
            this.comboBoxStrategyPVE.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrategyPVE_SelectedIndexChanged);
        }
        private void comboBoxMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] strArray = comboBoxMap.SelectedItem.ToString().Split(new char[] { ' ' });
            if (strArray.Length > 0)
            {
                int id = Convert.ToInt32(strArray[0]);
                MyHsHelper.MyHsHelper.mapID = MyHsHelper.MyHsHelper.地图ID.Value = id;
            }
        }

        private void comboBoxTeamPVE_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.PVEteamName = MyHsHelper.MyHsHelper.PVEteam.Value = comboBoxTeamPVE.SelectedItem.ToString();
        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMode.SelectedItem.ToString() == "任务模式")
            {
                MyHsHelper.MyHsHelper.PVEMode = MyHsHelper.MyHsHelper.PVE模式.Value = true;
                label5.Visible = comboBoxS.Visible = true;
            }
            else
            {
                MyHsHelper.MyHsHelper.PVEMode = MyHsHelper.MyHsHelper.PVE模式.Value = false;
                label5.Visible = comboBoxS.Visible = false;
            }

        }

        private void comboBoxTeamPVP_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.PVPteamName = MyHsHelper.MyHsHelper.PVPteam.Value = comboBoxTeamPVP.SelectedItem.ToString();
        }

        private void checkBoxonlypc_CheckedChanged(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.onlyPC = MyHsHelper.MyHsHelper.只打电脑.Value = checkBoxonlypc.Checked;
        }

        private void checkBoxautoConcede_CheckedChanged(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.认输 = MyHsHelper.MyHsHelper.autoConcede.Value = checkBoxautoConcede.Checked;
        }

        private void Concedeline_TextChanged(object sender, EventArgs e)
        {
            if (ConcedeLine.Text.Length > 0)
            {
                MyHsHelper.MyHsHelper.分数线 = MyHsHelper.MyHsHelper.Concedeline.Value = Convert.ToInt32(ConcedeLine.Text);
            }
        }

        private void Concedeline_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void checkBoxSwitchPVE_CheckedChanged(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.自动切换  = MyHsHelper.MyHsHelper.autoSwitch.Value = checkBoxSwitchPVE.Checked;
        }

        private void SwitchLine_TextChanged(object sender, EventArgs e)
        {
            if (SwitchLine.Text.Length >0)
            {
            MyHsHelper.MyHsHelper.切换线 = MyHsHelper.MyHsHelper.SwitchLine.Value = Convert.ToInt32(SwitchLine.Text);
            }
        }
        private void SwitchLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void comboBoxS_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyHsHelper.MyHsHelper.PVEstep = MyHsHelper.MyHsHelper.步数.Value = Convert.ToInt32(comboBoxS.SelectedItem.ToString());
        }

        private void comboBoxStrategyPVP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyHsHelper.MyHsHelper.StrategyPVP != comboBoxStrategyPVP.SelectedItem.ToString())
            {
                MyHsHelper.MyHsHelper.StrategyPVP = MyHsHelper.MyHsHelper.PVP策略.Value = comboBoxStrategyPVP.SelectedItem.ToString();
                MyHsHelper.MyHsHelper.LoadPolicy();
            }
        }

        private void comboBoxStrategyPVE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyHsHelper.MyHsHelper.StrategyPVE != comboBoxStrategyPVE.SelectedItem.ToString())
            {
                MyHsHelper.MyHsHelper.StrategyPVE = MyHsHelper.MyHsHelper.PVE策略.Value = comboBoxStrategyPVE.SelectedItem.ToString();
                MyHsHelper.MyHsHelper.LoadPolicy();
            }
        }

    }
}
