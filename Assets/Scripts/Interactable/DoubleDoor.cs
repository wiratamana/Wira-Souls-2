using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class DoubleDoor : Interactable
    {
        [SerializeField] private DoubleDoor doubleDoor;
        [SerializeField] private Vector3 closePosition;
        [SerializeField] private Vector3 openPosition;

        public bool isOpening { protected set; get; }

        private string message
        {
            get
            {
                if (!isOpening) return Localization.GetText("openDoor");
                else return Localization.GetText("closeDoor");
            }
        }

        protected override void OnInteract() => GM.interactableMessage.OpenPS4(PS4.ButtonName.Circle, message);
        protected override void OnLossInteract() => GM.interactableMessage.Close();

        private void Update()
        {
            if (!isMoving)
            {
                if (isPlayerFacingMe) onInteract?.Invoke();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    isMoving = true;
                    isOpening = !isOpening;

                    if (doubleDoor.isMoving != true)
                        doubleDoor.OpenDoor(isOpening);

                    onLossInteract?.Invoke();
                }

                return;
            }

            if (isOpening)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(openPosition), 180 * Time.deltaTime);
                if (transform.rotation == Quaternion.Euler(openPosition))
                {
                    isMoving = false;
                    enabled = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(closePosition), 180 * Time.deltaTime);
                if (transform.rotation == Quaternion.Euler(closePosition))
                {
                    isMoving = false;
                    enabled = false;
                }
            }
        }

        public void OpenDoor(bool isOpening)
        {
            enabled = true;
            isMoving = true;
            this.isOpening = isOpening;
        }
    }
}