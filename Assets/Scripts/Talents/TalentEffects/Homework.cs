using UnityEngine;
using System.Collections;

public class Homework : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeBacteriaSpreadReduction(3);
        TalentGlobals.ChangeVirusSpreadReduction(3);
        TalentGlobals.ChangeFungiSpreadReduction(3);
    }
}