using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private bool isGrounded = true;
    private animationScript anim;

    void Awake(){
        anim = GetComponent<animationScript>();
    }

    void Update()
    {
        moveCharacter();
    }


    private void moveCharacter(){
        if (anim.canMove = true){

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }else{
            Debug.Log("No te puedes mover estas cojiendo algo.");
        }
    }
}
