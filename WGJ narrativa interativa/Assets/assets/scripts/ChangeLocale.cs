using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class ChangeLocale : MonoBehaviour
{
    public void loadLocale(Locale language) {
        LocalizationSettings.SelectedLocale = language;
    }
}
