using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodigoPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject menuSalir;
    public AudioSource sonidoDeFondo; // <--- solo este sonido se reanudará

    private bool enPausa = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!enPausa)
            {
                ActivarPausa();
            }
            else
            {
                Reanudar();
            }
        }
    }

    private void ActivarPausa()
    {
        menuPausa.SetActive(true);
        enPausa = true;

        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Pausar todos los sonidos
        AudioSource[] todosLosSonidos = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in todosLosSonidos)
        {
            audio.Pause();
        }
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        if (menuSalir != null) menuSalir.SetActive(false);
        enPausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Solo reanudar el sonido de fondo
        if (sonidoDeFondo != null)
        {
            sonidoDeFondo.Play();
        }
    }

    public void IrAlMenu(string nombreEscena)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nombreEscena);
    }

    public void Salir()
    {
        Application.Quit();
    }
}


