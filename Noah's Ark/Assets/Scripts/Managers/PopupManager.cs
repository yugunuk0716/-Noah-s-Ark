using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupManager : MonoBehaviour
{
    public Transform popupParent;

    private CanvasGroup popupCanvasGroup;
    public CreateTowerPopup createTowerPopupPrefab;
    public UpgradeTowerPopup upgradeTowerPopupPrefab;
    public MenuPopup menuPopupPrefab;

    private Dictionary<string, Popup> popupDic = new Dictionary<string, Popup>();
    private Stack<Popup> popupStack = new Stack<Popup>();

    public static PopupManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 팝업매니저가 실행중");
        }
        instance = this;
    }

    private void Start()
    {
        popupCanvasGroup = popupParent.GetComponent<CanvasGroup>();
        if (popupCanvasGroup == null)
        {
            popupCanvasGroup = popupParent.gameObject.AddComponent<CanvasGroup>();
        }
        popupCanvasGroup.alpha = 0;
        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;

        popupDic.Add("createTower", Instantiate(createTowerPopupPrefab, popupParent));
        popupDic.Add("upgradeTower", Instantiate(upgradeTowerPopupPrefab, popupParent));
        popupDic.Add("menu", Instantiate(menuPopupPrefab, popupParent));
    }

    public void OpenPopup(string name, object data = null, int closeCount = 1)
    {
        //최초로 열리는 팝업
        if (popupStack.Count == 0)
        {
            //popupCanvasGroup.interactable = true;
            //DOTween.To(() => popupCanvasGroup.alpha, value => popupCanvasGroup.alpha = value, 1, 0.8f).OnComplete(() =>
            //{
                popupCanvasGroup.alpha = 1;
                popupCanvasGroup.interactable = true;
                popupCanvasGroup.blocksRaycasts = true;
            //});

        }
        popupStack.Push(popupDic[name]);
        popupDic[name].Open(data, closeCount);
    }

    public void ClosePopup()
    {
        popupStack.Pop().Close();
        if (popupStack.Count == 0)
        {
            //DOTween.To(() => popupCanvasGroup.alpha, value => popupCanvasGroup.alpha = value, 0, 0.8f).OnComplete(() =>
            //{
                popupCanvasGroup.alpha = 0;
                popupCanvasGroup.interactable = false;
                popupCanvasGroup.blocksRaycasts = false;
            //});
        }


    }
}
