using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject item;
    public int id;
    public string type;
    public string description;
    public bool empty;
    public Sprite icon;

    public Transform slotIconGameObject;

    // Se referencia el primer hijo del slot como el lugar donde mostrar el ícono
    void Start()
    {
        slotIconGameObject = transform.GetChild(0);
    }

    // Actualiza el ícono visual del slot
    public void UpdateSlot()
    {
        slotIconGameObject.GetComponent<Image>().sprite = icon;
    }
}
