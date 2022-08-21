using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnHover : MonoBehaviour
{
    MouseManager mouse;
    [SerializeField] bool alwaysAcceptMouse;

    public UnityEvent enter;
    public UnityEvent exit;
    private void Start()
    {
        if (!alwaysAcceptMouse)
            mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }
    private void OnMouseEnter()
    {
        if (alwaysAcceptMouse || (!mouse.onDialogue && !mouse.onInventory))
        {
            enter.Invoke();
        }
    }
    private void OnMouseExit()
    {
        if (alwaysAcceptMouse || (!mouse.onDialogue && !mouse.onInventory))
        {
            exit.Invoke();
        }
    }
}