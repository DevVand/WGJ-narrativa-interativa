using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteAtStart : MonoBehaviour
{
    public UnityEvent action;
    void Start()
    {
           action.Invoke();
    }

    void destroyThis()
    {
        Destroy(this.gameObject);
    }
}
