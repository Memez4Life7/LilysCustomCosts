using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lilys_Custom_Costs
{
    [HarmonyPatch]
    public class CanPlay_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.CanPlay))]
        public static void CanPlay(ref bool __result, ref PlayableCard __instance)
        {
            if (Soul_Cost.Souls < __instance.Info.SoulCost())
            {
                __result = false;
            }
        }
    }
}
