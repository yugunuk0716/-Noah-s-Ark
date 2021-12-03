using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    public static TowerSpawner instance;

    public Tower towerPrefab;

    private Transform towerSpawnPos;

    public LayerMask isGround;
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
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 pos = Input.mousePosition;
            pos.z = Camera.main.farClipPlane;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit, Mathf.Infinity,1 << isGround))
            {
                Ground ground = hit.transform.GetComponent<Ground>();

                if (ground == null) return;

                if (ground.state == TowerGroundState.Builded)
                {
                    //이미 건설되어 있음
                }
                else
                {
                    //건설 해야함
                    PopupManager.instance.OpenPopup("createTower");
                    towerSpawnPos = ground.towerPos;
                    ground.ChangeTowerGroundState(TowerGroundState.Builded);
                }
            }
        }
    }
    

    public Transform GetTowerSpawnPos()
    {
        return towerSpawnPos;
    }
    public Tower CreateTower(Transform parent)
    {
        return Instantiate(towerPrefab, towerSpawnPos.position,Quaternion.identity, parent);
    }
}
