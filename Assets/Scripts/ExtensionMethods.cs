using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ExtensionMethods
{
    public static void ShiftLeft<T>(ref T[] array, int shifts = 1)
    {
        T firstElement = array[0];
        Array.Copy(array, shifts, array, 0, array.Length - shifts);
        Array.Clear(array, array.Length - shifts, shifts);
        array[array.Length - 1] = firstElement;
    }

    public static void ShiftRight<T>(ref T[] array, int shifts = 1)
    {
        T lastElement = array[array.Length - 1];
        Array.Copy(array, 0, array, shifts, array.Length - shifts);
        Array.Clear(array, 0, shifts);
        array[0] = lastElement;
    }

    public static GameObject InstantiateAtLocalPosition(GameObject gameObject, Transform parent, Vector2 localPosition)
    {
        GameObject newObject = GameObject.Instantiate(gameObject, parent) as GameObject;
        newObject.transform.localPosition = localPosition;
        return newObject;
    }

    public static string NameFromBuildIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    public static bool StringStartsWith(this string a, string b)
    {
        int aLength = a.Length;
        int bLength = b.Length;

        int aCounter = 0;
        int bCounter = 0;

        while (aCounter < aLength && bCounter < bLength && a[aCounter] == b[bCounter])
        {
            aCounter++;
            bCounter++;
        }

        return (bCounter == bLength);
    }

    public static void FadeOut(this AudioSource audioSource, float duration)
    {
        audioSource.GetComponent<MonoBehaviour>().StartCoroutine(FadeOutCore(audioSource, duration));
    }

    private static IEnumerator FadeOutCore(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return new WaitForEndOfFrame();
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}