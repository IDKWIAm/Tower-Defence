using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefence.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float deacceleration;

        private float _currentSpeed;
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
            GetRotation();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
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
            if (_moveDirection != Vector2.zero) _currentSpeed += acceleration * Time.fixedDeltaTime;
            else _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0, deacceleration * Time.fixedDeltaTime);
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, maxSpeed);
        }
        private void Movement()
        {
            if (_moveDirection != Vector2.zero) _currentDirection = _moveDirection;
            _rb.velocity = _currentDirection * (_currentSpeed * Time.fixedDeltaTime);
        }
        private void FixedUpdate()
        {
            CalculateSpeed();
            Movement();
        }
    }
}