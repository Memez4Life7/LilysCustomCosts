using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lilys_Custom_Costs
{
    [HarmonyPatch]
    public class CardCanBePlayedByTurn2_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Deck), nameof(Deck.CardCanBePlayedByTurn2WithHand))]
        public static void CardCanBePlayedByTurn2WithHand(ref CardInfo card, ref bool __result)
        {
            if (card.SoulCost() > 0)
            {
                __result = false;
                return;
            }
        }
    }
}
