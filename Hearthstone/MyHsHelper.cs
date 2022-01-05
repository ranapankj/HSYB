using System.Reflection;
using BepInEx;
using PegasusShared;
using UnityEngine;
using HarmonyLib;
using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading.Tasks;


namespace MyHsHelper
{
    [BepInPlugin("zyz.plugins.MyHsHelper", "佣兵挂机插件", "1.0.2.0")]
    public class MyHsHelper : BaseUnityPlugin
    {

        public static bool enableAutoPlay = false;
        public static bool Initialize = false;
        public static float FindingGameTime;
        public static float sleeptime;
        public static bool fakeClick;
        public static bool fakeClickDown;
        public static bool fakeClickUp;
        public static bool fakePos;
        public static int fakeClickDownCount = 0;
        public static float idleTime;
        public static float fakeMouseX;
        public static float fakeMouseY;
        static Queue clickqueue = new Queue();
        //private static float queuetimer;
        //private static IntPtr WindowHandle;
        public static bool hideGui;
        private struct Reward
        {
            public int Level;
            public int Xp;
            public int XpNeeded;
        };
        private static Reward reward;
        private string Labelstr;
        private static int 当日经验;
        private static float 当日时间;
        private static int 昨日经验;
        private static float 昨日时间;
        public static BepInEx.Configuration.ConfigEntry<bool> autorun;
        public static BepInEx.Configuration.ConfigEntry<bool> 只打电脑;
        public static BepInEx.Configuration.ConfigEntry<bool> PVP;
        public static BepInEx.Configuration.ConfigEntry<string> PVPteam;
        public static BepInEx.Configuration.ConfigEntry<string> PVEteam;
        public static BepInEx.Configuration.ConfigEntry<bool> autoConcede;
        public static BepInEx.Configuration.ConfigEntry<int> Concedeline;
        public static BepInEx.Configuration.ConfigEntry<int> 地图ID;
        public static BepInEx.Configuration.ConfigEntry<bool> PVE模式;      //true任务模式 false刷图模式
        public static BepInEx.Configuration.ConfigEntry<int> 步数;
        public static BepInEx.Configuration.ConfigEntry<string> 策略;
        public static BepInEx.Configuration.ConfigEntry<long> 开始时间;
        public static BepInEx.Configuration.ConfigEntry<bool> Hidemain;
        public static BepInEx.Configuration.ConfigEntry<int> todayxp;
        public static BepInEx.Configuration.ConfigEntry<float> todaytime;
        public static BepInEx.Configuration.ConfigEntry<int> yesterdayxp;
        public static BepInEx.Configuration.ConfigEntry<float> yesterdaytime;
        public static bool 认输;
        public static bool isPVP;
        public static bool PVEMode;
        public static int PVEstep;
        public static string Strategy;
        public static long StartTime;
        public static bool onlyPC;
        public static string PVPteamName;
        public static string PVEteamName;
        public static int mapID;
        public static int 分数线;
        private float Labeltimer;
        //private float 投降时间;
        private static List<PegasusLettuce.LettuceMapNode> minNode = new List<PegasusLettuce.LettuceMapNode>();
        private static bool flag = true;
        private static bool isHaveRewardTask;
        private static List<string> TaskMercenary = new List<string>();
        private static List<string> TaskAbilityName = new List<string>();
        private static Hearthstone.Form1 form;
        private static string path;
        private static int GetHashCount;
        private static bool HashOK;
        private static object StrategyInstance;
        private static MethodInfo Entrance;
        private static MethodInfo Battle;
        private static bool StrategyOK;
        private static int phaseID;
        private static bool StrategyRun = false;
        public static Queue<Entity> EntranceQueue = new Queue<Entity>();
        public static Queue<Battles> BattleQueue = new Queue<Battles>();
        private static Battles battles;
        private static bool HandleQueueOK = true;

        public struct Battles
        {
            public Entity source;
            public Entity target;
            public Entity Ability;
            public string SubName;
        };

        public struct State
        {
            public bool IsPVP
            { 
                get
                {
                    return isPVP;
                }
            }
            public long 开始时间
            {
                get
                {
                    return StartTime;
                }
            }
            public List<string> 任务佣兵
            {
                get
                {
                    return TaskMercenary;
                }
            }
            public List<string> 任务技能
            {
                get
                {
                    return TaskAbilityName;
                }
            }
        };

        // 在插件启动时会直接调用Awake()方法
        void Awake()
        {
            // 使用Debug.Log()方法来将文本输出到控制台
            //Debug.Log("Hello, world!");
            //clickqueue.Enqueue(new float[] { 584f, 550f });
            //object tmp = clickqueue.Dequeue();
            //Process cur = Process.GetCurrentProcess();
            //UnityEngine.Debug.Log("2" + Assembly.GetExecutingAssembly().Location);
            //UnityEngine.Debug.Log(Path);
            //WindowHandle = Macroresolute.ProcessEx.GetMainWindowHandle(cur.Id);
            //UnityEngine.Debug.Log(cur.Id);
            //UnityEngine.Debug.Log("窗口句柄: " + WindowHandle);
            //GetVer();   //对比sha1，不对则下载
        }

