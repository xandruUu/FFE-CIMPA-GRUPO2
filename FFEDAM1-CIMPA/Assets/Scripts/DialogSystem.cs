/*using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager3D : MonoBehaviour
{
    [Header("Configuración del Diálogo")]
    public GameObject dialoguePanel; // Panel de diálogo
    public TextMeshProUGUI dialogueText; // Texto del diálogo
    public float typingSpeed = 0.05f; // Velocidad de escritura

    [Header("Configuración de Diálogos")]
    public string[] dialogueLines; // Líneas de diálogo

    [Header("Configuración de Interacción")]
    public KeyCode interactionKey = KeyCode.E; // Tecla para interactuar

    private bool isInTriggerZone = false;
    private bool isDialogueActive = false;
    private int currentLineIndex = 0;

    void Start()
    {
        // Asegurarse que el panel de diálogo esté oculto al inicio
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // Verificar si está en zona de interacción y presiona la tecla
        if (isInTriggerZone && Input.GetKeyDown(interactionKey))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                ContinueDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador entró en la zona de interacción
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            // Opcional: Mostrar mensaje de interacción
            Debug.Log("Presiona " + interactionKey + " para hablar");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reiniciar cuando el jugador sale de la zona
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        // Activar panel de diálogo
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        currentLineIndex = 0;

        // Iniciar primera línea de diálogo
        StartCoroutine(TypeDialogueLine());
    }

    IEnumerator TypeDialogueLine()
    {
        // Efecto de escritura
        dialogueText.text = "";
        foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void ContinueDialogue()
    {
        // Pasar a la siguiente línea
        if (currentLineIndex < dialogueLines.Length - 1)
        {
            currentLineIndex++;
            StartCoroutine(TypeDialogueLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        // Desactivar panel de diálogo
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        currentLineIndex = 0;
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTriggerTypewriter : MonoBehaviour
{
    [Header("Dialogue Setup")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    [Header("Dialogue Configuration")]
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;

    [Header("Interaction Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public AudioClip typingSound;

    private bool isInTriggerZone = false;
    private bool isDialogueActive = false;
    private int currentLineIndex = 0;
    private AudioSource audioSource;

    void Start()
    {
        // Hide dialogue panel initially
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Setup audio source for typing sound
        if (typingSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = typingSound;
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Check for interaction when in trigger zone
        if (isInTriggerZone && Input.GetKeyDown(interactionKey))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if player entered interaction zone
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            Debug.Log("Press " + interactionKey + " to interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset when player leaves interaction zone
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        // Activate dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        // Reset dialogue state
        currentLineIndex = 0;
        isDialogueActive = true;

        // Start typing first line
        StartCoroutine(TypeDialogueLine());
    }

    IEnumerator TypeDialogueLine()
    {
        // Clear previous text
        dialogueText.text = "";

        // Type each character
        foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;

            // Play typing sound if available
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void AdvanceDialogue()
    {
        // Check if current line is fully typed
        if (dialogueText.text != dialogueLines[currentLineIndex])
        {
            // Stop typing and show full text
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            // Move to next line
            currentLineIndex++;

            // Check if more lines exist
            if (currentLineIndex < dialogueLines.Length)
            {
                StartCoroutine(TypeDialogueLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        // Hide dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Reset dialogue state
        isDialogueActive = false;
        currentLineIndex = 0;
    }
}
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager3D : MonoBehaviour
{
    [Header("Dialogue Panel Configuration")]
    public GameObject dialoguePanel; // Dialogue panel
    public TextMeshProUGUI dialogueText; // Dialogue text component

    [Header("Dialogue Settings")]
    public string[] dialogueLines; // Dialogue lines
    public float typingSpeed = 0.05f; // Writing speed

    [Header("Interaction Settings")]
    public KeyCode interactionKey = KeyCode.E; // Interaction key
    public AudioClip typingSound; // Optional typing sound

    [Header("Optional Components")]
    public bool useAudioFeedback = true; // Toggle audio feedback

    // Dialogue state variables
    private bool isInTriggerZone = false;
    private bool isDialogueActive = false;
    private int currentLineIndex = 0;
    private AudioSource audioSource;

    void Start()
    {
        // Ensure dialogue panel is hidden at start
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Setup audio source if typing sound is provided
        if (typingSound != null && useAudioFeedback)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = typingSound;
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Check for interaction when in trigger zone
        if (isInTriggerZone && Input.GetKeyDown(interactionKey))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if player entered interaction zone
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            Debug.Log($"Press {interactionKey} to interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset when player leaves interaction zone
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        // Activate dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        // Reset dialogue state
        currentLineIndex = 0;
        isDialogueActive = true;

        // Start typing first line
        StartCoroutine(TypeDialogueLine());
    }

    IEnumerator TypeDialogueLine()
    {
        // Clear previous text
        dialogueText.text = "";

        // Type each character
        foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;

            // Play typing sound if available
            if (useAudioFeedback && typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void AdvanceDialogue()
    {
        // Check if current line is fully typed
        if (dialogueText.text != dialogueLines[currentLineIndex])
        {
            // Stop typing and show full text immediately
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            // Move to next line
            currentLineIndex++;

            // Check if more lines exist
            if (currentLineIndex < dialogueLines.Length)
            {
                StartCoroutine(TypeDialogueLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        // Hide dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Reset dialogue state
        isDialogueActive = false;
        currentLineIndex = 0;
        isInTriggerZone = false;
    }

    // Optional method to set new dialogue lines dynamically
    public void SetDialogueLines(string[] newDialogueLines)
    {
        dialogueLines = newDialogueLines;
    }
}
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTriggerTypewriter : MonoBehaviour
{
    [Header("Dialogue Setup")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    [Header("Dialogue Configuration")]
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;

    [Header("Interaction Settings")]
    public KeyCode interactionKey = KeyCode.E;
    public AudioClip typingSound;

    private bool isInTriggerZone = false;
    private bool isDialogueActive = false;
    private int currentLineIndex = 0;
    private AudioSource audioSource;

    void Start()
    {
        // Hide dialogue panel initially
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Setup audio source for typing sound
        if (typingSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = typingSound;
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Check for interaction when in trigger zone
        if (isInTriggerZone && Input.GetKeyDown(interactionKey))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if player entered interaction zone
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            Debug.Log("Press " + interactionKey + " to interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset when player leaves interaction zone
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        // Activate dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        // Reset dialogue state
        currentLineIndex = 0;
        isDialogueActive = true;

        // Start typing first line
        StartCoroutine(TypeDialogueLine());
    }

    IEnumerator TypeDialogueLine()
    {
        // Clear previous text
        dialogueText.text = "";

        // Type each character
        foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;

            // Play typing sound if available
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void AdvanceDialogue()
    {
        // Check if current line is fully typed
        if (dialogueText.text != dialogueLines[currentLineIndex])
        {
            // Stop typing and show full text
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            // Move to next line
            currentLineIndex++;

            // Check if more lines exist
            if (currentLineIndex < dialogueLines.Length)
            {
                StartCoroutine(TypeDialogueLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        // Hide dialogue panel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Reset dialogue state
        isDialogueActive = false;
        currentLineIndex = 0;
    }
}
using UnityEngine;
using TMPro;
using System.Collections;

public class SimpleDialogueInteraction : MonoBehaviour
{
    [Header("Configuración del Diálogo")]
    public GameObject dialoguePanel; // Panel de diálogo
    public TextMeshProUGUI dialogueText; // Texto del diálogo
    public string dialogueMessage = "¡Hola! Este es un mensaje de ejemplo."; // Mensaje predeterminado

    [Header("Configuración de Interacción")]
    public KeyCode interactionKey = KeyCode.E; // Tecla para interactuar

    private bool isInTriggerZone = false;

    void Start()
    {
        // Asegurarse que el panel de diálogo esté oculto al inicio
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // Verificar si está en zona de interacción y presiona la tecla
        if (isInTriggerZone && Input.GetKeyDown(interactionKey))
        {
            ToggleDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador entró en la zona de interacción
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            Debug.Log($"Presiona {interactionKey} para interactuar");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reiniciar cuando el jugador sale de la zona
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
            // Ocultar panel de diálogo al salir
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
        }
    }

    void ToggleDialogue()
    {
        // Alternar visibilidad del panel de diálogo
        if (dialoguePanel != null)
        {
            bool isActive = dialoguePanel.activeSelf;
            dialoguePanel.SetActive(!isActive);

            // Establecer texto del diálogo
            if (!isActive && dialogueText != null)
            {
                dialogueText.text = dialogueMessage;
            }
        }
    }

    // Método para cambiar el mensaje de diálogo
    public void SetDialogueMessage(string newMessage)
    {
        dialogueMessage = newMessage;
    }
}
using UnityEngine;
using TMPro;
using System.Collections;
 
public class DialogueScript : MonoBehaviour 
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.1f;

    private int index;
    private bool canInteract = false;

    void Update() 
    {
        if (canInteract && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Assuming the player has a specific tag
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            dialogueText.text = string.Empty;
            StartDialogue();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            StopAllCoroutines();
            dialogueText.text = string.Empty;
            gameObject.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            canInteract = false;
            gameObject.SetActive(false);
        }
    }
}
*/
using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    // [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;

    private float typingTime=0.05f;

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    void Update() 
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if(!didDialogueStart)
            {
                StartDialogue();
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
                
        }
    }

    private void StartDialogue()
    {
        didDialogueStart=true;
        dialoguePanel.SetActive(true);
        //dialogueMark.SetActive(false);
        lineIndex = 0; 
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            //dialogueMark.SetActive(true);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            //dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //dialoguePanel.SetActive(false);

            //dialogueMark.SetActive(false); 
        }
    }
}
/*
using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;

    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
        }
        else if (isPlayerInRange && !didDialogueStart) // ClickSystem functionality
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void ShowLineInstantly() // Added method for ClickSystem behavior
    {
        dialogueText.text = dialogueLines[lineIndex];
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }
}*/

