using DiskCardGame;
using HarmonyLib;
using Pixelplacement;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Lilys_Custom_Costs
{
    [HarmonyPatch]
    public class TryOrganizeTokens_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Part1ResourcesManager), nameof(Part1ResourcesManager.TryOrganizeBoneTokens))]
        public static bool TryOrganizeBoneTokens(ref bool __result, ref Part1ResourcesManager __instance)
        {
            if (Plugin.AllTokens == Singleton<Part1ResourcesManager>.Instance.boneTokens)
            {
                return true;
            }

            int num = 5;
            if (!__instance.isOrganized && Plugin.AllTokens.Count > num)
            {
                while (num * __instance.pileMarkers.Length < Plugin.AllTokens.Count)
                {
                    num += 5;
                }
                for (int i = 0; i < Plugin.AllTokens.Count; i++)
                {
                    int num2 = Mathf.FloorToInt((float)i / (float)num);
                    int num3 = i % num;
                    Vector3 endValue = __instance.pileMarkers[num2].position + Vector3.up * (float)num3 * 0.1f;
                    BoneTokenInteractable token = Plugin.AllTokens[i];
                    token.GetComponent<Collider>().enabled = false;
                    if (num3 < num - 1 && i < Plugin.AllTokens.Count - 1)
                    {
                        token.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    Tween.Rotation(token.transform, new Vector3(-90f, 0f, 0f), 0.1f, 0f, Tween.EaseInOut, Tween.LoopType.None, null, null, true);
                    Tween.Position(token.transform, endValue, 0.15f, 0f, Tween.EaseInOut, Tween.LoopType.None, null, delegate ()
                    {
                        token.GetComponent<Collider>().enabled = true;
                    }, true);
                }
                __instance.isOrganized = true;
                __result = true;
            }
            __result = false;

            return false;
        }
    }
}
