using DiskCardGame;
using HarmonyLib;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lilys_Custom_Costs
{
    public static class Soul_Cost
    {
        public static int Souls = 0;
        public static List<BoneTokenInteractable> soulTokens = new List<BoneTokenInteractable>();

        public static void AddSoulCost()
        {
            Part1CardCostRender.UpdateCardCost += delegate (CardInfo card, List<Texture2D> costs)
            {
                if (card.SoulCost() > 0)
                {
                    Texture2D CostTexture = Plugin.GetTexture($"soul_cost/soul{card.SoulCost()}");
                    if (card.SoulCost() > 3)
                    {
                        CostTexture = CostTexture.ChangeSize(64, 28);
                    }
                    costs.Add(CostTexture);
                }
            };
        }

        public static int SoulCost(this CardInfo card)
        {
            return card.GetExtendedPropertyAsInt("SoulCost").GetValueOrDefault();
        }

        public static void AddSouls(int amount, CardSlot slot = null)
        {
            Souls += amount;
            Singleton<Part1ResourcesManager>.Instance.StartCoroutine(ShowAddSouls(amount, slot));
        }

        public static void SpendSouls(int amount)
        {
            Souls -= amount;
            Singleton<Part1ResourcesManager>.Instance.StartCoroutine(RemoveSouls(amount));
        }

        public static IEnumerator ShowAddSouls(int amount, CardSlot slot)
        {
            int num;
            for (int i = 0; i < amount; i = num + 1)
            {
                GameObject SoulToken = Object.Instantiate<GameObject>(Singleton<Part1ResourcesManager>.Instance.boneTokenPrefab);
                GameObject.Destroy(SoulToken.transform.GetChild(0).gameObject);
                GameObject SoulTokenMesh = Object.Instantiate<GameObject>(Plugin.SoulTokenPrefab);
                SoulTokenMesh.transform.SetParent(SoulToken.transform);
                SoulToken.transform.localPosition = new Vector3(0, 0, 0);

                BoneTokenInteractable component = SoulToken.GetComponent<BoneTokenInteractable>();
                Rigidbody tokenRB = SoulToken.GetComponent<Rigidbody>();
                if (slot != null)
                {
                    tokenRB.Sleep();
                    SoulToken.transform.position = slot.transform.position + Vector3.up;
                    Singleton<Part1ResourcesManager>.Instance.PushTokenDown(tokenRB);
                    Vector3 endValue = Singleton<Part1ResourcesManager>.Instance.GetRandomLandingPosition() + Vector3.up;
                    Tween.Position(component.transform, endValue, 0.25f, 0.5f, Tween.EaseInOut, Tween.LoopType.None, null, delegate ()
                    {
                        tokenRB.WakeUp();
                        Singleton<Part1ResourcesManager>.Instance.PushTokenDown(tokenRB);
                    }, true);
                }
                else
                {
                    SoulToken.transform.position = Singleton<Part1ResourcesManager>.Instance.GetRandomLandingPosition() + Vector3.up * 5f;
                    Singleton<Part1ResourcesManager>.Instance.PushTokenDown(tokenRB);
                }
                soulTokens.Add(component);
                Singleton<Part1ResourcesManager>.Instance.isOrganized = false;
                yield return new WaitForSeconds(0.05f);
                num = i;
            }
            yield break;
        }

        public static IEnumerator RemoveSouls(int amount)
        {
            int num;
            for (int i = 0; i < amount; i = num + 1)
            {
                if (soulTokens.Count > 0)
                {
                    BoneTokenInteractable component = soulTokens[soulTokens.Count - 1].GetComponent<BoneTokenInteractable>();
                    component.FlyOffBoard();
                    soulTokens.Remove(component);
                    yield return new WaitForSeconds(0.075f);
                }
                num = i;
            }
            yield return new WaitForSeconds(0.05f);
            yield break;
        }
    }
}
