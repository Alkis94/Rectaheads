using UnityEngine;
using System.Collections;

public class Tax : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeExtraMoney(3);
    }
}
