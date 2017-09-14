using UnityEngine;

class PlayerMovementController : MyMonoBehaviour
{
    private float _moveSpeed = 40f;
    private float _gridSize = 16f;
    private Vector2 _input;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _t;
    private readonly Move _move = new Move();
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        // Strange bug, but without it doesnt _move ?!?!?
        transform.Translate(0f, 0f, 0f);
    }

    public void Update()
    {
        _moveSpeed = Input.GetButton("Sprint") ? 100 : 40;
        if (_animator.GetInteger("Direction") != _move.Direction)
            _animator.SetInteger("Direction", _move.Direction);
        if (_move.IsMoving)
        {
            return;
        }
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Mathf.Abs(_input.x) > Mathf.Abs(_input.y))
        {
            _input.y = 0;
        }
        else
        {
            _input.x = 0;
        }
        if (_input != Vector2.zero)
        {
            if (_gridSize < 16)
            {
                _gridSize = _gridSize * 2;
            }
            _animator.SetInteger("Action", _moveSpeed == 100 ? 3 : 1);
            StartCoroutine(_move.MoveTransform(transform, _input, _gridSize, _moveSpeed, GetComponent<Rigidbody2D>()));
        }
        else
        {
            _gridSize = 1;
            _animator.SetInteger("Action", 0);
        }
    }
}