using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteWithDelay : MonoBehaviour
{
    public List<DelayedAction> actions;
    public void execute() {
        StartCoroutine(callActions());
    }
    public void cancel() {
        StopAllCoroutines();
    }
    IEnumerator callActions()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            yield return new WaitForSeconds(actions[i].delay);
            actions[i].action.Invoke();
        }
    }
}

[System.Serializable]
public class DelayedAction
{
    public float delay;
    public UnityEvent action;
}
