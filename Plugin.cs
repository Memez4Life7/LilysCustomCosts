using APIPlugin;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static Lilys_Custom_Costs.Soul_Cost;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Lilys_Custom_Costs
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "Lily.LCC";
        private const string PluginName = "Lily's Custom Costs";
        private const string PluginVersion = "1.0.0";

        public static string Directory;
        internal static ManualLogSource Log;

        public static GameObject SoulTokenPrefab;

        public static List<BoneTokenInteractable> AllTokens
        {
            get
            {
                List<BoneTokenInteractable> alltokens = new List<BoneTokenInteractable>();
                alltokens.AddRange(Singleton<Part1ResourcesManager>.Instance.boneTokens);
                alltokens.AddRange(Soul_Cost.soulTokens);
                return alltokens;
            }
        }

        public void Awake()
        {
            base.Logger.LogInfo("Loaded Lily's Custom Costs!");
            Plugin.Log = base.Logger;

            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll();

            Directory = base.Info.Location;

            if (AssetBundleHelper.TryGet<GameObject>(Path.Combine(Path.GetDirectoryName(Directory), "custom_costs"),
                "soul_coin", out GameObject prefab))
            {
                SoulTokenPrefab = prefab;
            }

            AddSoulCost();

            //AddDevStuff();
        }

        public void AddDevStuff()
        {
            CardInfo Squirrel = CardManager.BaseGameCards.CardByName("Squirrel");
            //Squirrel.SetBloodCost(2);
            //Squirrel.bonesCost = 3;
            Squirrel.SetExtendedProperty("SoulCost", 3);
            Squirrel.baseHealth = 99;
            Squirrel.baseAttack = 1;

            CardInfo Geck = CardManager.BaseGameCards.CardByName("Geck");
            Geck.AddAbilities(Ability.Deathtouch);
            Geck.baseHealth = 99;
            Geck.baseAttack = 1;
        }

        public static Texture2D GetTexture(string path)
        {
            byte[] imgBytes = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Directory), "Artwork/", $"{path}.png"));
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);
            tex.filterMode = FilterMode.Point;
            return tex;
        }
    }
}