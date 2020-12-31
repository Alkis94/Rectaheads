using UnityEngine;
using System.Collections;

public class VitaminEffect: TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeExtraVitaminEffect(3);
    }
}