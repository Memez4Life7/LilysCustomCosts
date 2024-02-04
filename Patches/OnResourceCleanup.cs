using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lilys_Custom_Costs
{
    [HarmonyPatch]
    public class OnResourceCleanup_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Part1ResourcesManager), nameof(Part1ResourcesManager.CleanUp))]
        public static void CleanUp()
        {
            Singleton<Part1ResourcesManager>.Instance.StartCoroutine(Soul_Cost.RemoveSouls(Soul_Cost.Souls));
            Soul_Cost.Souls = 0;
        }
    }
}
