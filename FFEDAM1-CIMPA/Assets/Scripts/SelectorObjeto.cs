using UnityEngine;
 
public class SelectorObjeto : MonoBehaviour
{
    Camera cam;
    public minijuego1Controlador controlador;
 
    void Start()
    {
        cam = Camera.main;
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ObjetoReciclable obj = hit.collider.GetComponent<ObjetoReciclable>();
                if (obj != null)
                {
                    controlador.SeleccionarObjeto(obj.gameObject, obj.categoria);
                }
            }
        }
    }
}