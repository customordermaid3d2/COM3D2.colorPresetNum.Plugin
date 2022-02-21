using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace COM3D2.colorPresetNum.Plugin
{
    class MyAttribute
    {
        public const string PLAGIN_NAME = "colorPresetNum";
        public const string PLAGIN_VERSION = "22.2.22";
        public const string PLAGIN_FULL_NAME = "COM3D2.colorPresetNum.Plugin";
    }

    [BepInPlugin(MyAttribute.PLAGIN_FULL_NAME, MyAttribute.PLAGIN_NAME, MyAttribute.PLAGIN_VERSION)]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    //[BepInPlugin("COM3D2.Sample.Plugin", "COM3D2.Sample.Plugin", "21.6.6")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    //[BepInProcess("COM3D2x64.exe")]
    public class ColorPresetNum : BaseUnityPlugin
    {
        // 단축키 설정파일로 연동
        //private ConfigEntry<BepInEx.Configuration.KeyboardShortcut> ShowCounter;

        Harmony harmony;

        //public static Sample sample;

        //public static MyLog myLog = new MyLog(MyAttribute.PLAGIN_NAME);

        public static ManualLogSource myLog;

        /*
        public Sample()
        {
            sample = this;
        }
        */
        /// <summary>
        ///  게임 실행시 한번만 실행됨
        /// </summary>
        public void Awake()
        {
            myLog = Logger;
            Logger.LogMessage("Awake "+Get_BuildDateTime(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version));
            ColorPresetNumPatch.init(Config);
            harmony = Harmony.CreateAndPatchAll(typeof(ColorPresetNumPatch));
            // 단축키 기본값 설정
            //ShowCounter = Config.Bind("KeyboardShortcut", "KeyboardShortcut0", new BepInEx.Configuration.KeyboardShortcut(KeyCode.Alpha9, KeyCode.LeftControl));

            //SampleConfig.Install(Sample.myLog.log);

            // 기어 메뉴 추가. 이 플러그인 기능 자체를 멈추려면 enabled 를 꺽어야함. 그러면 OnEnable(), OnDisable() 이 작동함
        }

        public static System.DateTime Get_BuildDateTime(System.Version version = null)
        {
            // 주.부.빌드.수정
            // 주 버전    Major Number
            // 부 버전    Minor Number
            // 빌드 번호  Build Number
            // 수정 버전  Revision NUmber

            //매개 변수가 존재할 경우
            if (version == null)
                version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            System.DateTime dtBuild = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);

            return dtBuild;
        }
        /*

        public void OnEnable()
        {
            Sample.myLog.LogMessage("OnEnable");

            //SceneManager.sceneLoaded += this.OnSceneLoaded;

            // 하모니 패치
            harmony = Harmony.CreateAndPatchAll(typeof(SamplePatch));

        }

        /// <summary>
        /// 게임 실행시 한번만 실행됨
        /// </summary>
        public void Start()
        {
            Sample.myLog.LogMessage("Start");

            //SampleGUI.Install(gameObject, Config);

            SampleGUI2.Install<SampleGUI2>(gameObject, Config, MyAttribute.PLAGIN_NAME, "SP", COM3D2.colorPresetNum.Plugin.Properties.Resources.icon, new BepInEx.Configuration.KeyboardShortcut(KeyCode.Alpha9, KeyCode.LeftControl));

            //SystemShortcutAPI.AddButton(MyAttribute.PLAGIN_FULL_NAME, new Action(delegate () { enabled = !enabled; }), MyAttribute.PLAGIN_NAME, MyUtill.ExtractResource(BepInPluginSample.Properties.Resources.icon));
        }
        /*
        public string scene_name = string.Empty;

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Sample.myLog.LogMessage("OnSceneLoaded", scene.name, scene.buildIndex);
            //  scene.buildIndex 는 쓰지 말자 제발
            scene_name = scene.name;
        }
        */
        /*
        public void FixedUpdate()
        {

        }
        */
        /*
        public void Update()
        {
            //if (ShowCounter.Value.IsDown())
            //{
            //    Sample.myLog.LogMessage("IsDown", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            //if (ShowCounter.Value.IsPressed())
            //{
            //    Sample.myLog.LogMessage("IsPressed", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            //if (ShowCounter.Value.IsUp())
            //{
            //    Sample.myLog.LogMessage("IsUp", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
        }
        */
        /*
        public void LateUpdate()
        {

        }
        */

        /*
        public void OnGUI()
        {
          
        }
        */

        /*
        public void OnDisable()
        {
            Sample.myLog.LogMessage("OnDisable");

            //SceneManager.sceneLoaded -= this.OnSceneLoaded;

            harmony.UnpatchSelf();// ==harmony.UnpatchAll(harmony.Id);
            //harmony.UnpatchAll(); // 정대 사용 금지. 다름 플러그인이 패치한것까지 다 풀려버림
        }
        /*
        public void Pause()
        {
            Sample.myLog.LogMessage("Pause");
        }

        public void Resume()
        {
            Sample.myLog.LogMessage("Resume");
        }
        */
        /*
        /// <summary>
        /// 게임 X 버튼 눌렀을때 반응
        /// </summary>
        public void OnApplicationQuit()
        {
            Sample.myLog.LogMessage("OnApplicationQuit");
        }
        */
    }
}
