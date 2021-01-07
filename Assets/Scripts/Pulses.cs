using UnityEngine;
using System.Collections;

public class Pulses : MonoBehaviour
{

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PulsesHandler());
    }

    private IEnumerator PulsesHandler()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);

            int total = RectaheadManager.Instance.RectaheadTotalCount;
            int count = RectaheadManager.Instance.RectaheadCurrentCount;
            float percentageAlive = (100 * count) / total;

            if (percentageAlive > 75)
            {
                animator.SetTrigger("NoPulse");
            }
            else if (percentageAlive > 50)
            {
                animator.SetTrigger("SmallPulse");
            }
            else if (percentageAlive > 25)
            {
                animator.SetTrigger("BigPulse");
            }
        }
    }
}
