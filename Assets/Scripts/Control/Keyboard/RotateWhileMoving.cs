using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Tamana
{
    public class RotateWhileMoving : MonoBehaviour
    {
        public static RotateWhileMoving instance { private set; get; }
        public bool isRotatingTowardCameraDirection;
        public float lookSpeed;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            RotatePlayerTowardCameraDirection();
        }

        private void RotatePlayerTowardCameraDirection()
        {
            if (!isRotatingTowardCameraDirection)
                return;

            var camDir = GM.MainCamera.transform.forward;
            camDir.y = 0;

            var lookRotation = Quaternion.LookRotation(camDir);

            GM.player.transform.rotation = Quaternion.Slerp(GM.player.transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
        }

    }
}
