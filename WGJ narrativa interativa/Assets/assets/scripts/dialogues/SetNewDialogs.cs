using UnityEngine;

public class SetNewDialogs : MonoBehaviour
{
    [SerializeField] DialogueController dialogueController;
    [SerializeField] Dialogs[] dialogs;

    public void setDialogs(int to) {
        dialogueController.setNewDialogLines(dialogs[to]);
    }
}
