using UnityEngine;
using System.Collections;

public class Fungi : Microbe
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ExplodeAfterTime());
    }

    protected IEnumerator ExplodeAfterTime()
    {
        yield return new WaitForSeconds(5f);
        var result = Physics2D.OverlapCircle(transform.position, 0.1f, 1 << LayerMask.NameToLayer("Rectaheads"));
        if (result != null)
        {
            Rectahead rectahead = result.gameObject.GetComponent<Rectahead>();
            if (rectahead != null)
            {
                SicknessManager.Instance.SicknessAttack(rectahead, SicknessType.fungi, 60, 5);
            }
        }
        AudioManager.Instance.PlayMicrobeAttackSound();
        Die("Attack");
    }
}
