using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class StrafeMovement : MonoBehaviour
    {
        private AnimatorParameter animParam { get { return GM.animParam; } }
        private Animator animator;

        private Transform playerTransform { get { return GM.PlayerController.transform; } }
        private Transform analogTransform { get { return GM.PlayerController.analogLeft; } }
        private Transform target;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayeStrafeMovement()
        {
            bool result;
            var signedAngle = GetSignedAngle(out result);
            if (result == false) return;


        }

        private float GetSignedAngle(out bool isSuccess)
        {
            if(target == null)
            {
                isSuccess = false;
                return -1.0f;
            }

            var dirToAnalog = (analogTransform.position - transform.position).normalized;
            var forward = transform.forward;
            dirToAnalog.y = 0;
            forward.y = 0;

            isSuccess = true;
            return Vector3.SignedAngle(forward, dirToAnalog, Vector3.up);
        }

        public void StartStrafe()
        {
            animParam.isStrafing = true;

            PlayStartStrafeMovement();
        }

        private void PlayStartStrafeMovement()
        {
            var cols = GM.GetNearbyUnits(5);
            if (cols == null) return;

            var nearestUnit = GetNearestUnit(cols);

            var directionToTarget = (nearestUnit.position - transform.position).normalized;
            directionToTarget.y = 0;
            var forward = transform.forward;
            forward.y = 0;

            var signedAngle = Vector3.SignedAngle(forward, directionToTarget, Vector3.up);
            var animationName = GetAnimationName(signedAngle);

            if (animationName == string.Empty)
                return;

            GM.PlayerController.StopRotating();
            animParam.isCannotMove = true;
            animParam.isMoving = false;
            animator.Play(animationName);
            target = nearestUnit;
            StartCoroutine(UpdateDirectionToAnalog());
        }

        private Transform GetNearestUnit(Collider[] colliders)
        {
            float distance = 10000.0f;
            Transform targetUnit = null;

            for(int i = 0; i < colliders.Length; i ++)
            {
                float magnitude = (colliders[i].transform.position - transform.position).sqrMagnitude;
                if (magnitude < distance)
                {
                    distance = magnitude;
                    targetUnit = colliders[i].transform;
                }
            }

            return targetUnit;
        }

        private string GetAnimationName(float signedAngle)
        {
            bool forward = signedAngle >= -45 && signedAngle <= 45;
            bool left = signedAngle < -45 && signedAngle >= -135;
            bool right = signedAngle > 45 && signedAngle <= 135;
            bool backLeft = signedAngle < -135;

            if (forward)
                return string.Empty;
            else if (left)
                return AnimName.SwordAnimsetPro.Sword1h_Walks.i04_Sword1h_TurnL_90;
            else if (right)
                return AnimName.SwordAnimsetPro.Sword1h_Walks.i05_Sword1h_TurnR_90;
            else if (backLeft)
                return AnimName.SwordAnimsetPro.Sword1h_Walks.i06_Sword1h_TurnL_180;
            else return AnimName.SwordAnimsetPro.Sword1h_Walks.i07_Sword1h_TurnR_180;
        }

        public void LookTarget() => StartCoroutine(RotateTowardTarget());


        private IEnumerator RotateTowardTarget()
        {
            while(true)
            {
                if (target == null)
                {
                    break;
                }

                var directionTowardTarget = (target.position - transform.position).normalized;
                directionTowardTarget.y = 0;
                var forward = transform.forward;
                forward.y = 0;

                Quaternion lookRotation = Quaternion.LookRotation(directionTowardTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 3 * Time.deltaTime);

                var angle = Vector3.Angle(forward, directionTowardTarget);
                if (angle < 5)
                {
                    break;
                }

                yield return null;
            }
        }

        private IEnumerator UpdateDirectionToAnalog()
        {
            StartCoroutine(StopStrafingWhenTargetIsTooFar());
            while (true)
            {
                if (!animParam.isStrafing || target == null)
                {
                    animParam.dirToAnalogX = 0;
                    animParam.dirToAnalogY = 0;
                    animParam.isStrafing = false;
                    target = null;
                    StopAllCoroutines();
                    break;
                }

                var camPos = GM.MainCamera.transform.position;
                var camForward = camPos + GM.MainCamera.transform.forward;
                var camRight = camPos + GM.MainCamera.transform.right;
                var lengthForward = (camForward - camPos).sqrMagnitude;
                var lengthRight = (camRight - camPos).sqrMagnitude;

                animParam.dirToAnalogX = lengthRight * PS4.GetAxis(PS4.AxisName.AnalogLeft_X);
                animParam.dirToAnalogY = lengthForward * PS4.GetAxis(PS4.AxisName.AnalogLeft_Y);

                var dirToTarget = (target.position - playerTransform.position).normalized;
                playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation,
                    Quaternion.LookRotation(dirToTarget), 3 * Time.deltaTime);               

                yield return null;
            }
        }

        private float distance = 15;
        private IEnumerator StopStrafingWhenTargetIsTooFar()
        {
            while(true)
            {
                if (!animParam.isStrafing || target == null)
                    break;

                var sqrLength = (target.position - playerTransform.position).sqrMagnitude;
                if (sqrLength > distance * distance)
                {
                    animParam.isStrafing = false;
                    target = null;
                    StopAllCoroutines();
                    break;
                }

                yield return GM.waitForHalfSecond;
            }
        }
    }
}