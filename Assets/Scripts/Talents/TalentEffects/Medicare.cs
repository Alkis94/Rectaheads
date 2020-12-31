using UnityEngine;
using System.Collections;

public class Medicare: TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeAntibioDiscount(1);
        TalentGlobals.ChangeVitaminDiscount(1);
        TalentGlobals.ChangeVaccineDiscount(2);
        TalentGlobals.ChangeAntifungalDiscount(1);
    }
}

