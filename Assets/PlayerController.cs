using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Managers;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputClass input = null;
        Rigidbody rb => GetComponent<Rigidbody>();
        Vector3 moveVector;

        [Header("Movement Values")]
        [SerializeField] float walkSpeed = 5f;
        [SerializeField] float runSpeed = 10f;
        [SerializeField] float jumpForce = 300f;
        [SerializeField] Transform groundPos;
        [SerializeField] LayerMask groundMask;
        [SerializeField] LayerMask movingGroundMask;
        bool IsGrounded()
        {
            if (Physics.CheckSphere(groundPos.position, 0.1f, groundMask)) return true;
            else return false;
        }

        public bool IsRunning = false;

        [Header("Rotation Values")]
        [SerializeField] float MouseSens = 150f;
        Vector2 CamRot;
        private void Awake()
        {
            input = new PlayerInputClass();

        }
        private void OnEnable()
        {
            StartCoroutine(SubscribeToEventsDelayed(0.1f));
        }

        private void OnDisable()
        {
            CursorStateManager.Instance.UnlockCursorState();
            input.Disable();
            input.Player.Movement.performed -= OnMovementPerformed;
            input.Player.Movement.canceled -= OnMovementCancelled;
            input.Player.Run.performed -= OnRunCalled;
            input.Player.Run.canceled -= OnRunCancelled;
        }

        public void OnMovementPerformed(InputAction.CallbackContext value)
        {
            moveVector = value.ReadValue<Vector3>();
        }

        public void OnMovementCancelled(InputAction.CallbackContext value)
        {
            rb.velocity = Vector3.zero;
        }

        public void OnRunCalled(InputAction.CallbackContext value)
        {
            IsRunning = true;
        }
        public void OnRunCancelled(InputAction.CallbackContext value)
        {
            IsRunning = false;
        }
        // Update is called once per frame
        private void Update()
        {
            Rotate();
        }
        void FixedUpdate()
        {
            Move();
            Jump();
        }

        void Move()
        {
            Vector3 MoveVec = moveVector.x * transform.right + moveVector.z * transform.forward;
            rb.velocity = new Vector3(MoveVec.normalized.x * (IsRunning ? runSpeed : walkSpeed), rb.velocity.y, MoveVec.normalized.z * (IsRunning ? runSpeed : walkSpeed));
        }

        void Jump()
        {
            if (moveVector.y > 0 && IsGrounded())
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Force);
            }
        }
        void Rotate()
        {
            // Since the camera is a child of the player, what I did was just rotate the player, and rotated the camera's y axis separately 
            // With this, each axis is calculated and set once only.
            CamRot.y += -Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;
            CamRot.x += Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;

            CamRot.y = Mathf.Clamp(CamRot.y, -80, 80);
            Camera.main.transform.localRotation = Quaternion.Euler(CamRot.y, 0, 0);
            transform.rotation = Quaternion.Euler(0, CamRot.x, 0); // changed to rotation only, cause localrotation causes issues when setting player parent
        }

        IEnumerator SubscribeToEventsDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            CursorStateManager.Instance.LockCursorState();
            input.Enable();
            input.Player.Movement.performed += OnMovementPerformed;
            input.Player.Movement.canceled += OnMovementCancelled;
            input.Player.Run.performed += OnRunCalled;
            input.Player.Run.canceled += OnRunCancelled;
        }
    }
}

