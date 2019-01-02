using System.Collections;
using UnityEngine;

namespace Tamana
{
    public class ControllerPS4 : Controller
    {
        public Transform analogLeft { private set; get; }
        public bool isAnalogLeftMoved { get { return animParam.isLeftAnalogMoving; } }

        protected override void Awake()
        {
            base.Awake();

            InstantiateAnalog();
        }

        protected override void Start()
        {
            base.Start();

            thirdPersonCamera.followSpeed = 10;
            thirdPersonCamera.rotationSpeed = 360;
            thirdPersonCamera.lookSpeed = 10;
        }

        protected override void Update()
        {
            base.Update();

            thirdPersonCamera.SetEulerX(PS4.GetAxis(PS4.AxisName.AnalogRight_Y) * thirdPersonCamera.rotationSpeed * Time.deltaTime);
            thirdPersonCamera.SetEulerY(PS4.GetAxis(PS4.AxisName.AnalogRight_X) * thirdPersonCamera.rotationSpeed * Time.deltaTime);

            DoMovementControl();
            MovePathTrail(analogLeft, isAnalogLeftMoved);

            equipHolster.DoEquipHolster(PS4.ButtonName.Square);
        }

        private void OnDrawGizmos()
        {
            if(UnityEditor.EditorApplication.isPlaying)
            {
                Gizmos.DrawWireSphere(analogLeft.position, 0.2f);

                Gizmos.color = Color.red;

                for (int i = 0; i < trails.Length; i++)
                    Gizmos.DrawWireSphere(trails[i].rayHitPoint, 0.2f);
            }
        }


        private void InstantiateAnalog()
        {
            analogLeft = new GameObject("AnalogLeft").transform;
            analogLeft.position = GM.PlayerController.transform.position;
        }

        private void DoMovementControl()
        {
            if (!isAbleToMove)
                return;

            var analogLeftX = PS4.GetAxis(PS4.AxisName.AnalogLeft_X);
            var analogLeftY = PS4.GetAxis(PS4.AxisName.AnalogLeft_Y);

            if (analogLeftX == 0 && analogLeftY == 0)
            {
                animParam.isLeftAnalogMoving = false;
                return;
            }

            animParam.isLeftAnalogMoving = true;

            var camdir = camDir;
            camdir.y = 0;

            analogLeft.rotation = Quaternion.LookRotation(camdir);
            analogLeft.position = playerPos + (analogLeft.forward * analogLeftY) + (analogLeft.right * analogLeftX);

            animParam.AnalogLeftX = analogLeft.localPosition.x;
            animParam.AnalogLeftY = analogLeft.localPosition.z;

            if (!animParam.isMoving && distanceFromFurthestTail > 3)
            {
                switch (animationState)
                {
                    case AnimationState.RunAnimsetBasic:
                        Move_RunAnimsetBasic(GetMovingDirection_RunAnimsetBasic());
                        break;
                    case AnimationState.SwordAnimsetPro:
                        Move_SwordAnimsetPro(GetMovingDirection_SwordAnimsetPro());
                        break;
                    case AnimationState.LongswordAnimsetPro:
                        Move_LongswordAnimsetPro(GetMovingDirection_RunAnimsetBasic());
                        break;
                }

                animParam.isMoving = true;
            }

            RotateTowardTrail(analogLeft);
        }

        public MovingDirection GetMovingDirection_RunAnimsetBasic()
        {
            var directionToAnalog = (analogLeft.position - playerTransform.position).normalized;
            var signedAngle       = Vector3.SignedAngle(playerTransform.forward, directionToAnalog, Vector3.up);

            bool forward = signedAngle > -40 && signedAngle < 40;
            bool right   = signedAngle >= 40 && signedAngle < 140;
            bool left    = signedAngle <= -40 && signedAngle > -140;
            bool back_right = signedAngle > 140;

            if        (forward)      return MovingDirection.Forward;
            else if   (right)        return MovingDirection.Right;
            else if   (left)         return MovingDirection.Left;
            else if   (back_right)   return MovingDirection.Back_Right180;
            else                     return MovingDirection.Back_Left180;
        }

        public MovingDirection GetMovingDirection_SwordAnimsetPro()
        {
            var directionToAnalog = (analogLeft.position - playerTransform.position).normalized;
            var signedAngle = Vector3.SignedAngle(playerTransform.forward, directionToAnalog, Vector3.up);

            bool forward = signedAngle >= -70 && signedAngle <= 70;
            bool right = signedAngle > 70 && signedAngle <= 100;
            bool left = signedAngle < -70 && signedAngle >= -100;
            bool back_right135 = signedAngle > 100 && signedAngle <= 150;
            bool back_left135 = signedAngle < -100 && signedAngle >= -150;
            bool back_right_180 = signedAngle > 150;

            if (forward) return MovingDirection.Forward;
            else if (right) return MovingDirection.Right;
            else if (left) return MovingDirection.Left;
            else if (back_right135) return MovingDirection.Back_Right135;
            else if (back_left135) return MovingDirection.Back_Left135;
            else if (back_right_180) return MovingDirection.Back_Right180;
            else return MovingDirection.Back_Left180;
        }

        private void DebugButton(PS4.ButtonName buttonName)
        {
            if (PS4.GetButton(buttonName)) Debug.Log(buttonName.ToString());
        }
        private void DebugAxis(PS4.AxisName axisName)
        {
            var axis = PS4.GetAxis(axisName);
            if (axis > 0.1f || axis < -0.1f) Debug.Log(axisName.ToString() + " : " + axis);
        }
    }
}

