using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Attacking : MonoBehaviour
    {
        private Controller controller;
        private Animator animator;
        private AnimatorParameter animParam;
        private Collider myCollider;

        private Transform target;

        private void Start()
        {
            controller = GetComponent<Controller>();
            animator = controller.animator;
            animParam = controller.animParam;
            myCollider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (PS4.GetButtonDown(PS4.ButtonName.Triangle))
            {
                if (animParam.isAttacking || !animParam.isSword1h_Equip)
                    return;

                animator.Play(AnimName.SwordAnimsetPro.Sword1h_Attacks.i01_Attack_Move_slow_L_1);
                animParam.isAttacking = true;
                animParam.isMoving = false;
                controller.StopRotating();
                target = GetTargetWithSmallestAngle();

                if (target != null)
                    StartCoroutine(RotateTowardTarget());
            }
        }

        private Transform GetTargetWithSmallestAngle()
        {
            var cols = Physics.OverlapSphere(transform.position, 3.0f, GM.LayerUnit);

            if (cols.Length == 0)
                return null;

            Transform nearestTarget = null;
            int angleToNearestTarget = 181;

            foreach (Collider c in cols)
                if (c == myCollider)
                    continue;
                else if (Vector3.Angle(transform.forward, (c.transform.position - transform.position).normalized) < angleToNearestTarget)
                    nearestTarget = c.transform;

            return nearestTarget;
        }

        public void DealDamage()
        {
            var targets = GetTargets();

            if (targets != null)
                foreach (AI.Dummy s in targets)
                {
                    s.TakeDamage(transform);
                }
        }

        private AI.Dummy[] GetTargets()
        {
            var cols = Physics.OverlapSphere(GM.PlayerController.bodyPart.dummyNeck.position + GM.PlayerController.transform.forward,
                .5f, GM.LayerUnit);

            if (cols.Length == 0)
                return null;

            AI.Dummy[] statuses = new AI.Dummy[cols.Length];

            for (int i = 0; i < cols.Length; i++)
                statuses[i] = cols[i].GetComponent<AI.Dummy>();

            return statuses;
        }

        private IEnumerator RotateTowardTarget()
        {
            while(true)
            {
                if (target == null || !animParam.isAttacking)
                    break;

                var dir = (target.transform.position - transform.position).normalized;
                var angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);
                if (angle < 5 && angle > -5) break;

                dir.y = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 3 * Time.deltaTime);

                yield return null;
            }
        }
    }
}