using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;


public class DialogueController : MonoBehaviour
{

    //public LocalizedString textAtTop;
    //[SerializeField] TextMeshPro textAtTopComponent;
    //[SerializeField] LocalizeStringEvent textAtTopLocalize;
    //[SerializeField] protected float angle = 45;
    [SerializeField] protected float delayBetweenSubMain = .2f;
    [SerializeField] Color exclColor = Color.white;
    //[SerializeField] bool returnControlsAfterFinished = true;

    public bool skippableText = true;
    public bool dontSkipThisText = false;


    bool textActive = false;
    protected int i = 0;

    [SerializeField] protected Dialogs dialogLines;

    //[SerializeField] TextMeshProUGUI textMesh;
    //protected InterrogationController interrogation;
    public DialogueMenuController dialogueMenu;
    protected Transform player;
    //protected PlayerInputGetter input;
    //protected PlayerManager manager;
    protected AudioSource audioSource;

    MouseManager mouse;
        
    virtual protected void Start()
    {
        mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();

        dialogueMenu = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueMenuController>();
        //manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        //player = manager.returnMiddle().transform;

        //input = manager.returnInput();
        audioSource = GetComponent<AudioSource>();
        //interrogation = manager.returnExclController();

        //textAtTopComponent.text = " ";
        //textAtTopLocalize.StringReference = dialogueMenu.emptyText;
    }

    virtual public void startDialog()
    {
        mouse.onDialogue = true;
       // textAtTopLocalize.StringReference = dialogueMenu.emptyText;
        dialogueMenu.eraseTexts();
        dialogLines.dialogueStarted.Invoke();

    }
    virtual protected void openDialogue()
    {
        //dialogueMenu.open(0);
        //Invoke(nameof(startDialogging), dialogueMenu.closeOpenSpeed * 2);
    }
    protected void startDialogging()
    {
        i = 0;
        textActive = true;
        say();
        StartCoroutine(checkInput());
    }
    virtual public void endDialogue()
    {
        StopAllCoroutines();
        closeDialogue();
        textActive = false;
        mouse.onDialogue = false;
    }
    virtual protected void closeDialogue()
    {
        /*if (returnControlsAfterFinished)
        {
        }
        */
        dialogLines.dialogueEnded.Invoke();
    }
    protected IEnumerator checkInput() {
        while (true)
        {
            if (Input.GetMouseButtonDown(0) && ( (skippableText && !dontSkipThisText) || !dialogueMenu.playingText))
            {
                dontSkipThisText = false;

                if (dialogueMenu.playingText)
                {
                    CancelInvoke(nameof(sayMainText));
                    sayMainText();
                    dialogueMenu.Invoke(nameof(dialogueMenu.skipTyperitter), .04f);
                }
                else
                {
                    say();
                }
            }
            yield return null;
        }
    }
    public virtual void say()
    {
        CancelInvoke();
        i++;
        if (dialogLines.dialogs.Length - 1 >= i - 1)
        {
            dialogLines.dialogs[i - 1].beforeAction.Invoke();
            if (dialogLines.dialogs[i - 1].sameCharacterAsBefore)
            {
                sayMainText();
            }
            else
            {
                //dialogueMenu.setMainText(dialogueMenu.emptyText, Vector2.zero, false);
                dialogueMenu.eraseMainText();
                dialogueMenu.setSubText(dialogLines.dialogs[i - 1].subText, Vector2.zero);
                Invoke(nameof(sayMainText), StrippedString(dialogLines.dialogs[i - 1].subText.GetLocalizedString()).Length * dialogueMenu.normalCharSpeed + delayBetweenSubMain);
            }
        }
        else {
            endDialogue();
        }
    }
    public void sayMainText()
    {
        if (dialogueMenu.preMainText.GetLocalizedString() != dialogLines.dialogs[i - 1].mainText.GetLocalizedString())
        {
            if (dialogLines.dialogs[i - 1].action != null)
                dialogLines.dialogs[i - 1].action.Invoke();
            if (dialogLines.dialogs[i - 1].sound != null)
                audioSource.PlayOneShot(dialogLines.dialogs[i - 1].sound);
            dialogueMenu.setMainText(dialogLines.dialogs[i - 1].mainText, Vector2.zero);
        }
    }
    protected static string StrippedString(string richStr)
    {
        StringBuilder sb = new StringBuilder(richStr.Length);
        bool tag = false;
        for (int index = 0; index < richStr.Length; index++)
        {
            char c = richStr[index];
            if (tag)
            {
                if (c == '>')
                    tag = false;
            } else {
                if (c == '<')
                    tag = true;
                else if (c != ' ')
                    sb.Append(c);
            }
        }
        string strippedStr = sb.ToString();
        return strippedStr;
    }
    public void generateDialogsNames() {
        foreach (Dialog dialog in dialogLines.dialogs)
        {
            dialog.setName(dialog.mainText);
        }
    }
    public void fadeIn(float time = 1) { dialogueMenu.fadeIn(time); }
    public void fadeOut(float time = 1) { dialogueMenu.fadeOut(time); }

    public void setSkippableTo(bool to) { skippableText = to; }
    public void DontSkipThisText() { dontSkipThisText = true; }
}
