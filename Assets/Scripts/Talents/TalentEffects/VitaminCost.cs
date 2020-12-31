using UnityEngine;
using System.Collections;

public class VitaminCost : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeVitaminDiscount(3);
    }
}