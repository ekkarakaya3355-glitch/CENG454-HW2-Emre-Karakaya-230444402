using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;
    public float flySpeed = 5f;

    private Rigidbody rb;
    private InputSystem_Actions input;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        float move = moveInput.y;
        float turn = moveInput.x;

        transform.Translate(Vector3.forward * move * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);

        if (input.Player.Fly.IsPressed())
        {
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
        }
    }
}