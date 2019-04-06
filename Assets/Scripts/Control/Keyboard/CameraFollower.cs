using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class CameraFollower : MonoBehaviour
    {
        public float followSpeed;

        public Transform player;

        public static CameraFollower instance { private set; get; }

        private void OnValidate()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Awake()
        {
            instance = this;    
        }

        // Update is called once per frame
        void Update()
        {
            var center = CameraRotator.instance.center;
            center.position = Vector3.Lerp(center.position, player.position, followSpeed * Time.deltaTime);
        }
    }

}
