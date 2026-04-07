using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("Speed")]
    public float currentSpeed = 15f;
    public float minSpeed = 10f;
    public float maxSpeed = 35f;
    public float acceleration = 8f;

    [Header("Movement")]
    public float turnSpeed = 60f;
    public float climbSpeed = 12f;
    public float descendSpeed = 18f;

    [Header("Visual Rotation")]
    public float maxBankAngle = 30f;
    public float maxPitchAngle = 20f;
    public float rotationSmooth = 3f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    private float verticalInput;
    private float currentYaw;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 1f;
        rb.angularDamping = 2f;

        currentYaw = transform.eulerAngles.y;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
            verticalInput = 1f;
        else if (Input.GetKey(KeyCode.LeftShift))
            verticalInput = -1f;
        else
            verticalInput = 0f;

        currentSpeed += moveInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * currentSpeed;

        float verticalSpeed = 0f;
        if (verticalInput > 0f)
            verticalSpeed = climbSpeed;
        else if (verticalInput < 0f)
            verticalSpeed = -descendSpeed;

        Vector3 verticalMove = Vector3.up * verticalSpeed;
        rb.linearVelocity = moveDirection + verticalMove;

        currentYaw += turnInput * turnSpeed * Time.fixedDeltaTime;

        float targetPitch = -verticalInput * maxPitchAngle;
        float targetRoll = -turnInput * maxBankAngle;

        Quaternion targetRotation = Quaternion.Euler(targetPitch, currentYaw, targetRoll);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth * Time.fixedDeltaTime);
    }
}