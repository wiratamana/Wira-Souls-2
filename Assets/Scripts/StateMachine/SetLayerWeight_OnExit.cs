using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class SetLayerWeight_OnExit : SetLayerWeight
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetLayerWeight(animator.GetLayerIndex(layerName), weight);
        }
    }
}