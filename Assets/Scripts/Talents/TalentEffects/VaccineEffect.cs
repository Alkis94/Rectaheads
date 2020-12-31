using UnityEngine;
using System.Collections;

public class VaccineEffect : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeExtraVaccineEffect(1);
    }
}