        void OnGUI()
        {
            if (hideGui || !HashOK) { return; }
            if (Initialize)
            {
                if (GUI.Button(new Rect(1f, 1f, 90f, 29f), new GUIContent("自动佣兵：" + (enableAutoPlay ? "开" : "关"))))
                {
                    Resetidle();
                    enableAutoPlay = !enableAutoPlay;
                    autorun.Value = enableAutoPlay;
                    flag = enableAutoPlay;
                    StrategyRun = false;
                    HandleQueueOK = true;
                    EntranceQueue.Clear();
                    BattleQueue.Clear();
                }

                if (GUI.Button(new Rect(92f, 1f, 90f, 29f), new GUIContent("设置")))
                {
                    //isPVP = !isPVP;
                    //PVP.Value = isPVP;
                    if (form == null) //如果子窗体为空则创造实例 并显示
                    {
                        form = new Hearthstone.Form1();
                        form.Show();
                    }
                    else
                    {
                        if (form.IsDisposed) //若子窗体关闭 则打开新子窗体 并显示
                        {
                            form = new Hearthstone.Form1();
                            form.Show();
                        }
                        else
                        {
                            form.Activate(); //使子窗体获得焦点
                        }
                    }
                }
            }
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.background = null;
            fontStyle.normal.textColor = new Color(1, 0, 0);
            fontStyle.fontSize = 22;
            GUI.Label(new Rect(190f, 1f, 1800f, 38f), Labelstr, fontStyle);

            //认输 = GUI.Toggle(new Rect(1f, 30f, 150f, 38f), 认输, "自动认输，当分数高于");
            //autoConcede.Value = 认输;
            //var Tmpstr = GUI.TextField(new Rect(150f, 30f, 40f, 20f), 分数线.ToString(), 4);

            //分数线 = int.Parse(Tmpstr);
            //Concedeline.Value = 分数线;

        }
        // 在所有插件全部启动完成后会调用Start()方法，执行顺序在Awake()后面；
        void Start()
        {
            path = System.Windows.Forms.Application.StartupPath + "\\BepInEx\\idleTime.log";
            var tmp = Config.Bind(DateTime.Now.AddDays(1).ToString("MM-dd"), "时间", 0f);
            var tmp1 = Config.Bind(DateTime.Now.AddDays(1).ToString("MM-dd"), "经验", 0);
            Config.Clear();
            tmp.Value = 0f;
            tmp1.Value = 0;
            for (int i = 3; i < 30; i++)
            {
                tmp = Config.Bind(DateTime.Now.AddDays(-i).ToString("MM-dd"), "时间", 0f);
                tmp1 = Config.Bind(DateTime.Now.AddDays(-i).ToString("MM-dd"), "经验", 0);
                Config.Clear();
                tmp.Value = 0f;
                tmp1.Value = 0;
            }
            Config.Clear();                             //上面这一段是清除多余的配置
            GetVer();   //对比sha1，不对则下载
            autorun = Config.Bind("配置", "AutoRun", false, "是否自动佣兵挂机");
            enableAutoPlay = autorun.Value;
            PVP = Config.Bind("配置", "PVP", false, "PVP或者PVE");
            isPVP = PVP.Value;
            只打电脑 = Config.Bind("配置", "onlyPC", true, "PVP只打电脑");
            onlyPC = 只打电脑.Value;
            autoConcede = Config.Bind("配置", "投降", true, "是否自动认输");
            认输 = autoConcede.Value;
            Concedeline = Config.Bind("配置", "分数线", 6000, "自动认输分数线，高于此分数自动认输");
            分数线 = Concedeline.Value;
            PVE模式 = Config.Bind("配置", "PVE模式", true, "true任务模式 false刷图模式");
            PVEMode = PVE模式.Value;
            步数 = Config.Bind("配置", "步数", 2, "距离神秘选项怪物数，超过则重开地图。");
            PVEstep = 步数.Value;
            地图ID = Config.Bind("配置", "地图ID", 92, "地图ID");
            mapID = 地图ID.Value;
            PVPteam = Config.Bind("配置", "PVPteam", "", "PVP队伍名字");
            PVPteamName = PVPteam.Value;
            PVEteam = Config.Bind("配置", "PVEteam", "", "PVE队伍名字");
            PVEteamName = PVEteam.Value;
            策略 = Config.Bind("配置", "策略", "", "策略文件名");
            Strategy = 策略.Value;
            开始时间 = Config.Bind("配置", "开始时间", 0L, "对局开始时间");
            StartTime = 开始时间.Value;
            Hidemain = Config.Bind("配置", "隐藏", false, "是否隐藏GUI");
            hideGui = Hidemain.Value;
            todayxp = Config.Bind(DateTime.Now.ToString("MM-dd"), "经验", 0, "当日获得的通行证经验值");
            当日经验 = todayxp.Value;
            todaytime = Config.Bind(DateTime.Now.ToString("MM-dd"), "时间", 0f, "当日挂机总时间");
            当日时间 = todaytime.Value;
            yesterdayxp = Config.Bind(DateTime.Now.AddDays(-1).ToString("MM-dd"), "经验", 0, "当日获得的通行证经验值");
            昨日经验 = yesterdayxp.Value;
            yesterdaytime = Config.Bind(DateTime.Now.AddDays(-1).ToString("MM-dd"), "时间", 0f, "当日挂机总时间(分钟)");
            昨日时间 = yesterdaytime.Value;
            GetHash();      //获取文件sha1
            LoadPolicy();

            Harmony harmony = new Harmony("MyHsHelper.patch");
            harmony.PatchAll();

            //MethodInfo method1 = typeof(InputCollection).GetMethod("GetMousePosition");
            //MethodInfo method2 = typeof(MyHsHelper).GetMethod("MousePos");
            //harmony.Patch(method1, null, new HarmonyMethod(method2));

            //method1 = typeof(InputCollection).GetMethod("GetMouseButton");
            //method2 = typeof(MyHsHelper).GetMethod("MouseButton");
            //harmony.Patch(method1, null, new HarmonyMethod(method2));

            //method1 = typeof(InputCollection).GetMethod("GetMouseButtonDown");
            //method2 = typeof(MyHsHelper).GetMethod("MouseButtonDown");
            //harmony.Patch(method1, null, new HarmonyMethod(method2));

            //method1 = typeof(InputCollection).GetMethod("GetMouseButtonUp");
            //method2 = typeof(MyHsHelper).GetMethod("MouseButtonUp");
            //harmony.Patch(method1, null, new HarmonyMethod(method2));

            MethodInfo method1 = typeof(RewardPopups).GetMethod("ShowMercenariesRewards");
            MethodInfo method2 = typeof(MyHsHelper).GetMethod("____________");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(MercenariesSeasonRewardsDialog).GetMethod("ShowWhenReady", BindingFlags.Instance | BindingFlags.NonPublic);
            method2 = typeof(MyHsHelper).GetMethod("_____________");
            harmony.Patch(method1,  new HarmonyMethod(method2));

            method1 = typeof(RewardBoxesDisplay).GetMethod("OnDoneButtonShown", BindingFlags.Instance | BindingFlags.NonPublic);
            method2 = typeof(MyHsHelper).GetMethod("___________");
            harmony.Patch(method1, null, new HarmonyMethod(method2));

            method1 = typeof(RewardBoxesDisplay).GetMethod("RewardPackageOnComplete", BindingFlags.Instance | BindingFlags.NonPublic);
            method2 = typeof(MyHsHelper).GetMethod("__________");
            harmony.Patch(method1, null, new HarmonyMethod(method2));

            method1 = typeof(LettuceMap).GetMethod("CreateMapFromProto", BindingFlags.Instance | BindingFlags.Public);
            method2 = typeof(MyHsHelper).GetMethod("________");
            harmony.Patch(method1, null, new HarmonyMethod(method2));

            method1 = typeof(LettuceMapDisplay).GetMethod("TryAutoNextSelectCoin", BindingFlags.Instance | BindingFlags.NonPublic);
            method2 = typeof(MyHsHelper).GetMethod("_________");
            harmony.Patch(method1, null, new HarmonyMethod(method2));

            method1 = typeof(LettuceMapDisplay).GetMethod("DisplayNewlyGrantedAnomalyCards", BindingFlags.Instance | BindingFlags.NonPublic);
            method2 = typeof(MyHsHelper).GetMethod("_______");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(LettuceMapDisplay).GetMethod("ShouldShowVisitorSelection", BindingFlags.Instance | BindingFlags.NonPublic);    //弹出选择来访者界面
            method2 = typeof(MyHsHelper).GetMethod("______");
            harmony.Patch(method1, null, new HarmonyMethod(method2));

            method1 = typeof(LettuceMapDisplay).GetMethod("OnVisitorSelectionResponseReceived", BindingFlags.Instance | BindingFlags.NonPublic);    //来访者选择完毕界面
            method2 = typeof(MyHsHelper).GetMethod("OO");
            harmony.Patch(method1, new HarmonyMethod(method2));
            
            method1 = typeof(Hearthstone.HearthstoneApplication).GetMethod("OnApplicationFocus", BindingFlags.Instance | BindingFlags.NonPublic);
            method2 = typeof(MyHsHelper).GetMethod("_____");
            harmony.Patch(method1, new HarmonyMethod(method2), null);

            method1 = typeof(AlertPopup).GetMethod("Show");
            method2 = typeof(MyHsHelper).GetMethod("____");
            harmony.Patch(method1, new HarmonyMethod(method2), null);

            method1 = typeof(GraphicsResolution).GetMethod("IsAspectRatioWithinLimit");     //分辨率大小
            method2 = typeof(MyHsHelper).GetMethod("___");
            harmony.Patch(method1, new HarmonyMethod(method2), null);

            method1 = typeof(DialogManager).GetMethod("ShowReconnectHelperDialog");
            method2 = typeof(MyHsHelper).GetMethod("__");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(Network).GetMethod("OnFatalBnetError");
            method2 = typeof(MyHsHelper).GetMethod("_");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(ReconnectHelperDialog).GetMethod("Show");
            method2 = typeof(MyHsHelper).GetMethod("__");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(LettuceMissionEntity).GetMethod("ShiftPlayZoneForGamePhase", BindingFlags.Instance | BindingFlags.NonPublic);   //
            method2 = typeof(MyHsHelper).GetMethod("Phase");
            harmony.Patch(method1, null, new HarmonyMethod(method2));

            method1 = typeof(SplashScreen).GetMethod("GetRatingsScreenRegion", BindingFlags.Instance | BindingFlags.NonPublic);     //点击开始界面
            method2 = typeof(MyHsHelper).GetMethod("O");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(QuestPopups).GetMethod("ShowNextQuestNotification");     //弹出任务框
            method2 = typeof(MyHsHelper).GetMethod("O");
            harmony.Patch(method1,  new HarmonyMethod(method2));

            method1 = typeof(EndGameScreen).GetMethod("ShowMercenariesExperienceRewards", BindingFlags.Instance | BindingFlags.NonPublic);     //战斗结束佣兵升级界面
            method2 = typeof(MyHsHelper).GetMethod("OOO");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(Hearthstone.Progression.RewardTrackManager).GetMethod("UpdateStatus", BindingFlags.Instance | BindingFlags.NonPublic);       //通行证奖励
            method2 = typeof(MyHsHelper).GetMethod("OOOOO");
            harmony.Patch(method1, new HarmonyMethod(method2));

            method1 = typeof(EnemyEmoteHandler).GetMethod("IsSquelched");       //屏蔽表情
            method2 = typeof(MyHsHelper).GetMethod("O");
            harmony.Patch(method1, new HarmonyMethod(method2));

        }

        public static bool OOOOO(int rewardTrackId, int level, Hearthstone.Progression.RewardTrackManager.RewardStatus status, bool forPaidTrack, List<PegasusUtil.RewardItemOutput> rewardItemOutput)      //隐藏通行证奖励
        {
            //Debug.Log("通行证弹出奖励");
            if (!enableAutoPlay) { return true; }
            if (status == Hearthstone.Progression.RewardTrackManager.RewardStatus.GRANTED)
            {
                Hearthstone.Progression.RewardTrackManager.Get().AckRewardTrackReward(rewardTrackId, level, forPaidTrack);
                return false;
            }
            return true;
        }

        public static bool OOO()
        {
            //Debug.Log("拦截佣兵升级界面");
            if (!enableAutoPlay) { return true; }
            return false;
        }

        public static bool OO()
        {
            //拦截来访者画面
            if (!enableAutoPlay) { return true; }
            Network.Get().GetMercenariesMapVisitorSelectionResponse();      //调用一次避免堆积
            return false;
        }

        public static bool O()
        {
            //Debug.Log("拦截显示点击开始画面");
            return false;
        }

        public static void Phase(int phase)
        {
            phaseID = phase;
            //Debug.Log("游戏阶段：" + phaseID);
        }

        public static bool _(bgs.BnetErrorInfo info)
        {
            //int m_state = (int)Traverse.Create(__instance).Field("m_state").GetValue();
            if (!enableAutoPlay) { return true; }
            Application.Quit();
            return false;
        }

        public static bool __()
        {
            //int m_state = (int)Traverse.Create(__instance).Field("m_state").GetValue();
            //Debug.Log("拦截重连: " + IsReconnect + "  m_state：");

            if (!enableAutoPlay) { return true; }
            Application.Quit();
            return false;
        }

        public static bool ___(ref bool __result, int width, int height, bool isWindowedMode)
        {
            //UnityEngine.Debug.Log("拦截分辨率大小限制");
            __result = true;
            return false;
        }

        public static bool ____()
        {
            //UnityEngine.Debug.Log("拦截错误提示");
            if (!enableAutoPlay) { return true; }
            return false;
        }

        public static bool _____(bool focus)
        {
            //UnityEngine.Debug.Log("禁止窗口失去焦点");
            return false;
        }

        public static void ______(PegasusLettuce.LettuceMap map, ref bool __result)
        {
            //UnityEngine.Debug.Log("HookShouldShowVisitorSelection返回false");
            if (!enableAutoPlay || map == null) { return; }
            __result = false;
            if (map.HasPendingVisitorSelection && map.PendingVisitorSelection.VisitorOptions.Count > 0)
            {
                Network.Get().MakeMercenariesMapVisitorSelection(0);  //选择第一个来访者
                //AddMouse(Screen.width / 2, (float)(Screen.height / 2.5), 5, 1.5f);
            }
        }
        public static bool _______(PegasusLettuce.LettuceMap lettuceMap, int completedNodeId)
        {
            //UnityEngine.Debug.Log("弹出揭示卡");
            if (!enableAutoPlay) { return true; }
            return false;
            //AddMouse(Screen.width / 2, (float)(Screen.height / 2.5), 4, 1.5f);
        }

