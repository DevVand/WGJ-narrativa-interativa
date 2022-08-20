using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnHover : MonoBehaviour
{
    public UnityEvent enter;
    public UnityEvent exit;
    private void OnMouseEnter()
    {
        enter.Invoke();
    }
    private void OnMouseExit()
    {
        print("a");
        exit.Invoke();
    }
}