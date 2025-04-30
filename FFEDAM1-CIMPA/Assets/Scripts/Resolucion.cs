using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Resolucion : MonoBehaviour
{

    public  Toggle toggle;
    public TMP_Dropdown resolucioneDropDown;
    Resolution[] resoluciones;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }else
        {
            toggle.isOn = false;
        }
        RevisarResolucion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarPantallaCompleta(bool pantallacCompleta)
    {
        Screen.fullScreen = pantallacCompleta;
    }
    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucioneDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for(int i = 0; i<resoluciones.Length;i++)
        {
            string opcion  = resoluciones[i].width +"x"+resoluciones[i].height;
            opciones.Add(opcion);

            if(Screen.fullScreen &&  resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
            
        }
            resolucioneDropDown.AddOptions(opciones);
            resolucioneDropDown.value = resolucionActual;
            resolucioneDropDown.RefreshShownValue();
            
            resolucioneDropDown.value = PlayerPrefs.GetInt("numeroResolucion", resolucioneDropDown.value);
    }
    
    public void CambiarResolucion (int incideResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion",resolucioneDropDown.value);

        Resolution resolucion = resoluciones[incideResolucion];
        Screen.SetResolution(resolucion.width,resolucion.height,Screen.fullScreen);
    }
}
