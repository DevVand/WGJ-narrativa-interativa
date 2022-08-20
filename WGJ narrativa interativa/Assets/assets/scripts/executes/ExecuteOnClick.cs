using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnClick : MonoBehaviour
{
    public UnityEvent action;
    private void OnMouseDown()
    {
        action.Invoke();
    }
}