using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    int charactersFinished = 0;
    [SerializeField] int needToFinish = 5;
    [SerializeField] UnityEvent finished;
    List<FinishedCharacter> finishedCharacters;
    private void Start()
    {
        finishedCharacters = new List<FinishedCharacter>();
    }
    public void characterFinished(int charIndex, bool rightItem) {
        finishedCharacters.Add(new FinishedCharacter(charIndex, rightItem));
        charactersFinished++;
        if (charactersFinished >= needToFinish) {
            finished.Invoke();
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
