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

        public GameObject attackPossibleAngle;
        float arrange = 0;

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
            targetPos = headTransform.transform.position + headTransform.transform.forward * 100f;
            defaultDir = Swivel.transform.forward;
            defaultRot = Quaternion.FromToRotation(transform.forward, defaultDir);
            fullAccess = true;
            StopAnimation();
            //sr = attackPossibleAngle.GetComponent<SpriteRenderer>();
            //sr.gameObject.SetActive(false);

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


                if (barrelTransform != null)
                {
                    /////// Heading
                    headingVetor =
                        Vector3.Normalize(F3DMath.ProjectVectorOnPlane(headTransform.up,
                            targetPos - headTransform.position));
                    float headingAngle =
                        F3DMath.SignedVectorAngle(headTransform.forward, headingVetor, headTransform.up);
                    float turretDefaultToTargetAngle = F3DMath.SignedVectorAngle(defaultRot * headTransform.forward,
                        headingVetor, headTransform.up);
                    float turretHeading = F3DMath.SignedVectorAngle(defaultRot * headTransform.forward,
                        headTransform.forward, headTransform.up);

                    float headingStep = HeadingTrackingSpeed * Time.deltaTime;

                    // Heading step and correction
                    // Full rotation
                    if (HeadingLimit.x <= -180f && HeadingLimit.y >= 180f)
                        headingStep *= Mathf.Sign(headingAngle);
                    else // Limited rotation
                        headingStep *= Mathf.Sign(turretDefaultToTargetAngle - turretHeading);

                    // Hard stop on reach no overshooting
                    if (Mathf.Abs(headingStep) > Mathf.Abs(headingAngle))
                        headingStep = headingAngle;

                    // Heading limits
                    if (curHeadingAngle + headingStep > HeadingLimit.x &&
                        curHeadingAngle + headingStep < HeadingLimit.y ||
                        HeadingLimit.x <= -180f && HeadingLimit.y >= 180f || fullAccess)
                    {
                        curHeadingAngle += headingStep;
                        headTransform.rotation = headTransform.rotation * Quaternion.Euler(0f, headingStep, 0f);
                    }

                    /////// Elevation
                    Vector3 elevationVector =
                        Vector3.Normalize(F3DMath.ProjectVectorOnPlane(headTransform.right,
                            targetPos - barrelTransform.position));
                    float elevationAngle =
                        F3DMath.SignedVectorAngle(barrelTransform.forward, elevationVector, headTransform.right);

                    // Elevation step and correction
                    float elevationStep = Mathf.Sign(elevationAngle) * ElevationTrackingSpeed * Time.deltaTime;
                    if (Mathf.Abs(elevationStep) > Mathf.Abs(elevationAngle))
                        elevationStep = elevationAngle;

                    // Elevation limits
                    if (curElevationAngle + elevationStep < ElevationLimit.y &&
                        curElevationAngle + elevationStep > ElevationLimit.x)
                    {
                        curElevationAngle += elevationStep;
                        barrelTransform.rotation = barrelTransform.rotation * Quaternion.Euler(elevationStep, 0f, 0f);
                    }
                }


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
                    barrelX.transform.localEulerAngles.x <= (360f - ElevationLimit.y))
                {
                    barrelX.transform.localEulerAngles = new Vector3(360f - ElevationLimit.y, 0f, 0f);
                }

                //down
                else if (barrelX.transform.localEulerAngles.x <= 180f &&
                         barrelX.transform.localEulerAngles.x >= -ElevationLimit.x)
                {
                    barrelX.transform.localEulerAngles = new Vector3(-ElevationLimit.x, 0f, 0f);
                }

                //finding position for turning just for Y axis

                Vector3 targetY = targetPos;
                targetY.y = barrelY.position.y;
                //Mathf.Clamp( targetY.y, HeadingLimit.x, HeadingLimit.y);
                Quaternion targetRotationY = Quaternion.LookRotation(targetY - barrelY.position, barrelY.transform.up);

                barrelY.transform.rotation = Quaternion.Slerp(barrelY.transform.rotation, targetRotationY,
                    ElevationTrackingSpeed * Time.deltaTime);


                

                if (Quaternion.FromToRotation(Vector3.right, barrelY.right).eulerAngles.y >= 300f && Quaternion.FromToRotation(Vector3.right, barrelY.right).eulerAngles.y <= 360 || 0 <= Quaternion.FromToRotation(Vector3.right, barrelY.right).eulerAngles.y && Quaternion.FromToRotation(Vector3.right, barrelY.right).eulerAngles.y  <= 60f) 
                {
                    
                    arrange = Quaternion.FromToRotation(Vector3.right, barrelY.right).eulerAngles.y;
                    
                }
                
                barrelY.transform.localEulerAngles = new Vector3(0f, arrange, 0f);

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



                if (DebugDraw)
                    Debug.DrawLine(barrelTransform.position,
                        barrelTransform.position +
                        barrelTransform.forward * Vector3.Distance(barrelTransform.position, targetPos), Color.red);
            }
            //else 
            //{
            //    Transform barrelX = barrelTransform;
            //    Transform barrelY = Swivel.transform;

            //    //finding position for turning just for X axis (down-up)



            //    if (ActiveEnemyManager.Instance.GetEnemy(ActiveEnemyManager.SearchType.CLOSEST, transform.position) != null) 
            //    {

            //        Vector3 targetX = ActiveEnemyManager.Instance.GetEnemy(ActiveEnemyManager.SearchType.CLOSEST, transform.position).transform.position - barrelX.transform.position;

            //        Quaternion targetRotationX = Quaternion.LookRotation(targetX, headTransform.up);

            //        barrelX.transform.rotation = Quaternion.Slerp(barrelX.transform.rotation, targetRotationX,
            //            HeadingTrackingSpeed * Time.deltaTime);
            //        barrelX.transform.localEulerAngles = new Vector3(barrelX.transform.localEulerAngles.x, 0f, 0f);

            //    }
               
               


            //    //checking for turning up too much
            //    if (barrelX.transform.localEulerAngles.x >= 180f &&
            //        barrelX.transform.localEulerAngles.x < (360f - ElevationLimit.y))
            //    {
            //        barrelX.transform.localEulerAngles = new Vector3(360f - ElevationLimit.y, 0f, 0f);
            //    }

            //    //down
            //    else if (barrelX.transform.localEulerAngles.x < 180f &&
            //             barrelX.transform.localEulerAngles.x > -ElevationLimit.x)
            //    {
            //        barrelX.transform.localEulerAngles = new Vector3(-ElevationLimit.x, 0f, 0f);
            //    }

            //    if (ActiveEnemyManager.Instance.GetEnemy(ActiveEnemyManager.SearchType.CLOSEST, transform.position) != null)
            //    {
            //        //finding position for turning just for Y axis
            //        Vector3 targetY = new Vector3(30f, 0);
            //        Vector3 targetDir = (ActiveEnemyManager.Instance.GetEnemy(ActiveEnemyManager.SearchType.CLOSEST, transform.position).transform.position - transform.position).normalized;
            //        float dot = Vector3.Dot(transform.forward, targetDir);
            //        float degree = Mathf.Acos(dot) * Mathf.Rad2Deg;

            //        if (HeadingLimit.x < degree && degree < HeadingLimit.y)
            //        {
            //            targetY = ActiveEnemyManager.Instance.GetEnemy(ActiveEnemyManager.SearchType.CLOSEST, transform.position).transform.position;
            //            targetY.y = barrelY.position.y;
            //        }
            //        Quaternion targetRotationY = Quaternion.LookRotation(targetY - barrelY.position, barrelY.transform.up);

            //        barrelY.transform.rotation = Quaternion.Slerp(barrelY.transform.rotation, targetRotationY,
            //            ElevationTrackingSpeed * Time.deltaTime);
            //        barrelY.transform.localEulerAngles = new Vector3(0f, barrelY.transform.localEulerAngles.y, 0f);

            //    }
                
                




            //}

        }


        

        
    }
}