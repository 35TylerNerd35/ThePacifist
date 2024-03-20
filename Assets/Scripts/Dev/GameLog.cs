using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLog
{
    static string[] colors = new string[3] {"6CF1E7", "FFA81F", "FF1400"};

    public static void Log(int priority, string name, string message)
    {
        Debug.Log($"{name}\n<color=#{colors[priority]}>{message}</color>");
    }
}
