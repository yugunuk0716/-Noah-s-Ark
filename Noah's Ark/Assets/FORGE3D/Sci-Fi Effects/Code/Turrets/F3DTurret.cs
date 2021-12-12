using UnityEngine;
using System.Collections;
using System;

namespace FORGE3D
{
    public class F3DTurret : MonoBehaviour
    {
        #region Turret
        [HideInInspector] public bool destroyIt;

        public enum TurretTrackingType
        {
            Step,
            Smooth,
        }

        public TurretTrackingType TrackingType;

        public GameObject Mount;
        public GameObject Swivel;
        public Transform camTrm;
        //public Transform[] testTrms;
        public bool isPlayer = false;

        private Vector3 defaultDir;
        private Quaternion defaultRot;

        private Transform headTransform;
        private Transform barrelTransform;

        public float HeadingTrackingSpeed = 1f;
        public float ElevationTrackingSpeed = 1f;

        private Vector3 targetPos;
        [HideInInspector] public Vector3 headingVetor;

        private float curHeadingAngle;
        private float curElevationAngle;

        public Vector2 HeadingLimit;
        public Vector2 ElevationLimit;

        [HideInInspector]
        public PlayerShooting shooter;

        public bool DebugDraw;

        [HideInInspector]
        public Transform DebugTarget;

        private bool fullAccess;
        public Animator[] Animators;
        #endregion

        //public GameObject attackPossibleAngle;
        public GameObject crosshair;
        float arrange = 0;

        private Vector3 originVec;

        private void Awake()
        {
            if (HeadingLimit.y - HeadingLimit.x >= 359.9f)
                fullAccess = true;
            headTransform = Swivel.GetComponent<Transform>();
            barrelTransform = Mount.GetComponent<Transform>();
            shooter = GetComponent<PlayerShooting>();
        }

        public void PlayAnimation()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetTrigger("FireTrigger");
        }

