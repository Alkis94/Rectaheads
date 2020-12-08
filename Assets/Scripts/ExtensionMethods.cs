using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}