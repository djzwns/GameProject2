using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelectScreen : ScreenManager {
    public Button[] BigStageButton;
    public Button[] SmallStageButton;

    //private StageManager stage;
    private int bigStageNum = 0;

    void Start()
    {
        //stage = StageManager.Instance;
    }

    // 큰 스테이지 선택
    public void BigStageSelect(int _stage)
    {
        // 10 단위 증가.
        bigStageNum = _stage*10;
        SmallButtonDisable();
        SmallStageEnable(_stage, stage.AchieveStage);
    }

    // 작은 스테이지 선택
    public void SmallStageSelect(int _stage)
    {
        ScreenDisable();
        // 1 단위 증가.
        stage.currentStage = bigStageNum + _stage;
        stage.GameStart();
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().CamReset();

        if (stage.stage[stage.currentStage].gameMode == GAMEMODE.Gamemode.ARCADE)
            icicles.SetActive(true);
    }

    // 활성화 되면..
    void OnEnable()
    {
        stage = StageManager.Instance;
        bigStageNum = stage.currentStage / 10;
        BigButtonDisable();
        SmallButtonDisable();
        BigStageEnable(stage.AchieveStage);
        SmallStageEnable(bigStageNum, stage.AchieveStage);
    }

    void OnDisable()
    {
    }


    // 큰 스테이지 버튼 활성화
    void BigStageEnable(int _achieveStage)
    {
        int buttonCount = _achieveStage / 10;
        for (int i = 0; i <= buttonCount; ++i)
        {
            BigStageButton[i].interactable = true;
            BigStageButton[i].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/UI/" + (i+1).ToString());
        }
    }

    // 작은 스테이지 버튼 활성화
    void SmallStageEnable(int _bigStage, int _achieveStage)
    {
        // 달성 스테이지의 앞 자리보다 작으면
        if (_bigStage < _achieveStage / 10)
        {// 버튼 전부 활성화
            int i = 0;
            foreach (var button in SmallStageButton)
            {
                button.interactable = true;
                button.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/UI/" + (i + 1).ToString());
                ++i;
            }
        }
        else
        {// 달성한 곳 까지만 활성화
            for (int i = 0; i <= _achieveStage % 10; ++i)
            {
                SmallStageButton[i].interactable = true;
                SmallStageButton[i].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/UI/" + (i+1).ToString());
            }
        }
    }

    void BigButtonDisable()
    {
        foreach (var button in BigStageButton)
        {
            button.interactable = false;
            button.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/UI/lock");
        }
    }

    void SmallButtonDisable()
    {
        foreach (var button in SmallStageButton)
        {
            button.interactable = false;
            button.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/UI/lock");
        }
    }

    // 외부에서 스크린 켜고 끄고..
    public void ScreenEnable()
    {
        //if (currentScreen == E_SCREEN.STAGESELECT || currentScreen == E_SCREEN.NONE)
        //{
        gameObject.SetActive(true);
        //}
    }

    public void ScreenDisable()
    {
        gameObject.SetActive(false);
    }
}
