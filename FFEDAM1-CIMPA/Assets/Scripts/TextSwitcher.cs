using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher : MonoBehaviour
{
    public Text uiText; // Componente de texto
    public Button changeTextButton; // Botón para cambiar el texto
    public string newText = "Texto cambiado"; // Nuevo texto a mostrar

    private string originalText; // Almacena el texto original

    void Start()
    {
        // Guardar el texto original
        originalText = "Has completado tu formación con éxito descarga tu certificado!!";
        uiText.text= originalText;

        // Asignar el evento del botón
        changeTextButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Cambiar el texto al hacer clic en el botón
        uiText.text = newText;
    }

    void OnEnable()
    {
        // Al entrar en la escena, restaurar el texto original
        uiText.text = originalText;
    }

    void OnDisable()
    {
        // Al salir de la escena, restaurar el texto original
        uiText.text = originalText;
    }
}