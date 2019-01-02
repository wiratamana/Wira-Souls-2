using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Interactable : MonoBehaviour
    {
        protected Transform playerTransform { get { return GM.PlayerController.transform; } }
        protected bool isPlayerFacingMe
        {
            get
            {
                Vector3 playerForward = playerTransform.forward;
                Vector3 playerTowardMe = (center.position - playerTransform.position).normalized;

                playerForward.y = 0;
                playerTowardMe.y = 0;

                if (Vector3.Angle(playerTowardMe, playerForward) < 90)
                {
                    return true;
                }

                onLossInteract?.Invoke();
                return false;
            }
        }
        protected Transform center { private set; get; }
        public new Collider collider { private set; get; }

        public bool isMoving { protected set; get; }

        protected System.Action onInteract { private set; get; }
        protected System.Action onLossInteract { private set; get; }

        protected bool isDelay { private set; get; }

        protected virtual void Awake()
        {
            if(center == null)
            {
                center = new GameObject("Center").transform;
                center.SetParent(transform);
                center.localPosition = GetComponent<UnityEngine.AI.NavMeshObstacle>().center;

                collider = GetComponent<Collider>();

                enabled = false;

                onInteract = OnInteract;
                onLossInteract = OnLossInteract;
            }
        }

        protected virtual void OnInteract() { }
        protected virtual void OnLossInteract() { }

        protected IEnumerator SetDelay()
        {
            yield return GM.waitForSecond;

            isDelay = true;
            enabled = true;
        }
    }
}

