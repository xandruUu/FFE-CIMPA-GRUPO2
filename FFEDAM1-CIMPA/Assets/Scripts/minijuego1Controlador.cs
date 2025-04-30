using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
 
public class minijuego1Controlador : MonoBehaviour
{
    public GameObject panelFinal;
    public GameObject botonSalir;
 
    // Nuevos paneles
    public GameObject panelCorrecto;
    public GameObject panelIncorrecto;
    public GameObject panelSeleccionaObjeto;
 
    // Texto opcional dentro de cada panel si quieres cambiar el mensaje dinámicamente
    public TextMeshProUGUI textoCorrecto;
    public TextMeshProUGUI textoIncorrecto;
    public TextMeshProUGUI textoSeleccionaObjeto;
 
    private GameObject objetoSeleccionado;
    private string categoriaObjetoSeleccionado;
    private int objetosRestantes;
 
    void Start()
    {
        panelFinal.SetActive(false);
        botonSalir.SetActive(false);
 
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        panelSeleccionaObjeto.SetActive(false);
 
        objetosRestantes = GameObject.FindObjectsOfType<ObjetoReciclable>().Length;
    }
 
    public void SeleccionarObjeto(GameObject obj, string categoria)
    {
        objetoSeleccionado = obj;
        categoriaObjetoSeleccionado = categoria;
 
        // Ocultar todos los paneles al seleccionar un objeto
        OcultarPanelesMensaje();
    }
 
  public void SeleccionarPapelera(string papeleraCategoria)
{
    OcultarPanelesMensaje(); // Ocultar antes de evaluar
 
    if (objetoSeleccionado == null)
    {
        MostrarPanelSeleccionaObjeto();
        return;
    }
 
    if (papeleraCategoria == categoriaObjetoSeleccionado)
    {
        objetoSeleccionado.SetActive(false);
        objetosRestantes--;
 
        MostrarPanelCorrecto();
 
        if (objetosRestantes <= 0)
        {
            panelFinal.SetActive(true);
            botonSalir.SetActive(true);
        }
    }
    else
    {
        MostrarPanelIncorrecto();
    }
 
    objetoSeleccionado = null;
}
 
    void MostrarPanelCorrecto()
    {  
        panelCorrecto.SetActive(true);
        Invoke(nameof(OcultarPanelesMensaje), 2f);
    }
 
    void MostrarPanelIncorrecto()
    {
        panelIncorrecto.SetActive(true);
        Invoke(nameof(OcultarPanelesMensaje), 2f);
    }
 
    void MostrarPanelSeleccionaObjeto()
    {
        panelSeleccionaObjeto.SetActive(true);
        Invoke(nameof(OcultarPanelesMensaje), 2f);
    }
 
 
    void OcultarPanelesMensaje()
    {
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        panelSeleccionaObjeto.SetActive(false);
    }
 
    public void SalirDelJuego()
    {
        SceneManager.LoadScene("Menu");
    }
}
 