using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool onDialogue = false;
    public bool onInventory = false;

    public void setOnDialogue(bool to) {
        onDialogue = to;
    }

    public void setOnInventory(bool to)
    {
        onInventory = to;
    }
}
