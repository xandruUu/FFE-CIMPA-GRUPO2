using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterControls : MonoBehaviour
{
    public float speed = 10.0f;
    public float airVelocity = 8f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float jumpHeight = 2.0f;
    public float maxFallSpeed = 20.0f;
    public float rotateSpeed = 25f;

    private Vector3 moveDir;
    public GameObject cam;
    private Rigidbody rb;

    private float distToGround;

    private bool canMove = true;
    private bool isStuned = false;
    private bool wasStuned = false;
    private float pushForce;
    private Vector3 pushDir;

    public Vector3 checkPoint;
    public bool slide = false;

    private animationScript anim;


    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;

        checkPoint = transform.position;
        Cursor.visible = false;
        anim = GetComponent<animationScript>();
    }

    void FixedUpdate()
    {
        if (canMove && 1==1 && anim != null && anim.canMove)
        {
            if (moveDir.sqrMagnitude > 0.01f)
            {
                Vector3 targetDir = moveDir;
                targetDir.y = 0;
                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed);
                transform.rotation = targetRotation;
            }

            if (IsGrounded())
            {
                Vector3 targetVelocity = moveDir * speed;
                Vector3 velocity = rb.velocity;

                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                if (!slide)
                {
                    rb.AddForce(velocityChange, ForceMode.VelocityChange);
                }
                else
                {
                    rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
                }

                if (Input.GetButton("Jump"))
                {
                    rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }
            }
            else
            {
                if (!slide)
                {
                    Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
                    Vector3 velocity = rb.velocity;
                    Vector3 velocityChange = targetVelocity - velocity;

                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    velocityChange.y = 0;

                    rb.AddForce(velocityChange, ForceMode.VelocityChange);

                    if (velocity.y < -maxFallSpeed)
                        rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
                }
                else
                {
                    rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
                }
            }
        }
        else
        {
            rb.velocity = pushDir * pushForce;
        }

        rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 v2 = v * cam.transform.forward;
        Vector3 h2 = h * cam.transform.right;
        moveDir = (v2 + h2).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
        {
            // Asegúrate de que el objeto tenga el tag "Slide" definido en el proyecto
            slide = hit.transform.CompareTag("Slide");
        }
        else
        {
            slide = false; // Por si no hay nada debajo
        }
    }

    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void HitPlayer(Vector3 velocityF, float time)
    {
        rb.velocity = velocityF;

        pushForce = velocityF.magnitude;
        pushDir = Vector3.Normalize(velocityF);
        StartCoroutine(Decrease(velocityF.magnitude, time));
    }

    public void LoadCheckPoint()
    {
        transform.position = checkPoint;
    }

    private IEnumerator Decrease(float value, float duration)
    {
        if (isStuned)
            wasStuned = true;
        isStuned = true;
        canMove = false;

        float delta = value / duration;

        for (float t = 0; t < duration / 2; t += Time.deltaTime)
        {
            yield return null;
            if (!slide)
            {
                pushForce -= Time.deltaTime * delta;
                pushForce = Mathf.Max(0, pushForce);
            }

            rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));
        }

        if (wasStuned)
        {
            wasStuned = false;
        }
        else
        {
            isStuned = false;
            canMove = true;
        }
    }
}