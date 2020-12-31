using UnityEngine;
using System.Collections;

public class VaccineCost : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeVaccineDiscount(8);
    }
}