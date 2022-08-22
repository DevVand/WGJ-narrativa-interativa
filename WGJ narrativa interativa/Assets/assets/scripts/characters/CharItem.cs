using UnityEngine;
using UnityEngine.Events;
using Lean.Transition;
public class CharItem : MonoBehaviour
{
    [SerializeField] CharAppear appear;

    [SerializeField] int rightItemIndex = 0;
    [SerializeField] UnityEvent rightItem;
    [SerializeField] UnityEvent wrongItem;

    [SerializeField] int wrongCount = 0;
    [SerializeField] int wrongResistence = 2;
    [SerializeField] UnityEvent giveUp;

    [SerializeField] GameObject mainObject;
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider2D col;
    [SerializeField] UnityEvent fadeOff;
    [SerializeField] float fadeTime = 1;
    bool acceptingItem = true;
    public bool giveItem(int index) {
        if (index == rightItemIndex && acceptingItem)
        {
            acceptingItem = false;
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
    public void clean() {
        col.offset = Vector2.up * 100;
        fadeOff.Invoke();
        sprRenderer.enabled = false;
        appear.disappear();
    }
    public void leave() {
        col.offset = Vector2.up * 100;
        fadeOff.Invoke();
        sprRenderer.colorTransition(new Color(0, 0, 0, 0), fadeTime);
        Destroy(mainObject, 2);
    }
}
