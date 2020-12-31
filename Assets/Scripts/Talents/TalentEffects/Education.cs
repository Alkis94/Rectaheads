using UnityEngine;
using System.Collections;

public class Education : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeBacteriaSpreadReduction(2);
        TalentGlobals.ChangeVirusSpreadReduction(2);
        TalentGlobals.ChangeFungiSpreadReduction(2);
    }
}