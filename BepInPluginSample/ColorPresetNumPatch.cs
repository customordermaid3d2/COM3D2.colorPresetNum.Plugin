using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.colorPresetNum.Plugin
{
    class ColorPresetNumPatch
    {
        static ConfigFile config;

        static ConfigEntry<int> colorPresetNum;
        static ConfigEntry<float> cellWidth;
        static ConfigEntry<float> cellHeight;
        static ConfigEntry<int> maxPerLine;
        private static bool isAplly;

        public static void init(ConfigFile config)
        {
            ColorPresetNumPatch.config = config;
        }

        [HarmonyPatch(typeof(ColorPresetItem), "Awake")]
        [HarmonyPrefix]// CharacterMgr의 SetActive가 실행 후에 아래 메소드 작동
        public static void ColorPresetItem_Awake(Texture2D ___greyTex,Texture2D ___emptyTex , int ___slotNo_
            , ref byte[] ___grayTexArray
            , ref Texture2D ___colorChangeTex
            ) {

            ColorPresetNum.myLog.LogMessage("ColorPresetItem.Awake", ___slotNo_, ___greyTex.width, ___greyTex.height, ___emptyTex.width, ___emptyTex.height);

            if (!isAplly )
            {
                ColorPresetNum.myLog.LogMessage(___greyTex.Resize((int)cellWidth.Value, (int)cellHeight.Value));
                ColorPresetNum.myLog.LogMessage(___emptyTex.Resize((int)cellWidth.Value, (int)cellHeight.Value));
                ___greyTex.Apply();
                ___emptyTex.Apply();
                isAplly = true;
            }
            
            //grayTexArray = UTY.GetPixelArray(___greyTex);
            //___colorChangeTex = new Texture2D((int)cellWidth.Value, (int)cellWidth.Value, TextureFormat.RGBA32, false, true);
            //___greyTex.width = (int)cellWidth.Value; // 미구현되서 사용 불가
            //___greyTex.height = (int)cellHeight.Value;
        }
                
        [HarmonyPatch(typeof(ColorPresetItem), "OnButtonClick")]
        [HarmonyPrefix]// CharacterMgr의 SetActive가 실행 후에 아래 메소드 작동
        public static void OnButtonClick(int ___slotNo_) {
            ColorPresetNum.myLog.LogMessage("ColorPresetItem.OnButtonClick", ___slotNo_);
        }

        [HarmonyPatch(typeof(ColorPaletteManager), "Awake")]
        [HarmonyPrefix]// CharacterMgr의 SetActive가 실행 후에 아래 메소드 작동
        public static void ColorPaletteManager_Awake(ref int ___colorPresetNum, UIGrid ___colorPresetGrid)
        {
            if (___colorPresetGrid != null)
            {
                cellWidth = config.Bind("ColorPaletteManager", "cellWidth", ___colorPresetGrid.cellWidth / 2);
                cellHeight = config.Bind("ColorPaletteManager", "cellHeight", ___colorPresetGrid.cellHeight / 2);
                maxPerLine = config.Bind("ColorPaletteManager", "maxPerLine", ___colorPresetGrid.maxPerLine * 2);

                ColorPresetNum.myLog.LogMessage("ColorPaletteManager.Awake"
                    , ___colorPresetGrid.cellWidth, cellWidth.Value
                    , ___colorPresetGrid.cellHeight, cellHeight.Value
                    , ___colorPresetGrid.maxPerLine, maxPerLine.Value
                    );

                ___colorPresetGrid.cellWidth = cellWidth.Value;
                ___colorPresetGrid.cellHeight = cellHeight.Value;
                ___colorPresetGrid.maxPerLine = maxPerLine.Value;
            }
            else
            {
                ColorPresetNum.myLog.LogFatal("ColorPaletteManager.Awake", "___colorPresetGrid null");
            }

            colorPresetNum = config.Bind("ColorPaletteManager", "colorPresetNum", ___colorPresetNum * 4);

            ColorPresetNum.myLog.LogMessage("ColorPaletteManager.Awake", ___colorPresetNum, colorPresetNum.Value);
            ___colorPresetNum = colorPresetNum.Value;

        }

    }
}
