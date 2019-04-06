using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class CameraRotator : MonoBehaviour
    {
        public float speedX;
        public float speedY;

        public bool invertMouseY;

        public float verticalMax;
        public float verticalMin;

        public Vector3 offset;

        public Transform center;
        public Transform player;
        public Camera mainCamera;

        private float mouseX { get { return Input.GetAxis("Mouse Y"); } }
        private float mouseY { get { return Input.GetAxis("Mouse X"); } }

        [SerializeField] private float currentX;

        public static CameraRotator instance { private set; get; }

        private void OnValidate()
        {
            player = GameObject.FindWithTag("Player").transform;
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            // Instantiate Center and reset position
            center = new GameObject("Center").transform;
            mainCamera.transform.SetParent(center);
            mainCamera.transform.position = offset;            
        }

        // Update is called once per frame
        void Update()
        {
            var rot = center.rotation;
            var euler = center.eulerAngles;

            rot = Quaternion.Euler(euler + new Vector3(mouseX, mouseY));            

            euler = center.eulerAngles;
            currentX += mouseX;

            if (currentX > verticalMax)
            {
                rot = Quaternion.Euler(new Vector3(verticalMax, euler.y, euler.z));
                currentX = verticalMax;
            }
            else if (currentX < verticalMin)
            {
                rot = Quaternion.Euler(new Vector3(verticalMin, euler.y, euler.z));
                currentX = verticalMin;
            }

            center.transform.rotation = rot;
        }
    }
}
