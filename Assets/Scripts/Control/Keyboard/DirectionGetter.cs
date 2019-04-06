using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class DirectionGetter : MonoBehaviour
    {
        public Transform player;
        public Camera mainCamera;

        public enum Direction { Left, Right, Forward, Backward, ForwardLeft, ForwardRight, BackwardLeft, BackwardRight }
        public Direction movingDirection;

        public static DirectionGetter instance { private set; get; }

        private void Awake()
        {
            instance = this;            
        }
        private void OnValidate()
        {
            player = GameObject.FindWithTag("Player").transform;

            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        void Update()
        {
            GetDirection();
        }        

        private void GetDirection()
        {
            var playerForwardDirection = player.forward;
            playerForwardDirection.y = 0;

            var cameraForwardDirection = mainCamera.transform.forward;
            cameraForwardDirection.y = 0;

            var angle = Vector3.SignedAngle(cameraForwardDirection, playerForwardDirection, Vector2.up);

            if(angle > -22.5f && angle <= 22.5f)
            {
                movingDirection = Direction.Forward;
            }
            else if(angle > 22.5f && angle <= 67.5f)
            {
                movingDirection = Direction.ForwardLeft;
            }
            else if(angle > 67.5f && angle <= 112.5f)
            {
                movingDirection = Direction.Left;
            }
            else if(angle > 112.5f && angle <= 157.5f)
            {
                movingDirection = Direction.BackwardLeft;
            }
            else if(angle > 157.5f || angle <= -157.5f)
            {
                movingDirection = Direction.Backward;
            }
            else if(angle <= -112.5f && angle > -157.5f)
            {
                movingDirection = Direction.BackwardRight;
            }
            else if(angle <= -67.5f && angle > -112.5f)
            {
                movingDirection = Direction.Right;
            }
            else if(angle <= -22.5f && angle > -67.5f)
            {
                movingDirection = Direction.ForwardRight;
            }
        }
    }
}
