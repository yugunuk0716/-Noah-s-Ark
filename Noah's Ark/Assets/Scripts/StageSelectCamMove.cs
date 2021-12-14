using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageSelectCamMove : MonoBehaviour
{
    public float rotateDuration = 1.0f;
    public List<Vector3> targetCamRotation = new List<Vector3>(); // 회전 목표 리스트

    private int curIdx = 0;
    private bool isRotEnded = true;

    private void Start() {
        transform.eulerAngles = targetCamRotation[0];
    }

    void Update()
    {
        if(!isRotEnded) return;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            curIdx = curIdx + 1 >= targetCamRotation.Count ? 0 : curIdx + 1;
            SetRotation();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            curIdx = curIdx - 1 < 0 ? targetCamRotation.Count - 1 : curIdx - 1;
            SetRotation();
        }
    }

    public void SetRotation()
    {
        isRotEnded = false;
        transform.DORotate(targetCamRotation[curIdx], rotateDuration).SetEase(Ease.InOutSine).OnComplete(() => {
            isRotEnded = true;
        });
    }
}
