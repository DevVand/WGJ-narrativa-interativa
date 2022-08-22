using UnityEngine;
using Lean.Transition;

public class InventoryInteraction : MonoBehaviour
{

    [SerializeField] Transform inventory;
    [SerializeField] Transform openPos;
    [SerializeField] Transform closePos;

    [SerializeField] float time = .8f;
    bool opened;

    MouseManager mouse;
    [SerializeField] bool alwaysAcceptMouse;
    private void Start()
    {
        if (!alwaysAcceptMouse)
            mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }
    private void OnMouseDown()
    {

        if (alwaysAcceptMouse || (!mouse.onDialogue))
        {
            if (opened)
            {
                close();
               }
            else {
                opened = true;
                Invoke(nameof(setInventoryTrue), .1f);
                inventory.localPositionTransition(openPos.localPosition, time, LeanEase.QuadInOut);
            }
        }
    }
    public void close()
    {
        opened = false;
        mouse.setOnInventory(false);
        inventory.localPositionTransition(closePos.localPosition, time, LeanEase.QuadInOut);
    }
    void setInventoryTrue()
    {
        mouse.setOnInventory(true);
    }
}
