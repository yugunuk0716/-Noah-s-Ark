using FORGE3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoSingleton<TurretManager>
{

    public List<F3DTurret> turrets = new List<F3DTurret>();

    private int currentTurretIndex = 0;

    public Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        turrets[0].isPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        NextTurret();
    }


    public void SetTurret(F3DTurret turret)
    {
        turrets.Add(turret);
    }

    public void NextTurret()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            turrets[currentTurretIndex].isPlayer = false;
            turrets[currentTurretIndex].shooter.fxController.Stop();
            currentTurretIndex++;
            if (turrets.Count <= currentTurretIndex)
            {
                currentTurretIndex = 0;
            }
            mainCam.transform.SetParent(turrets[currentTurretIndex].camTrm);
            mainCam.transform.localPosition = Vector3.zero;
            mainCam.transform.localRotation = Quaternion.identity;
            turrets[currentTurretIndex].isPlayer = true;
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            turrets[currentTurretIndex].isPlayer = false;
            turrets[currentTurretIndex].shooter.fxController.Stop();
            currentTurretIndex--;
            if (0 > currentTurretIndex)
            {
                currentTurretIndex = turrets.Count-1;
            }
            mainCam.transform.SetParent(turrets[currentTurretIndex].camTrm);
            mainCam.transform.localPosition = Vector3.zero;
            mainCam.transform.localRotation = Quaternion.identity;
            turrets[currentTurretIndex].isPlayer = true;
        }

    }
}
