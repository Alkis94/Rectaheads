using UnityEngine;
using System.Collections;

public class Masks : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeVirusSpreadReduction(5);
    }
}