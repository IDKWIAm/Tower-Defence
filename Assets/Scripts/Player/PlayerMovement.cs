using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float rotationSpeed;
    Rigidbody2D rb;
    Vector3 moveDirection;
    Quaternion currentRotation;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentRotation = transform.rotation;
    }
    void Update()
    {
        GetInput();
        GetRotation();
    }
    private void GetInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection = moveDirection.normalized;
    }
    private void GetRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            currentRotation = Quaternion.RotateTowards(currentRotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        transform.rotation = currentRotation;
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }
}