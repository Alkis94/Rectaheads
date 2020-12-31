using UnityEngine;
using System.Collections;

public class Sanitizer : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeBacteriaSpreadReduction(5);
    }
}
