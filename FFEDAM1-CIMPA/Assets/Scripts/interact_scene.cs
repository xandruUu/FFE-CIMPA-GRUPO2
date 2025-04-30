using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class interact_scene : MonoBehaviour
{
    public string sceneToLoad; // ← Aquí defines el nombre de la escena en el Inspector
    public bool canInteract = false;  //CanInteract es si el jugador esta dentro del trigger (puede interactuar)
    public QuestSystem questSystem;
    private bool mision = false;
    private string currentScene;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            canInteract = false;
        }
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (canInteract && Input.GetKeyDown(KeyCode.Space)) //Si se puede interactuar(el player esta dentro del trigger) y se presiona espacio
        {
             if(questSystem.GetMision4() == true && currentScene.name != "certificado"){
                SceneManager.LoadScene("certificado");
            } else if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);

            }                                        //La escena que metas por parametro en el nspector se reproducira siguiente
        }
    }
}
