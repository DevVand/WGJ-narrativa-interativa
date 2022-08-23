using UnityEngine;
using Lean.Transition;

public class CharAppear : MonoBehaviour
{
    [SerializeField] float fadeTime = 1;
    [SerializeField] Transform cleanPos;
    [SerializeField] GameObject cleanLight;
    [SerializeField] string animName;
    [SerializeField] Color appearColor;

    [SerializeField] Transform mainPos;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sprRenderer;

    public void disappear()
    {
        sprRenderer.enabled = true;
        sprRenderer.colorTransition(new Color(0, 0, 0, 0), fadeTime);
        Invoke(nameof(appear), fadeTime + .1f);
    }
    public void appear()
    {
        mainPos.position = cleanPos.position;
        cleanLight.SetActive(true);
        sprRenderer.enabled = true;
        sprRenderer.colorTransition(appearColor, fadeTime * 2);
        mainPos.position = cleanPos.position;
        mainPos.localScale = cleanPos.localScale;
        anim.SetTrigger(animName);
    }
}
