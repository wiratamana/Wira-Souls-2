using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tamana
{
    public class Attacking : MonoBehaviour
    {
        private Controller controller;
        private Animator animator;
        private AnimatorParameter animParam;
        private Collider myCollider;
        private BoxCollider swordCollider;

        private Transform target;

        public bool canDoConsecutiveAttack;

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
                if (!animParam.isSword1h_Equip || (!canDoConsecutiveAttack && animParam.isAttacking))
                    return;

                if (canDoConsecutiveAttack)
                    DoConsecutiveAttack();
                else animator.Play(AnimName.SwordAnimsetPro.Sword1h_Attacks.i01_Attack_Move_slow_L_1);
                animParam.isAttacking = true;
                animParam.isMoving = false;
                controller.StopRotating();
                target = GetTargetWithSmallestAngle();

                if (target != null)
                    StartCoroutine(RotateTowardTarget());
            }
        }

        public void DoConsecutiveAttack()
        {
            StopAllCoroutines();
            canDoConsecutiveAttack = false;
            animParam.canDoConsecutiveAttack = true;
        }

        public void SetSwordCollider(Transform swordTransform) => swordCollider = swordTransform.GetComponent<BoxCollider>();

        private Transform GetTargetWithSmallestAngle()
        {
            var cols = Physics.OverlapSphere(transform.position, 3.0f, GM.LayerUnit);

            if (cols.Length == 0)
                return null;

            Transform nearestTarget = null;
            float angleToNearestTarget = 181.0f;

            foreach (Collider c in cols)
            {
                var signedAngle = Vector3.Angle(transform.forward, (c.transform.position - transform.position).normalized);
                if (c == myCollider)
                    continue;
                else if (signedAngle < angleToNearestTarget)
                {
                    angleToNearestTarget = signedAngle;
                    nearestTarget = c.transform;
                }
            }

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

        List<AI.Dummy> dummies = new List<AI.Dummy>();
        AI.Dummy tempDummy;
        private AI.Dummy[] GetTargets()
        {
            var cols = Physics.OverlapBox(swordCollider.transform.position, swordCollider.bounds.extents,
                swordCollider.transform.rotation, GM.LayerUnit);

            if (cols.Length == 0)
                return null;

            dummies.Clear();
            tempDummy = null;

            for (int i = 0; i < cols.Length; i++)
            {
                tempDummy = cols[i].GetComponent<AI.Dummy>();
                if (tempDummy == null) continue;
                dummies.Add(tempDummy);
            }

            return dummies.ToArray();
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