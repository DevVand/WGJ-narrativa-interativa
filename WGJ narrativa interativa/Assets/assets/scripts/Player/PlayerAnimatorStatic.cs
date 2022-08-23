using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorStatic : MonoBehaviour
{
    int step = 1;
    [SerializeField] float timeToWait = 2;
    [SerializeField] Animator anim;

    private void OnEnable()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player Anim");
        if(obj != null)
            anim = obj.GetComponent<Animator>();
    }

    public void Step() {
        if (anim == null)
            checkNull();
        CancelInvoke(nameof(Wait));
        Invoke(nameof(Wait), timeToWait);
        step = step > 1 ? 1 : 2;
        anim.SetTrigger("step"+step);
    }
    public void Wait()
    {
        if (anim == null)
            checkNull();
        anim.SetTrigger("wait");
    }

    public void Look()
    {
        if (anim == null)
            checkNull();
        CancelInvoke(nameof(Wait));
        anim.SetBool("look", true);
    }

    public void Unlook()
    {
        if (anim == null)
            checkNull();
        CancelInvoke(nameof(Wait));
        anim.SetBool("look", false);
    }

    void checkNull() {
        anim = GameObject.FindGameObjectWithTag("Player Anim").GetComponent<Animator>();
    }
}
