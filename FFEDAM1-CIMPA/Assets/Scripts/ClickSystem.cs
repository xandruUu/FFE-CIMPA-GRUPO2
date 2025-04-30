/*using UnityEngine;
using TMPro;

public class ClickSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel2;
    [SerializeField] private TextMeshProUGUI dialogueText2;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines2;

    private bool isPlayerInRange2;
    private bool didDialogueStart2;
    private int lineIndex2;

    void Update2() 
    {
        if(isPlayerInRange2 && !didDialogueStart2)
        {
            StartDialogue2();
        }
    }

    private void StartDialogue2()
    {
        didDialogueStart2 = true;
        dialoguePanel2.SetActive(true);
        lineIndex2 = 0; 
        ShowLine2();
    }

    private void ShowLine2()
    {
        dialogueText2.text = dialogueLines2[lineIndex2];
    }

    private void OnTriggerEnter(Collider collision2) 
    {
        if (collision2.gameObject.CompareTag("Player"))
        {
            isPlayerInRange2 = true;
        }
    }

    private void OnTriggerExit(Collider collision2) 
    {
        if (collision2.gameObject.CompareTag("Player"))
        {
            isPlayerInRange2 = false;
            didDialogueStart2 = false;
            dialoguePanel2.SetActive(false);
        }
    }
}*/

using UnityEngine;
using TMPro;

public class ClickSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel2;
    [SerializeField] private TextMeshProUGUI dialogueText2;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines2;

    private bool isPlayerInRange2;
    private bool didDialogueStart2;
    private int lineIndex2;

    void Update() 
    {
        if (isPlayerInRange2 && !didDialogueStart2)
        {
            StartDialogue2();
        }       
        if (Input.GetKey(KeyCode.Space))
        {
            CloseAllDialogues();
        }
    }

    private void StartDialogue2()
    {
        
        didDialogueStart2 = true;
        dialoguePanel2.SetActive(true); 
        ShowLine2();
    }

    private void ShowLine2()
    {
        lineIndex2 = 0;
        dialogueText2.text = dialogueLines2[lineIndex2];
    }

    private void CloseAllDialogues()
    {
        isPlayerInRange2 = false;
        didDialogueStart2 = false;
        dialoguePanel2.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision2) 
    {
        if (collision2.gameObject.CompareTag("Player"))
        {
            isPlayerInRange2 = true;
        }
    }

    private void OnTriggerExit(Collider collision2) 
    {
        if (collision2.gameObject.CompareTag("Player"))
        {
            isPlayerInRange2 = false;
            CloseAllDialogues();
        }
    }
}


