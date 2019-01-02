using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AnimCrossFade : StateMachineBehaviour
    {
        [SerializeField] private string animName;
        [SerializeField] private float time;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.CrossFade(animName, time);
    }
}
