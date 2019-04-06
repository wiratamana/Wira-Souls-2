using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimatorEventReceiver : MonoBehaviour
    {
        public void StartRotating() { RotateWhileMoving.instance.isRotatingTowardCameraDirection = true; }
        public void StopRotating() { RotateWhileMoving.instance.isRotatingTowardCameraDirection = false; }
    }
}
