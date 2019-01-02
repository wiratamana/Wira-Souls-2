using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimPlay : StateMachineBehaviour
    {
        [SerializeField] private string animName;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.Play(animName);
    }
}