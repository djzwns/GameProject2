using UnityEngine;
using System.Collections;

public class Player : UnitEntity {
    private static Player instance;
    private int accuracy = 80;    // 명중률
    public int Accuracy { get{ return accuracy; } set{ accuracy = value; } }

    private int gold = 0;
    public int Gold {get{ return gold; } }

    private int arcadePoint = 0;
    public int ArcadePoint { get{ return arcadePoint; } }

    private int deadEnemyCount = 0;
    public int DeadEnemyCount { get{ return deadEnemyCount; } }
    public void EnemyCounting() { ++deadEnemyCount; }
    public void CountReset() { deadEnemyCount = 0; }

    // 인스턴스 받아옴
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<Player>() as Player;
                instance.LoadData();
            }

            return instance;
        }
    }

    void Awake()
    {
        unitName = "player";
        evasion = 20;
        attackSpeed = 0.25f;
    }

    // 돈 사용
    public bool UseGold(int _gold)
    {
        // 소유한 돈보다 많은 양을 필요로 할 떄 false
        if (_gold > gold)
        {
            return false;
        }
        else
        {
            gold -= _gold;
            return true;
        }
    }

    // 아케이드 포인트 사용
    public bool UseArcadePoint(int _ap)
    {
        // 소유한 포인트보다 많은 양을 필요로 할 떄 false
        if (_ap > arcadePoint)
        {
            return false;
        }
        else
        {
            arcadePoint -= _ap;
            return true;
        }
    }

    // 보상 받기
    public void RewardMoney(string _moneyType, int _money)
    {
        if (_moneyType.Equals("AP"))
        {
            arcadePoint += _money;
        }
        else
        {
            gold += _money;
        }
    }

    public void StrengthAdvantageOrPenalty(float _strength, float _percent = 0f)
    {
        if (_percent == 0f)
        {
            strength = _strength;
        }
        else
        {
            strength *= _percent;
        }
    }

    // 함수 재정의 -----------------------------------------------------
    // 데이터 저장
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt("Accuracy", accuracy);
        PlayerPrefs.SetInt("Evasion", evasion);
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("ArcadePoint", arcadePoint);
    }

    // 데이터 불러오기
    public override void LoadData()
    {
        base.LoadData();
        accuracy = PlayerPrefs.GetInt("Accuracy", accuracy);
        evasion = PlayerPrefs.GetInt("Evasion", evasion);
        gold = PlayerPrefs.GetInt("Gold", gold);
        arcadePoint = PlayerPrefs.GetInt("ArcadePoint", arcadePoint);
        //currentHealthPoint = healthPoint;
        //jumpPower = jumpPower + (speed * 0.01f);
    }

    public override float Attack(UnitEntity unit)
    {
        // 명중률에 따라 몬스터를 때리거나 못 때리거나
        if (accuracy >= Random.Range(1, 100))
        {
            //Debug.Log("명중");
            return base.Attack(unit);
        }
        return 0;
    }

    // 초기화
    public override void UnitInitialize()
    {
        base.UnitInitialize();
        accuracy = 80;    // 명중률
        gold = 0;
        arcadePoint = 0;
    }

    // 프로퍼티 get -------------------------------------------
    //public int Evasion { get { return evasion; } }
}
