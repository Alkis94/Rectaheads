using UnityEngine;
using System.Collections;

public class AntibioCost : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeAntibioDiscount(2);
    }
}