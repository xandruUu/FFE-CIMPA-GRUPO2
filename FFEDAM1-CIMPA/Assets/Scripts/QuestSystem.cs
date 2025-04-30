using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestSystem : MonoBehaviour{
// Nuevo Adrián Simarro
    public bool mision1 = false;
    public bool mision2 = false;
    public bool mision3 = false;
    public bool mision4 = false;
    public bool mision5 = false;
    public int basura = 0;
    public Text missionText;
    public GameObject popup;

    void Start(){
        popup = GameObject.FindGameObjectWithTag("POPUP");

        if (popup != null)
        {
            popup.SetActive(false);
        }
    }

    private void Update(){

        if(mision1 == false && mision2 == false && mision3 == false && mision4 == false && mision5 == false){
            if(basura ==5 ){
                mision1 = true;
                ShowPopup();
            }else{
            missionText.text = "▶ Primera tarea: Recoje la basura para reciclarla | Basura Recojida: " + basura + "/5" ;
            }
        }
        if(mision1 == true && mision2 == false && mision3 == false && mision4 == false && mision5 == false){
            missionText.text = "▶ Segunda tarea: Ve a reciclar la basura" ;
        }
        else if(mision1 == true && mision2 == true && mision3 == false && mision4 == false && mision5 == false){
            missionText.text= "▶ Tercera tarea: Ve a revisar tus correos.";
        }
        else if(mision1 == true && mision2 == true && mision3 == true && mision4 == false && mision5 == false) {
            missionText.text = "▶ Cuarta: Ve a ver a encargada de recursos humanos.";
        }
        else if(mision1 == true && mision2 == true && mision3 == true && mision4 == true && mision5 == false) {
            missionText.text = "▶ Quinta tarea: Imprime tu certificado.";
        }
        else if(mision1 == true && mision2 == true && mision3 == true && mision4 == true && mision5 == true) {
            missionText.text = "Certificado entregado. Buen trabajo!";
        }

    }

    public void SetMission1(bool value){
        mision5 = value;
    }
    public void SetMission2(bool value){
        mision5 = value;
    }
    public void SetMission3(bool value){
        mision5 = value;
    }
    public void SetMission4(bool value){
        mision4 = value;
    }
    public void SetMission5(bool value){
        mision5 = value;
    }

    public bool GetMision4(){
        return mision4;
    }

    public void IncrementarBasura()
    {
        basura++;
    }

    // POPUP

    public void ShowPopup()
    {
        if (popup != null)
        {
            popup.SetActive(true);
            StartCoroutine(HidePopupAfterDelay(3f)); // Llama a la coroutine para esconderlo después de 3 segundos
        }
    }

    // Coroutine para esconder el popup después de un tiempo
    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        popup.SetActive(false); // Esconde el popup
    }

    // Método para esconder el popup manualmente (opcional)
    public void HidePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }
}