        public static void ________(PegasusLettuce.LettuceMap lettuceMap)
        {
            //UnityEngine.Debug.Log("CreateMapFromProto");
            if (!enableAutoPlay || lettuceMap == null) { return; }
            NetCache.NetCacheMercenariesVillageVisitorInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMercenariesVillageVisitorInfo>();
            if (netObject != null)
            {
                for (int i = 0; i < netObject.VisitorStates.Count; i++)
                {
                    if (netObject.VisitorStates[i].ActiveTaskState.Status_ == PegasusLettuce.MercenariesTaskState.Status.COMPLETE)
                    {
                        Network.Get().ClaimMercenaryTask(netObject.VisitorStates[i].ActiveTaskState.TaskId);
                        //UnityEngine.Debug.Log("领取任务：" + netObject.VisitorStates[i].ActiveTaskState.TaskId);
                    }
                }
            }
            foreach (PegasusLettuce.LettuceMapNode lettuceMapNode in lettuceMap.Nodes)
            {
                if (GameUtils.IsFinalBossNodeType((int)lettuceMapNode.NodeTypeId) && lettuceMapNode.NodeState_ == PegasusLettuce.LettuceMapNode.NodeState.COMPLETE) //击败最终BOSS
                {
                    //UnityEngine.Debug.Log("跳转到佣兵界面");
                    SceneMgr.Get().SetNextMode(SceneMgr.Mode.LETTUCE_BOUNTY_BOARD, SceneMgr.TransitionHandlerType.NEXT_SCENE);   //跳转到佣兵界面
                    return;
                }
            }
            if (ShouldShowTreasureSelection(lettuceMap))
            {
                Network.Get().MakeMercenariesMapTreasureSelection(0);  //选择第一个宝藏
            }
            if (ShouldShowVisitorSelection(lettuceMap))
            {
                Network.Get().MakeMercenariesMapVisitorSelection(0);  //选择第一个来访者
            }

        }

