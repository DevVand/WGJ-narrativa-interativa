using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using Lean.Transition;

public class ItemDrag : MonoBehaviour
{
    [SerializeField] int itemIndex = 0;
    [SerializeField] LocalizedString itemName;
    [SerializeField] LocalizedString empty;
    [SerializeField] LocalizeStringEvent itemLocalize;

    [SerializeField] Transform returnPos;
    [SerializeField] Transform realPos;

    [SerializeField] float zPos;
    [SerializeField] float distanceFromOrigin=1.2f;

    [SerializeField] float smooth = 10;
    [SerializeField] float returnTime = .4f;

    [SerializeField] bool alwaysAcceptMouse;
    Camera mainCamera;
    MouseManager mouse;

    InventoryInteraction inv;
    GameObject actualCharacter;

    bool draggingThis = false;
    private void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Inv").GetComponent<InventoryInteraction>();
        zPos = realPos.position.z;
        mainCamera = Camera.main;
        if (!alwaysAcceptMouse)
            mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MouseManager>();
    }
    private void OnMouseEnter()
    {
        if (!draggingThis)
            itemLocalize.StringReference = itemName;
    }
    private void OnMouseExit()
    {
        if (!draggingThis)
            itemLocalize.StringReference = empty;

    }
    private void OnMouseDown()
    {
        if (!draggingThis)
            itemLocalize.StringReference = itemName;
        draggingThis = true;
    }
    private void OnMouseUp()
    {
        draggingThis = false;
        itemLocalize.StringReference = empty;
        if (actualCharacter!=null && Mathf.Abs(realPos.localPosition.y-returnPos.localPosition.y)>distanceFromOrigin && giveItem())
        {
            inv.close();
            //MUDAR PARA TIRAR O ITEM
            realPos.localPositionTransition(returnPos.localPosition, returnTime, LeanEase.CubicInOut);
        }
        else {
            realPos.localPositionTransition(returnPos.localPosition, returnTime, LeanEase.CubicInOut);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.CompareTag("Char")) {
            actualCharacter = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Char"))
        {
            actualCharacter = null;
        }
    }

    bool giveItem() {
        return actualCharacter.GetComponent<CharItem>().giveItem(itemIndex);
    }
}
