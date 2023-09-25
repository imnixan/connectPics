using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void Shuffle<T>(this T[] array)
    {
        int n = array.Length;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public static void CloseApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
