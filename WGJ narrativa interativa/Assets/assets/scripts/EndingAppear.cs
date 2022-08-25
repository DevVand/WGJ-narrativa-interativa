using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingAppear : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI minutesTakenText;
    [SerializeField] float minutesMultplier;

    [SerializeField] ExecuteWithDelay showTexts;

    GameManager manager;
    List<Animator> anims;
    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        anims = new List<Animator>();
        for (int i = 0; i < transform.childCount; i++)
        {
            anims.Add(transform.GetChild(i).GetComponent<Animator>());
        }
        manager.getEndAnims();
        manager.Invoke(nameof(manager.showChars),2.5f);
    }

    public void addAnim(int index) {
        anims[index].SetTrigger("play");
    }
    public void showText(int cleannedAmount)
    {
        minutesTakenText.text = (130 - (cleannedAmount * minutesMultplier)).ToString();
        showTexts.execute();
    }

}
