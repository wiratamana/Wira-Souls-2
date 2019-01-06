using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Controller : MonoBehaviour
    {
        [Header("Settings")]
        [Range(1, 15)]
        [SerializeField] protected float trailMoveSpeed;
        [Range(1, 15)]
        [SerializeField] protected float lookSpeed;

        public AnimationState animationState { private set; get; }

        public Transform analogLeft { private set; get; }
        public BodyPart bodyPart { private set; get; }
        public Animator animator { private set; get; }
        public ThirdPersonCamera thirdPersonCamera { private set; get; }
        public FootPlacement footPlacement { private set; get; }
        public AnimatorParameter animParam { private set; get; }
        public Detector detector { private set; get; }
        public EquipHolster equipHolster { private set; get; }
        public Attacking attacking { private set; get; }
        public GaugeManager gaugeManager { private set; get; }
        public StrafeMovement strafeMovement { private set; get; }

        public Trail[] trails { private set; get; }

        protected Transform playerTransform { get { return GM.PlayerController.transform; } }
        protected Vector3 camDir { get { return GM.MainCamera.transform.forward; } }
        protected Vector3 playerPos { get { return GM.PlayerController.transform.position; } }
        protected float distanceFromFurthestTail { get { return Vector3.Distance(playerPos, trails[trails.Length - 1].position); } }

        protected bool isRotating { private set; get; }
        protected bool isAbleToMove
        {
            get
            {
                if (animParam.isCannotMove || animParam.isAttacking || animParam.isHolsterOrEquip)
                {
                    return false;
                }

                return true;
            }
        }

        public enum MovingDirection
        { Forward, Left, Right, Back_Left135, Back_Right135, Back_Left180, Back_Right180 }
        public enum AnimationState
        { RunAnimsetBasic, SwordAnimsetPro, LongswordAnimsetPro }

        protected virtual void Awake()
        {
            GM.SetPlayer(this);
            bodyPart = new BodyPart(transform);
            InstantiateAnalog();

            if (animator == null)
            {
                animator = GetComponent<Animator>();
                animParam = new AnimatorParameter(animator);
                GM.SetAnimParam(animParam);
            }

            thirdPersonCamera = gameObject.AddComponent<ThirdPersonCamera>();
            footPlacement = gameObject.AddComponent<FootPlacement>();
            detector = gameObject.AddComponent<Detector>();
            equipHolster = gameObject.AddComponent<EquipHolster>();
            attacking = gameObject.AddComponent<Attacking>();
            strafeMovement = gameObject.AddComponent<StrafeMovement>();
            gaugeManager = FindObjectOfType<GaugeManager>();
            InstantiateTrails();
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
        }

        private void OnDrawGizmos()
        {

        }

        private void InstantiateAnalog()
        {
            analogLeft = new GameObject("AnalogLeft").transform;
            analogLeft.position = GM.PlayerController.transform.position;
        }

        protected void Move_RunAnimsetBasic(MovingDirection direction)
        {
            switch (direction)
            {
                case MovingDirection.Forward:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart);
                    break;
                case MovingDirection.Left:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart90_L);
                    break;
                case MovingDirection.Right:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart90_R);
                    break;
                case MovingDirection.Back_Left180:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart180_L);
                    break;
                case MovingDirection.Back_Right180:
                    animator.Play(AnimName.RunAnimsetBasic.RunFwdStart180_R);
                    break;
            }
        }

        protected void Move_SwordAnimsetPro(MovingDirection direction)
        {
            if (animParam.isStrafing)
                Move_SwordAnimsetPro_StrafeMovement(direction);
            else Move_SwordAnimsetPro_NormalMovement(direction);
        }

        private void Move_SwordAnimsetPro_StrafeMovement(MovingDirection direction) => strafeMovement.PlayeStrafeMovement();
        private void Move_SwordAnimsetPro_NormalMovement(MovingDirection direction)
        {
            switch (direction)
            {
                case MovingDirection.Forward:
                    animator.Play(AnimName.SwordAnimsetPro.Sword1h_Walks.i08_Sword1h_WalkFwdStart);
                    break;
                case MovingDirection.Left:
                    animator.Play(AnimName.SwordAnimsetPro.Sword1h_Walks.i14_Sword1h_WalkFwdStart90_L);
                    break;
                case MovingDirection.Right:
                    animator.Play(AnimName.SwordAnimsetPro.Sword1h_Walks.i15_Sword1h_WalkFwdStart90_R);
                    break;
                case MovingDirection.Back_Left135:
                    animator.Play(AnimName.SwordAnimsetPro.Sword1h_Walks.i16_Sword1h_WalkFwdStart135_L);
                    break;
                case MovingDirection.Back_Right135:
                    animator.Play(AnimName.SwordAnimsetPro.Sword1h_Walks.i17_Sword1h_WalkFwdStart135_R);
                    break;
                case MovingDirection.Back_Left180:
                    animator.Play(AnimName.SwordAnimsetPro.Sword1h_Walks.i12_Sword1h_WalkFwdStart180_L);
                    break;
                case MovingDirection.Back_Right180:
                    animator.Play(AnimName.SwordAnimsetPro.Sword1h_Walks.i13_Sword1h_WalkFwdStart180_R);
                    break;
            }
        }

        protected void Move_LongswordAnimsetPro(MovingDirection direction)
        {

        }

        protected void MovePathTrail(Transform destination, bool isMoving)
        {
            var directionToDestination = (destination.position - playerTransform.position).normalized;
            RaycastHit hit;

            if (!isMoving)
            {
                for (int i = 0; i < trails.Length; i++)
                {
                    trails[i].transform.localPosition = Vector3.MoveTowards(trails[i].transform.localPosition,
                        Vector3.zero + new Vector3(0, 2, 0), trailMoveSpeed * Time.deltaTime);

                    bool rayHit = Physics.Raycast(trails[i].position, Vector3.down, out hit, 10, GM.LayerGround);
                    if (rayHit)
                    {
                        trails[i].SetRayHitPoint(hit.point);
                    }
                    else
                    {
                        for (int j = i; j < trails.Length; j++)
                            trails[j].isTraversable = false;

                        return;
                    }
                }
                return;
            }

            for (int i = 0; i < trails.Length; i++)
            {
                trails[i].transform.position = Vector3.MoveTowards(trails[i].transform.position,
                playerTransform.position + (directionToDestination * 3 * ((i + 1) * 0.1f)) + new Vector3(0, 2, 0), trailMoveSpeed * Time.deltaTime);

                bool rayHit = Physics.Raycast(trails[i].position, Vector3.down, out hit, 10, GM.LayerGround);
                if (rayHit)
                {
                    trails[i].SetRayHitPoint(hit.point);
                }
                else
                {
                    for (int j = i; j < trails.Length; j++)
                        trails[j].isTraversable = false;

                    return;
                }
            }
        }

        private void InstantiateTrails()
        {
            trails = new Trail[10];
            for (int i = 0; i < 10; i++)
            {
                trails[i] = new Trail(new GameObject("Trails " + i).transform);
                trails[i].transform.SetParent(playerTransform);
                trails[i].transform.localPosition = Vector3.zero + new Vector3(0, 2, 0);
            }
        }

        protected void RotateTowardTrail(Transform destination)
        {
            if (!animParam.isLeftAnalogMoving || !isRotating)
                return;

            var directionToTrail = (destination.position - playerPos).normalized;
            directionToTrail.y = 0;
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation,
                Quaternion.LookRotation(directionToTrail), lookSpeed * Time.deltaTime);
        }

        public void StopRotating() { isRotating = false; }
        public void StartRotating() { isRotating = true; }

        public void SetAnimationState(AnimationState state) => animationState = state;
    }
}
