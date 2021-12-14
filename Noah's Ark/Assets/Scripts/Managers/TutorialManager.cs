using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    private CanvasGroup cg = null;

    [Header("����")]
    [SerializeField] private Text tutorialText = null;
    [SerializeField] private Image skipImg = null;
    [SerializeField] private Text tipText = null;

    private string curText;

    private bool isText = false;

    private bool isTextEnd = false;
    private bool isFinished = false;
    public bool isTuto = false;
    public bool isWaveStart = false;
    public bool isTutoStart = false;

    private int tutoComplete = 0;

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

        tutoComplete = PlayerPrefs.GetInt("tutoComplete", 0);
    }

    private void Start()
    {
        if(StageManager.instance.Stage == 0 && tutoComplete == 0)
        {
            StartCoroutine(Tutorial());
        }
    }

    private void Update()
    {
        SkipText();
    }

    public bool Tuto_Ing()
    {
        return (isTutoStart || isWaveStart);
    }

    IEnumerator Tutorial()
    {
        isTuto = true;
        isTutoStart = true;
        HidePanel(false, 1f);
        yield return oneSecWait;

        ShowText("���Ӽ����� �ص帮�ڽ��ϴ�.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� ��ܿ��� HP�� MP�� �����ֽ��ϴ�.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("�� �ؿ��� ��ġ�� �� �ִ� Ÿ�� ���� ���� ������, �� ����ŭ ��ġ�� �� �ֽ��ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("��� ������ Ÿ���� ��ġ�� �� �ֽ��ϴ�.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("Ÿ���� ��ġ�ϱ�����, ���� ���� �� A,D Ű�� ����Ͽ� Ÿ���� ���� ������ ���� �� �ֽ��ϴ�.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("���� ��ܿ��� Wave�� ������ ���������� Wave�� ���� ��ĥ�� Ŭ�����ϰ� �˴ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("Ÿ���� ��ġ�Ͽ� �� ���� ���´ٸ�, ���� �ϴܿ� �ִ� ���۹�ư�� ���� Wave�� ������ �� �ֽ��ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("����, EscŰ�� ���� �Ͻ����� �ϰ� �޴��� Ȱ��ȭ ��ų �� �ֽ��ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("������ �÷��� ����� �ʿ��� �� �����ĵ帮���� �ϰڽ��ϴ�.", 1.8f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;
        isTutoStart = false;

        HidePanel(true, 1f);
        yield return oneSecWait;

        yield return new WaitUntil(() => isWaveStart);

        HidePanel(false, 1f);
        yield return oneSecWait;

        ShowText("Wave�� �����ϸ� �ͷ��� �� �� �ֽ��ϴ�. ���� Q,EŰ�� ����Ͽ� Ÿ�� �ִ� �ͷ��� �ٲܼ� �ֽ��ϴ�.", 2.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("��Ŭ���� ������ �� �� ������, Space Bar�� MP�� ����Ͽ� ���� ��ų�� ����� �� �ֽ��ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("�׸��� ���� 1 Ű�� MP�� ����Ͽ� ������ 3�ʵ��� ������ �� �� �ֽ��ϴ�.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("�̻����� ������ ��ġ�ڽ��ϴ�. ������ �����ϼ���!", 1.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        isWaveStart = false;
        WaveManager.Instance.DoNotSpawn = false;
        PlayerPrefs.SetInt("tutoComplete", 1);
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
