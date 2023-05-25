using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioClip _playerStep;
    
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
    
#endregion

#region Rotation
    [Space(5)]
    [Header("Rotation field")]

    [Range(0.0f, 0.3f)]
    [Tooltip("How fast the character face the direction")]
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private Transform _toRotate;

    private float _rotationVelocity;
#endregion

    private CharacterController _controller;
    private GameObject _mainCamera;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _drawGizmos;
    
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Victory = Animator.StringToHash("Victory");
    
    private bool _gameFinished;
    private bool _gamePaused;


    private void OnEnable()
    {
        TimerSystem.TimerFinished += OnVictory;
        PauseSystem.GamePaused += OnGamePaused;
        GameManager.GameUnpaused += OnGameUnpaused;
    }
    private void OnDisable()
    {
        TimerSystem.TimerFinished -= OnVictory;
        PauseSystem.GamePaused -= OnGamePaused;
        GameManager.GameUnpaused -= OnGameUnpaused;
    }

    private void OnGameUnpaused()
    {
        _gamePaused = false;
    }

    private void OnGamePaused()
    {
        _gamePaused = true;
    }

    private void Awake()
    {
        if (Camera.main is not null) _mainCamera = Camera.main.gameObject;
        _controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (_gameFinished || _gamePaused) return;
        
        GravityField();
        Rotate();
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
        
        Vector3 inputDirection = _horizontalAxis.normalized;
		
        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        float zDirection = inputDirection.z;
        float xDirection = inputDirection.x;

        float rotationAngle = Mathf.Atan2(xDirection, zDirection) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
		
        //normalise input direction
        Vector3 targetDirection = Quaternion.Euler(0.0f, rotationAngle, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed  * Time.deltaTime));
    }
    
    private void Rotate()
    {
        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
      
        float rotationAngle = _mainCamera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(_toRotate.eulerAngles.y, rotationAngle, ref _rotationVelocity, _rotateSpeed);
        _toRotate.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
    
    private void GroundChecker()
    {
        _grounded = Physics.CheckSphere(SpherePosition.position, GroundRadius, GroundLayers);
    }
    
    private void GravityField()
    {
        if (_controller.isGrounded)
        {
            //stop the infinite fall
            if (_verticalVelocity < -1f)
            {
                _verticalVelocity = -1f;
            }
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
        if (_gameFinished || _gamePaused) return;

        Vector2 move = value.Get<Vector2>();
		
        _horizontalAxis = new Vector3(move.x, 0, move.y);

        _animator.SetFloat(X, move.x);
        _animator.SetFloat(Y, move.y);
    }

    public void OnJump()
    {
        if (!_grounded || _gameFinished || _gamePaused) return;
        
        _animator.SetTrigger(Jump);
    }

    private void OnVictory()
    {
        _gameFinished = true;
            
        _animator.SetTrigger(Victory);
    }

    #endregion

    private void OnDrawGizmos()
    {
        if (!_drawGizmos) return;
        Gizmos.color = _grounded ? Color.green : Color.red;
        
        Gizmos.DrawSphere(SpherePosition.position, GroundRadius);
    }

    public void PlayerStep()
    {
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Sound, _playerStep);
        //TODO: camera shake
    }
}
