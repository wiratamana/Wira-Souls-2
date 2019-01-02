using UnityEngine;
using System.Collections;

namespace Tamana
{
    [System.Serializable]
    public class BodyPart
    {
        [Header ("Body References")]
        public Transform neck;
        public Transform spine;
        public Transform handLeft;
        public Transform handRight;
        public Transform ankleLeft;
        public Transform ankleRight;
        public Transform toeLeft;
        public Transform toeRight;
        public Transform weaponBelt;
        public Transform weaponGrab;

        [Header("Dummy Objects")]
        public Transform dummyNeck;
        public Transform dummySpine;
        public Transform dummyHandLeft;
        public Transform dummyHandRight;
        public Transform dummyAnkleLeft;
        public Transform dummyAnkleRight;
        public Transform dummyToeLeft;
        public Transform dummyToeRight;

        public BodyPart(Transform body)
        {
            GetAllParts(body);
        }

        private void GetAllParts(Transform body)
        {
            foreach(Transform part in body)
            {
                switch(part.name)
                {
                    case "Neck":
                        neck = part;
                        dummyNeck = InstantiateDummy(part);
                        break;
                    case "Spine_03":
                        spine = part;
                        dummySpine = InstantiateDummy(part);
                        break;

                    case "Hand_L":
                        handLeft = part;
                        dummyHandLeft = InstantiateDummy(part);
                        break;
                    case "Hand_R":
                        handRight = part;
                        dummyHandRight = InstantiateDummy(part);
                        break;

                    case "Ankle_L":
                        ankleLeft = part;
                        dummyAnkleLeft = InstantiateDummy(part);
                        break;
                    case "Ankle_R":
                        ankleRight = part;
                        dummyAnkleRight = InstantiateDummy(part);
                        break;

                    case "Toes_L":
                        toeLeft = part;
                        dummyToeLeft = InstantiateDummy(part);
                        break;
                    case "Toes_R":
                        toeRight = part;
                        dummyToeRight = InstantiateDummy(part);
                        break;
                    case "WeaponBelt":
                        weaponBelt = part;
                        break;
                    case "WeaponGrab":
                        weaponGrab = part;
                        break;
                }

                if (part.childCount > 0)
                    GetAllParts(part);
            }
        }

        private Transform InstantiateDummy(Transform parent)
        {
            var dummy = new GameObject("Dummy " + parent.name).transform;
            dummy.SetParent(parent);
            dummy.localPosition = Vector3.zero;
            return dummy;
        }
    }
}

