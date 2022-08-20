using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimBool : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] string animName;
    [SerializeField] bool boolValue;

    public void setBoolToDefinedAnim(bool to)
    {
        anim.SetBool(animName, to);
    }
    public void setAnimToDefinedBool(string to)
    {
        anim.SetBool(to, boolValue);
    }
    public void setAll()
    {
        anim.SetBool(animName, boolValue);
    }
}