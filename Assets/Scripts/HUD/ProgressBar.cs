using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {
    public Slider progressBar;
    public Text advantageText;
    public Text penaltyText;

    public GameObject player;
    public GameObject floor;

    private float floorSizeX;

    private float playerStrength;

    void OnEnable()
    {
        // 바닥의 길이 구해옴
        floorSizeX = floor.GetComponent<BoxCollider2D>().size.x;
        playerStrength = Player.Instance.strength;      
    }

    void OnDisable()
    {
        Player.Instance.StrengthAdvantageOrPenalty(playerStrength);
        penaltyText.gameObject.SetActive(false);
        advantageText.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        MoveHandle();
        AdvantageOrPenaltyPercent();
    }

    // 핸들이 플레이어의 위치에 따라 이동
    void MoveHandle()
    {
        float playerX = player.transform.position.x + 6.4f;
        progressBar.value = playerX / floorSizeX;
    }

    // 화면 일정 수준에 도달 시 어드밴티지나 패널티 부여
    void AdvantageOrPenaltyPercent()
    {
        if (progressBar.value <= 0.2f)
        {
            Player.Instance.StrengthAdvantageOrPenalty(playerStrength);
            Player.Instance.StrengthAdvantageOrPenalty(0f, 0.7f);

            if (!penaltyText.IsActive())
                penaltyText.gameObject.SetActive(true);
        }

        else if (progressBar.value >= 0.5f)
        {
            Player.Instance.StrengthAdvantageOrPenalty(playerStrength);
            Player.Instance.StrengthAdvantageOrPenalty(0f, 1.2f);

            if (!advantageText.IsActive())
                advantageText.gameObject.SetActive(true);
        }
        else
        {
            Player.Instance.StrengthAdvantageOrPenalty(playerStrength);

            if (penaltyText.IsActive())
                penaltyText.gameObject.SetActive(false);
            if (advantageText.IsActive())
                advantageText.gameObject.SetActive(false);
        }
    }
}
