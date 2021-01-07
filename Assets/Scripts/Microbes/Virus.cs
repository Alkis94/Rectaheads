using UnityEngine;
using System.Collections;

public class Virus : Microbe
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(MoveToTarget(SicknessType.virus));
    }
}
