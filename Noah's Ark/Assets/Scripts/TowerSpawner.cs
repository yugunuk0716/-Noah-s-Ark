using FORGE3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    public static TowerSpawner instance;

    public bool isCreate = false;
    public Camera minimapCam;

    public F3DTurret towerPrefab;

    private Transform towerSpawnPos;


    public Ground ground;

    Quaternion towerRot;
    Quaternion initRot;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        towerRot = Quaternion.identity;
        initRot = Quaternion.Euler(90f, 0, 0);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !TurretManager.Instance.IsPlayer() && !TutorialManager.instance.Tuto_Ing())
        {
            //Vector3 pos = Input.mousePosition;

            //pos.z = Camera.main.farClipPlane;
            //print("A");
            //print($"{pos.x}, {pos.y},{pos.z}");
            ray = minimapCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                ground = hit.transform.GetComponent<Ground>();

                if (ground == null) return;

                if (ground.state == TowerGroundState.Builded)
                {
                    //이미 건설되어 있음
                    //정보를 넘겨줄게 있으면 여기서 넘겨주면됨
                    //물론 거기에 변수가 없다면 만들어야함 
                    //PopupManager.instance.OpenPopup("upgradeTower");
                }
                else
                {
                    //건설 해야함
                    
                        towerSpawnPos = ground.towerPos;
                        ground.attackRange.enabled = true;
                        isCreate = true;
                        PopupManager.instance.OpenPopup("createTower");
                }
            }
        }

        if(isCreate)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                towerRot.y -= 90f;

                ground.attackRange.transform.eulerAngles = new Vector3(90f, towerRot.y + 270f, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                towerRot.y += 90f;

                ground.attackRange.transform.eulerAngles = new Vector3(90f, towerRot.y +270f, 0f);
            }
        }
    }

    public void Init()
    {
        isCreate = false;
        if(ground != null)
        {
            ground.attackRange.enabled = false;
            ground.attackRange.transform.rotation = towerRot = initRot;
        }
        ground = null;
    }

    public void GroundStateChange()
    {
        if(ground != null)
        {
            ground.ChangeTowerGroundState(TowerGroundState.Builded);
        }
    }
    

    public Transform GetTowerSpawnPos()
    {
        return towerSpawnPos;
    }
    public void CreateTower(Transform parent)
    {
        F3DTurret obj =  Instantiate(towerPrefab, towerSpawnPos.position,Quaternion.identity, parent);

        obj.transform.localEulerAngles = new Vector3(0, towerRot.y, 0);
        print(towerRot.y);
        obj.transform.localScale = new Vector3(0.25f, 0.33f, 0.25f);
        TurretManager.Instance.AddTurret(obj);

    }

    public Vector2 GetLimit() 
    {
        float limitAngle = 120f;

        return new Vector2(-(limitAngle / 2f), limitAngle / 2f);
    }
}
