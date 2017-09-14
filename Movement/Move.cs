using UnityEngine;
using System.Collections;

public class Move
{
    public int Direction = 1;
    public bool IsMoving = false;
    public bool Stop = false;
    Vector3 _startPosition;
    Vector3 _endPosition;
    private float _t;

    public IEnumerator MoveTransform(Transform transform, Vector2 input, float gridSize, float moveSpeed, Rigidbody2D rigid)
    {
        if ((int)Time.timeScale != 0)
        {
            IsMoving = true;
            _startPosition = transform.position;
            _t = 0;
            _endPosition = new Vector2(_startPosition.x + System.Math.Sign(input.x) * gridSize,
                       _startPosition.y + System.Math.Sign(input.y) * gridSize);

            if (input.x > 0)
                Direction = 1;
            else if (input.x < 0)
                Direction = 2;
            if (input.y > 0)
                Direction = 4;
            else if (input.y < 0)
                Direction = 3;
            while (_t < 1f)
            {
                if (!Stop)
                {
                    _t += Time.deltaTime * (moveSpeed / gridSize);
                    if (rigid == null)
                    {
                        var vec = Vector3.Lerp(_startPosition, _endPosition, _t);
                        vec.z = _startPosition.z;
                        transform.position = vec;
                    }
                    else
                    {
                        rigid.MovePosition(Vector2.Lerp(_startPosition, _endPosition, _t));
                    }
                }
                yield return null;
            }
            IsMoving = false;
        }
        yield return 0;

    }
}
