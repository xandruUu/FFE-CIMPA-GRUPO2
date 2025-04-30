using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    public void ActivarObjeto(Transform transform)
    {
        animator = transform.GetComponent<Animator>();
        animator.SetBool("abierta", !animator.GetBool("abierta"));
    }
}
