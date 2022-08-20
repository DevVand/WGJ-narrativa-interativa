using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class PlayerWalk : MonoBehaviour
{
    Camera mainCamera;
    MouseManager mouse;
    [SerializeField] float distanceMultplier = .1f;
    [SerializeField] float velocity = .6f;
    [SerializeField] float time = .2f;
    void Start()
    {
        mainCamera = Camera.main;
        mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && (!mouse.onDialogue)) {
            Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Ray ray;
            ray = mainCamera.ScreenPointToRay(mouse);
            if (Physics.Raycast (ray, out RaycastHit hit, 1000))
            {
                float dist = Mathf.Abs(hit.point.x - transform.position.x) * distanceMultplier;
                if (hit.point.x < transform.position.x) {
                    transform.positionTransition_x(
                        transform.position.x + -1 * ((velocity / 2) + ((velocity / 2) * dist)),
                        time
                        );
                }
                else
                {
                    transform.positionTransition_x(
                        transform.position.x + 1 * ((velocity / 2) + ((velocity / 2) * dist)),
                        time
                        );
                }
            }
        }
    }

}
