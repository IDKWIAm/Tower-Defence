using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    
    private float currentSpeed;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private Vector2 _currentDirection;
    private Quaternion _currentRotation;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentRotation = transform.rotation;
    }
    void Update()
    {
        GetInput();
        GetRotation();
    }
    private void GetInput()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");
        _moveDirection = _moveDirection.normalized;
    }
    private void GetRotation()
    {
        if (_moveDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);
            _currentRotation = Quaternion.RotateTowards(_currentRotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        transform.rotation = _currentRotation;
    }
    private void CalculateSpeed()
    {
        if (_moveDirection != Vector2.zero)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deacceleration * Time.fixedDeltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }
    private void Movement()
    {
        if (_moveDirection != Vector2.zero)
        {
            _currentDirection = _moveDirection;
        }
        _rb.velocity = _currentDirection * (currentSpeed * Time.fixedDeltaTime);
    }
    private void FixedUpdate()
    {
        CalculateSpeed();
        Movement();
    }
}