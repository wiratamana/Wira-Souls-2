using UnityEngine;
using System.Collections;

namespace Tamana.AI
{
    public class Dummy : MonoBehaviour
    {
        private Animator animator;
        private Status status;
        private void Start()
        {
            animator = GetComponent<Animator>();
            status = GetComponent<Status>();
        }

        public void TakeDamage(Transform damageSourceTransform)
        {
            var dir = (damageSourceTransform.position - transform.position).normalized;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);

            animator.Play(AnimName.SwordAnimsetPro.Sword1h_HitsDeaths.i11_Sword1h_Hit_Torso_Front);
        }
    }
}