using UnityEngine;
using System.Collections;

public class Emergency : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ActivateEmergency();
    }
}