using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FORGE3D 
{
    public class PlayerShooting : MonoBehaviour
    {

        RaycastHit hitInfo; // Raycast structure
        private bool isFiring; // Is turret currently in firing state
        private F3DTurret turret;
        private int mpDecreaseAmount = 20;
        [HideInInspector]
        public F3DFXController fxController;



        private void Start()
        {
            turret = GetComponent<F3DTurret>();
            fxController = GetComponent<F3DFXController>();
        }

        void Update()
        {
            CheckForTurn();
            CheckForFire();

            
        }

        void CheckForFire()
        {

            if (!TutorialManager.instance.Tuto_Ing()) 
            {
                // Fire turret
                if (turret.isPlayer && !isFiring && Input.GetMouseButtonDown(0))
                {
                    isFiring = true;
                    fxController.Fire();
                }

                // Stop firing
                if ((turret.isPlayer && isFiring && Input.GetMouseButtonUp(0)) || WaveManager.Instance.IsWaveFinished)
                {
                    isFiring = false;
                    fxController.Stop();
                }
                if (turret.isPlayer && !isFiring && Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.Mp >= mpDecreaseAmount)
                {
                    isFiring = true;
                    GameManager.Instance.Mp -= mpDecreaseAmount;
                    fxController.Fire(isFiring);
                    fxController.Stop();
                    isFiring = false;
                }
            }

        }

        void CheckForTurn()
        {
            // Construct a ray pointing from screen mouse position into world space
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Raycast
            if (Physics.Raycast(cameraRay, out hitInfo, 500f))
            {
                turret.SetNewTarget(hitInfo.point);
            }
        }


    }
}