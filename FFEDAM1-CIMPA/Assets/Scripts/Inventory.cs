using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;
    public GameObject inventory;
    private int allSlots;
    private GameObject[] slot;
    public GameObject slotHolder;
    private GameObject currentItem; 
    private bool isCursorLocked = false;
    // audio de items
    private AudioSource audioSource;

    void Start()
    {
        allSlots = slotHolder.transform.childCount;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }

        GameObject sonidoBasura = GameObject.Find("SonidoBasura");
        if (sonidoBasura != null)
        {
            audioSource = sonidoBasura.GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Manejo de entrada para arrastrar y soltar - aun no va
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Item") && hit.collider.gameObject.GetComponent<Item>().pickedUp)
                {
                    currentItem = hit.collider.gameObject;
                    currentItem.transform.SetParent(null); // Desvincular del slot
                    Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor
                    Cursor.visible = false; // Hacer el cursor invisible
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && currentItem != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    currentItem.SetActive(true);
                    currentItem.transform.position = hit.point;
                }
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            currentItem = null;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            ToggleCursorLock();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            GameObject itemPickedUp = col.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            AddItem(itemPickedUp, item.id, item.type, item.description, item.icon);
            // Audio pop al cojerlo
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogError("El AudioSource no tiene un audio asignado.");
            }
        }
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().id = itemID;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().description = itemDescription;
                slot[i].GetComponent<Slot>().icon = itemIcon;

                itemObject.transform.SetParent(slot[i].transform);
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;

                return;
            }
        }
    }

    private void ToggleCursorLock()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        isCursorLocked = !isCursorLocked;
    }


}