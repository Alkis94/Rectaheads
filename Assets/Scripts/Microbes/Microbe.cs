using UnityEngine;
using System.Collections;

public class Microbe : MonoBehaviour
{
    public Vector3 TargetRectahead { get; set; }
    protected Animator animator;
    protected bool isAlive = true;
    protected float speed = 0.5f;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if(isAlive)
        {
            AudioManager.Instance.PlayMicrobeDeathSound();
            Die("Die");
        }
    }

    protected IEnumerator MoveToTarget(SicknessType sicknessType)
    {
        while(true)
        {
            yield return null;
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, TargetRectahead, step);

            if(Vector2.Distance(TargetRectahead, transform.position) < 0.1f)
            {
                var result = Physics2D.OverlapCircle(transform.position, 0.1f, 1 << LayerMask.NameToLayer("Rectaheads"));
                if (result != null)
                {
                    Rectahead rectahead = result.gameObject.GetComponent<Rectahead>();
                    if (rectahead != null)
                    {
                        SicknessManager.Instance.SicknessAttack(rectahead, sicknessType, 60, 5);
                    }
                }
                AudioManager.Instance.PlayMicrobeAttackSound();
                Die("Attack");
                break;
            }
        }
    }

    protected void Die(string animation)
    {
        isAlive = false;
        animator.SetTrigger(animation);
        Destroy(gameObject, 0.5f);
    }

    
}
