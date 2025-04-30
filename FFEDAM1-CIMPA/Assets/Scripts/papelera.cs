using UnityEngine;

public class papelera : MonoBehaviour
{
    public string categoria;
    public minijuego1Controlador controlador;

    private void OnMouseDown()
    {
        controlador.SeleccionarPapelera(categoria);
    }
}
