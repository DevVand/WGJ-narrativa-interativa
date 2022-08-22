using UnityEngine;
using UnityEngine.Events;
using Lean.Transition;

public class CharItem : MonoBehaviour
{
    [SerializeField] int rightItemIndex = 0;
    [SerializeField] UnityEvent rightItem;
    [SerializeField] UnityEvent wrongItem;

    [SerializeField] int wrongCount = 0;
    [SerializeField] int wrongResistence = 2;
    [SerializeField] UnityEvent giveUp;

    [SerializeField] GameObject mainObject;
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] BoxCollider2D col;
    [SerializeField] UnityEvent fadeOff;
    [SerializeField] float fadeTime = 1;
    public bool giveItem(int index) {
        if (index == rightItemIndex)
        {
            rightItem.Invoke();
        }
        else
        {
            wrongItem.Invoke();
        }
        return index == rightItemIndex;
    }
    public void wrongItemGiven() {
        wrongCount++;
        if (wrongCount >= wrongResistence) {
            giveUp.Invoke();
        }
    }

    public void leave() {
        col.offset = Vector2.up * 100;
        fadeOff.Invoke();
        sprRenderer.colorTransition(new Color(0, 0, 0, 0), fadeTime);
        Destroy(mainObject, 2);
    }

}
