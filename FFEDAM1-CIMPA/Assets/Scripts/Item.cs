using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public string type;
    public string description;
    public Sprite icon;
    public QuestSystem quest;
    
    [HideInInspector]
    public bool pickedUp;  // Indica si el objeto fue recogido

    [HideInInspector]
    public bool equipped;  // Indica si el objeto está equipado

    void Start(){
    }

    void Update(){
        if(equipped){
            // Aquí iría la lógica de si el objeto está equipado
        }
    }

        void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player")) // Verifica si el objeto que toca tiene la etiqueta "Player"
        {
            gameObject.SetActive(false); // Desactiva el objeto
            quest.IncrementarBasura();
        }
    }
}
