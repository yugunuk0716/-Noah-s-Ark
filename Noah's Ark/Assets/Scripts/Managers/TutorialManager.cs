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

        ShowText("게임설명을 해드리겠습니다.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("좌측 상단에는 HP와 MP가 나와있습니다.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("그 밑에는 설치할 수 있는 타워 수가 나와 있으며, 이 수만큼 설치할 수 있습니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("흰색 땅에만 타워를 설치할 수 있습니다.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("타워를 설치하기전에, 땅을 누른 후 A,D 키를 사용하여 타워가 보는 방향을 정할 수 있습니다.", 1f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("우측 상단에는 Wave의 정보가 나와있으며 Wave를 전부 마칠시 클리어하게 됩니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("타워를 설치하여 할 일을 끝냈다면, 우측 하단에 있는 시작버튼을 눌러 Wave를 시작할 수 있습니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("또한, Esc키를 눌러 일시정지 하고 메뉴를 활성화 시킬 수 있습니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("나머지 플레이 방식은 필요할 때 가르쳐드리도록 하겠습니다.", 1.8f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;
        isTutoStart = false;

        HidePanel(true, 1f);
        yield return oneSecWait;

        yield return new WaitUntil(() => isWaveStart);

        HidePanel(false, 1f);
        yield return oneSecWait;

        ShowText("Wave가 시작하면 터렛에 들어갈 수 있습니다. 또한 Q,E키를 사용하여 타고 있는 터렛을 바꿀수 있습니다.", 2.5f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("좌클릭시 공격을 할 수 있으며, Space Bar로 MP를 사용하여 공격 스킬을 사용할 수 있습니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("그리고 숫자 1 키로 MP를 사용하여 적들을 3초동안 느리게 할 수 있습니다.", 2f);
        yield return new WaitUntil(() => isFinished);
        isFinished = false;

        ShowText("이상으로 설명을 마치겠습니다. 적들을 섬멸하세요!", 1.5f);
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
