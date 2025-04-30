using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirBasura : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();       //Get animator para la animacion de las basuras
    }
    void Update()
    {
        anim.SetBool("abierta", true);         //Al iniciar la escena se reproduce la animacion de las basuras abiertas
    }
}
