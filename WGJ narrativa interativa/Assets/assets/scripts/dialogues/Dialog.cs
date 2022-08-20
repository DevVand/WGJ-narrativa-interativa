using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using System;

[System.Serializable]
public class Dialog
{
    public string name;
    public AudioClip sound;
    public bool sameCharacterAsBefore;
    public LocalizedString subText;

    public LocalizedString mainText;

    public UnityEvent beforeAction;
    public UnityEvent action;


    public void setName(LocalizedString to) {
        try {
            this.name = to.GetLocalizedString().Substring(0, 14);
        }
        catch
        {
            this.name = to.GetLocalizedString();
        }
    }
    /*
    public Dialog(string sbtxt, string mtxt) {
        subText = sbtxt;
        mainText = mtxt;
    }
    */
}
