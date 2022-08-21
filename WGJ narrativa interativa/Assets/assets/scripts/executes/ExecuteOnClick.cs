using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnClick : MonoBehaviour
{
    MouseManager mouse;
    [SerializeField] bool alwaysAcceptMouse;
    private void Start()
    {
        if(!alwaysAcceptMouse)
            mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }
    public UnityEvent action;
    
    virtual protected void OnMouseDown()
    {
        if (alwaysAcceptMouse || (!mouse.onDialogue && !mouse.onInventory)) { 
            action.Invoke();
        }
    }
}