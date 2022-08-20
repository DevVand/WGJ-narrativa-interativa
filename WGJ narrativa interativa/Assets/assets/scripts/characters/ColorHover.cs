using UnityEngine;
using Lean.Transition;

public class ColorHover : MonoBehaviour
{
    [SerializeField] Color idleC = Color.white;
    [SerializeField] Color hoverC = Color.gray;
    [SerializeField] Color clickC = Color.black;
    [SerializeField] SpriteRenderer spr;

    private void Start()
    {
        spr.colorTransition(idleC, .2f);
    }
    private void OnMouseExit()
    {
        LeanTransition.ResetState();
        spr.colorTransition(idleC, .2f);
    }
    private void OnMouseEnter()
    {
        LeanTransition.ResetState();
        spr.colorTransition(hoverC, .2f);
    }

    private void OnMouseDown()
    {
        spr.colorTransition(clickC, .05f).JoinDelayTransition(.05f).colorTransition(hoverC, .1f);
    }
}
