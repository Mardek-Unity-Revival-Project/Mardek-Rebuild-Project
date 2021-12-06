using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP
{
    public class CameraController : MonoBehaviour
    {
        static CameraController instance = null;

        [SerializeField] GameObject followTarget = null;

        float zOffset = -100;
        Vector3 lastPosition = Vector3.zero;

        private void Awake()
        {
            instance = this;
            zOffset = transform.position.z;
        }

        private void Start()
        {
            MoveCameraXY(followTarget.transform.position);
            lastPosition = followTarget.transform.position;
        }

        private void LateUpdate()
        {
            Vector3 camPos = transform.position;
            Vector3 targetPos = followTarget.transform.position;
            Vector3 targetDelta = targetPos - lastPosition;

            if (targetPos.x > camPos.x)
            {
                if (lastPosition.x <= camPos.x)
                    camPos.x = targetPos.x;
                else if (targetDelta.x > 0)
                    camPos.x += targetDelta.x;
            }
            else if (targetPos.x < camPos.x)
            {
                if (lastPosition.x >= camPos.x)
                    camPos.x = targetPos.x;
                else if (targetDelta.x < 0)
                    camPos.x += targetDelta.x;
            }

            if (targetPos.y > camPos.y)
            {
                if(lastPosition.y <= camPos.y)
                    camPos.y = targetPos.y;
                else if(targetDelta.y > 0)
                    camPos.y += targetDelta.y;
            }
            else if (targetPos.y < camPos.y)
            {
                if (lastPosition.y >= camPos.y)
                    camPos.y = targetPos.y;
                else if (targetDelta.y < 0)
                    camPos.y += targetDelta.y;
            }

            MoveCameraXY(camPos);
            lastPosition = targetPos;
        }



        void MoveCameraXY(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, zOffset);
        }
    } 
}
