using UnityEngine;
using System.Collections;

namespace Tamana
{
    public abstract class SetLayerWeight : StateMachineBehaviour
    {
        [Range(0.0f, 1.0f)]
        [SerializeField] protected float weight;
        [SerializeField] protected string layerName;
    }
}
