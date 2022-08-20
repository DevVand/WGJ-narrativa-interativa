using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogs : MonoBehaviour
{
    public DialogueController DCmain;

    public UnityEvent dialogueStarted;
    public UnityEvent dialogueEnded;
    public Dialog[] dialogs;
}
