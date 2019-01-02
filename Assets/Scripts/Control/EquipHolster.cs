using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class EquipHolster : MonoBehaviour
    {
        private Transform weaponGrab { get { return GM.PlayerController.bodyPart.weaponGrab; } }
        private Transform weaponBelt { get { return GM.PlayerController.bodyPart.weaponBelt; } }
        private Animator animator { get { return GM.PlayerController.animator; } }
        private AnimatorParameter animParam { get {return GM.PlayerController.animParam; } }
        private Controller controller { get { return GM.PlayerController; } }

        private Transform weapon;

        private static readonly Vector3 equip_weaponLocalPosition = new Vector3 (-1.4f, -0.4f, 3.8f);
        private static readonly Vector3 equip_weaponLocalEulerAngle = new Vector3(-176.619f, 67.271f, -95.46698f);

        private static readonly Vector3 holster_weaponLocalPosition = new Vector3(-11.8f, 15.2f, 23.4f);
        private static readonly Vector3 holster_weaponLocalEulerAngle = new Vector3(-214.489f, 89.99999f, 0f);

        private float weight = 0;
        private bool isActiveRightHandIK = false;

        private void Start()
        {

        }

        public void DoEquipHolster(PS4.ButtonName buttonName)
        {
            if (PS4.GetButtonDown(buttonName))
            {
                if (animParam.isAttacking || animParam.isHolsterOrEquip)
                    return;

                animParam.isMoving = false;
                controller.StopRotating();

                if (!animParam.isSword1h_Equip)
                    Equip();
                else Holster();
            }
        }

        private void Equip()
        {
            animator.Play(AnimName.SwordAnimsetPro.Sword1h_Various.i23_Sword1h_Equip);
            animParam.isHolsterOrEquip = true;
        }

        private void Holster()
        {
            animator.Play(AnimName.SwordAnimsetPro.Sword1h_Various.i24_Sword1h_Holster);
            animParam.isHolsterOrEquip = true;
        }


        public void Equip1h()
        {
            StartCoroutine(ChangeWeaponParent());
            weight = 1.0f;
            isActiveRightHandIK = true;
        }

        public void Holster1h()
        {
            weapon.SetParent(weaponBelt);

            weapon.localPosition = holster_weaponLocalPosition;
            weapon.localRotation = Quaternion.Euler(holster_weaponLocalEulerAngle);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!isActiveRightHandIK) return;

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);

            animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.position);
        }

        private IEnumerator ChangeWeaponParent()
        {
            weapon = weaponBelt.transform.GetChild(0);           
            yield return null;
            weapon.SetParent(weaponGrab);

            weapon.localPosition = equip_weaponLocalPosition;
            weapon.localRotation = Quaternion.Euler(equip_weaponLocalEulerAngle);

            weight = 0;
            isActiveRightHandIK = false;
        }
    }
}