using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAppear : MonoBehaviour
{
    List<Animator> anims;
    void Start()
    {
        anims = new List<Animator>();
        for (int i = 0; i < transform.childCount-1; i++)
        {
            anims.Add(transform.GetChild(i).GetComponent<Animator>());
        }
    }

    public void addAnim(int index) {
        anims[index].SetTrigger("play");
    }
}
