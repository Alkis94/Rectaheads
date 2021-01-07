using UnityEngine;
using System.Collections;

public class Bacteria : Microbe
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(MoveToTarget(SicknessType.bacteria));
    }
}
