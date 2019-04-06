using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class CameraLooker : MonoBehaviour
    {
        public float lookSpeed;
        public Vector3 lookPointOffset;

        public Camera mainCamera;
        public Transform player;

        public CameraLooker instance { private set; get; }

        private void OnValidate()
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            var lookTarget = player.position + lookPointOffset;

            var rot = mainCamera.transform.rotation;
            var dir2Player = (lookTarget - mainCamera.transform.position).normalized;
            var lookRot = Quaternion.LookRotation(dir2Player);

            rot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, lookSpeed * Time.deltaTime);

            mainCamera.transform.rotation = rot;
        }
    }

}
