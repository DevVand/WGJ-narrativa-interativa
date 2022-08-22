using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorStatic : MonoBehaviour
{
    int step = 1;
    [SerializeField] float timeToWait = 2;
    [SerializeField] Animator anim;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player Anim").GetComponent<Animator>();
    }

    public void Step() {
        CancelInvoke(nameof(Wait));
        Invoke(nameof(Wait), timeToWait);
        step = step > 1 ? 1 : 2;
        anim.SetTrigger("step"+step);
    }
    public void Wait() {
        anim.SetTrigger("wait");
    }

    public void Look()
    {
        CancelInvoke(nameof(Wait));
        anim.SetBool("look", true);
    }

    public void Unlook()
    {
        CancelInvoke(nameof(Wait));
        anim.SetBool("look", false);
    }
}
