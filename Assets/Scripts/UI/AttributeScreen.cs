using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributeScreen : ScreenManager {
    // 플레이어 정보 출력할 텍스트
    // 체력, 공격력, 힘, 방어력, 명중률, 회피율, 돈, 아케이드 포인트
    public Text[] playerInfoText;
    public Text[] totalGainAmount;
    public Text[] price;

    // 골드 능력치 구매 가격
    private int gold_healthPrice = 500;
    private int gold_strengthPrice = 500;
    private int gold_powerPrice = 500;

    // 골드 능력치 증가량
    private int gold_increaseHealth = 50;
    private int gold_increaseStrength = 5;
    private int gold_increasePower = 1;

    // 골드 구매로 그 동안 얼만큼 증가시켰는지 누적 증가량
    private int gold_totalGainHealth = 0;
    private int gold_totalGainStrength = 0;
    private int gold_totalGainPower = 0;

    // 아케이드 포인트 구매 가격
    private int ap_healthPrice = 1;
    private int ap_strengthPrice = 1;
    private int ap_defencePrice = 1;
    private int ap_accuracyPrice = 1;
    private int ap_evasionPrice = 1;

    // 아케이드 포인트 구매 증가량
    private int ap_increaseHealth = 1;       // 퍼센트 증가
    private int ap_increaseStrength = 1;     // 퍼센트 증가
    private int ap_increaseDefence = 2;      // 퍼센트 x   증가량 고정
    private int ap_increaseAccuracy = 1;     // 퍼센트 x   증가량 고정
    private int ap_increaseEvasion = 1;      // 퍼센트 x   증가량 고정

    // 골드 구매로 그 동안 얼만큼 증가시켰는지 누적 증가량
    private int ap_totalGainHealth = 0;
    private int ap_totalGainStrength = 0;
    private int ap_totalGainDefence = 0;
    private int ap_totalGainAccuracy = 0;
    private int ap_totalGainEvasion = 0;


    // 골드 총 사용량
    private int usedGold = 0;
    // 아케이드 포인트 총 사용량
    private int usedAP = 0;

    // 버튼 누르면 업뎃
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
        playerInfoText[0].text = player.healthPoint.ToString("0.0");
        playerInfoText[1].text = player.strength.ToString("0.0");
        playerInfoText[2].text = player.power.ToString();
        playerInfoText[3].text = player.defence.ToString();
        playerInfoText[4].text = player.Accuracy.ToString();
        playerInfoText[5].text = player.evasion.ToString();
    }

    void IncreaseAmountUpdate()
    {
        totalGainAmount[0].text = gold_totalGainHealth.ToString();
        price[0].text = gold_healthPrice.ToString();
        totalGainAmount[1].text = gold_totalGainStrength.ToString();
        price[1].text = gold_strengthPrice.ToString();
        totalGainAmount[2].text = gold_totalGainPower.ToString();
        price[2].text = gold_powerPrice.ToString();

        totalGainAmount[3].text = ap_totalGainHealth.ToString();
        price[3].text = ap_healthPrice.ToString();
        totalGainAmount[4].text = ap_totalGainStrength.ToString();
        price[4].text = ap_strengthPrice.ToString();
        totalGainAmount[5].text = ap_totalGainDefence.ToString();
        price[5].text = ap_defencePrice.ToString();
        totalGainAmount[6].text = ap_totalGainAccuracy.ToString();
        price[6].text = ap_accuracyPrice.ToString();
        totalGainAmount[7].text = ap_totalGainEvasion.ToString();
        price[7].text = ap_evasionPrice.ToString();
    }

    // 초기화
    void AttributeReset()
    {
        // 골드 능력치 구매 가격
        gold_healthPrice = 500;
        gold_strengthPrice = 500;
        gold_powerPrice = 500;

        // 골드 구매로 그 동안 얼만큼 증가시켰는지 누적 증가량
        gold_totalGainHealth = 0;
        gold_totalGainStrength = 0;
        gold_totalGainPower = 0;

        // 아케이드 포인트 구매 가격
        ap_healthPrice = 1;
        ap_strengthPrice = 1;
        ap_defencePrice = 1;
        ap_accuracyPrice = 1;
        ap_evasionPrice = 1;

        // 골드 구매로 그 동안 얼만큼 증가시켰는지 누적 증가량
        ap_totalGainHealth = 0;
        ap_totalGainStrength = 0;
        ap_totalGainDefence = 0;
        ap_totalGainAccuracy = 0;
        ap_totalGainEvasion = 0;

        usedGold = 0;
        usedAP = 0;
    }

    // 세이브
    public void SaveAttribute()
    {
        PlayerPrefs.SetInt("GoldHealthPrice", gold_healthPrice);
        PlayerPrefs.SetInt("GoldStrengthPrice", gold_strengthPrice);
        PlayerPrefs.SetInt("GoldPowerPrice", gold_powerPrice);

        PlayerPrefs.SetInt("GoldTotalHealth", gold_totalGainHealth);
        PlayerPrefs.SetInt("GoldTotalStrength", gold_totalGainStrength);
        PlayerPrefs.SetInt("GoldTotalPower", gold_totalGainPower);

        PlayerPrefs.SetInt("APHealthPrice", ap_healthPrice);
        PlayerPrefs.SetInt("APStrengthPrice", ap_strengthPrice);
        PlayerPrefs.SetInt("APDefencePrice", ap_defencePrice);
        PlayerPrefs.SetInt("APAccuracyPrice", ap_accuracyPrice);
        PlayerPrefs.SetInt("APEvasionPrice", ap_evasionPrice);

        PlayerPrefs.SetInt("APTotalHealth", ap_totalGainHealth);
        PlayerPrefs.SetInt("APTotalStrength", ap_totalGainStrength);
        PlayerPrefs.SetInt("APTotalDefence", ap_totalGainDefence);
        PlayerPrefs.SetInt("APTotalAccuracy", ap_totalGainAccuracy);
        PlayerPrefs.SetInt("APTotalEvasion", ap_totalGainEvasion);

        PlayerPrefs.SetInt("UsedGold", usedGold);
        PlayerPrefs.SetInt("UsedAP", usedAP);
    }

    // 로드
    public void LoadAttribute()
    {
        gold_healthPrice = PlayerPrefs.GetInt("GoldHealthPrice", gold_healthPrice);
        gold_strengthPrice = PlayerPrefs.GetInt("GoldStrengthPrice", gold_strengthPrice);
        gold_powerPrice = PlayerPrefs.GetInt("GoldPowerPrice", gold_powerPrice);

        gold_totalGainHealth = PlayerPrefs.GetInt("GoldTotalHealth", gold_totalGainHealth);
        gold_totalGainStrength = PlayerPrefs.GetInt("GoldTotalStrength", gold_totalGainStrength);
        gold_totalGainPower = PlayerPrefs.GetInt("GoldTotalPower", gold_totalGainPower);

        ap_healthPrice = PlayerPrefs.GetInt("APHealthPrice", ap_healthPrice);
        ap_strengthPrice = PlayerPrefs.GetInt("APStrengthPrice", ap_strengthPrice);
        ap_defencePrice = PlayerPrefs.GetInt("APDefencePrice", ap_defencePrice);
        ap_accuracyPrice = PlayerPrefs.GetInt("APAccuracyPrice", ap_accuracyPrice);
        ap_evasionPrice = PlayerPrefs.GetInt("APEvasionPrice", ap_evasionPrice);

        ap_totalGainHealth = PlayerPrefs.GetInt("APTotalHealth", ap_totalGainHealth);
        ap_totalGainStrength = PlayerPrefs.GetInt("APTotalStrength", ap_totalGainStrength);
        ap_totalGainDefence = PlayerPrefs.GetInt("APTotalDefence", ap_totalGainDefence);
        ap_totalGainAccuracy = PlayerPrefs.GetInt("APTotalAccuracy", ap_totalGainAccuracy);
        ap_totalGainEvasion = PlayerPrefs.GetInt("APTotalEvasion", ap_totalGainEvasion);

        usedGold = PlayerPrefs.GetInt("UsedGold", usedGold);
        usedAP = PlayerPrefs.GetInt("UsedAP", usedAP);
    }

    // 속성 초기화
    public void AttributeInitialize()
    {
        AttributeReset();
        SaveAttribute();
        LoadAttribute();
        PlayerMoneyUpdate();
    }

    /// ====== 버튼 입력시 사용할 함수 =================================================
    /// 

    /* -------- 골드를 사용한 구매 함수들 -------- */
    // 체력 구매
    public void PurchaseHealthGold()
    {
        if (player.UseGold(gold_healthPrice))
        {
            usedGold += gold_healthPrice;

            player.healthPoint += gold_increaseHealth;
            gold_healthPrice += 10;
            gold_totalGainHealth += gold_increaseHealth;
        }
    }
    // 공격력 구매
    public void PurchaseStrenghGold()
    {
        if (player.UseGold(gold_strengthPrice))
        {
            usedGold += gold_strengthPrice;

            player.strength += gold_increaseStrength;
            gold_strengthPrice += 10;
            gold_totalGainStrength += gold_increaseStrength;
        }
    }
    // 힘 구매
    public void PurchasePowerGold()
    {
        if (player.UseGold(gold_powerPrice))
        {
            usedGold += gold_powerPrice;

            player.power += gold_increasePower;
            gold_powerPrice += 10;
            gold_totalGainPower += gold_increasePower;
        }
    }

    /* -------- 아케이드 포인트를 사용한 구매 함수들 -------- */
    // 퍼센트가 붙는 능력치는 바로 능력치를 올리지 않고
    // 최종 능력치에 퍼센트를 붙이기 때문에 나중에 증가 시킴
    // 여기서는 체력과 공격력만 퍼센트
    // 체력
    public void PurchaseHealthAP()
    {
        if (player.UseArcadePoint(ap_healthPrice))
        {
            usedAP += ap_healthPrice;

            ap_healthPrice += 1;

            float percent = 1f + (ap_totalGainHealth * 0.01f);
            player.healthPoint *= (1 / percent);   // 퍼센트 효과 받기 전의 체력으로 되돌리기

            ap_totalGainHealth += ap_increaseHealth;

            percent = 1f + (ap_totalGainHealth * 0.01f);
            player.healthPoint *= percent; // 증가한 퍼센트만큼 체력 올림
        }
    }
    // 공격력
    public void PurchaseStrenthAP()
    {
        if (player.UseArcadePoint(ap_strengthPrice))
        {
            usedAP += ap_strengthPrice;

            ap_strengthPrice += 1;

            float percent = 1f + (ap_totalGainStrength * 0.01f);
            player.strength *= (1 / percent);   // 퍼센트 효과 받기 전의 체력으로 되돌리기

            ap_totalGainStrength += ap_increaseStrength;

            percent = 1f + (ap_totalGainStrength * 0.01f);
            player.strength *= percent; // 증가한 퍼센트만큼 체력 올림
        }
    }
    // 방어력
    public void PurchaseDefenceAP()
    {
        if (player.UseArcadePoint(ap_defencePrice))
        {
            usedAP += ap_defencePrice;

            player.defence += ap_increaseDefence;
            ap_defencePrice += 1;
            ap_totalGainDefence += ap_increaseDefence;
        }
    }
    // 명중률
    public void PurchaseAccuracyAP()
    {
        if (player.UseArcadePoint(ap_accuracyPrice))
        {
            usedAP += ap_accuracyPrice;

            player.Accuracy += ap_increaseAccuracy;
            ap_accuracyPrice += 1;
            ap_totalGainAccuracy += ap_increaseAccuracy;
        }
    }
    // 회피율
    public void PurchaseEvasionAP()
    {
        if (player.UseArcadePoint(ap_evasionPrice))
        {
            usedAP += ap_evasionPrice;

            player.evasion += ap_increaseEvasion;
            ap_evasionPrice += 1;
            ap_totalGainEvasion += ap_increaseEvasion;
        }
    }


    // AttributeScreen 활성화
    public void ScreenEnable()
    {
        gameObject.SetActive(true);
        LoadAttribute();
    }

    // AttributeScreen 비활성화
    public void ScreenDisable()
    {
        //if (gameObject.activeSelf)
        //{
            SaveAttribute();
            gameObject.SetActive(false);
        //}
    }
}
