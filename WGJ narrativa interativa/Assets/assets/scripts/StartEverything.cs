using UnityEngine;
using Lean.Transition;
using UnityEngine.Events;

public class StartEverything : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprRenderers;
    [SerializeField] BoxCollider2D collider;
    [SerializeField] float timeToFade = 2;
    [SerializeField] UnityEvent endOfTransition;
    [SerializeField] UnityEvent start;

    private void OnMouseDown()
    {
        foreach (SpriteRenderer sprR in sprRenderers)
        {
            sprR.colorTransition(new Color(1, 1, 1, 0), timeToFade);
            Invoke(nameof(callEndOfTransition), timeToFade);
        }
    }

    public void callEndOfTransition()
    {
        collider.offset = Vector2.up * 100;
        endOfTransition.Invoke();
    }

    public void callStart()
    {
        start.Invoke();
    }
}
