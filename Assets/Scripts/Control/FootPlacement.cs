using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class FootPlacement : MonoBehaviour
    {
        private Transform playerTransform;
        private Vector3 playerPos
        {
            get { return playerTransform.position; }
            set { playerTransform.position = value; }
        }
        private Animator _animator;
        private Animator animator
        {
            get
            {
                if (_animator == null)
                    _animator = transform.GetComponent<Animator>();

                return _animator;
            }
        }
        private AnimatorParameter animParam { get { return AnimatorParameter.instance; } }
        private BodyPart bodyPart;

        private Vector3 anklePosL { get { return GM.bodyPart.ankleLeft.position; } }
        private Vector3 anklePosR { get { return GM.bodyPart.ankleRight.position; } }

        private Transform legAnkleL;
        private Transform legAnkleR;
        private Transform legToeL;
        private Transform legToeR;

        private Vector3 rayPointLegAnkleL;
        private Vector3 rayPointLegAnkleR;
        private Vector3 rayPointLegToeL;
        private Vector3 rayPointLegToeR;

        private Vector3 posAnkleL;
        private Vector3 posAnkleR;

        private bool legIK_L;
        private bool legIK_R;

        private float ankleTravelSpeed = 15;

        void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            if (bodyPart == null)
                bodyPart = new BodyPart(playerTransform);

            InstantiateRaycasterLeg();

            posAnkleL = legAnkleL.position;
            posAnkleR = legAnkleR.position;
        }

        void Update()
        {
            Raycasting();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);
            
            if (legIK_L)
            {
                posAnkleL = Vector3.MoveTowards(posAnkleL, new Vector3(anklePosL.x, rayPointLegAnkleL.y + 0.05f, anklePosL.z), ankleTravelSpeed * Time.deltaTime);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, posAnkleL);
            }
            else
            {
                posAnkleL = Vector3.MoveTowards(posAnkleL, new Vector3(anklePosL.x, anklePosL.y, anklePosL.z), ankleTravelSpeed * Time.deltaTime);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, posAnkleL);
            }

            if (legIK_R)
            {
                posAnkleR = Vector3.MoveTowards(posAnkleR, new Vector3(anklePosR.x, rayPointLegAnkleR.y + 0.05f, anklePosR.z), ankleTravelSpeed * Time.deltaTime);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(anklePosR.x, rayPointLegAnkleR.y + 0.05f, anklePosR.z));
            }
            else
            {
                posAnkleR = Vector3.MoveTowards(posAnkleR, new Vector3(anklePosR.x, anklePosR.y, anklePosR.z), ankleTravelSpeed * Time.deltaTime);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(anklePosR.x, anklePosR.y, anklePosR.z));
            }

        }

        private void InstantiateRaycasterLeg()
        {
            if (legAnkleL != null) return;

            legAnkleL = new GameObject("LegAnkleL Raycaster").transform;
            legAnkleR = new GameObject("LegAnkleR Raycaster").transform;

            legToeL = new GameObject("LegToeL Raycaster").transform;
            legToeR = new GameObject("LegToeR Raycaster").transform;

            legAnkleL.position = bodyPart.ankleLeft.position + new Vector3(0, 2, 0);
            legAnkleR.position = bodyPart.ankleRight.position + new Vector3(0, 2, 0);

            legToeL.position = bodyPart.toeLeft.position + new Vector3(0, 2, 0);
            legToeR.position = bodyPart.toeRight.position + new Vector3(0, 2, 0);
        }

        private void Raycasting()
        {
            legAnkleL.position = bodyPart.ankleLeft.position + new Vector3(0, 2, 0);
            legAnkleR.position = bodyPart.ankleRight.position + new Vector3(0, 2, 0);

            legToeL.position = bodyPart.toeLeft.position + new Vector3(0, 2, 0);
            legToeR.position = bodyPart.toeRight.position + new Vector3(0, 2, 0);

            RaycastHit hit;

            bool ankleL = Physics.Raycast(legAnkleL.position, Vector3.down, out hit, 10.0f, GM.LayerGround);
            rayPointLegAnkleL = hit.point;
            bool ankleR = Physics.Raycast(legAnkleR.position, Vector3.down, out hit, 10.0f, GM.LayerGround);
            rayPointLegAnkleR = hit.point;

            bool toeL = Physics.Raycast(legToeL.position, Vector3.down, out hit, 10.0f, GM.LayerGround);
            rayPointLegToeL = hit.point;
            bool toeR = Physics.Raycast(legToeR.position, Vector3.down, out hit, 10.0f, GM.LayerGround);
            rayPointLegToeR = hit.point;

            if (rayPointLegAnkleL.y < rayPointLegAnkleR.y)
            {
                var newPos = new Vector3(playerPos.x, rayPointLegAnkleL.y, playerPos.z);
                playerPos = Vector3.MoveTowards(playerPos, newPos, 25 * Time.deltaTime);
            }
            else
            {
                var newPos = new Vector3(playerPos.x, rayPointLegAnkleR.y, playerPos.z);
                playerPos = Vector3.MoveTowards(playerPos, newPos, 25 * Time.deltaTime);
            }
        }

        public void LegL_OFF() => legIK_L = false;
        public void LegL_ON() => legIK_L = true;
        public void LegR_OFF() => legIK_R = false;
        public void LegR_ON() => legIK_R = true;
        public void StopL() => animParam.isStopMovingL = true;
        public void StopR() => animParam.isStopMovingL = false;
    }
}

