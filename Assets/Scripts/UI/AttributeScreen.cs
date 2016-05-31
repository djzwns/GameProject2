using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributeScreen : MonoBehaviour {
    // 플레이어 정보 및 함수들 사용
    private Player player;

    // 플레이어 정보 출력할 텍스트
    // 체력, 공격력, 힘, 방어력, 명중률, 회피율, 돈, 아케이드 포인트
    public Text[] playerInfoText;
    public Text[] increaseAmount;
    public Text[] price;

    // 골드 구매 가격
    private int healthGoldPrice = 10;
    private int strengthGoldPrice = 10;
    private int powerGoldPrice = 10;

    // 골드 구매 증가량
    private int increaseHealth_Gold = 10;
    private int increaseStrength_Gold = 10;
    private int increasePower_Gold = 1;

    // 아케이드 포인트 구매 가격
    private int healthAPPrice = 1;
    private int strengthAPPrice = 1;
    private int defenceAPPrice = 1;
    private int accuracyAPPrice = 1;
    private int evasionAPPrice = 1;

    // 아케이드 포인트 구매 증가량
    private int increaseHealth_AP = 1;       // 퍼센트 증가
    private int increaseStrength_AP = 1;     // 퍼센트 증가
    private int increaseDefence_AP = 1;      // 퍼센트 x   증가량 고정
    private int increaseAccuracy_AP = 1;     // 퍼센트 x   증가량 고정
    private int increaseEvasion_AP = 1;      // 퍼센트 x   증가량 고정

    // 골드 총 사용량
    private int usedGold = 0;
    // 아케이드 포인트 총 사용량
    private int usedAP = 0;

    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttributeUpdate();
        PlayerMoneyUpdate();
        IncreaseAmountUpdate();
    }

    // 플레이어 능력치 업데이트
    void PlayerAttributeUpdate()
    {
        // 체력, 공격력, 힘, 방어력, 명중률, 회피율 각각 텍스트에 입력
        playerInfoText[0].text = player.healthPoint.ToString();
        playerInfoText[1].text = player.strength.ToString();
        playerInfoText[2].text = player.power.ToString();
        playerInfoText[3].text = player.defence.ToString();
        playerInfoText[4].text = player.Accuracy.ToString();
        playerInfoText[5].text = player.evasion.ToString();
    }

    void IncreaseAmountUpdate()
    {
        increaseAmount[0].text = increaseHealth_Gold.ToString();
        price[0].text = healthGoldPrice.ToString();
        increaseAmount[1].text = increaseStrength_Gold.ToString();
        price[1].text = strengthGoldPrice.ToString();
        increaseAmount[2].text = increasePower_Gold.ToString();
        price[2].text = powerGoldPrice.ToString();

        increaseAmount[3].text = increaseHealth_AP.ToString();
        price[3].text = healthAPPrice.ToString();
        increaseAmount[4].text = increaseStrength_AP.ToString();
        price[4].text = strengthAPPrice.ToString();
        increaseAmount[5].text = increaseDefence_AP.ToString();
        price[5].text = defenceAPPrice.ToString();
        increaseAmount[6].text = increaseAccuracy_AP.ToString();
        price[6].text = accuracyAPPrice.ToString();
        increaseAmount[7].text = increaseEvasion_AP.ToString();
        price[7].text = evasionAPPrice.ToString();
    }

    // 돈, 아케이드 포인트 업데이트
    void PlayerMoneyUpdate()
    {
        // 골드, 아케이드 포인트 각각 입력
        playerInfoText[6].text = player.Gold.ToString();
        playerInfoText[7].text = player.ArcadePoint.ToString();
    }


    // 세이브 로드
    void SaveAttribute()
    {
        //PlayerPrefs.SetInt("UsedGold", usedGold);
        //PlayerPrefs.SetInt("UsedAP", usedAP);
    }
    void LoadAttribute()
    {
    }

    /// ====== 버튼 입력시 사용할 함수 =================================================
    /// 

    /* -------- 골드를 사용한 구매 함수들 -------- */
    // 체력 구매
    public void PurchaseHealthGold()
    {
        if (player.UseGold(healthGoldPrice))
        {
            usedGold += healthGoldPrice;

            player.healthPoint += increaseHealth_Gold;
            healthGoldPrice += 10;
            increaseHealth_Gold += 10;
        }
    }
    // 공격력 구매
    public void PurchaseStrenghGold()
    {
        if (player.UseGold(strengthGoldPrice))
        {
            usedGold += strengthGoldPrice;

            player.strength += increaseStrength_Gold;
            strengthGoldPrice += 10;
            increaseStrength_Gold += 10;
        }
    }
    // 힘 구매
    public void PurchasePowerGold()
    {
        if (player.UseGold(powerGoldPrice))
        {
            usedGold += powerGoldPrice;

            player.power += increasePower_Gold;
            powerGoldPrice += 10;
            increasePower_Gold += 1;
        }
    }

    /* -------- 아케이드 포인트를 사용한 구매 함수들 -------- */
    // 퍼센트가 붙는 능력치는 바로 능력치를 올리지 않고
    // 최종 능력치에 퍼센트를 붙이기 때문에 나중에 증가 시킴
    // 여기서는 체력과 공격력만 퍼센트
    // 체력
    public void PurchaseHealthAP()
    {
        if (player.UseArcadePoint(healthAPPrice))
        {
            usedAP += healthAPPrice;

            healthAPPrice += 1;
            increaseHealth_AP += 1;
        }
    }
    // 공격력
    public void PurchaseStrenthAP()
    {
        if (player.UseArcadePoint(strengthAPPrice))
        {
            usedAP += strengthAPPrice;

            strengthAPPrice += 1;
            increaseStrength_AP += 1;
        }
    }
    // 방어력
    public void PurchaseDefenceAP()
    {
        if (player.UseArcadePoint(defenceAPPrice))
        {
            usedAP += defenceAPPrice;

            player.defence += increaseDefence_AP;
            defenceAPPrice += 1;
        }
    }
    // 명중률
    public void PurchaseAccuracyAP()
    {
        if (player.UseArcadePoint(accuracyAPPrice))
        {
            usedAP += accuracyAPPrice;

            player.Accuracy += increaseAccuracy_AP;
            accuracyAPPrice += 1;
        }
    }
    // 회피율
    public void PurchaseEvasionAP()
    {
        if (player.UseArcadePoint(evasionAPPrice))
        {
            usedAP += evasionAPPrice;

            player.evasion += increaseEvasion_AP;
            evasionAPPrice += 1;
        }
    }


    // AttributeScreen 활성화
    public void ScreenEnable()
    {
        SaveAttribute();
        gameObject.SetActive(true);
    }

    // AttributeScreen 비활성화
    public void ScreenDisable()
    {
        LoadAttribute();
        gameObject.SetActive(false);
    }
}
