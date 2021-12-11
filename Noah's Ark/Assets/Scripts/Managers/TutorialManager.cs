using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    private CanvasGroup cg = null;

    [Header("설명")]
    [SerializeField] private Text tutorialText = null;
    [SerializeField] private Image skipImg = null;
    [SerializeField] private Text tipText = null;

    private string curText;

    private bool isText = false;

    private bool isTextEnd = false;
    private bool isFinished = false;
    public bool isTuto = false;

    private Tweener textTween = null;

    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        tutorialText.text = " ";

        skipImg.enabled = false;
        skipImg.transform.DOLocalMoveY(skipImg.transform.localPosition.y + 10f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        SkipText();
    }

    IEnumerator Tutorial()
    {
        isTuto = true;
        HidePanel(false, 1f);
        yield return oneSecWait;

        ShowText("할말", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        HidePanel(true, 1f);
        yield return oneSecWait;
        isTuto = false;
    }

    private void ShowText(string text, float dur = 1f)
    {
        skipImg.enabled = false;
        tipText.DOKill();
        tipText.color = new Color(1, 1, 1, 0);

        isText = true;
        isTextEnd = false;

        curText = text;

        tutorialText.text = "";
        textTween = tutorialText.DOText(text, dur)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        isTextEnd = true;
                        skipImg.enabled = true;
                        tipText.DOFade(1, 0.75f).SetLoops(-1, LoopType.Yoyo);
                    });
    }

    private void HidePanel(bool isHide, float dur = 1f)
    {
        if (isHide)
        {
            cg.DOFade(0f, dur);
        }
        else
        {
            cg.DOFade(1f, dur);
        }
    }

    private void SkipText()
    {
        if (!isText) return;

        if (!isTextEnd && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            isTextEnd = true;
            skipImg.enabled = true;

            textTween.Kill();
            tutorialText.text = curText;
            tipText.DOFade(1, 0.75f).SetLoops(-1, LoopType.Yoyo);
        }
        else if (isTextEnd && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            isText = false;
            isFinished = true;
        }
    }
}
