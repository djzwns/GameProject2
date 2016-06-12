using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {
    private Text gold;
    private Text arcade;

    // 스크린 관련 오브젝트 및 스크립트들
    private GameObject stage_attribute;
    private AttributeScreen attributeScreen;
    private StageSelectScreen stageSelectScreen;
    private ResultScreen resultScreen;
    private GameObject tapToStart;
    private GameObject newLoadScreen;
    private GameObject timer;
    private Text timerText;
    private GameObject progressBar;

    protected GameObject icicles;

    // 스테이지 매니저
    protected StageManager stage;

    // NONE tap to start, ATTRIBUTE 강화소, STAGESELECT 스테이지 선택
    protected enum E_SCREEN { NONE, NEWLOAD, SELECT, ATTRIBUTE, STAGESELECT, STAGE }

    // 현재 어느 스크린을 띄우고 있는지,
    protected E_SCREEN currentScreen = E_SCREEN.NONE;

    // 뒤로가기 연속으로 못누르게 시간사용
    private float dTime = 0f;

    protected Player player;

    void Awake()
    {
        currentScreen = E_SCREEN.NONE;
        stage = StageManager.Instance;
        stage.LoadStage();
        player = Player.Instance;

        gold = GameObject.Find("Gold_AP").GetComponentsInChildren<Text>()[0];
        arcade = GameObject.Find("Gold_AP").GetComponentsInChildren<Text>()[1];

        //stage_attribute = GameObject.FindGameObjectWithTag("stage_attribute");
        icicles = GameObject.Find("Icicles");

        stage_attribute = GameObject.Find("Stage_Attribute");
        tapToStart = GameObject.Find("TapToStart");
        newLoadScreen = GameObject.Find("New_Load");
        attributeScreen = GameObject.Find("AttributeScreen").GetComponent<AttributeScreen>();
        stageSelectScreen = GameObject.Find("StageScreen").GetComponent<StageSelectScreen>();
        resultScreen = GameObject.Find("GameResultScreen").GetComponent<ResultScreen>();

        timer = GameObject.Find("Timer");
        timerText = timer.GetComponentInChildren<Text>();

        progressBar = GameObject.Find("ProgressBar");

        // 도대체 뭐가 문젠지 ㅡㅡ 짜증나게 하네
        // 나중에라도 원인 알아낸다 진짜
        //attributeScreen.ScreenDisable();
        //stageSelectScreen.ScreenDisable();
        // 안되는 이유를 찾은 듯,
        // 오브젝트를 불러오자마자 비활성화 시키는 과정에서
        // 오브젝트가 제대로 완성도 안된 상태(awake나 start 함수 미실행)
        // 로 비활성화를 시켜서
        // 다음 사용 시에 문제가 생기는 듯 Start 함수 쪽으로 빼서 사용하니 잘됨
        ////////////////////////////////////////

        PlayerMoneyUpdate();
    }

    void Start()
    {
        icicles.SetActive(false);
        timer.SetActive(false);
        progressBar.SetActive(false);
        // 여기로 옮기고도 문제가 생기네 아 이해 할 수가 없다 ㅂㄷㅂㄷ
        //attributeScreen.ScreenDisable();
        //stageSelectScreen.ScreenDisable();
        //resultScreen.ScreenDisable();
        //stage_attribute.SetActive(false);
        //newLoadScreen.SetActive(false);
    }

    void Update()
    {
        if (currentScreen == E_SCREEN.STAGE && stage.GameClear() || player.IsDead())
        {
            resultScreen.ScreenEnable();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            // 터치시 실행
            if (currentScreen == E_SCREEN.NONE && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                currentScreen = E_SCREEN.NEWLOAD;
                tapToStart.SetActive(false);
                newLoadScreen.SetActive(true);
            }
        }
        else
        {
            // 컴터용
            if (currentScreen == E_SCREEN.NONE && Input.GetMouseButtonDown(0))
            {
                currentScreen = E_SCREEN.NEWLOAD;
                tapToStart.SetActive(false);
                newLoadScreen.SetActive(true);
            }
        }
        // 뒤로가기 버튼 한번에 여러번 실행 안되도록 시간 넣어줌
        if (dTime > 1f && Input.GetKey(KeyCode.Escape))
        {
            ReturnScreen();
            dTime = 0;
        }
        dTime += Time.deltaTime;

        UpdateTimer();
    }

    // 강화소나 스테이지 버튼 눌렀을 때 실행
    public void GoScreen(string _screen)
    {
        if (_screen.Equals("SELECT"))
        {
            currentScreen = E_SCREEN.SELECT;
            attributeScreen.LoadAttribute();
            // 이쪽으로 옮기니 정상 작동하는 ..
            // 도저히 이해 할 수 없다 ㅡㅡ
            attributeScreen.ScreenDisable();
            stageSelectScreen.ScreenDisable();
            resultScreen.ScreenDisable();
            ///////////////////////////////////
            newLoadScreen.SetActive(false);
            stage_attribute.SetActive(true);
            PrintGoldAP(true);
        }
        else if (_screen.Equals("ATTRIBUTE"))
        {
            currentScreen = E_SCREEN.ATTRIBUTE;
            stage_attribute.SetActive(false);
            attributeScreen.ScreenEnable();
        }
        else if (_screen.Equals("STAGESELECT"))
        {
            currentScreen = E_SCREEN.STAGESELECT;
            stage_attribute.SetActive(false);
            stageSelectScreen.ScreenEnable();
        }
        else
        {
            currentScreen = E_SCREEN.STAGE;
            stageSelectScreen.ScreenDisable();
            PrintGoldAP(false);
            progressBar.SetActive(true);
        }
        //stage_attribute.SetActive(false);
    }

    // 뒤로가기 누르면 실행시킴, 
    public void ReturnScreen()
    {
        switch (currentScreen)
        {
            case E_SCREEN.NONE:
                Application.Quit();
                break;

            case E_SCREEN.NEWLOAD:
                currentScreen = E_SCREEN.NONE;
                newLoadScreen.SetActive(false);
                tapToStart.SetActive(true);
                break;

            case E_SCREEN.SELECT:
                currentScreen = E_SCREEN.NEWLOAD;
                stage_attribute.SetActive(false);
                newLoadScreen.SetActive(true);
                PrintGoldAP(false);
                break;

            case E_SCREEN.ATTRIBUTE:
                // 강화소나 스테이지 선택창일 때 뒤로가면 강화소-스테이지 선택창
                currentScreen = E_SCREEN.SELECT;
                attributeScreen.ScreenDisable();
                stage_attribute.SetActive(true);
                break;

            case E_SCREEN.STAGESELECT:
                // 강화소나 스테이지 선택창일 때 뒤로가면 강화소-스테이지 선택창
                currentScreen = E_SCREEN.SELECT;
                stageSelectScreen.ScreenDisable();
                stage_attribute.SetActive(true);
                break;

            default:
                // 겜 진행 중 일 때.
                currentScreen = E_SCREEN.STAGESELECT;
                stageSelectScreen.ScreenEnable();
                //stage_attribute.SetActive(true);
                PrintGoldAP(true);
                progressBar.SetActive(false);
                if (icicles.activeSelf)
                    icicles.SetActive(false);
                stage.GameEnd();
                break;
        }
        player.UnitReset();
    }

    // 돈, 아케이드 포인트 업데이트
    protected void PlayerMoneyUpdate()
    {
        // 골드, 아케이드 포인트 각각 입력
        gold.text = player.Gold.ToString();
        arcade.text = player.ArcadePoint.ToString();
    }

    // 골드랑 아케이드 포인트 on off 시킴
    private void PrintGoldAP(bool _switch)
    {
        gold.transform.parent.gameObject.SetActive(_switch);
        arcade.transform.parent.gameObject.SetActive(_switch);
    }


    // 타이머 업뎃
    void UpdateTimer()
    {
        if (stage.stage[stage.currentStage].gameMode == GAMEMODE.Gamemode.ARCADE 
            && currentScreen == E_SCREEN.STAGE
            && stage.ArcadeTime > 0 
            && !player.IsDead())
        {
            timerText.text = stage.ArcadeTime.ToString("000");

            stage.ArcadeTime -= Time.deltaTime;
        }
        else
        {
            timer.SetActive(false);
        }
    }

    // 시간 초기화
    protected void TimerReset(float _time)
    {
        stage.TimerReset(_time);
        timer.SetActive(true);
        timerText.text = "";
    }
}
