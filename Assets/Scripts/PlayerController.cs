using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Рух")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Огляд")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float maxPitch = 80f;

    private Rigidbody rb;
    private float pitch;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // огляд мишею: по горизонталі крутимо тіло, по вертикалі — тільки голову
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        transform.Rotate(Vector3.up, mouseX);

        pitch = Mathf.Clamp(pitch - mouseY, -maxPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // стрибок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }

        // Esc — звільнити курсор, щоб вийти з Play mode
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = (transform.right * h + transform.forward * v).normalized;
        Vector3 target = direction * moveSpeed;

        // підміняємо горизонтальну швидкість, вертикальну лишаємо фізиці
        Vector3 velocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(target.x, velocity.y, target.z);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}