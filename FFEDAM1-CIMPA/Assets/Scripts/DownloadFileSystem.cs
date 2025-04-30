using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System.Collections;
// Adrian simarro nuevo - 0.1
public class DownloadFileSystem : MonoBehaviour
{
    public string imageUrl = "https://i.imgur.com/1Aiktpa.jpeg"; // URL de la imagen JPEG
    public Button downloadButton;
    public Text uiText;
    private QuestSystem questSystem;
    private changescene scenes;

    void Start()
    {
        downloadButton.onClick.AddListener(() => StartCoroutine(DownloadImage()));
        questSystem = FindObjectOfType<QuestSystem>();
        scenes = FindObjectOfType<changescene>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    
    private IEnumerator DownloadImage(){
    using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl))
    {
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al descargar la imagen: " + request.error);
        }
        else
        {
            string path = Path.Combine(Application.persistentDataPath, "certificadocimpa.jpeg");
            File.WriteAllBytes(path, request.downloadHandler.data);
            Debug.Log("Imagen descargada en: " + path);
            
            // asegurarse de que questSystem no sea nulo antes de usarlo
            if (questSystem != null)
            {
                questSystem.SetMission5(true);
                questSystem.ShowPopup();
                scenes.setEnd(true);
            }
            else
            {
                Debug.LogError("questSystem es nulo al intentar establecer mision5.");
            }
        }
    }
}

}
