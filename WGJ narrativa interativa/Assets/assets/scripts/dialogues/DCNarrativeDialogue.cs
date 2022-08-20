using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCNarrativeDialogue : DialogueController
{
    [SerializeField] float focusedDelayOut = 1;
    [SerializeField] float focusedDelayIn = 1;
    [SerializeField] float firstAppearDelay = .1f;
    [SerializeField] float betweenDelay = .3f;
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
        dialogueMenu.openNarPanel();
        Invoke(nameof(startDialogging), dialogueMenu.closeOpenSpeed * 2);
    }
    protected override void closeDialogue()
    {
        base.closeDialogue();

        dialogueMenu.closeNarPanel();
    }

    public override void endDialogue()
    {
        if (fadeAfterFinished)
        {
            dialogueMenu.fadeIn(focusedDelayIn);
        }
        base.endDialogue();
    }

    public override void say()
    {
        if(dialogLines.dialogs.Length - 1 >= i - 1)
            dialogueMenu.closeNarPanel();
        Invoke(nameof(sayContinue), betweenDelay+dialogueMenu.closeOpenSpeed*2);
    }
    private void sayContinue()
    {
        dialogueMenu.openNarPanel();
        base.say();
    }

}
