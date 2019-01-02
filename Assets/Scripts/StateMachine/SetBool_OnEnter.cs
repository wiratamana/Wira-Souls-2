using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class SetBool_OnEnter : SetBool
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.SetBool(paramName, value);
    }
}