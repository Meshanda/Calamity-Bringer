using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    
#region Movement
    [Space(5)]
    [Header("Movement Field")]

    [Tooltip("Max Walk speed of our character")]
    [SerializeField] private float _walkSpeed;

    private Vector3 _horizontalAxis;

    private float _speed;
#endregion
    
#region Gravity
    [Space(5)]
    [Header("Gravity Field")]
	    
    [Tooltip("Gravity applied when the character fall (negative value)")]
    [SerializeField] protected float Gravity;

    [Tooltip("Max fall speed of our character")]
    [SerializeField] private float MaxVelocity;
	    
    [Tooltip("transform position for checking the ground")]
    [SerializeField] private Transform SpherePosition;
    
    [Tooltip("How far the ground is (match the radius of the character controller)")]
    [SerializeField] private float GroundRadius;

    [Tooltip("What is ground ? where the character can walk on")]
    [SerializeField] private LayerMask GroundLayers;
    
    private float _verticalVelocity;
    
    protected bool _grounded;
    
    
#endregion
    
#region Jump
    [Space(5)]
    [Header("JumpField")]

    [Tooltip("How high the character can jump")]
    [SerializeField] protected float JumpHeight;
    
    private bool IsJumping = false;
#endregion

    private CharacterController _controller;
    private GameObject _mainCamera;
    [SerializeField] private Animator _animator;


    private void Awake()
    {
        if (Camera.main is not null) _mainCamera = Camera.main.gameObject;
        _controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        GravityField();
        Move();
    }
    
    protected void FixedUpdate()
    {
        GroundChecker();
    }
    
    private void Move()
    {
        if (_horizontalAxis == Vector3.zero)
            return;
		
		
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _walkSpeed;
        _speed = targetSpeed;
		
        //normalise input direction
        Vector3 inputDirection = _horizontalAxis.normalized;
        
		
        float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed  * Time.deltaTime));
    }
    
    private void GroundChecker()
    {
        _grounded = Physics.CheckSphere(SpherePosition.position, GroundRadius, GroundLayers);
    }
    
    private void GravityField()
    {
        Debug.Log(_controller.isGrounded);
        if (_controller.isGrounded)
        {
            //stop the infinite fall
            if (_verticalVelocity < -1f)
            {
                _verticalVelocity = -1f;
            }
            IsJumping = false;
        }
        else
        {
            if(_verticalVelocity > MaxVelocity)
                _verticalVelocity += Gravity * Time.deltaTime;
        }

        _controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    
#region InputsCall

    public void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
		
        _horizontalAxis = new Vector3(move.x, 0, move.y);

    }

    public void OnJump()
    {
        if (!_grounded) return;

        IsJumping = true;
        _verticalVelocity = (float)Math.Sqrt(Gravity * -2f * JumpHeight);
    }
    
#endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = _grounded ? Color.green : Color.red;
        
        Gizmos.DrawSphere(SpherePosition.position, GroundRadius);
    }
}