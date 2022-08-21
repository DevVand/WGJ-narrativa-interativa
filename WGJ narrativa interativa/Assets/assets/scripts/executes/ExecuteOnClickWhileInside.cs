using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteOnClickWhileInside : ExecuteOnClick
{
    [SerializeField] ExecuteOnPlayerTrigger trigger;
    protected override void OnMouseDown()
    {
        if (trigger.inside)
        {
            base.OnMouseDown();
        }
    }
}
