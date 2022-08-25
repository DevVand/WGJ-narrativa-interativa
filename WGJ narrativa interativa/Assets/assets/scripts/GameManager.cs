using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] int needToFinish = 5;
    [SerializeField] UnityEvent finished;
    List<FinishedCharacter> finishedCharacters;
    [SerializeField] float appearDelay = .3f;
    EndingAppear anim;
    int actualChar = 0;
    private void Start()
    {
        finishedCharacters = new List<FinishedCharacter>();
    }
    public void characterFinished(int charIndex, bool rightItem) {
        finishedCharacters.Add(new FinishedCharacter(charIndex, rightItem));
        if (finishedCharacters.Count >= needToFinish) {
            finished.Invoke();
        }
    }
    public void getEndAnims() {
        anim = GameObject.FindGameObjectWithTag("End Anim").GetComponent<EndingAppear>();
    }
    public void showChars() {
    }

    IEnumerator showCharsCO() {

        foreach (var ch in finishedCharacters)
        {
            if (ch.rightItem)
            {
                anim.addAnim(ch.charIndex);
                yield return new WaitForSeconds(appearDelay);
            }
        }
    }
}
public class FinishedCharacter {
    public int charIndex;
    public bool rightItem;
    public FinishedCharacter(int charIndex, bool rightItem) {
        this.charIndex = charIndex;
        this.rightItem = rightItem;
    }
}
