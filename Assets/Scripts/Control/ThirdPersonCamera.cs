using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [Header("Settings")]
        public float followSpeed;
        public float rotationSpeed;
        public float lookSpeed;

        public float AxisX_MAX = 89;
        public float AxisX_MIN = -30;

        public Transform center { private set; get; }
        public Transform offset { private set; get; }

        private Vector3 spinePosition { get { return GM.PlayerController.bodyPart.spine.position; } }
        private Vector3 camToCenter { get { return (center.position - GM.MainCamera.transform.position).normalized; } }
        private Vector3 camPos
        {
            get { return GM.MainCamera.transform.position; }
            set { GM.MainCamera.transform.position = value; }
        }
        private Quaternion camRot
        {
            get { return GM.MainCamera.transform.rotation; }
            set { GM.MainCamera.transform.rotation = value; }
        }
        private Quaternion lookRot { get { return Quaternion.LookRotation(camToCenter); } }

        private float eulerX = 0;
        private float eulerY = 0;

        private void Awake()
        {
            center = new GameObject("Center").transform;
            center.position = spinePosition;

            offset = new GameObject("Offset").transform;
            offset.SetParent(center);
            offset.localPosition = Vector3.zero + new Vector3(0, 0, -3);

            GM.MainCamera.transform.SetParent(offset);
            camPos = offset.position;
            camRot = lookRot;            
        }

        private void Update()
        {
            center.position = Vector3.Lerp(center.position, spinePosition, followSpeed * Time.deltaTime);
            camRot = Quaternion.Slerp(camRot, lookRot, lookSpeed * Time.deltaTime);

            center.transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }

        public void SetEulerX(float value)
        {
            if (value > 0.1)
                eulerX = Mathf.MoveTowards(eulerX, AxisX_MAX, value);
            else if(value < -0.1)
            {
                var a = Mathf.Abs(value);
                eulerX = Mathf.MoveTowards(eulerX, AxisX_MIN, a);
            }
        }
        public void SetEulerY(float value) => eulerY += value;

    }
}

