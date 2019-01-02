using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public abstract class SetBool : StateMachineBehaviour
    {
        [SerializeField] protected string paramName;
        [SerializeField] protected bool value;
    }
}

