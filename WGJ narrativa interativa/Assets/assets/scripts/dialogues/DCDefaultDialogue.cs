using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCDefaultDialogue : DialogueController
{

    public override void startDialog() {
        base.startDialog();
        openDialogue();
    }
    protected override void openDialogue()
    {
        dialogueMenu.openMainPanel();
        Invoke(nameof(startDialogging), dialogueMenu.closeOpenSpeed * 2);
    }
    protected override void closeDialogue()
    {
        base.closeDialogue();

        dialogueMenu.closeMainPanel();
    }
}
