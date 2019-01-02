using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class SetBool_OnExit : SetBool
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.SetBool(paramName, value);
    }
}