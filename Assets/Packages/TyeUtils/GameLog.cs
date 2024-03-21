using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLog
{
    static string[] colors = new string[3] {"6CF1E7", "FFA81F", "FF1400"};

    public static void Log(string title, string message, int priority = 0)
    {
        Debug.Log($"{title}\n<color=#{colors[priority]}>{message}</color>");
    }

    public static void Warning(string title, string message, int priority = 0)
    {
        Debug.LogWarning($"{title}\n<color=#{colors[priority]}>{message}</color>");
    }

    public static void Error(string title, string message, int priority = 0)
    {
        Debug.LogError($"{title}\n<color=#{colors[priority]}>{message}</color>");
    }
}
