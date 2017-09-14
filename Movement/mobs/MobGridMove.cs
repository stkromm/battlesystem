using UnityEngine;

class MobGridMove : MyMonoBehaviour
{
    private const float MoveSpeed = 10f;
    private const float GridSize = 16f;
    private Vector2 _input;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _t;
    private int _i;
    public Vector2[] Path = new Vector2[5];
    private readonly Move _move = new Move();
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        _move.Stop = true;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        _move.Stop = false;

    }

    public void Update()
    {
        if (_animator.GetInteger("Direction") != _move.Direction)
            _animator.SetInteger("Direction", _move.Direction);
        _input = Path[_i];
        if (_move.IsMoving)
        {
            return;
        }
        _animator.SetInteger("Direction", _move.Direction);
        if (_i < Path.Length - 1)
            _i++;
        else
            _i = 0;
        if (_input != Vector2.zero)
        {

            StartCoroutine(_move.MoveTransform(transform, _input, GridSize, MoveSpeed, null));
        }
        else
        {
            if (_animator.GetInteger("Direction") > 0)
                _animator.SetInteger("Direction", _animator.GetInteger("Direction") * (-1));
        }
    }

}