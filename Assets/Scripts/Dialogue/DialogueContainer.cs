using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    public DialogueData dialogue;
    private bool hasPlayed = false;

    void OnTriggerEnter()
    {
        if (hasPlayed) return;
        else
        {
            DialogueManager.Instance.LaunchDialogue(dialogue);
            hasPlayed = true;
        }
    }
}
