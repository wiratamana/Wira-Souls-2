using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class SetLayerWeight_OnEnter : SetLayerWeight
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetLayerWeight(animator.GetLayerIndex(layerName), weight);
        }
    }
}