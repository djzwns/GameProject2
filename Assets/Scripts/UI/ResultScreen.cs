using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScreen : ScreenManager {
    public Image resultImage;
    public Button resultButton;
    public Text resultText;
    private Color red = new Color(255, 0, 0);
    private Color green = new Color(0, 255, 0);

    void OnEnable()
    {
        resultImage.sprite = Resources.Load<Sprite>("Textures/background" + (int)(stage.currentStage/10 + 1));
        if (player.IsDead())
        {
            resultText.color = red;
            resultText.text = "MISSON FAILED";
        }
        else
        {
            resultText.color = green;
            resultText.text = "MISSON CLEAR";
        }
        StartCoroutine(FadeIn());
    }

    void OnDisable()
    {
        StopCoroutine(FadeIn());
    }

    // 이미지 점점 보이게 fixedUpdate 주기마다 알파 값 증가 시켜줌.
    IEnumerator FadeIn()
    {
        for (float i = 0; i < 256; i += 0.02f)
        {
            Color ImageColor = new Color(resultImage.color.r, resultImage.color.g, resultImage.color.b, i);
            //Color textColor = new Color(resultText.color.r, resultText.color.g, resultText.color.b, i);
            resultImage.color = ImageColor;
            //resultText.color = textColor;
            if(!resultButton.gameObject.activeSelf)
                resultButton.gameObject.SetActive(true);
            yield return new WaitForFixedUpdate();
        }
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
}
