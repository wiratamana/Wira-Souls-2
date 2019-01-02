using UnityEngine;
using UnityEngine.UI;
using System.Collections;



namespace Tamana
{
    public class InteractableMessage : MonoBehaviour
    {
        [SerializeField] Image buttonImage;
        [SerializeField] Text messageText;
        [SerializeField] RectTransform parentRT;

        [Header("Button Sprites")]
        [SerializeField] Sprite square;
        [SerializeField] Sprite triangle;
        [SerializeField] Sprite circle;
        [SerializeField] Sprite cross;

        public void Open(Sprite buttonSprite, string message)
        {
            if (gameObject.activeSelf) return;
            gameObject.SetActive(true);

            messageText.text = message;
            buttonImage.sprite = buttonSprite;
        }

        public void OpenPS4(PS4.ButtonName buttonName, string message)
        {
            if (gameObject.activeSelf) return;
            gameObject.SetActive(true);

            messageText.text = message;
            buttonImage.sprite = GetButtonName(buttonName);
        }

        public void Close() => gameObject.SetActive(false);

        private Sprite GetButtonName(PS4.ButtonName buttonName)
        {
            switch (buttonName)
            {
                case PS4.ButtonName.R1:
                    return circle;
                case PS4.ButtonName.R2:
                    return circle;
                case PS4.ButtonName.R3:
                    return circle;
                case PS4.ButtonName.L1:
                    return circle;
                case PS4.ButtonName.L2:
                    return circle;
                case PS4.ButtonName.L3:
                    return circle;
                case PS4.ButtonName.Triangle:
                    return triangle;
                case PS4.ButtonName.Circle:
                    return circle;
                case PS4.ButtonName.Cross:
                    return cross;
                case PS4.ButtonName.Square:
                    return square;
                case PS4.ButtonName.Option:
                    return circle;
                case PS4.ButtonName.Share:
                    return circle;
                case PS4.ButtonName.TouchPad:
                    return circle;
                case PS4.ButtonName.PS:
                    return circle;
                default:
                    return circle;
            }
        }
    }
}