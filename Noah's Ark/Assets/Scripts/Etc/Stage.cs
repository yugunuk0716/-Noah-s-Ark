using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    // public Transform btnParent;
    public Button[] stageBtns;

    private void Start()
    {
        // stageBtns = btnParent.GetComponentsInChildren<Button>();

        //for (int i = 0; i < stageBtns.Length; i++)
        //{
        //    int a = i;
        //    stageBtns[a].onClick.AddListener(() =>
        //    {
                
        //        SceneManager.LoadScene("InGame");
        //    });
        //}

        stageBtns[0].onClick.AddListener(() =>
        {
            StageManager.instance.Stage = 0;
            SceneManager.LoadScene("InGame");
        });
        stageBtns[1].onClick.AddListener(() =>
        {
            StageManager.instance.Stage = 1;
            SceneManager.LoadScene("InGame2");
        });
    }
}
