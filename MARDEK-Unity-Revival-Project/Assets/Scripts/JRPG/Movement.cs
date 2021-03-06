using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //[SerializeField] bool gridMovement = true;
    [SerializeField] float movementSpeed = 1f;

    public bool isMoving { get; private set; }
    Vector2 targetPosition = Vector2.zero;
    Queue<Vector2> queuedMoves = new Queue<Vector2>();

    public void EnqueueMoves(List<MoveDirection> moves)
    {
        foreach(MoveDirection m in moves)
            queuedMoves.Enqueue(m.value);
        UpdateMoveState();
    }

    public void Move(MoveDirection direction)
    {
        queuedMoves.Enqueue(direction.value); 
        UpdateMoveState();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            isMoving = !MoveToFixed(transform, targetPosition, movementSpeed);
        }
        UpdateMoveState();
    }

    void UpdateMoveState()
    {
        if (isMoving == false)
        {
            if (queuedMoves.Count > 0)
            {
                targetPosition = (Vector2)transform.position + queuedMoves.Dequeue();
                isMoving = true;
            }
        }
    }

    bool MoveToFixed(Transform transform, Vector2 targetPosition, float movementSpeed)
    {
        Vector2 positionDifferece = new Vector2(targetPosition.x, targetPosition.y) - (Vector2)transform.position;
        if (positionDifferece == Vector2.zero)
        {
            return true;
        }
        Vector2 increment = positionDifferece.normalized * Time.fixedDeltaTime * movementSpeed;
        if (increment.sqrMagnitude < positionDifferece.sqrMagnitude)
        {
            transform.position = ((Vector2)transform.position + increment);
            return false;
        }
        else
        {
            //end movement
            transform.position = (targetPosition);
            return true;
        }
    }

}
