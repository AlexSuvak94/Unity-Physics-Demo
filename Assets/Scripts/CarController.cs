using UnityEngine;

public class CarController : MonoBehaviour
{
    public CanvasScript joystick;
    public float forwardSpeed = 10f;
    private float maxForwardSpeed;
    public float turnSpeed = 100f;
    public float maxSteerAngle = 25f;

    public GameObject frontLeftWheel;
    public GameObject frontRightWheel;

    private Rigidbody rb;
    void Start()
    {
        maxForwardSpeed = forwardSpeed;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void ReverseFunction()
    {
        forwardSpeed = -100f;
    }

    void FixedUpdate()
    {
        if (forwardSpeed < maxForwardSpeed)
        {
            forwardSpeed++;
        }
        if (forwardSpeed > maxForwardSpeed)
        {
            forwardSpeed = maxForwardSpeed;
        }

        Vector3 forwardVelocity = transform.forward * forwardSpeed;
        rb.linearVelocity = new Vector3(forwardVelocity.x, rb.linearVelocity.y, forwardVelocity.z);

        float steerInput = joystick.Horizontal();

        frontLeftWheel.transform.localRotation = Quaternion.Euler(0f, steerInput * 45f, 0f);
        frontRightWheel.transform.localRotation = Quaternion.Euler(0f, steerInput * 45f, 0f);

        if (Mathf.Abs(steerInput) > 0.05f)
        {
            float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}