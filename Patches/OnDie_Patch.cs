using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lilys_Custom_Costs
{
    [HarmonyPatch]
    public class OnDie_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Die))]
        public static void CanPlay(ref PlayableCard __instance)
        {
            if (__instance.OpponentCard)
            {
                Soul_Cost.AddSouls(1);
            }
        }
    }
}
