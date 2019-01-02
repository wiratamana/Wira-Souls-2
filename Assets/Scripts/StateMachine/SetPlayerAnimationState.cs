using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class SetPlayerAnimationState : StateMachineBehaviour
    {
        [SerializeField] private StateAnimation state;
        [SerializeField] private Controller.AnimationState value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(state == StateAnimation.OnStateEnter)
                animator.GetComponent<Controller>().SetAnimationState(value);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (state == StateAnimation.OnStateExit)
                animator.GetComponent<Controller>().SetAnimationState(value);
        }
    }
}

