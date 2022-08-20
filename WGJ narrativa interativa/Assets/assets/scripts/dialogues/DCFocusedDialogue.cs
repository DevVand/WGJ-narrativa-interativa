using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCFocusedDialogue : DialogueController
{
    [SerializeField] float focusedDelayOut = 1;
    [SerializeField] float focusedDelayIn = 1;
    [SerializeField] float firstAppearDelay = .1f;
    [SerializeField] bool fadeAfterFinished = true;

    protected override void Start()
    {
        base.Start();
    }
    public override void startDialog()
    {
        base.startDialog();
        dialogueMenu.fadeOut(focusedDelayOut);
        Invoke(nameof(openDialogue), focusedDelayIn);
        Invoke(nameof(startDialogging), focusedDelayIn + firstAppearDelay);
    }

    protected override void openDialogue()
    {
        dialogueMenu.openFocPanel();
        Invoke(nameof(startDialogging), dialogueMenu.closeOpenSpeed * 2);
    }
    protected override void closeDialogue()
    {
        base.closeDialogue();

        dialogueMenu.closeFocPanel();
    }

    public override void endDialogue()
    {
        if (fadeAfterFinished)
        {
            dialogueMenu.fadeIn(focusedDelayIn);
        }
        base.endDialogue();
    }
}
