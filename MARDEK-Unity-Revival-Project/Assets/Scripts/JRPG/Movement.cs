using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] bool gridMovement = true;
    [SerializeField] float movementSpeed = 1f;

    bool isMoving = false;
    Vector2 targetPosition = Vector2.zero;
    Queue<Vector2> queuedMoves = new Queue<Vector2>();

    public void Move(Vector2 direction)
    {
        queuedMoves.Enqueue(direction);
    }

    private void FixedUpdate()
    {
        if (isMoving == false)
        {
            if(queuedMoves.Count > 0)
            {
                targetPosition = (Vector2)transform.position + queuedMoves.Dequeue();
                isMoving = true;
            }
        }
        if (isMoving)
        {
            isMoving = !MoveToFixed(transform, targetPosition, movementSpeed);
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
