using DiskCardGame;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Lilys_Custom_Costs
{
    public static class Utils
    {
        public static void ShowLeshyMessage(string message)
        {
            Singleton<TextDisplayer>.Instance.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear(message, 1.5f, 0, Emotion.Neutral, TextDisplayer.LetterAnimation.Jitter, DialogueEvent.Speaker.Leshy));
        }
        public static Texture2D ChangeSize(this Texture2D texture2D, int targetX, int targetY)
        {
            RenderTexture rt = new RenderTexture(targetX, targetY, 24);
            RenderTexture.active = rt;
            Graphics.Blit(texture2D, rt);
            Texture2D result = new Texture2D(targetX, targetY);
            result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
            result.Apply();
            return result;
        }
    }
}
