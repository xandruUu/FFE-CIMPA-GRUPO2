using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    Animator animator;
    float velZ = 0.0f;
    public float acceleration = 2f;
    public float decceleration = 2f;
    public float maxWalkVel = 0.5f;
    public float maxRunVel = 2f;
    //Optimización
    int VelXHash;
    int VelZHash;

    //Manejo de la aceleración y deceleración.
    void changeVelocity(bool wPressed, bool sPressed, bool aPressed, bool dPressed, bool lShiftPressed, float currentMaxVal)
    {
        //Si el usuario pulsa las telcas aceleramos en esa dirección hasta el límite indicado en el editor.
        if (wPressed && velZ < currentMaxVal)
        {
            velZ += Time.deltaTime * acceleration;
        }

        if (sPressed && velZ < currentMaxVal)
        {
            velZ += Time.deltaTime * acceleration;
        }

        if (aPressed && velZ < currentMaxVal)
        {
            velZ += Time.deltaTime * acceleration;
        }

        if (dPressed && velZ < currentMaxVal)
        {
            velZ += Time.deltaTime * acceleration;
        }

        //Si el usuario suelta las teclas, deceleramos en esa dirección hasta el límite.
        if (!wPressed && velZ > 0.0f)
        {
            velZ -= Time.deltaTime * decceleration;
        }

        if (!sPressed && velZ > 0.0f)
        {
            velZ -= Time.deltaTime * decceleration;
        }

        if (!aPressed && velZ > 0.0f)
        {
            velZ -= Time.deltaTime * decceleration;
        }

        if (!dPressed && velZ > 0.0f)
        {
            velZ -= Time.deltaTime * decceleration;
        }
    }

    void lockOrResetVelocity(bool wPressed, bool sPressed, bool aPressed, bool dPressed, bool lShiftPressed, float currentMaxVal)
    {
        if (!aPressed && !dPressed && velZ != 0.0f && (velZ > -0.05f && velZ < 0.05f))
        {
            velZ = 0.0f;
        }

        //Decelerar a la velocidad máxima de caminado
        if (wPressed && velZ > currentMaxVal)
        {
            velZ -= Time.deltaTime * decceleration;
            //Redondear a la currentMaxVal if está dentro del offset.
            if (velZ > currentMaxVal && velZ < (currentMaxVal + 0.05f))
            {
                velZ = currentMaxVal;
            }
        }
        //Redondear a la currentMaxVal si está dentro del offset.
        else if (wPressed && velZ < currentMaxVal && velZ > (currentMaxVal - 0.05f))
        {
            velZ = currentMaxVal;
        }

        //Decelerar a la velocidad máxima de caminado
        if (sPressed && velZ > currentMaxVal)
        {
            velZ -= Time.deltaTime * decceleration;
            //Redondear a la currentMaxVal si está dentro del offset.
            if (velZ > currentMaxVal && velZ < (currentMaxVal + 0.05f))
            {
                velZ = currentMaxVal;
            }
        }
        //Redondear a la currentMaxVal si está dentro del offset.
        else if (sPressed && velZ < currentMaxVal && velZ > (currentMaxVal - 0.05f))
        {
            velZ = currentMaxVal;
        }

        //Decelerar a la velocidad máxima de caminado
        if (aPressed && velZ > currentMaxVal)
        {
            velZ -= Time.deltaTime * decceleration;
            //Redondear a currentMaxVal si está dentro del offset.
            if (velZ > currentMaxVal && velZ < (currentMaxVal + 0.05f))
            {
                velZ = currentMaxVal;
            }
        }
        //Redondear a currentMaxVal si está dentro del offset.
        else if (aPressed && velZ < currentMaxVal && velZ > (currentMaxVal - 0.05f))
        {
            velZ = currentMaxVal;
        }

        //Decelerar a la velocidad máxima de caminado
        if (dPressed && velZ > currentMaxVal)
        {
            velZ -= Time.deltaTime * decceleration;
            //Redondear a currentMaxVel si está dentro del offset.
            if (velZ > currentMaxVal && velZ < (currentMaxVal + 0.05f))
            {
                velZ = currentMaxVal;
            }
        }
        //Redondear a currentMaxVal si está dentro del offset.
        else if (dPressed && velZ < currentMaxVal && velZ > (currentMaxVal - 0.05f))
        {
            velZ = currentMaxVal;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        //Optimización
        VelZHash = Animator.StringToHash("VelZ");
    }

    // Update is called once per frame
    void Update()
    {
        bool wPressed = Input.GetKey(KeyCode.W);
        bool sPressed = Input.GetKey(KeyCode.S);
        bool aPressed = Input.GetKey(KeyCode.A);
        bool dPressed = Input.GetKey(KeyCode.D);
        bool lShiftPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVal = lShiftPressed ? maxRunVel : maxWalkVel;

        //Manejar cambios en la velocidad
        changeVelocity(wPressed, sPressed, aPressed, dPressed, lShiftPressed, currentMaxVal);
        lockOrResetVelocity(wPressed, sPressed, aPressed, dPressed, lShiftPressed, currentMaxVal);
        
        //Setear los parámetros de nuestras variables locales a las variables del editor.
        animator.SetFloat(VelZHash, velZ);
    }
}
