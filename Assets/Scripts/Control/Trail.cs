using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Trail
    {
        public Transform transform { private set; get; }
        public Vector3 rayHitPoint { get; set; }
        public bool isTraversable { get; set; }

        /// <summary>
        /// The world space position of the Transform.
        /// </summary>
        public Vector3 position { get { return transform.position; } }
        /// <summary>
        /// Position of the transform relative to the parent transform.
        /// </summary>
        public Vector3 localPosition { get { return transform.localPosition; } }

        public Trail(Transform transform)
        {
            this.transform = transform;
        }

        public void SetRayHitPoint(Vector3 hitPoint)
        {
            rayHitPoint = hitPoint;
            isTraversable = true;
        }
    }
}