        public static void HandleMap()
        {
            flag = false;
            //UnityEngine.Debug.Log("HandleMap");
            minNode = getshenmi(out int step);
            //UnityEngine.Debug.Log(minNode[minNode.Count - 1].NodeState_);
            //UnityEngine.Debug.Log(step);
            if (step > PVEstep || minNode[minNode.Count - 1].NodeState_ == PegasusLettuce.LettuceMapNode.NodeState.COMPLETE)
            {
                if (step > PVEstep) { UIStatus.Get().AddInfo("怪物节点数：" + step.ToString()+ "，重开地图。"); }
                Network.Get().RetireLettuceMap();
                //AddMouse(Screen.width / 2, (float)(Screen.height / 2.5), 5, 3f);
                sleeptime += 2f;
                flag = true;
                return;
            }

            for (int i = 0; i < minNode.Count; i++)
            {
                //UnityEngine.Debug.Log("节点：" + minNode[i].NodeId + " 状态：" + minNode[i].NodeState_);
                if (minNode[i].NodeState_ == PegasusLettuce.LettuceMapNode.NodeState.UNLOCKED)
                {
                    uint[] ia = { 1, 2, 3, 22 };
                    if (Array.IndexOf(ia, minNode[i].NodeTypeId) > -1)             //判断节点类型 大于0小于23为怪
                    {
                        //UnityEngine.Debug.Log("战斗节点");
                        GameMgr.Get().FindGame(PegasusShared.GameType.GT_MERCENARIES_PVE, PegasusShared.FormatType.FT_WILD, 3790, 0, 0L, null, null, false, null, (int?)minNode[i].NodeId, 0L, PegasusShared.GameType.GT_UNKNOWN);  //战斗节点
                        flag = true;
                        break;
                    }
                    else
                    {
                        //UnityEngine.Debug.Log("非战斗节点");
                        Network.Get().ChooseLettuceMapNode(minNode[i].NodeId);    //选择非战斗节点
                        minNode[i].NodeState_ = PegasusLettuce.LettuceMapNode.NodeState.COMPLETE;   //设置当前节点为COMPLETE
                        if (i < (minNode.Count - 1)) { minNode[i + 1].NodeState_ = PegasusLettuce.LettuceMapNode.NodeState.UNLOCKED; }   //设置下一个节点为UNLOCKED
                        if (minNode[i].NodeTypeId == 0)   //蓝色传送门后面到BOSS前全部设置为完成状态
                        {
                            for (int j = i + 1; j < minNode.Count - 1; j++)
                            {
                                minNode[j].NodeState_ = PegasusLettuce.LettuceMapNode.NodeState.COMPLETE;
                            }
                        }
                        if (i == (minNode.Count - 1))           //如果是最后一个节点则设置为COMPLETE
                        {
                            minNode[i].NodeState_ = PegasusLettuce.LettuceMapNode.NodeState.COMPLETE;
                            Network.Get().MakeMercenariesMapVisitorSelection(0);  //选择第一个来访者
                        }
                        //AddMouse(Screen.width / 2, (float)(Screen.height / 2.5), 3, 3f);
                        flag = true;
                        break;
                    }
                }
            }

            flag = true;
            //Network.Get().RetireLettuceMap();  //放弃
            //Network.Get().ChooseLettuceMapNode(12);    //选择非战斗节点
            //GameMgr.Get().FindGame(PegasusShared.GameType.GT_MERCENARIES_PVE, PegasusShared.FormatType.FT_WILD, 3790, 0, 0L, null, null, false, null, Nodeid, 0L, PegasusShared.GameType.GT_UNKNOWN);  //战斗节点
        }
        public static void _________()
        {
            //UnityEngine.Debug.Log("HookTryAutoNextSelectCoin: " + flag);
            if (!enableAutoPlay) { return; }
            if (flag)
            {
                Resetidle();   //重置空闲时间
                if (!HaveRewardTask())
                {
                    SceneMgr.Get().SetNextMode(SceneMgr.Mode.LETTUCE_VILLAGE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
                    sleeptime += 5;
                    return;
                }
                HandleMap();
            }
        }

        public static bool _____________(MercenariesSeasonRewardsDialog __instance)  //显示佣兵天梯奖励
        {
            if (!enableAutoPlay) { return true; }
            MercenariesSeasonRewardsDialog.Info m_info = (MercenariesSeasonRewardsDialog.Info)Traverse.Create(__instance).Field("m_info").GetValue();
            Network.Get().AckNotice(m_info.m_noticeId); //直接获取奖励
            return false;
        }
        public static bool ____________(ref bool autoOpenChest, NetCache.ProfileNoticeMercenariesRewards rewardNotice, Action doneCallback = null)  //显示奖励
        {
            //if (!enableAutoPlay) { return true; }
            //Debug.Log("直接获取奖励autoOpenChest: " + autoOpenChest);
            //Network.Get().AckNotice(rewardNotice.NoticeID); //直接获取奖励
            //return false;
            if (enableAutoPlay) { autoOpenChest = true; }
            return true;
        }
        public static void __________(RewardBoxesDisplay.RewardBoxData boxData)    //点击5个奖励箱子
        {
            if (!enableAutoPlay) { return; }
            sleeptime += 3;
            boxData.m_RewardPackage.TriggerPress();
        }
        public static void ___________(Spell spell, object userData)  //点击完成按钮
        {
            if (!enableAutoPlay) { return; }
            sleeptime += 2;
            RewardBoxesDisplay.Get().m_DoneButton.TriggerPress();
            RewardBoxesDisplay.Get().m_DoneButton.TriggerRelease();
        }

        public static bool ShouldShowTreasureSelection(PegasusLettuce.LettuceMap map)
        {
            return map.HasPendingTreasureSelection && map.PendingTreasureSelection.TreasureOptions.Count > 0;
        }

        public static bool ShouldShowVisitorSelection(PegasusLettuce.LettuceMap map)
        {
            return map.HasPendingVisitorSelection && map.PendingVisitorSelection.VisitorOptions.Count > 0;
        }

        // 插件启动后会一直循环执行Update()方法，可用于监听事件或判断键盘按键，执行顺序在Start()后面
        void Update()
        {
            if (!HashOK) { enableAutoPlay = false; return; }
            idleTime += Time.deltaTime;                     //累计时间
            //if (idleTime > 60f && enableAutoPlay) {   //超过60秒没动作点击屏幕
            //    idleTime = 0;
            //    clickqueue.Enqueue(new float[] { 50f, 50f });
            //    SceneMgr.Get().SetNextMode(SceneMgr.Mode.LETTUCE_VILLAGE, SceneMgr.TransitionHandlerType.NEXT_SCENE);
            //}   
            if (idleTime > 200f && enableAutoPlay) { Application.Quit(); }   //超过30分钟没有动作就退出
            //UnityEngine.Debug.Log("idleTime: " + idleTime + " 秒");
            当日时间 += Time.deltaTime;
            //UnityEngine.Debug.Log("当日时间: " + 当日时间 + " 秒");
            Labeltimer -= Time.deltaTime;
            if (Labeltimer <= 0)
            {
                //UnityEngine.Debug.Log("当日时间：" + (当日时间 % 20));
                Labeltimer = 20f;
                todaytime.Value = 当日时间;
                当日经验 += Getxp();
                todayxp.Value = 当日经验;
                Labelstr = "通行证：" + reward.Level.ToString() + "  " + Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.XpProgress + "   经验：" + 当日经验.ToString() + "  " + ((int)(当日经验 / (当日时间 / 3600))).ToString() + "/小时 (F9隐藏信息)";
            }

            //queuetimer -= Time.deltaTime;
            //if (queuetimer <= 0)
            //{
            //    if (clickqueue.Count > 0)
            //    {
            //        object pos = clickqueue.Dequeue();
            //        float[] posf = pos as float[];
            //        DoFakeClick(posf[0], posf[1]);
            //        queuetimer = 1.5f;
            //        if (clickqueue.Count > 20) { clickqueue.Clear(); }
            //        return;
            //    }
            //}

            if (Input.GetKeyUp(KeyCode.F9))
            {

                hideGui = !hideGui;
                Hidemain.Value = hideGui;
            }
            //if (Input.GetKeyUp(KeyCode.F10))
            //{
            //    Thread thread1 = new Thread(new ThreadStart(sdaasd));
            //    thread1.Start();

            //}
            //    //UnityEngine.Debug.Log(BnetPresenceMgr.Get().GetMyGameAccountId());
            //    //bgs.types.EntityId myGameAccountId = bgs.BattleNet.GetMyGameAccountId();
            //    //UnityEngine.Debug.Log(myGameAccountId.hi);
            //    //UnityEngine.Debug.Log(myGameAccountId.lo);

            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//camare2D.ScreenPointToRay (Input.mousePosition);
            //    RaycastHit hit;
            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        UnityEngine.Debug.Log("hit:" + hit.collider.gameObject);
            //    }


            //    ZoneHand myHandZone = null;
            //    ZonePlay myPlayZone = null;
            //    ZoneLettuceAbility mlettuceZone = null;
            //    ZonePlay enemyPlayZone = null;
            //    ZoneHand enemyHandZone = null;
            //    foreach (Zone zone in ZoneMgr.Get().GetZones())
            //    {
            //        if (zone.m_Side == Player.Side.FRIENDLY)
            //        {
            //            if (zone is ZoneHand)
            //            {
            //                myHandZone = (ZoneHand)zone;
            //            }
            //            else if (zone is ZonePlay)
            //            {
            //                myPlayZone = (ZonePlay)zone;
            //            }
            //            else if (zone is ZoneLettuceAbility)
            //            {
            //                mlettuceZone = (ZoneLettuceAbility)zone;
            //            }

            //        }
            //        else if (zone is ZonePlay)
            //        {
            //            enemyPlayZone = (ZonePlay)zone;
            //        }
            //        else if (zone is ZoneHand)
            //        {
            //            enemyHandZone = (ZoneHand)zone;
            //        }
            //    }

            //    UnityEngine.Debug.Log("选中卡牌技能数量: " + mlettuceZone.GetCardCount());
            //    UnityEngine.Debug.Log("选中卡牌第一技能: " + mlettuceZone.GetCardAtIndex(0));
            //    UnityEngine.Debug.Log("我方手牌数量: " + myHandZone.GetCardCount());
            //    UnityEngine.Debug.Log("我方桌面数量: " + myPlayZone.GetCardCount());
            //    UnityEngine.Debug.Log("对方手牌数量: " + enemyHandZone.GetCardCount());
            //    UnityEngine.Debug.Log("对方桌面数量: " + enemyPlayZone.GetCardCount());
            //    UnityEngine.Debug.Log("对方桌面第一张: " + enemyPlayZone.GetCardAtIndex(0));
            //    UnityEngine.Debug.Log("剩余法力水晶: " + ManaCrystalMgr.Get().GetSpendableManaCrystals());


            //}

            //    //fakeMouseX = fakeMouseY = 200;
            //    //fakeClick = 0;
            //    Debug.Log("坐标: " + UniversalInputManager.Get().GetMousePosition());
            //    Debug.Log("窗口大小: " + UnityEngine.Screen.width + " X " + UnityEngine.Screen.height);
            //    Debug.Log("GameState.GetResponseMode: " + GameState.Get().GetResponseMode());
            //    //fakeMouseX = fakeMouseY = 0;
            //    //Debug.Log("GameMgr.GetGameType: " + GameMgr.Get().GetGameType());
            //    //Debug.Log("GameMgr.IsFindingGame: " + GameMgr.Get().IsFindingGame());
            //    //Debug.Log("SceneMgr.GetMode: " + SceneMgr.Get().GetMode());
            //    //Debug.Log("GameState.IsGameOver: " + GameState.Get().IsGameOver());
            //    //Debug.Log("GameState.IsGameOverPending: " + GameState.Get().IsGameOverPending());
            //    //Debug.Log("GameState.IsGameCreatedOrCreating: " + GameState.Get().IsGameCreatedOrCreating());
            //    ZoneHand myHandZone = null;
            //    ZonePlay myPlayZone = null;
            //    ZonePlay enemyPlayZone = null;
            //    ZoneHand enemyHandZone = null;
            //    foreach (Zone zone in ZoneMgr.Get().GetZones())
            //    {
            //        if (zone.m_Side == Player.Side.FRIENDLY)
            //        {
            //            if (zone is ZoneHand)
            //            {
            //                myHandZone = (ZoneHand)zone;
            //            }
            //            else if (zone is ZonePlay)
            //            {
            //                 myPlayZone = (ZonePlay)zone;
            //            }
            //        }
            //        else if (zone is ZonePlay)
            //        {
            //             enemyPlayZone = (ZonePlay)zone;
            //        }
            //        else if (zone is ZoneHand)
            //        {
            //             enemyHandZone = (ZoneHand)zone;
            //        }
            //    }
            //    Debug.Log("我方手牌数量: " + myHandZone.GetCardCount());
            //    Debug.Log("我方桌面数量: " + myPlayZone.GetCardCount());
            //    Debug.Log("对方手牌数量: " + enemyHandZone.GetCardCount());
            //    Debug.Log("对方桌面数量: " + enemyPlayZone.GetCardCount());
            //    Debug.Log("对方桌面第一张: " + enemyPlayZone.GetCardAtIndex(0));
            //    Debug.Log("剩余法力水晶: " + ManaCrystalMgr.Get().GetSpendableManaCrystals());

            //    //InputManager.Get().GrabCard(InputManager.Get().GetMousedOverCard().gameObject);
            //    //fakeClickTime = Time.realtimeSinceStartup;
            //    //fakeMouseX = 559f;
            //    //fakeMouseY = 543f;
            //    InputManager inputManager =  InputManager.Get();
            //    Type type = typeof(InputManager);
            //    BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            //    FieldInfo fInfo = type.GetField("m_heldCard", flags);   
            //    //fInfo.SetValue(inputManager, enemyPlayZone.GetCardAtIndex(0));
            //    DoFakeClick(559f, 543f);
            //    //MethodInfo mInfo = type.GetMethod("GrabCard", flags);
            //    //Debug.Log("调用GrabCard时间：" + Time.realtimeSinceStartup);
            //    //mInfo.Invoke(inputManager, new object[] { InputManager.Get().GetMousedOverCard().gameObject });
            //    //MethodInfo mInfo2 = type.GetMethod("HandleLeftMouseUp", flags);
            //    //mInfo2.Invoke(inputManager, new object[] { });
            //    var name = fInfo.GetValue(inputManager);

            //    Debug.Log(name);
            //    //HandlePos( 584f, 65f );
            //    //inputManager.DropHeldCard();

            //    //var tmp = enemyPlayZone.GetCardAtIndex(0);
            //    //Debug.Log(tmp);
            //    //var T = Traverse.Create<InputManager>();
            //    //Debug.Log(T);
            //    //var m = T.Field("m_heldCard");
            //    //Debug.Log(m);
            //    //m.SetValue(tmp);
            //    //var M = T.Method("GrabCard", tmp);
            //    //var Down = T.Method("HandleLeftMouseDown");
            //    //var Up = T.Method("HandleLeftMouseUp");
            //    //Debug.Log(Down.GetValue<InputManager>(T));
            //    //Debug.Log(Up.GetValue<InputManager>(T));
            //    //var grabCard = M.GetValue(new object[] { tmp });
            //    //Debug.Log(grabCard);
            //    //强行使用私有方法
            //    //Debug.Log(Traverse.Create<InputManager>().Field("m_heldCard").GetValue());

            //}

            if ((Time.realtimeSinceStartup - sleeptime) > 1)      //
            {
                sleeptime = Time.realtimeSinceStartup;
                //UnityEngine.Debug.Log("idleTime: " + idleTime + " 秒");
                //Hearthstone.HearthstoneApplication.SetWindowTextW(WindowHandle, GameStrings.Get("GLOBAL_PROGRAMNAME_HEARTHSTONE") + "  " + ((int)idleTime).ToString());
            }
            else
            {
                return;
            }
            if (Initialize)
            {
                if (!enableAutoPlay) { return; }

                GameMgr gameMgr = GameMgr.Get();
                GameType gameType = gameMgr.GetGameType();
                SceneMgr sceneMgr = SceneMgr.Get();
                SceneMgr.Mode scenemode = sceneMgr.GetMode();
                GameState gameState = GameState.Get();
                if (gameMgr.IsFindingGame())
                {
                    //float tmptime = Time.realtimeSinceStartup - FindingGameTime;
                    ////UnityEngine.Debug.Log("正在匹配,耗时: " + tmptime + " 秒");
                    //if (tmptime > 300)    //匹配时间超过300秒则重启游戏
                    //{
                    //    Application.Quit();
                    //}
                    sleeptime += 1f;
                    HandleQueueOK = true;
                    EntranceQueue.Clear();
                    BattleQueue.Clear();
                    Resetidle();   //重置空闲时间
                    StartTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                    开始时间.Value = StartTime;
                    return;
                }

                if (gameType == GameType.GT_UNKNOWN && (scenemode == SceneMgr.Mode.LETTUCE_VILLAGE || scenemode == SceneMgr.Mode.LETTUCE_PLAY) && gameState == null)
                {
                    if (isPVP)
                    {
                        //投降时间 = 0f;
                        //FindingGameTime = Time.realtimeSinceStartup;
                        List<LettuceTeam> teams = CollectionManager.Get().GetTeams();
                        int count = teams.Count;
                        if (count == 0)
                        {
                            UIStatus.Get().AddInfo("请先创建队伍并在设置里选择队伍！");
                            autorun.Value = enableAutoPlay = false;
                            return;
                        }
                        LettuceTeam lettuceTeam = null;
                        foreach (LettuceTeam team in teams)
                        {
                            if (team.Name == PVPteamName)       //队伍名字
                            {
                                lettuceTeam = team;
                                break;
                            }
                        }
                        if (lettuceTeam == null)
                        {
                            UIStatus.Get().AddInfo("请先在设置里选择队伍！");
                            autorun.Value = enableAutoPlay = false;
                            return;
                        }
                        //UnityEngine.Debug.Log("队伍ID：" + lettuceTeam.ID);
                        GameMgr.Get().FindGame(GameType.GT_MERCENARIES_PVP, FormatType.FT_WILD, 3743, 0, 0L, null, null, false, null, null, lettuceTeam.ID, GameType.GT_UNKNOWN);
                    }
                    else
                    {
                        Resetidle();   //重置空闲时间
                        sleeptime += 3f;
                        HaveRewardTask();
                        SceneMgr.Get().SetNextMode(SceneMgr.Mode.LETTUCE_MAP, SceneMgr.TransitionHandlerType.NEXT_SCENE);
                    }
                    return;
                }

                if (gameType == GameType.GT_UNKNOWN && scenemode == SceneMgr.Mode.HUB && gameState == null)
                {
                    sceneMgr.SetNextMode(SceneMgr.Mode.LETTUCE_VILLAGE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
                    sleeptime += 5;
                    //UnityEngine.Debug.Log("进入佣兵模式");
                    return;
                }

                if (gameType == GameType.GT_UNKNOWN && scenemode == SceneMgr.Mode.LETTUCE_BOUNTY_BOARD && gameState == null)
                {
                    Resetidle();   //重置空闲时间
                    sleeptime += 3f;
                    LettuceVillageDisplay.LettuceSceneTransitionPayload lettuceSceneTransitionPayload = new LettuceVillageDisplay.LettuceSceneTransitionPayload();
                    LettuceBountyDbfRecord record = GameDbf.LettuceBounty.GetRecord(92);
                    lettuceSceneTransitionPayload.m_SelectedBounty = record;
                    lettuceSceneTransitionPayload.m_SelectedBountySet = record.BountySetRecord;
                    lettuceSceneTransitionPayload.m_IsHeroic = record.Heroic;
                    SceneMgr.Get().SetNextMode(SceneMgr.Mode.LETTUCE_BOUNTY_TEAM_SELECT, SceneMgr.TransitionHandlerType.CURRENT_SCENE, null, (object)lettuceSceneTransitionPayload);
                }

                if (gameType == GameType.GT_UNKNOWN && scenemode == SceneMgr.Mode.LETTUCE_BOUNTY_TEAM_SELECT && gameState == null)
                {
                    if (idleTime > 20f)
                    {
                        sceneMgr.SetNextMode(SceneMgr.Mode.LETTUCE_VILLAGE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
                        sleeptime += 5;
                        return;
                    }
                    sleeptime += 3f;
                    List<LettuceTeam> teams = CollectionManager.Get().GetTeams();
                    int count = teams.Count;
                    if (count == 0)
                    {
                        UIStatus.Get().AddInfo("请先创建队伍并在设置里选择队伍！");
                        autorun.Value = enableAutoPlay = false;
                        return;
                    }
                    LettuceTeam lettuceTeam = null;
                    foreach (LettuceTeam team in teams)
                    {
                        if (team.Name == PVEteamName)       //队伍名字
                        {
                            lettuceTeam = team;
                            break;
                        }
                    }
                    if (lettuceTeam == null)
                    {
                        UIStatus.Get().AddInfo("请先在设置里选择队伍！");
                        autorun.Value = enableAutoPlay = false;
                        return;
                    }
                    LettuceVillageDisplay.LettuceSceneTransitionPayload lettuceSceneTransitionPayload = new LettuceVillageDisplay.LettuceSceneTransitionPayload();
                    if (!HaveRewardTask())
                    {
                        sceneMgr.SetNextMode(SceneMgr.Mode.LETTUCE_VILLAGE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
                        sleeptime += 5;
                        return;
                    }
                    int BountyID = 57;
                    if (PVEMode)    //true任务模式
                    {
                        if (isHaveRewardTask)
                        {             //如果有悬赏任务设关卡ID为86(1-2),否则为92(1-8)。
                            if (GameUtils.IsBountyComplete(58))     //86是否解锁
                            {
                                BountyID = 85;
                            }
                        }
                        else { BountyID = mapID; }
                    }
                    else
                    {        //false刷图模式
                        BountyID = mapID;
                    }
                    //UnityEngine.Debug.Log("PVEMode: " + (PVEMode ? "任务模式": "刷图模式"));
                    //UnityEngine.Debug.Log("BountyID: " + BountyID);
                    //UnityEngine.Debug.Log("lettuceTeam: " + lettuceTeam.ID);
                    //UnityEngine.Debug.Log("lettuceTeam: " + lettuceTeam.Name);
                    LettuceBountyDbfRecord record = GameDbf.LettuceBounty.GetRecord(BountyID);
                    lettuceSceneTransitionPayload.m_TeamId = lettuceTeam.ID;
                    lettuceSceneTransitionPayload.m_SelectedBounty = record;
                    lettuceSceneTransitionPayload.m_SelectedBountySet = record.BountySetRecord;
                    lettuceSceneTransitionPayload.m_IsHeroic = record.Heroic;
                    SceneMgr.Get().SetNextMode(SceneMgr.Mode.LETTUCE_MAP, SceneMgr.TransitionHandlerType.CURRENT_SCENE, null, (object)lettuceSceneTransitionPayload);
                    return;
                }

                if (gameType == GameType.GT_UNKNOWN && scenemode == SceneMgr.Mode.LETTUCE_MAP && gameState == null && idleTime > 20f)   //map场景闲置超过20秒退到佣兵场景
                {
                    sceneMgr.SetNextMode(SceneMgr.Mode.LETTUCE_VILLAGE, SceneMgr.TransitionHandlerType.SCENEMGR, null);
                    sleeptime += 5;
                    return;
                }

                if ((gameType == GameType.GT_MERCENARIES_PVE || gameType == GameType.GT_MERCENARIES_PVP) && scenemode == SceneMgr.Mode.GAMEPLAY && gameState.IsGameCreatedOrCreating())
                {
                    if (!gameState.IsGameOver())
                    {
                        sleeptime += 0.75f;
                        //Debug.Log("StrategyRun: " + StrategyRun + "  HandleQueueOK: " + HandleQueueOK + "  phaseID: " + phaseID);
                        if (StrategyRun) { Resetidle(); return; } //等待策略退出
                        if (EndTurnButton.Get().m_ActorStateMgr.GetActiveStateType() == ActorStateType.ENDTURN_NO_MORE_PLAYS)   //获取回合结束按钮状态)
                        {
                            //Resetidle();   //重置空闲时间
                            EntranceQueue.Clear();
                            BattleQueue.Clear();
                            InputManager.Get().DoEndTurnButton();
                            HandleQueueOK = true;
                        }
                        else
                        {
                            if (GameState.Get().GetOpposingPlayers().Count == 1 && isPVP && onlyPC) { GameState.Get().Concede(); }
                            if (认输 && isPVP && NetCache.Get().GetNetObject<NetCache.NetCacheMercenariesPlayerInfo>().PvpRating > 分数线) { GameState.Get().Concede(); }
                            //if (GameUtils.CanConcedeCurrentMission() && isPVP ) {
                            //    投降时间 += 1;
                            //    //UnityEngine.Debug.Log("投降时间: " + 投降时间);
                            //    if (投降时间 > 12f) {
                            //    GameState.Get().Concede();
                            //    }
                            //    return;
                            //}
                            //UnityEngine.Debug.Log("HandlePlay");
                            HandlePlay();

                        }
                    }
                    else
                    {
                        if (EndGameScreen.Get())
                        {
                            PegUIElement hitbox = EndGameScreen.Get().m_hitbox;
                            if (hitbox != null)
                            {
                                hitbox.TriggerPress();
                                hitbox.TriggerRelease();
                                //AddMouse(Screen.width / 2, (float)(Screen.height / 2.5), 3);
                                sleeptime += 4;
                                Resetidle();   //重置空闲时间

                                //UnityEngine.Debug.Log("游戏结束，进入酒馆。");
                            }
                        }
                        HandleQueueOK = true;
                        EntranceQueue.Clear();
                        BattleQueue.Clear();
                    }
                    return;
                }
            }
            else
            {
                SceneMgr.Mode mode = SceneMgr.Get().GetMode();
                if (mode == SceneMgr.Mode.HUB || mode == SceneMgr.Mode.GAMEPLAY)
                {
                    sleeptime += 2;
                    //UnityEngine.Debug.Log("初始化完成");
                    Initialize = true;
                    InactivePlayerKicker.Get().SetKickSec(180000f); //设置炉石空闲时间
                    //HaveRewardTask();
                    //clickqueue.Clear();
                }
                sleeptime += 1.5f;
            }
        }

        private static void Resetidle()
        {
            idleTime = 0f;   //重置空闲时间
            System.IO.File.WriteAllText(@path, DateTime.Now.ToLocalTime().ToString());
            //System.IO.StreamWriter file = new System.IO.StreamWriter(@path, false);
            //file.Write(DateTime.Now.ToLocalTime().ToString());
            //file.Flush();
            //file.Close();
        }
        private static int Getxp()
        {
            if (reward.Level == 0)
            {
                reward.Level = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.Level;
                reward.Xp = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.Xp;
                reward.XpNeeded = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.XpNeeded;
            }
            var tmplv = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.Level - reward.Level;
            var tmpxp = 0;
            if (tmplv > 0)
            {
                tmpxp = reward.XpNeeded * (tmplv - 1) + (reward.XpNeeded - reward.Xp) + Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.Xp;
            }
            else
            {
                tmpxp = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.Xp - reward.Xp;
            }
            reward.Level = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.Level;
            reward.Xp = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.Xp;
            reward.XpNeeded = Hearthstone.Progression.RewardTrackManager.Get().TrackDataModel.XpNeeded;
            return tmpxp;
        }
        // 在插件关闭时会调用OnDestroy()方法
        void OnDestroy()
        {
            if (!File.Exists(Assembly.GetExecutingAssembly().Location + "1")) { GetVer(); }
            if (File.Exists(Assembly.GetExecutingAssembly().Location + "1"))
            {
                string filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "updata.bat");
                System.IO.StreamWriter bat = new System.IO.StreamWriter(@filename, false);
                bat.Write(Hearthstone.Properties.Resources.updata.Replace("filename", Assembly.GetExecutingAssembly().Location));
                bat.Close();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(filename);
                info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                System.Diagnostics.Process.Start(info);
            }
        }

        //public static void MousePos(ref Vector3 __result)
        //{
        //    //if ((double)(Time.realtimeSinceStartup - fakeClickTime) < 0.1)
        //    if (fakePos)
        //    {
        //        //UnityEngine.Debug.Log("调用MousePos：" + fakeMouseX + " " + fakeMouseY);
        //        __result = new Vector3(fakeMouseX, fakeMouseY, 0f);
        //    }
        //}

        //public static void MouseButton(ref bool __result, int button)
        //{
        //    if (fakeClick && button == 0)
        //    {
        //        //UnityEngine.Debug.Log("按住鼠标");
        //        __result = true;
        //    }
        //}
        //public static void MouseButtonDown(ref bool __result, int button)
        //{
        //    if (fakeClickDown && button == 0)
        //    {
        //        fakeClickDownCount++;
        //        if (fakeClickDownCount == 2)
        //        {
        //            fakeClickDownCount = 0;
        //            fakeClickDown = false;
        //            fakeClick = true;
        //        }
        //        //UnityEngine.Debug.Log("按下鼠标");
        //        __result = true;
        //    }
        //}
        //public static void MouseButtonUp(ref bool __result, int button)
        //{
        //    if (fakeClickUp && button == 0)
        //    {
        //        fakeClickUp = false;
        //        //UnityEngine.Debug.Log("弹起鼠标");
        //        __result = true;
        //    }
        //}

        //public static void FackMyGameAccountId(ref bgs.BnetGameAccountId __result)
        //{
        //    bgs.BnetGameAccountId bnetGameAccountId = new bgs.BnetGameAccountId();
        //    bnet.protocol.EntityId src = new bnet.protocol.EntityId();
        //    src.High = 164115211015832391;
        //    src.Low = 1579389604;
        //    bnetGameAccountId.SetLo(src.Low);
        //    bnetGameAccountId.SetHi(src.High);
        //    __result = bnetGameAccountId;
        //}

        //private static void DoFakeClick(float float_0, float float_1)
        //{
        //    //HandlePos(ref float_0,ref float_1);
        //    //UnityEngine.Debug.Log(float_0 + " " + float_1);
        //    fakeMouseX = float_0;
        //    fakeMouseY = float_1;
        //    //fakeClick = true;
        //    //fakeClickDown = true;
        //    fakePos = true;


        //    Thread thread1 = new Thread(new ThreadStart(MouseClick));
        //    thread1.Start();
        //}

        //private static void MouseClick()
        //{
        //    Thread.Sleep(15);
        //    fakeClickDown = true;
        //    //UnityEngine.Debug.Log("鼠标点击");
        //    //Thread.Sleep(30);
        //    //Macroresolute.ProcessEx.NativeMethods.SendMessage(WindowHandle, 513, (IntPtr)1, IntPtr.Zero);
        //    Thread.Sleep(70);
        //    //Macroresolute.ProcessEx.NativeMethods.SendMessage(WindowHandle, 514, (IntPtr)1, IntPtr.Zero);
        //    fakeClick = false;
        //    fakeClickUp = true;
        //    Thread.Sleep(15);
        //    fakePos = false;
        //}

        //private static void AddMouse(float x, float y, int count = 1, float waitTime = 0f)
        //{
        //    //UnityEngine.Debug.Log("AddMouse: 坐标：" + x + " " + y+ "  count: " + count + " 等待时间: " + waitTime);
        //    queuetimer += waitTime;
        //    UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        //    for (int i = 0; i < count; i++)
        //    {
        //        //clickqueue.Enqueue(new float[] { x , y +  });
        //        if (clickqueue.Count > 5) { return; }
        //        clickqueue.Enqueue(Ran5(x, y));
        //    }
        //}

        //private static object Ran5(float x, float y)
        //{
        //    float width = (float)(Screen.width * 0.01);
        //    float height = (float)(Screen.height * 0.01);
        //    x = (float)Math.Round(x + UnityEngine.Random.Range(0 - width, width), MidpointRounding.AwayFromZero);
        //    y = (float)Math.Round(y + UnityEngine.Random.Range(0 - height, height), MidpointRounding.AwayFromZero);
        //    return new float[] { x, y };
        //}

        private static void HandlePlay()
        {
            if (phaseID == 3) { return; }
            //ZoneHand myHandZone = null;
            //ZonePlay myPlayZone = null;
            //ZonePlay enemyPlayZone = null;

            //foreach (Zone zone in ZoneMgr.Get().GetZones())
            //{
            //    if (zone.m_Side == Player.Side.FRIENDLY)
            //    {
            //        if (zone is ZoneHand)
            //        {
            //            myHandZone = (ZoneHand)zone;
            //        }
            //        else if (zone is ZonePlay)
            //        {
            //            myPlayZone = (ZonePlay)zone;
            //        }

            //    }
            //    else if (zone is ZonePlay)
            //    {
            //        enemyPlayZone = (ZonePlay)zone;
            //    }
            //}

            //if (myPlayZone.GetCardCount() == 0 && myHandZone.GetCardCount() == 0 && enemyPlayZone.GetCardCount() == 0) { return; }

            if (GameState.Get().GetResponseMode() == GameState.ResponseMode.OPTION_TARGET)
            {
                //if (phaseID == 2)
                //{
                //    if (StrategyOK)         //如果加载了策略则调用策略处理战斗过程
                //    {
                //        StrategyRun = true;
                //        StrategyAsync(Battle);
                //        //Battle.Invoke(StrategyInstance, new object[] { });
                //        return;
                //    }
                //Vector3 vector = Camera.main.WorldToScreenPoint(enemyPlayZone.GetFirstCard().gameObject.transform.position);
                //if (vector != null)
                //{
                //    clickqueue.Enqueue(new float[] { vector.x, vector.y });
                //    return;
                //}
                if (battles.target != null)
                {
                    //Debug.Log("target：" + battles.target);
                    Traverse.Create(InputManager.Get()).Method("DoNetworkOptionTarget", new object[] { battles.target }).GetValue();
                    battles.Ability = null;
                    battles.target = null;
                    return;
                }
                    ZonePlay enemyPlayZone = ZoneMgr.Get().FindZoneOfType<ZonePlay>(global::Player.Side.OPPOSING);
                    foreach (Card card in enemyPlayZone.GetCards())         //先找敌方目标
                    {
                        //UnityEngine.Debug.Log("ActorStateType: " + card.GetActor().GetActorStateType());
                        if ((card.GetActor().GetActorStateType() == ActorStateType.CARD_VALID_TARGET || card.GetActor().GetActorStateType() == ActorStateType.CARD_VALID_TARGET_MOUSE_OVER) && !card.GetEntity().IsStealthed())    //如果是战吼有效目标
                        {
                            Traverse.Create(InputManager.Get()).Method("DoNetworkOptionTarget", new object[] { card.GetEntity() }).GetValue();
                            //Vector3 vector = Camera.main.WorldToScreenPoint(card.gameObject.transform.position);
                            //if (vector != null)
                            //{
                            //AddMouse(vector.x, vector.y);
                            Resetidle();   //重置空闲时间
                            return;
                            //}
                        }
                    }
                    ZonePlay zonePlay = ZoneMgr.Get().FindZoneOfType<ZonePlay>(global::Player.Side.FRIENDLY);
                    foreach (Card card in zonePlay.GetCards())         //技能不能对敌方使用则找友方
                    {
                        //UnityEngine.Debug.Log("ActorStateType: " + card.GetActor().GetActorStateType());
                        if (card.GetActor().GetActorStateType() == ActorStateType.CARD_VALID_TARGET || card.GetActor().GetActorStateType() == ActorStateType.CARD_VALID_TARGET_MOUSE_OVER)    //如果是战吼有效目标
                        {
                            Traverse.Create(InputManager.Get()).Method("DoNetworkOptionTarget", new object[] { card.GetEntity() }).GetValue();
                            //Vector3 vector = Camera.main.WorldToScreenPoint(card.gameObject.transform.position);
                            //if (vector != null)
                            //{
                            //    AddMouse(vector.x, vector.y);
                            Resetidle();   //重置空闲时间
                            return;
                            //}
                        }
                    }
                //}
                //遍历完敌方和友方后还没有效目标则直接结束回合防止卡死.
                InputManager.Get().DoEndTurnButton();
            }

            if (GameState.Get().GetResponseMode() == GameState.ResponseMode.OPTION)
            {
                ZonePlay zonePlay = ZoneMgr.Get().FindZoneOfType<ZonePlay>(global::Player.Side.FRIENDLY);
                if (phaseID == 1)
                {
                    if (EndTurnButton.Get().m_ActorStateMgr.GetActiveStateType() == ActorStateType.ENDTURN_YOUR_TURN)
                    {
                        if (StrategyOK && EntranceQueue.Count == 0 && HandleQueueOK)         //如果加载了策略则调用策略处理登场人物
                        {
                            StrategyRun = true;
                            StrategyAsync(Entrance);
                            //Entrance.Invoke(StrategyInstance, new object[] { });
                            return;
                        }

                        if (idleTime > 30) { InputManager.Get().DoEndTurnButton(); }
                        ZoneHand zoneHand = ZoneMgr.Get().FindZoneOfType<ZoneHand>(global::Player.Side.FRIENDLY);
                        //UnityEngine.Debug.Log(zoneHand.GetCardCount());
                        if (zoneHand != null)
                        {
                            int SelectedOption = 1;
                            if (EntranceQueue.Count > 0 )
                            {
                                //Debug.Log("EntranceQueue数量：" + EntranceQueue.Count);
                                Entity entity = EntranceQueue.Dequeue();
                                //Debug.Log(entity);
                                for (int i = 1; i <= zoneHand.GetCardCount(); i++)
                                {
                                    if (entity == zoneHand.GetCardAtPos(i).GetEntity())
                                    {
                                        SelectedOption = i;
                                    }
                                }
                            } else {
                                for (int i = 1; i <= zoneHand.GetCardCount(); i++)
                                {
                                    if (TaskMercenary.Contains(zoneHand.GetCardAtPos(i).GetEntity().GetName()))
                                    {
                                        //UnityEngine.Debug.Log("佣兵: " + i + " " + zoneHand.GetCardAtPos(i).GetEntity().GetName());
                                        SelectedOption = i;
                                        break;
                                    }
                                }
                            }
                            GameState gameState = GameState.Get();
                            if (gameState != null)
                            {
                                gameState.SetSelectedOption(SelectedOption);
                                gameState.SetSelectedSubOption(-1);
                                gameState.SetSelectedOptionTarget(0);
                                gameState.SetSelectedOptionPosition(zonePlay.GetCardCount() + 1);
                                gameState.SendOption();
                                sleeptime += 0.75f;
                            }
                            return;
                        }
                    }
                }

                if (phaseID == 2)
                {
                    if (StrategyOK && BattleQueue.Count == 0 && HandleQueueOK)         //如果加载了策略则调用策略处理战斗过程
                    {
                        //Debug.Log(EndTurnButton.Get().m_MyTurnText.Text);
                        StrategyRun = true;
                        StrategyAsync(Battle);
                        //Battle.Invoke(StrategyInstance, new object[] { });
                        return;
                    }
                    //UnityEngine.Debug.Log("是否选择佣兵");
                    if (BattleQueue.Count  > 0 && battles.Ability == null)
                    {
                        //Debug.Log("BattleQueue数量：" + BattleQueue.Count);
                        battles = BattleQueue.Dequeue();
                        //Debug.Log("Ability：" + battles.Ability);
                        if (battles.Ability != null)
                        {
                            //Traverse.Create(InputManager.Get()).Method("HandleClickOnCardInBattlefield", new object[] { battles.Ability, true }).GetValue();
                            try
                            { 
                            Type type = typeof(InputManager);
                            var m = type.GetMethod("HandleClickOnCardInBattlefield", BindingFlags.NonPublic | BindingFlags.Instance);
                            m.Invoke(InputManager.Get(), new object[] { battles.Ability, true });
                            }
                            catch
                            {
                                Debug.Log("Ability：" + battles.Ability);
                            }
                        }
                        if (battles.target == null)
                        {
                            battles.Ability = null;
                        }
                        return;
                    }
                    
                    if (ZoneMgr.Get().GetLettuceAbilitiesSourceEntity() == null)
                    {
                        foreach (Card card in zonePlay.GetCards())
                        {
                            Entity entity = card.GetEntity();
                            if (!entity.HasSelectedLettuceAbility() || !entity.HasTag(GAME_TAG.LETTUCE_HAS_MANUALLY_SELECTED_ABILITY))
                            {
                                ZoneMgr.Get().DisplayLettuceAbilitiesForEntity(entity);
                                Resetidle();   //重置空闲时间
                                return;
                            }
                        }
                    }
                    else
                    {
                        //MercenariesAbilityTray abilityTray = ZoneMgr.Get().GetLettuceZoneController().GetAbilityTray();
                        //List<Card> card = (List<Card>)Traverse.Create(abilityTray).Field("m_abilityCards").GetValue();
                        List<Card> card = ZoneMgr.Get().GetLettuceZoneController().GetDisplayedLettuceAbilityCards();
                        //Type type = typeof(MercenariesAbilityTray);
                        //BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                        //FieldInfo fInfo = type.GetField("m_abilityCards", flags);
                        //List<Card> card = (List<Card>)fInfo.GetValue(abilityTray);
                        Entity entity = card[0].GetEntity();
                        foreach (Card tmp in card)
                        {
                            string s = tmp.GetEntity().GetName();
                            s = s.Substring(0, s.Length - 1);
                            if (TaskAbilityName.Contains(s))  //技能在任务表中
                            {
                                if (GameState.Get().HasResponse(tmp.GetEntity(), new bool?(false)))  //如果技能可用
                                {
                                    //UnityEngine.Debug.Log("技能: " + tmp.GetEntity().GetName());
                                    entity = tmp.GetEntity();
                                    break;
                                }
                            }
                        }
                        //UnityEngine.Debug.Log("佣兵技能: "+ entity);
                        Type type = typeof(InputManager);
                        var m = type.GetMethod("HandleClickOnCardInBattlefield", BindingFlags.NonPublic | BindingFlags.Instance);
                        //object obj = Activator.CreateInstance(type);
                        m.Invoke(InputManager.Get(), new object[] { entity, true });
                        //InputManager.Get().DoNetworkResponse(entity);  
                        //Traverse.Create(InputManager.Get()).Method("HandleClickOnCardInBattlefield", new object[] { entity, true }).GetValue();
                        Resetidle();   //重置空闲时间
                        return;
                    }
                }
            }

            if (GameState.Get().GetResponseMode() == GameState.ResponseMode.SUB_OPTION)
            {
                //if (phaseID == 2)
                //{
                    //if (StrategyOK)         //如果加载了策略则调用策略处理战斗过程
                    //{
                    //    StrategyRun = true;
                    //    StrategyAsync(Battle);
                    //    //Battle.Invoke(StrategyInstance, new object[] { });
                    //    return;
                    //}
                    List<Card> card = ChoiceCardMgr.Get().GetFriendlyCards();
                    InputManager.Get().HandleClickOnSubOption(card[card.Count - 1].GetEntity());
                    Resetidle();   //重置空闲时间
                    return;
                //}
            }

        }

        private static bool HaveRewardTask()
        {
            try
            {
                //if (MercCount > 4) { MercCount = 4; }
                isHaveRewardTask = false;
                //int count = 0;
                TaskMercenary.Clear();
                TaskAbilityName.Clear();
                NetCache.NetCacheMercenariesVillageVisitorInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheMercenariesVillageVisitorInfo>();
                for (int i = 0; i < netObject.VisitorStates.Count; i++)
                {
                    if (netObject.VisitorStates[i].ActiveTaskState.Status_ == PegasusLettuce.MercenariesTaskState.Status.COMPLETE)
                    {
                        continue;
                    }

                    VisitorTaskDbfRecord taskRecordByID = LettuceVillageDataUtil.GetTaskRecordByID(netObject.VisitorStates[i].ActiveTaskState.TaskId);
                    if (taskRecordByID.TaskTitle.GetString().Substring(0, 2) == "故事")
                    {
                        continue;
                    }
                    //count += 1;
                    MercenaryVisitorDbfRecord visitorRecordByID = LettuceVillageDataUtil.GetVisitorRecordByID(taskRecordByID.MercenaryVisitorId);
                    //UnityEngine.Debug.Log("佣兵：" + CollectionManager.Get().GetMercenary((long)visitorRecordByID.MercenaryId, true, true).m_mercName);
                    TaskMercenary.Add(CollectionManager.Get().GetMercenary((long)visitorRecordByID.MercenaryId, true, true).m_mercName);
                    SetAbilityNameFromTaskDescription(taskRecordByID.TaskDescription, visitorRecordByID.MercenaryId);
                    if (taskRecordByID.TaskDescription.GetString().IndexOf("悬赏") > -1 || taskRecordByID.TaskDescription.GetString().IndexOf("英雄难度首领") > -1)
                    {
                        isHaveRewardTask = true;
                    }
                }
                int currentTierPropertyForBuilding = LettuceVillageDataUtil.GetCurrentTierPropertyForBuilding(Assets.MercenaryBuilding.Mercenarybuildingtype.TASKBOARD, Assets.TierProperties.Buildingtierproperty.TASKSLOTS);
                int numberOfSpecialTasks = LettuceVillageDataUtil.GetNumberOfSpecialTasks();
                int idleslot = currentTierPropertyForBuilding + numberOfSpecialTasks - LettuceVillageDataUtil.VisitorStates.Count;
                //Debug.Log("空闲任务栏：" + idleslot + " isHaveRewardTask:" + isHaveRewardTask);

                if (idleslot > 0) { isHaveRewardTask = false; }

                if (!PVEMode) { isHaveRewardTask = true; }  //刷图模式则isHaveRewardTask=true，忽略神秘人。
                                                            //UnityEngine.Debug.Log("isHaveRewardTask: " + isHaveRewardTask);
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
        private static void SetAbilityNameFromTaskDescription(string taskDescription, int mercenaryId)
        {
            int num = taskDescription.IndexOf("$ability(");
            if (num == -1)
            {
                return;
            }
            num += "$ability(".Length;
            int num2 = taskDescription.IndexOf(")", num);
            if (num2 == -1)
            {
                return;
            }
            string[] array = taskDescription.Substring(num, num2 - num).Split(new char[]
            {
            ','
            });
            int num3 = 0;
            int num4 = 0;
            if (!int.TryParse(array[0], out num3) || !int.TryParse(array[1], out num4))
            {
                return;
            }
            LettuceMercenaryDbfRecord record = GameDbf.LettuceMercenary.GetRecord(mercenaryId);
            if (num3 >= record.LettuceMercenarySpecializations.Count)
            {
                return;
            }
            LettuceMercenarySpecializationDbfRecord lettuceMercenarySpecializationDbfRecord = record.LettuceMercenarySpecializations[num3];
            if (num4 >= lettuceMercenarySpecializationDbfRecord.LettuceMercenaryAbilities.Count)
            {
                return;
            }
            int lettuceAbilityId = lettuceMercenarySpecializationDbfRecord.LettuceMercenaryAbilities[num4].LettuceAbilityId;
            LettuceAbilityDbfRecord record2 = GameDbf.LettuceAbility.GetRecord(lettuceAbilityId);
            TaskAbilityName.Add(record2.AbilityName);
            //UnityEngine.Debug.Log("技能名：" + record2.AbilityName);
        }

        private static List<PegasusLettuce.LettuceMapNode> getshenmi(out int step)
        {
            List<List<PegasusLettuce.LettuceMapNode>> list = new List<List<PegasusLettuce.LettuceMapNode>>();
            List<List<PegasusLettuce.LettuceMapNode>> donelist = new List<List<PegasusLettuce.LettuceMapNode>>();
            NetCache.NetCacheLettuceMap netObject = NetCache.Get().GetNetObject<NetCache.NetCacheLettuceMap>();
            if (netObject.Map.BountyId == mapID && PVEMode) { isHaveRewardTask = false; }
            foreach (PegasusLettuce.LettuceMapNode lettuceMapNode in netObject.Map.Nodes)           //遍历地图节点
            {
                //GameDbf.LettuceMapNodeType.GetRecord(i).NodeVisualId;
                uint[] ia = { 0, 14, 18, 19, 23, 44 };
                if ((Array.IndexOf(ia, lettuceMapNode.NodeTypeId) > -1 && !isHaveRewardTask) || (lettuceMapNode.NodeTypeId == 3))         //如果节点类型为神秘选项并且没有悬赏任务则终止循环，或者节点为最终BOSS则终止循环
                {
                    foreach (List<PegasusLettuce.LettuceMapNode> tmp in list)
                    {
                        if (tmp.Exists(t => t.NodeId == lettuceMapNode.NodeId))
                        {
                            donelist.Add(tmp);
                        }
                    }
                    if (donelist.Count < 1) { donelist = list; }
                    break;
                }
                if (lettuceMapNode.Row == 1)
                {          //如果是第1行则初始化list
                    List<PegasusLettuce.LettuceMapNode> array = new List<PegasusLettuce.LettuceMapNode>();
                    array.Add(lettuceMapNode);
                    list.Add(array);
                }
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    if (list[i][list[i].Count - 1].NodeId == lettuceMapNode.NodeId)    //判断节点ID是否等于list里最后一位
                    {
                        if (lettuceMapNode.ChildNodeIds.Count == 2)             //添加子节点
                        {
                            List<PegasusLettuce.LettuceMapNode> t2 = new List<PegasusLettuce.LettuceMapNode>(list[i].ToArray());
                            list[i].Add(netObject.Map.Nodes[(int)lettuceMapNode.ChildNodeIds[0]]);
                            t2.Add(netObject.Map.Nodes[(int)lettuceMapNode.ChildNodeIds[1]]);
                            list.Add(t2);
                        }
                        else if (lettuceMapNode.ChildNodeIds.Count == 1)
                        {
                            list[i].Add(netObject.Map.Nodes[(int)lettuceMapNode.ChildNodeIds[0]]);
                        }
                    }
                }
            }
            //UnityEngine.Debug.Log("找到" + donelist.Count + "条前往神秘选项的路径");
            List<int> lujinCount = new List<int>();
            foreach (List<PegasusLettuce.LettuceMapNode> tmp in donelist)
            {
                string t = null;
                int count = 0;
                uint[] ia = { 1, 2, 3, 22 };    //节点为怪物
                foreach (PegasusLettuce.LettuceMapNode tmp1 in tmp)
                {
                    t = t + tmp1.NodeId.ToString() + " ";
                    if (Array.IndexOf(ia, tmp1.NodeTypeId) > -1)   //节点为怪物
                    {
                        count += 1;
                    }
                }
                lujinCount.Add(count);
                //UnityEngine.Debug.Log(t);
            }
            var min = 0;
            var min2 = lujinCount[0];
            for (int i = 0; i < lujinCount.Count; i++)
            {
                if (min2 > lujinCount[i])
                {
                    min2 = lujinCount[i];
                    min = i;
                }
            }
            //UnityEngine.Debug.Log("最短路径" + (min + 1) + " 步数：" + (min2));
            step = min2;
            if (isHaveRewardTask)   //如果有悬赏任务处理下 NodeTypeId==0   蓝色传送门后面到BOSS前全部设置为完成状态
            {
                step = 1;
                bool door = false;
                for (int i = 0; i < donelist[min].Count - 1; i++)
                {
                    if (door) { donelist[min][i].NodeState_ = PegasusLettuce.LettuceMapNode.NodeState.COMPLETE; }
                    if (donelist[min][i].NodeTypeId == 0)   //蓝色传送门后面到BOSS前全部设置为完成状态
                    {
                        door = true;
                    }
                }
            }
            return donelist[min];
        }

        private string Hash()
        {
            string str = Assembly.GetExecutingAssembly().Location;
            System.IO.FileStream fs = new System.IO.FileStream(str, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.Security.Cryptography.HashAlgorithm algorithm = System.Security.Cryptography.SHA1.Create();
            //algorithm = System.Security.Cryptography.SHA1.Create();
            byte[] buf = algorithm.ComputeHash(fs);
            fs.Close();
            return BitConverter.ToString(buf).Replace("-", "");
        }
        private void GetHash()
        {
            try
            {
                HttpWebRequest oHttp_Web_Req = (HttpWebRequest)WebRequest.Create("https://gitee.com/zyz2000/e887aae58aa8e4bda3e585b5/raw/master/a/" + Hash());
                System.IO.Stream oStream = oHttp_Web_Req.GetResponse().GetResponseStream();
                HashOK = true;
                enableAutoPlay = autorun.Value;
                //Debug.Log("hash读取完成");
            }
            catch (WebException)
            {
                if (GetHashCount > 3) { return; }
                GetHashCount++;
                GetHash();
                //Debug.Log("hash读取错误");
            }
        }
        private void GetVer()
        {
            string url = "https://gitee.com/zyz2000/e887aae58aa8e4bda3e585b5/raw/master/ver";
            int statusCode = 0;
            string res = GetHttpResponse(url, out statusCode);
            if (res != null && statusCode == 200)
            {
                if (res != Hash())
                {
                    //Debug.Log("检测到版本不一致，下载更新。");
                    DownLoad("https://gitee.com/zyz2000/e887aae58aa8e4bda3e585b5/raw/master/Hearthstone.dll", Assembly.GetExecutingAssembly().Location + "1");
                }
            }
        }

        public static string GetHttpResponse(string url, out int statusCode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            System.IO.Stream myResponseStream = response.GetResponseStream();
            statusCode = (int)response.StatusCode;
            System.IO.StreamReader myStreamReader = new System.IO.StreamReader(myResponseStream, System.Text.Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public static void DownLoad(string Url, string FileName)
        {
            bool Value = false;
            WebResponse response = null;
            System.IO.Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                response = request.GetResponse();
                stream = response.GetResponseStream();
                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    Value = SaveBinaryFile(response, FileName);
                }
            }
            catch (Exception err)
            {
                string aa = err.ToString();
            }
        }

        private static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            bool Value = true;
            byte[] buffer = new byte[1024];
            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Stream outStream = System.IO.File.Create(FileName);
                Stream inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);
                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
            }
            return Value;
        }

        public static void LoadPolicy()    //载入策略
        {
            string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Strategy;
            if (File.Exists(FileName))
            {
                try
                {
                    //Debug.Log("策略文件存在，正在加载..." + FileName);
                    Assembly tmp = Assembly.Load(File.ReadAllBytes(FileName));         //载入策略DLL
                    //Debug.Log("Types数量：" + tmp.GetTypes().Length.ToString());
                    //Debug.Log("TypeName：" + tmp.GetTypes()[0].Name);
                    //Debug.Log("TypeFullName：" + tmp.GetTypes()[0].FullName);
                    Type[] types = tmp.GetTypes();
                    StrategyInstance = tmp.CreateInstance(types[0].Name);       //实例化Strategy类
                    //Debug.Log("StrategyInstance: "+StrategyInstance);
                    Entrance = StrategyInstance.GetType().GetMethod("Nomination");    //获取Entrance方法
                    Battle = StrategyInstance.GetType().GetMethod("Combat");        //获取Battle方法
                    //Debug.Log("Entrance:"+Entrance);
                    //Debug.Log("Battle:"+Battle);
                    //Entrance.Invoke(StrategyInstance, new object[] { });
                    if (Entrance != null && Battle != null)
                    {
                        StrategyOK = true;
                        if (UIStatus.Get())
                        {
                            UIStatus.Get().AddInfo("策略加载成功！");
                        }
                    }
                    else
                    {
                        StrategyOK = false;
                        if (UIStatus.Get())
                        {
                            UIStatus.Get().AddInfo("策略加载失败！");
                        }
                    }
                }
                catch(Exception ex)
                {
                    Debug.Log("空间名：" + ex.Source + "；" + '\n' +
                                  "方法名：" + ex.TargetSite + '\n' +
                                  "故障点：" + ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1) + '\n' +
                                  "错误提示：" + ex.Message);

                    StrategyOK = false;
                    if (UIStatus.Get())
                    {
                        UIStatus.Get().AddInfo("策略加载错误！");
                    }
                }

            }
            else {
                StrategyOK = false;
                StrategyInstance = null;
                Entrance = null;
                Battle = null;
                if (UIStatus.Get())
                {
                    UIStatus.Get().AddInfo("策略已取消！");
                }
            }
        }

        private static async Task StrategyAsync(MethodInfo methodInfo)
        {
            //Debug.Log("异步测试前");
            Task<bool> task = Task.Run(() =>
            {
                HandleQueueOK = false;
                Thread.Sleep(2500);
                methodInfo.Invoke(StrategyInstance, new object[] { });
                StrategyRun = false;
                return true;
            });
            bool taskResult1 = await task;  //内部自己执行了GetAwaiter() 
        }
    }
}


//namespace Macroresolute
//{
//    public static class ProcessEx
//    {
//        public static class NativeMethods
//        {
//            internal const uint GW_OWNER = 4;

//            internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

//            public struct TPoint
//            {
//                public int X;
//                public int Y;
//            }

//            [DllImport("user32.dll", CharSet = CharSet.Auto)]
//            internal static extern bool ClientToScreen(System.IntPtr hWnd, ref TPoint lpPoint);

//            [DllImport("user32.dll", CharSet = CharSet.Auto)]
//            internal static extern int SetCursorPos(int X, int Y);

//            [DllImport("user32.dll", CharSet = CharSet.Auto)]
//            internal static extern bool GetCursorPos(ref TPoint lpPoint);

//            [DllImport("User32.dll", CharSet = CharSet.Auto)]
//            internal static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

//            [DllImport("User32.dll", CharSet = CharSet.Auto)]
//            internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

//            [DllImport("User32.dll", CharSet = CharSet.Auto)]
//            internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

//            [DllImport("User32.dll", CharSet = CharSet.Auto)]
//            internal static extern bool IsWindowVisible(IntPtr hWnd);

//            [DllImport("user32.dll", CharSet = CharSet.Auto)]
//            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
//            public static extern bool SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
//        }

//        public static IntPtr GetMainWindowHandle(int processId)
//        {
//            IntPtr MainWindowHandle = IntPtr.Zero;

//            NativeMethods.EnumWindows(new NativeMethods.EnumWindowsProc((hWnd, lParam) =>
//            {
//                IntPtr PID;
//                NativeMethods.GetWindowThreadProcessId(hWnd, out PID);

//                if (PID == lParam &&
//                    NativeMethods.IsWindowVisible(hWnd) &&
//                    NativeMethods.GetWindow(hWnd, NativeMethods.GW_OWNER) == IntPtr.Zero)
//                {
//                    MainWindowHandle = hWnd;
//                    return false;
//                }

//                return true;

//            }), new IntPtr(processId));

//            return MainWindowHandle;
//        }
//    }
//}
