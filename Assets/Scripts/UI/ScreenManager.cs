using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {
    private Text gold;
    private Text arcade;
    private GameObject stage_attribute;
    public AttributeScreen attributeScreen;
    public StageSelectScreen stageSelectScreen;
    protected StageManager stage;

    // NONE 강화소-스테이지, ATTRIBUTE 강화소, STAGESELECT 스테이지 선택
    protected enum E_SCREEN { NONE, ATTRIBUTE, STAGESELECT, STAGE }

    // 현재 어느 스크린을 띄우고 있는지,
    protected E_SCREEN currentScreen = E_SCREEN.NONE;

    protected Player player;

    void Awake()
    {
        stage = StageManager.Instance;
        player = Player.Instance;

        gold = GameObject.Find("Gold").GetComponentInChildren<Text>();
        arcade = GameObject.Find("ArcadePoint").GetComponentInChildren<Text>();

        stage_attribute = GameObject.FindGameObjectWithTag("stage_attribute");
        //attributeScreen = GameObject.FindGameObjectWithTag("attribute");
        //stageSelectScreen = GameObject.FindGameObjectWithTag("stage_select");
        //attributeScreen.ScreenDisable();
        //stageSelectScreen.ScreenDisable();

        PlayerMoneyUpdate();
    }

    // 강화소나 스테이지 버튼 눌렀을 때 실행
    public void GoScreen(string _screen)
    {
        if (_screen.Equals("ATTRIBUTE"))
        {
            currentScreen = E_SCREEN.ATTRIBUTE;
            attributeScreen.ScreenEnable();
        }
        else if (_screen.Equals("STAGESELECT"))
        {
            currentScreen = E_SCREEN.STAGESELECT;
            stageSelectScreen.ScreenEnable();
        }
        else
        {
            currentScreen = E_SCREEN.STAGE;
            stageSelectScreen.ScreenDisable();
            PrintGoldAP(false);
        }
        stage_attribute.SetActive(false);
    }

    // 뒤로가기 누르면 실행시킴, 
    public void ReturnScreen()
    {
        switch (currentScreen)
        {
            case E_SCREEN.NONE:
                // 메인 화면으로 나가요 만들면 추가해요
                break;
            case E_SCREEN.ATTRIBUTE:
                // 강화소나 스테이지 선택창일 때 뒤로가면 강화소-스테이지 선택창
                currentScreen = E_SCREEN.NONE;
                attributeScreen.ScreenDisable();
                stage_attribute.SetActive(true);
                break;
            case E_SCREEN.STAGESELECT:
                // 강화소나 스테이지 선택창일 때 뒤로가면 강화소-스테이지 선택창
                currentScreen = E_SCREEN.NONE;
                stageSelectScreen.ScreenDisable();
                stage_attribute.SetActive(true);
                break;
            default:
                // 겜 진행 중 일 때.
                currentScreen = E_SCREEN.STAGESELECT;
                stageSelectScreen.ScreenEnable();
                //stage_attribute.SetActive(true);
                PrintGoldAP(true);
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
}
