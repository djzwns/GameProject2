using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScreen : ScreenManager {
    public Image resultImage;
    public Button resultButton;
    public Text resultText;
    public Text rewardText;

    private Color red = new Color(255, 0, 0);
    private Color green = new Color(0, 255, 0);
    private Color blue = new Color(0, 0, 255);
    private Color yellow = new Color(255, 255, 0);

    void OnEnable()
    {
        resultImage.sprite = Resources.Load<Sprite>("Textures/background" + (int)(stage.currentStage/10 + 1));

        // 죽었을 때 FAILED
        if (player.IsDead())
        {
            resultText.color = red;
            rewardText.text = "";
            resultText.text = "MISSON FAILED";
        }
        // 이겼을 때 CLEAR
        else
        {
            resultText.color = green;
            resultText.text = "MISSON CLEAR";

            RewardUpdate();
        }
        StartCoroutine(FadeIn());
    }

    void OnDisable()
    {
        PlayerMoneyUpdate();
        StopCoroutine(FadeIn());
    }

    // 이미지 점점 보이게 fixedUpdate 주기마다 알파 값 증가 시켜줌.
    IEnumerator FadeIn()
    {
        for (float i = 0; i < 1f; i += 0.01f)
        {
            Color ImageColor = new Color(resultImage.color.r, resultImage.color.g, resultImage.color.b, i);
            //Color textColor = new Color(resultText.color.r, resultText.color.g, resultText.color.b, i);
            resultImage.color = ImageColor;
            //resultText.color = textColor;
            yield return new WaitForSeconds(0.001f);
        }
        if (!resultButton.gameObject.activeSelf)
            resultButton.gameObject.SetActive(true);
    }

    public void ScreenEnable()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    public void ScreenDisable()
    {
        gameObject.SetActive(false);
    }


    // 보상 결과
    private void RewardUpdate()
    {
        // 스테이지가 최대치로 갱신 될 때 보상줌
        if (stage.NextStage())
        {
            int money = 0;
            // 아케이드 모드일 때
            if (stage.stage[stage.currentStage].gameMode == GAMEMODE.Gamemode.ARCADE)
            {
                money = stage.stage[stage.currentStage].reward_ap;
                rewardText.color = blue;
                rewardText.text = "+" + money.ToString() + "ap";
                player.RewardMoney("AP", money);
            }
            // 일반 스토리 모드일 때
            else
            {
                money = stage.stage[stage.currentStage].reward_gold;
                rewardText.color = yellow;
                rewardText.text = "+" + money.ToString() + "G";
                player.RewardMoney("GOLD", money);
            }
        }
        else
        {
            rewardText.text = "";
        }
    }
}
