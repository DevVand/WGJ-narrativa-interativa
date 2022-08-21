using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    bool canGoLeft = true;
    bool canGoRight = true;
    void Start()
    {
        mainCamera = Camera.main;
        mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (!mouse.onDialogue && !mouse.onInventory)) {
            walk();
        }
        if (Input.GetMouseButtonUp(0) && (!mouse.onDialogue && !mouse.onInventory))
        {
            CancelInvoke(nameof(walk));
        }

    }

    void walk() {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Ray ray;
        ray = mainCamera.ScreenPointToRay(mouse);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            float dist = Mathf.Abs(hit.point.x - transform.position.x);
            if (dist > minDist) {
                dist*=distanceMultplier;
                if (hit.point.x < transform.position.x && canGoLeft)
                {
                    transform.positionTransition_x(
                        transform.position.x + -1 * ((velocity / 2) + ((velocity / 2) * dist)),
                        time
                        );
                }
                else if (hit.point.x > transform.position.x && canGoRight)
                {
                    transform.positionTransition_x(
                        transform.position.x + 1 * ((velocity / 2) + ((velocity / 2) * dist)),
                        time
                        );
                }
            } }
        Invoke(nameof(walk), holdTime);
    }

    public void setCanGoLeft(bool to) { canGoLeft = to; }
    public void setCanGoRight(bool to) { canGoRight = to; }
}
