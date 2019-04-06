using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class ForwardMover : MonoBehaviour
    {
        public Transform player;
        public Animator animator;

        public float turnSpeed;

        public static ForwardMover instance { private set; get; }

        private void Awake()
        {
            instance = this;
        }

        private void OnValidate()
        {
            player = GameObject.FindWithTag("Player").transform;
            animator = player.GetComponent<Animator>();
        }

        private void Update()
        {
            StartMoving();
        }

        private void StartMoving()
        {
            if (!Input.GetKey(KeyCode.W))
            {
                AnimatorParameter.instance.isLeftAnalogMoving = false;
                return;
            }
            else
            {
                AnimatorParameter.instance.isLeftAnalogMoving = true;
            }

            if (AnimatorParameter.instance.isMoving)
                return;

            AnimatorParameter.instance.isMoving = true;

            StartToPlayRunningAnimation();
        }
        private void StartToPlayRunningAnimation()
        {
            var direction = DirectionGetter.instance.movingDirection;
            switch (direction)
            {
                case DirectionGetter.Direction.Left:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart90_L);
                    break;
                case DirectionGetter.Direction.Right:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart90_R);
                    break;
                case DirectionGetter.Direction.Forward:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart);
                    break;
                case DirectionGetter.Direction.Backward:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart180_L);
                    break;
                case DirectionGetter.Direction.ForwardLeft:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart);
                    break;
                case DirectionGetter.Direction.ForwardRight:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart);
                    break;
                case DirectionGetter.Direction.BackwardLeft:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart180_L);
                    break;
                case DirectionGetter.Direction.BackwardRight:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart180_R);
                    break;
            }
        }

        private enum Direction { Left, Right }
        private void RotateCameraOnKeyboardPress(KeyCode keyCode, Direction direction)
        {
            if (!AnimatorParameter.instance.isMoving)
                return;

            if(Input.GetKey(keyCode))
            {
                var mainCamera = GM.MainCamera.transform;

                var euler = mainCamera.eulerAngles;
                var rot = mainCamera.rotation;

                var turningDirection = Vector3.zero;
                turningDirection.y = direction == Direction.Left ? turnSpeed : -turnSpeed;

                rot = Quaternion.Euler(euler + turningDirection);

                var right = mainCamera.right;
                right.y = 0;

                mainCamera.rotation = Quaternion.RotateTowards(mainCamera.rotation, Quaternion.LookRotation(right), turnSpeed * Time.deltaTime);
            }
        }
    }
}
