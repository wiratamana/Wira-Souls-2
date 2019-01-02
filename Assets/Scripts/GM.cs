using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tamana
{
    public enum StateAnimation { OnStateEnter, OnStateExit, OnStateUpdate }

    public class GM : MonoBehaviour
    {
        public static Camera MainCamera { private set; get; }
        public static Controller PlayerController { private set; get; }
        public static AnimatorParameter animParam { private set; get; }

        public static int LayerGround { get { return LayerMask.GetMask("Ground"); } }
        public static int LayerInteractableObject { get { return LayerMask.GetMask("InteractableObject"); } }
        public static int LayerUnit { get { return LayerMask.GetMask("Unit"); } }

        public static WaitForSeconds waitForSecond { get; } = new WaitForSeconds(1);
        public static WaitForSeconds waitForHalfSecond { get; } = new WaitForSeconds(.5f);
        public static WaitForSeconds waitForThirdSecond { get; } = new WaitForSeconds(.3333f);

        public static InteractableMessage interactableMessage { private set; get; }

        private void Awake()
        {
            MainCamera = Camera.main;

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

