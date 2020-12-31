using UnityEngine;
using System.Collections;

public class MegaVitamin: TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ActivateMegaVitamin();
    }
}