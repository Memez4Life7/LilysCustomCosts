using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using static Lilys_Custom_Costs.Utils;

namespace Lilys_Custom_Costs
{
    [HarmonyPatch]
    public class HintsHandler_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(HintsHandler), nameof(HintsHandler.OnNonplayableCardClicked))]
        public static bool OnNonplayableCardClicked(ref PlayableCard card)
        {
            if (Soul_Cost.Souls < card.Info.SoulCost())
            {
                ShowLeshyMessage("You don't have enough [c:bB]souls [c:]to play that creature.");
                return false;
            }
            return true;
        }
    }
}
