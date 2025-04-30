using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_puerta : MonoBehaviour
{
    Animator anim;
    public bool Dentro = false;
    bool puerta = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col){  //Si el jugador entra en el trigger de la puerta para activarlo
        if(col.tag == "Player"){
            Dentro = true;
        }
    }

    void OnTriggerExit(Collider col){  //Si el jugador sale del trigger de la puerta para desactivarlo
        if(col.tag == "Player"){
            Dentro = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(Dentro && Input.GetKeyDown(KeyCode.Space)){ //Si el jugador está dentro del trigger y presiona espacio
            puerta=!puerta;                            //Puerta alterna antre cerrada o abierta
       }
       if (puerta){                                     // Puerta abierta, reproduce animacion abrir
        anim.SetBool("abierta",true);
       }else{
        anim.SetBool("abierta",false);                  // Puerta cerrada, reproduce animacion cerrar
       }
    }
}
