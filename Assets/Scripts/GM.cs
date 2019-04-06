using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tamana
{
    public enum StateAnimation { OnStateEnter, OnStateExit, OnStateUpdate }

    public class GM : MonoBehaviour
    {
        private static Camera _mainCamera;
        public static Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                    _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

                return _mainCamera;
            }
        }
        public static Controller PlayerController { private set; get; }
        public static AnimatorParameter animParam { private set; get; }
        private static Animator _animator;
        public static Animator animator
        {
            get
            {
                if (_animator == null)
                    _animator = player.GetComponent<Animator>();

                return _animator;
            }
        }
        private static Transform _player;
        public static Transform player
        {
            get
            {
                if (_player == null)
                    _player = GameObject.FindWithTag("Player").transform;

                return _player;
            }
        }
        private static BodyPart _bodyPart;
        public static BodyPart bodyPart
        {
            get
            {
                if (_bodyPart == null)
                    _bodyPart = new BodyPart(player);

                return _bodyPart;
            }
        }

        public static int LayerGround { get { return LayerMask.GetMask("Ground"); } }
        public static int LayerInteractableObject { get { return LayerMask.GetMask("InteractableObject"); } }
        public static int LayerUnit { get { return LayerMask.GetMask("Unit"); } }

        public static WaitForSeconds waitForSecond { get; } = new WaitForSeconds(1);
        public static WaitForSeconds waitForHalfSecond { get; } = new WaitForSeconds(.5f);
        public static WaitForSeconds waitForThirdSecond { get; } = new WaitForSeconds(.3333f);

        public static InteractableMessage interactableMessage { private set; get; }


        private void Awake()
        {
            StartCoroutine(GetInteractableMessage());
        }

        public static void SetPlayer(Controller playerTransform)
        {
            if (PlayerController == null)
                PlayerController = playerTransform;
        }
        public static void SetAnimParam(AnimatorParameter animatorParameter)
        {
            if (animParam == null)
                animParam = animatorParameter;
        }

        private static List<Collider> cols = new List<Collider>();
        public static Collider[] GetNearbyUnits(float radius)
        {
            cols.Clear();
            cols.AddRange(Physics.OverlapSphere(PlayerController.transform.position, radius, LayerUnit));

            if (cols.Count == 1)
                return null;

            for(int i = 0; i < cols.Count; i++)
                if(cols[i].transform == PlayerController.transform)
                {                    
                    cols.RemoveAt(i);
                    break;
                }

            return cols.ToArray();
        }

        private IEnumerator GetInteractableMessage()
        {
            while(interactableMessage == null)
            {
                interactableMessage = FindObjectOfType<InteractableMessage>();

                yield return null;
            }
            
            interactableMessage.Close();
        }
    }
}

