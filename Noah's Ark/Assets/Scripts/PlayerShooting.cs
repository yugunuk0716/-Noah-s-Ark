using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FORGE3D 
{
    public class PlayerShooting : MonoBehaviour
    {

        RaycastHit hitInfo; // Raycast structure
        private bool isFiring; // Is turret currently in firing state
        private F3DTurret turret;
        private F3DFXController fxController;



        private void Start()
        {
            turret = GetComponent<F3DTurret>();
            fxController = F3DFXController.instance;
            //fxController.SetTurretSocket(turretSockets);
        }

        void Update()
        {
            CheckForTurn();
            CheckForFire();
        }

        void CheckForFire()
        {
            // Fire turret
            if (!isFiring && Input.GetMouseButtonDown(0))
            {
                isFiring = true;
                fxController.Fire();
                print("start");
            }

            // Stop firing
            if (isFiring && Input.GetMouseButtonUp(0))
            {
                isFiring = false;
                fxController.Stop();
                print("stop");
            }
            if (!isFiring && Input.GetKeyDown(KeyCode.Space))
            {
                isFiring = true;
                fxController.Fire(isFiring);
                fxController.Stop();
                isFiring = false;
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