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

    public F3DTurret towerPrefab;

    private Transform towerSpawnPos;

    Quaternion towerRot;

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

        towerRot = Quaternion.identity;
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
                    //�̹� �Ǽ��Ǿ� ����
                    //������ �Ѱ��ٰ� ������ ���⼭ �Ѱ��ָ��
                    //���� �ű⿡ ������ ���ٸ� �������� 
                    PopupManager.instance.OpenPopup("upgradeTower");
                }
                else
                {
                    //�Ǽ� �ؾ���
                    towerSpawnPos = ground.towerPos;
                    ground.ChangeTowerGroundState(TowerGroundState.Builded);
                    isCreate = true;
                    PopupManager.instance.OpenPopup("createTower");

                }
            }
        }

        if(isCreate)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                towerRot.y += 90f;
            } else if (Input.GetKeyDown(KeyCode.S))
            {
                towerRot.y -= 90f;
            }
        }
    }

    public void Init()
    {
        isCreate = false;
        towerRot = Quaternion.identity;
    }
    

    public Transform GetTowerSpawnPos()
    {
        return towerSpawnPos;
    }
    public F3DTurret CreateTower(Transform parent)
    {
        return Instantiate(towerPrefab, towerSpawnPos.position,towerRot, parent);
    }
}
