using UnityEngine;

public class animationScript : MonoBehaviour
{
        private bool isPickingUp = false; 
        public bool canMove = true;
        Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalkingStraight = Input.GetKey(KeyCode.W);
        bool isWalkingLeft = Input.GetKey(KeyCode.A);
        bool isWalkingRight = Input.GetKey(KeyCode.D);
        bool isWalkingBack = Input.GetKey(KeyCode.S);
        /*bool isWalkingStraightArrrow = Input.GetKey(KeyCode.UpArrow);
        bool isWalkingLeftArrrow = Input.GetKey(KeyCode.LeftArrow);
        bool isWalkingRightArrrow = Input.GetKey(KeyCode.RightArrow);
        bool isWalkingBackArrow = Input.GetKey(KeyCode.DownArrow);
        */
        if (isWalkingStraight || isWalkingBack || isWalkingLeft || isWalkingRight )
        {
            animator.SetBool("isWalking", true);
        }
        if (!isWalkingStraight && !isWalkingBack && !isWalkingLeft && !isWalkingRight)
        {
            animator.SetBool("isWalking", false);
        }
}

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && !isPickingUp) // Verifica si el objeto tiene la etiqueta "Item"
        {
            isPickingUp = true; // Cambia el estado a recogiendo
            canMove = false;
            animator.SetBool("pickup", true); // Activa la animación de recogida
            Destroy(other.gameObject); // Opcional: destruye el objeto recogido
            Invoke("ResetPickup", 4f); // Llama a ResetPickup después de 1 segundo
        }
    }

    private void ResetPickup()
    {
        isPickingUp = false; // Restablece el estado
        animator.SetBool("pickup", false); // Desactiva la animación de recogida
        canMove = true;
    }
}