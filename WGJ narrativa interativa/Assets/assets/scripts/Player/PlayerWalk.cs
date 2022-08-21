using UnityEngine;
using UnityEngine.Events;
using Lean.Transition;

public class PlayerWalk : MonoBehaviour
{
    Camera mainCamera;
    MouseManager mouse;
    [SerializeField] float minDist = .7f;
    [SerializeField] float distanceMultplier = .1f;
    [SerializeField] float velocity = .6f;
    [SerializeField] float time = .2f;
    [SerializeField] float holdTime = .5f;

    [SerializeField] UnityEvent step;
    [SerializeField] UnityEvent left;
    [SerializeField] UnityEvent right;

    bool canGoLeft = true;
    bool canGoRight = true;
    void Start()
    {
        mainCamera = Camera.main;
        mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            walk();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke(nameof(walk));
        }

    }

    void walk() {
        if (!mouse.onDialogue && !mouse.onInventory)
        {
            Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Ray ray;
            ray = mainCamera.ScreenPointToRay(mouse);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                float dist = Mathf.Abs(hit.point.x - transform.position.x);
                if (dist > minDist)
                {
                    dist *= distanceMultplier;
                    if (hit.point.x < transform.position.x && canGoLeft)
                    {
                        step.Invoke();
                        left.Invoke();
                        transform.positionTransition_x(
                            transform.position.x + -1 * ((velocity / 2) + ((velocity / 2) * dist)),
                            time
                            );
                    }
                    else if (hit.point.x > transform.position.x && canGoRight)
                    {
                        step.Invoke();
                        right.Invoke();
                        transform.positionTransition_x(
                            transform.position.x + 1 * ((velocity / 2) + ((velocity / 2) * dist)),
                            time
                            );
                    }
                }
            }
            Invoke(nameof(walk), holdTime);
        }
    }

    public void setCanGoLeft(bool to) { canGoLeft = to; }
    public void setCanGoRight(bool to) { canGoRight = to; }
}
