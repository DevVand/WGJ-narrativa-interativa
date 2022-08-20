using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Transition;
using Febucci.UI;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class DialogueMenuController : MonoBehaviour
{

    public bool opened = true;
    public int dialogueOpened = -1;

    public bool playingText = false;
    public bool transitioning = false;
    //UnityEvent TXTShowed;

    public LocalizedString emptyText;
    public LocalizedString preMainText;
    public LocalizedString preSubText;
    public string narrativeText { get; set; }

    [Header("settings")]
    [SerializeField] float subTextHeight;

    [Header("transitions")]

    public float continueSpeed = .3f;
    public float panelChangeSpeed = .4f;
    public float closeOpenSpeed = .5f;
    public float fadeSpeed = .3f;
    public float normalCharSpeed = 0.08f;

    [Header("colors")]
    [SerializeField] Color transparent;
    [SerializeField] Color white;


    [Header("config panel")]
    [SerializeField] RectTransform continuePanel;
    [SerializeField] RectTransform[] continueImgs;
    [SerializeField] Image continueImgFocused;
    [SerializeField] Image continueImgNarrative;

    [SerializeField] RectTransform mainPanel;
    //[SerializeField] Image mainPanelImg;
    [SerializeField] RectTransform openedMainPanel;
    [SerializeField] RectTransform closedMainPanel;

    public RectTransform subPanel;
    public Image subPanelImg;
    [SerializeField] RectTransform openedSubPanel;
    [SerializeField] RectTransform closedSubPanel;

    [Header("config text")]
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI subText;

    [SerializeField] TextMeshProUGUI mainFocText;
    [SerializeField] TextMeshProUGUI subFocText;

    [SerializeField] TextMeshProUGUI mainNarText;
    [SerializeField] TextMeshProUGUI subNarText;
    public float narFadeDelay = 3;


    [SerializeField] List<TextAnimatorPlayer> mainTextPlayers;
    [SerializeField] List<TextAnimatorPlayer> subTextPlayers;
    [SerializeField] List<LocalizeStringEvent> mainTextLocalizes;
    [SerializeField] List<LocalizeStringEvent> subTextLocalizes;
    /*
    TextAnimatorPlayer mainTextPlayer;
    TextAnimatorPlayer subTextPlayer;
    TextAnimatorPlayer mainFocTextPlayer;
    TextAnimatorPlayer subFocTextPlayer;
    LocalizeStringEvent mainLocalize;
    LocalizeStringEvent subLocalize;
    LocalizeStringEvent mainFocLocalize;
    LocalizeStringEvent subFocLocalize;
    */

    [Header("fade")]
    [SerializeField] Image fade;

    //GenericSoundPlayer soundManager;
    
    void Start()
    {
        //AudioSource a;
        white = Color.white;
        opened = true;

        mainTextPlayers = new List<TextAnimatorPlayer>();
        subTextPlayers = new List<TextAnimatorPlayer>();
        mainTextLocalizes = new List<LocalizeStringEvent>();
        subTextLocalizes = new List<LocalizeStringEvent>();

        mainTextPlayers.AddRange(new List<TextAnimatorPlayer> { 
            mainText.GetComponent<TextAnimatorPlayer>(),
            mainFocText.GetComponent<TextAnimatorPlayer>(),
            mainNarText.GetComponent<TextAnimatorPlayer>()}
        );
        subTextPlayers.AddRange(new List<TextAnimatorPlayer> {
            subText.GetComponent<TextAnimatorPlayer>(),
            subFocText.GetComponent<TextAnimatorPlayer>(),
            subNarText.GetComponent<TextAnimatorPlayer>()}
        );

        mainTextLocalizes.AddRange(new List<LocalizeStringEvent> {
            mainText.GetComponent<LocalizeStringEvent>(),
            mainFocText.GetComponent<LocalizeStringEvent>(),
            mainNarText.GetComponent<LocalizeStringEvent>()}
        );

        subTextLocalizes.AddRange(new List<LocalizeStringEvent> {
            subText.GetComponent<LocalizeStringEvent>(),
            subFocText.GetComponent<LocalizeStringEvent>(),
            subNarText.GetComponent<LocalizeStringEvent>()}
        );

    //soundManager = GetComponent<GenericSoundPlayer>();
    eraseAllText();
    forceClose();
        //TXTShowed.AddListener(stopPlayingText);
        //subTextPlayer.onTextShowed = TXTShowed;

    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (opened)
                close();
            else
                open();
        }
        */
    }
    public void openMainPanel()
    {
        if (!opened)
        {
            eraseAllText();
            transitioning = true;
            opened = true;
            dialogueOpened = 1;
            subText.colorTransition(transparent, closeOpenSpeed).JoinDelayTransition(0).colorTransition(Color.white, fadeSpeed);
            mainText.colorTransition(transparent, closeOpenSpeed).JoinDelayTransition(0).colorTransition(Color.white, fadeSpeed);

            mainPanel.sizeDeltaTransition(mainPanel.sizeDelta, closeOpenSpeed).JoinDelayTransition(0)
                .sizeDeltaTransition(new Vector2(openedMainPanel.rect.width, openedMainPanel.rect.height), closeOpenSpeed);
            mainPanel.positionTransition(openedMainPanel.position, closeOpenSpeed, LeanEase.QuintInOut);

            //mainPanelImg.colorTransition(Color.white, closeOpenSpeed);

            subPanel.sizeDeltaTransition(subPanel.sizeDelta, closeOpenSpeed).JoinDelayTransition(0)
                .sizeDeltaTransition(new Vector2(openedSubPanel.rect.width, openedSubPanel.rect.height), closeOpenSpeed).EventTransition(() => stopTransition(), closeOpenSpeed + closeOpenSpeed);
            //subPanelImg.colorTransition(Color.white, closeOpenSpeed);

            semiOpenContinue();
        }
    }
    public void closeMainPanel()
    {
        if (opened)
        {
            transitioning = true;
            opened = false;
            dialogueOpened = -1;
            subText.colorTransition(transparent, fadeSpeed);
            mainText.colorTransition(transparent, fadeSpeed);

            mainPanel.sizeDeltaTransition(mainPanel.sizeDelta, fadeSpeed).JoinDelayTransition(0)
                .sizeDeltaTransition(new Vector2(closedMainPanel.rect.width, closedMainPanel.rect.height), closeOpenSpeed);
            //mainPanel.DelayTransition(fadeSpeed).sizeDeltaTransition(new Vector2(0, 0), closeSpeed);
            mainPanel.positionTransition(mainPanel.position, fadeSpeed + closeOpenSpeed).JoinDelayTransition(0)
                .positionTransition(closedMainPanel.position, closeOpenSpeed, LeanEase.QuintInOut).EventTransition(() => stopTransition(), fadeSpeed + (closeOpenSpeed * 2)).EventTransition(() => eraseTexts(), 0);
            //mainPanelImg.colorTransition(white, fadeSpeed+closeOpenSpeed).JoinDelayTransition(0).colorTransition(transparent, fadeSpeed);

            subPanel.sizeDeltaTransition(subPanel.sizeDelta, fadeSpeed).JoinDelayTransition(0)
                .sizeDeltaTransition(new Vector2(closedSubPanel.rect.width, closedSubPanel.rect.height), closeOpenSpeed);
            //subPanel.DelayTransition(fadeSpeed).sizeDeltaTransition(new Vector2(0, 0), closeSpeed);
            //subPanelImg.colorTransition(white, fadeSpeed + closeOpenSpeed).JoinDelayTransition(0).colorTransition(transparent, fadeSpeed);

            fullCloseContinue();
        }
    }
    public void openFocPanel()
    {
        if (!opened)
        {
            eraseAllText();
            transitioning = true;
            opened = true;
            dialogueOpened = 2;
            continueImgFocused.colorTransition(white, closeOpenSpeed);
            mainFocText.colorTransition(white, closeOpenSpeed);
            subFocText.colorTransition(white, closeOpenSpeed).EventTransition(() => stopTransition(), closeOpenSpeed);

            semiOpenContinue();
        }
    }
    public void closeFocPanel()
    {
        if (opened)
        {
            transitioning = false;
            opened = false;
            dialogueOpened = -1;
            continueImgFocused.colorTransition(transparent, closeOpenSpeed);
            mainFocText.colorTransition(transparent, closeOpenSpeed);
            subFocText.colorTransition(transparent, closeOpenSpeed);

            fullCloseContinue();
        }
    }
    public void openNarPanel()
    {
        if (!opened)
        {
            eraseAllText();
            transitioning = true;
            opened = true;
            dialogueOpened = 3;
            continueImgNarrative.colorTransition(white, closeOpenSpeed);
            mainNarText.colorTransition(white, closeOpenSpeed);
            subNarText.colorTransition(white, closeOpenSpeed).EventTransition(() => stopTransition(), closeOpenSpeed);

            semiOpenContinue();
        }
    }
    public void closeNarPanel()
    {
        if (opened)
        {
            transitioning = false;
            opened = false;
            dialogueOpened = -1;
            continueImgNarrative.colorTransition(transparent, closeOpenSpeed * 2);
            mainNarText.colorTransition(transparent, closeOpenSpeed * 2);
            subNarText.colorTransition(transparent, closeOpenSpeed*2);

            fullCloseContinue();
        }
    }
    public void setMainText(LocalizedString to, Vector2 size, bool startPlayTextBool = true) {
        closeContinue();
        //preMainText = to;
        //mainText.text = to;
        //mainFocText.text = to;
        if (startPlayTextBool)
            startPlayingText();
        preMainText = to;
        mainTextLocalizes[dialogueOpened - 1].StringReference = to;
        /*
        foreach (LocalizeStringEvent mainTextLocalize in mainTextLocalizes)
        {
            
            mainTextLocalize.StringReference = to;
        }
        int i = 0;
        if (size.y <= 0)
        {
            i++;
            size.y = openedMainPanel.sizeDelta.y;
        }
        if (size.x <= 0)
        {
            i++;
            size.x = openedMainPanel.sizeDelta.x;
        }
        if (i < 2)
        {
            mainPanel.sizeDeltaTransition(size, panelChangeSpeed);
            openedMainPanel.sizeDelta = size;
        }
        */
    }
    public void setSubText(LocalizedString to, Vector2 size) {
        closeContinue();
        //preSubText = to;
        //subText.text = to;
        //subFocText.text = to;
        startPlayingText();
        preSubText = to;
        subTextLocalizes[dialogueOpened - 1].StringReference = to;
        /*
        foreach (LocalizeStringEvent subTextLocalize in subTextLocalizes)
        {
            subTextLocalize.StringReference = to;
        }
        int i = 0;
        if (size.y <= 0)
        {
            i++;
            size.y = openedSubPanel.sizeDelta.y;
        }
        if (size.x <= 0)
        {
            i++;
            size.x = openedSubPanel.sizeDelta.x;
        }
        if (i < 2)
        {
            subPanel.sizeDeltaTransition(size, panelChangeSpeed);
            openedSubPanel.sizeDelta = size;
        }
        */
    }

    public void eraseAllText()
    {
        foreach (TextAnimatorPlayer mainTextPlayer in mainTextPlayers)
        {
            mainTextPlayer.enabled = false;
            mainTextPlayer.GetComponent<TextMeshProUGUI>().text = "";
            mainTextPlayer.enabled = true;
        }
        foreach (TextAnimatorPlayer subTextPlayer in subTextPlayers)
        {
            subTextPlayer.enabled = false;
            subTextPlayer.GetComponent<TextMeshProUGUI>().text = "";
            subTextPlayer.enabled = true;
        }
    }
    public void eraseMainText() {
        if (dialogueOpened > -1)
        {
            mainTextPlayers[dialogueOpened - 1].enabled = false;
            mainTextPlayers[dialogueOpened - 1].GetComponent<TextMeshProUGUI>().text = "";
            mainTextPlayers[dialogueOpened - 1].enabled = true;
        }
    }
    public void setTypeSpeed(float to)
    {
        //mainTextPlayer.SetTypewriterSpeed(2);
        //subTextPlayer.SetTypewriterSpeed(2);

        foreach (TextAnimatorPlayer mainTextPlayer in mainTextPlayers)
        {
            mainTextPlayer.SetTypewriterSpeed(to);
        }
        stopPlayingText();
    }
    public void skipTyperitter() {
        //mainTextPlayer.SetTypewriterSpeed(2);
        //subTextPlayer.SetTypewriterSpeed(2);

        foreach (TextAnimatorPlayer mainTextPlayer in mainTextPlayers)
        {
            mainTextPlayer.SkipTypewriter();
        }

        foreach (TextAnimatorPlayer subTextPlayer in subTextPlayers)
        {
            subTextPlayer.SkipTypewriter();
        }
        stopPlayingText();
    }
    public void forceClose()
    {
        if (opened)
        {
            opened = false;
            subText.color = transparent;
            mainText.color = transparent;

            mainPanel.sizeDelta = closedMainPanel.sizeDelta;
            mainPanel.position = closedMainPanel.position;
            //mainPanelImg.color=transparent;

            subPanel.sizeDelta = closedSubPanel.sizeDelta;
            //subPanelImg.color=transparent;
            mainFocText.color = transparent;
            subFocText.color = transparent;
            continueImgFocused.colorTransition(transparent, closeOpenSpeed);
            fullCloseContinue();
            fadeIn(0);
        }
        eraseTexts();
    }
    public void forceOpen()
    {
        if (!opened)
        {
            opened = true;
            subText.color = white;
            mainText.color = white;

            mainPanel.sizeDelta = openedMainPanel.sizeDelta;
            mainPanel.position = openedMainPanel.position;
            //mainPanelImg.color = white;

            subPanel.sizeDelta = openedSubPanel.sizeDelta;
            //subPanelImg.color = white;
        }
    }
    public void eraseTexts() {
        preMainText = emptyText;
        preSubText = emptyText;
        
        mainText.text = "";
        subText.text = "";
        mainFocText.text = "";
        subFocText.text = "";

        foreach (LocalizeStringEvent mainTextLocalize in mainTextLocalizes)
        {
            mainTextLocalize.StringReference = emptyText;
        }
        foreach (LocalizeStringEvent subTextLocalize in subTextLocalizes)
        {
            subTextLocalize.StringReference = emptyText;
        }
    }
    public void stopTransition() { transitioning = false; }

    public void stopPlayingText()
    {
        
        if (!playingText || dialogueOpened<0)
        {
            return;
        } else if (dialogueOpened!=3) {
            playingTextOff();
        } else {
            Invoke(nameof(playingTextOff), narFadeDelay);
        }
    }
    public void playingTextOff()
    {
        print("off");
        CancelInvoke(nameof(playingTextOff));
        playingText = false;
        foreach (TextAnimatorPlayer mainTextPlayer in mainTextPlayers)
        {
            mainTextPlayer.SetTypewriterSpeed(1);
        }

        foreach (TextAnimatorPlayer subTextPlayer in subTextPlayers)
        {
            subTextPlayer.SetTypewriterSpeed(1);
        }
        openContinue(true);
    }
    public void startPlayingText() {
        playingText = true;
    }
    void semiOpenContinue()
    {
        if (true)
        {
            continuePanel.sizeDeltaTransition_y(114, continueSpeed*2, LeanEase.QuintInOut);
            foreach (RectTransform continueImg in continueImgs)
            {
                continueImg.sizeDeltaTransition(Vector2.zero, continueSpeed, LeanEase.QuintInOut);
            }
        }
    }
    void openContinue(bool closeFirst = false)
    {
        if (true)
        {
            if (closeFirst)
                forceCloseContinue();
            continuePanel.sizeDeltaTransition_x(92.7f, continueSpeed, LeanEase.QuintInOut);
            continuePanel.sizeDeltaTransition_y(114, continueSpeed, LeanEase.QuintInOut);
            foreach (RectTransform continueImg in continueImgs)
            {
                continueImg.sizeDeltaTransition(Vector2.zero, continueSpeed, LeanEase.QuintInOut);
                continueImg.sizeDeltaTransition(new Vector2(26, 53), continueSpeed, LeanEase.QuintInOut);

            }
        }
    }
    void closeContinue()
    {
        continuePanel.sizeDeltaTransition_x(30, continueSpeed, LeanEase.QuintInOut);
        foreach (RectTransform continueImg in continueImgs)
        {
            continueImg.sizeDeltaTransition_y(0, continueSpeed, LeanEase.QuintInOut);
        }
    }
    void fullCloseContinue(bool openFirst=false)
    {
        if(openFirst)
            forceOpenContinue();
        continuePanel.sizeDeltaTransition(new Vector2(30,60), continueSpeed, LeanEase.QuintInOut);
        foreach (RectTransform continueImg in continueImgs)
        {
            continueImg.sizeDeltaTransition(Vector2.zero, continueSpeed, LeanEase.QuintInOut);
        }
    }
    void forceOpenContinue() {
        continuePanel.sizeDelta = new Vector2(92.7f, 114);
        foreach (RectTransform continueImg in continueImgs)
        {
            continueImg.sizeDelta = new Vector2(26, 53);
        }
    }
    void forceCloseContinue()
    {
        continuePanel.sizeDelta = new Vector2(30, continuePanel.sizeDelta.y);
        foreach (RectTransform continueImg in continueImgs)
        {
            continueImg.sizeDelta = new Vector2(0, continueImg.sizeDelta.y);
        }
    }


    public void typeNarrativeDialogue() {
        if (dialogueOpened==3 && !this.narrativeText.Contains("{fade}"))
        {
            this.narrativeText = "{fade}" + this.narrativeText;
            TextMeshProUGUI narrativeText = mainTextPlayers[3 - 1].GetComponent<TextMeshProUGUI>();
            narrativeText.text = this.narrativeText;


        }
    }
    public void fadeTo(float to, float time=1) { fade.colorTransition(new Color(0, 0, 0, to), time); }
    public void fadeIn(float time = 1) { fade.colorTransition(new Color(0, 0, 0, 0), time); }
    public void fadeOut(float time = 1) { fade.colorTransition(new Color(0, 0, 0, 1), time); }

}
