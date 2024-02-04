using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lilys_Custom_Costs
{
    [HarmonyPatch]
    public class OnPlay_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerHand), nameof(PlayerHand.PlayCardOnSlot))]
        public static void PlayCardOnSlot(PlayerHand __instance, PlayableCard card, CardSlot slot)
        {
            if (card.Info.SoulCost() > 0)
            {
                Soul_Cost.SpendSouls(card.Info.SoulCost());
            }
        }
    }
}