        public void PlayAnimationLoop()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetBool("FireLoopBool", true);
        }

        public void StopAnimation()
        {
            for (int i = 0; i < Animators.Length; i++)
                Animators[i].SetBool("FireLoopBool", false);
        }

        // Use this for initialization
        private void Start()
        {
            HeadingLimit = TowerSpawner.instance.GetLimit();
            targetPos = headTransform.transform.position + headTransform.transform.forward * 100f;
            defaultDir = Swivel.transform.forward;
            defaultRot = Quaternion.FromToRotation(transform.forward, defaultDir);
            fullAccess = false;
            StopAnimation();

            originVec = transform.forward;
        }

        // Autotrack
        public void SetNewTarget(Vector3 _targetPos)
        {
            targetPos = _targetPos;
        }

        // Angle between mount and target
        public float GetAngleToTarget()
        {
            return Vector3.Angle(Mount.transform.forward, targetPos - Mount.transform.position);
        }

        private Vector3 PreviousTargetPosition = Vector3.zero;


        //public void TurretTurnRight(float turnAmount) 
        //{
        //    HeadingLimit.x += turnAmount;
        //    HeadingLimit.y += turnAmount;
        //    Swivel.transform.localEulerAngles = new Vector3(Swivel.transform.localEulerAngles.x, Swivel.transform.localEulerAngles.y + turnAmount, Swivel.transform.localEulerAngles.z);
        //}

        //public void TurretTurnLeft(float turnAmount)
        //{
        //    HeadingLimit.x -= turnAmount;
        //    HeadingLimit.y -= turnAmount;
        //    Swivel.transform.localEulerAngles = new Vector3(Swivel.transform.localEulerAngles.x, Swivel.transform.localEulerAngles.y - turnAmount, Swivel.transform.localEulerAngles.z);
        //}


        //public void Building(bool _isBuilding) 
        //{
        //    isBuilding = _isBuilding;
        //    sr.gameObject.SetActive(_isBuilding);
        //}


        private void Update()
        {
            if (isPlayer)
            {
                

                //return;
                if (DebugTarget != null)
                    targetPos = DebugTarget.transform.position;

                Transform barrelX = barrelTransform;
                Transform barrelY = Swivel.transform;

                //finding position for turning just for X axis (down-up)
                Vector3 targetX = targetPos - barrelX.transform.position;
                Quaternion targetRotationX = Quaternion.LookRotation(targetX, headTransform.up);

                barrelX.transform.rotation = Quaternion.Slerp(barrelX.transform.rotation, targetRotationX,
                    HeadingTrackingSpeed * Time.deltaTime);
                barrelX.transform.localEulerAngles = new Vector3(barrelX.transform.localEulerAngles.x, 0f, 0f);

                //checking for turning up too much
                if (barrelX.transform.localEulerAngles.x >= 180f &&
                    barrelX.transform.localEulerAngles.x < (360f - ElevationLimit.y))
                {
                    barrelX.transform.localEulerAngles = new Vector3(360f - ElevationLimit.y, 0f, 0f);
                }

                //down
                else if (barrelX.transform.localEulerAngles.x < 180f &&
                         barrelX.transform.localEulerAngles.x > -ElevationLimit.x)
                {
                    barrelX.transform.localEulerAngles = new Vector3(-ElevationLimit.x, 0f, 0f);
                }

                //finding position for turning just for Y axis
                Vector3 targetY = targetPos;
                targetY.y = barrelY.position.y;

                Quaternion targetRotationY = Quaternion.LookRotation(targetY - barrelY.position, barrelY.transform.up);

                barrelY.transform.rotation = Quaternion.Slerp(barrelY.transform.rotation, targetRotationY,
                    ElevationTrackingSpeed * Time.deltaTime);
                barrelY.transform.localEulerAngles = new Vector3(0f, barrelY.transform.localEulerAngles.y, 0f);

                if (!fullAccess)
                {
                    //checking for turning left
                    if (barrelY.transform.localEulerAngles.y >= 180f &&
                        barrelY.transform.localEulerAngles.y < (360f - HeadingLimit.y))
                    {
                        barrelY.transform.localEulerAngles = new Vector3(0f, 360f - HeadingLimit.y, 0f);
                    }

                    //right
                    else if (barrelY.transform.localEulerAngles.y < 180f &&
                             barrelY.transform.localEulerAngles.y > -HeadingLimit.x)
                    {
                        barrelY.transform.localEulerAngles = new Vector3(0f, -HeadingLimit.x, 0f);
                    }
                }
            }

            if (DebugDraw)
                Debug.DrawLine(barrelTransform.position,
                    barrelTransform.position +
                    barrelTransform.forward * Vector3.Distance(barrelTransform.position, targetPos), Color.red);
            //Transform barrelX = barrelTransform;
            //Transform barrelY = Swivel.transform;

            ////finding position for turning just for X axis (down-up)

            //Vector3 targetX = targetPos - barrelX.transform.position;

            //Quaternion targetRotationX = Quaternion.LookRotation(targetX, headTransform.up);

            //barrelX.transform.rotation = Quaternion.Slerp(barrelX.transform.rotation, targetRotationX,
            //    HeadingTrackingSpeed * Time.deltaTime);
            //barrelX.transform.localEulerAngles = new Vector3(barrelX.transform.localEulerAngles.x, 0f, 0f);



            ////checking for turning up too much
            //if (barrelX.transform.localEulerAngles.x >= 180f &&
            //    barrelX.transform.localEulerAngles.x <= (360f - ElevationLimit.y))
            //{
            //    barrelX.transform.localEulerAngles = new Vector3(360f - ElevationLimit.y, 0f, 0f);
            //}

            ////down
            //else if (barrelX.transform.localEulerAngles.x <= 180f &&
            //         barrelX.transform.localEulerAngles.x >= -ElevationLimit.x)
            //{
            //    barrelX.transform.localEulerAngles = new Vector3(-ElevationLimit.x, 0f, 0f);
            //}

            ////finding position for turning just for Y axis

            //Vector3 targetY = targetPos;
            //print(targetY);
            //targetY.y = barrelY.position.y;
            //Quaternion targetRotationY = Quaternion.LookRotation(targetY - barrelY.position, barrelY.transform.up);

            ////barrelY.transform.rotation = Quaternion.Slerp(barrelY.transform.rotation, targetRotationY,
            ////   ElevationTrackingSpeed * Time.deltaTime);


            //print(HeadingLimit);

            ////float angle = Quaternion.FromToRotation(Vector3.right, barrelY.right).eulerAngles.y;


            //Vector3 currentVec = barrelY.forward;


            //print($"anmgle : {Vector3.Angle(currentVec, originVec)}");


            //if (Vector3.Angle(currentVec, originVec) < HeadingLimit.y)

            //{


            //}







            //if (DebugDraw)
            //    Debug.DrawLine(barrelTransform.position,
            //        barrelTransform.position +
            //        barrelTransform.forward * Vector3.Distance(barrelTransform.position, targetPos), Color.red);
        }







    }





}
