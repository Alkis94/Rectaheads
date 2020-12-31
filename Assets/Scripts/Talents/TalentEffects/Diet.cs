using UnityEngine;
using System.Collections;

public class Diet : TalentEffect
{
    public override void Effect()
    {
        TalentGlobals.ChangeExtraImmuneDefense(5);
    }
}