using UnityEngine;
using Lean.Transition;
public class ItemDrag : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Transform returnPos;
    [SerializeField] Transform realPos;

    [SerializeField] float zPos;

    [SerializeField] float smooth = 10;
    [SerializeField] float returnTime = .5f;

    MouseManager mouse;
    [SerializeField] bool alwaysAcceptMouse;

    bool draggingThis = false;
    private void Start()
    {
        zPos = realPos.position.z;
        mainCamera = Camera.main;
        if (!alwaysAcceptMouse)
            mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }

    private void OnMouseDown()
    {
        draggingThis = true;
    }
    private void OnMouseUp()
    {
        draggingThis = false;
        realPos.localPositionTransition(returnPos.localPosition, returnTime);
    }
    private void OnMouseDrag()
    {
        if (draggingThis && (alwaysAcceptMouse || !mouse.onDialogue))
        {
            Vector3 mousePos = getMousePos();

            realPos.position = Vector3.Lerp(realPos.position,
                new Vector3(mousePos.x, mousePos.y, zPos), smooth * Time.deltaTime);
        }
    }
    Vector3 getMousePos()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPos);
        Ray ray;
        ray = mainCamera.ScreenPointToRay(mouse);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
