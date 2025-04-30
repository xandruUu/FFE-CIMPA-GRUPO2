using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitgame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CloseGame()
    {
        #if UNITY_EDITOR
            // Si estás en el editor, detiene la reproducción
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Si estás en una construcción, cierra la aplicación
            Application.Quit();
        #endif
    }
}
