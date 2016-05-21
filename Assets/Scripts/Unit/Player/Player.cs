using UnityEngine;
using System.Collections;

public class Player : UnitEntity {
    private static Player instance;
    private int accuracy = 80;    // 명중률

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
    }

    // 함수 재정의 -----------------------------------------------------
    // 데이터 저장
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt("Accuracy", accuracy);
        PlayerPrefs.SetInt("Evasion", evasion);
    }

    // 데이터 불러오기
    public override void LoadData()
    {
        base.LoadData();
        accuracy = PlayerPrefs.GetInt("Accuracy", accuracy);
        evasion = PlayerPrefs.GetInt("Evasion", evasion);
        //currentHealthPoint = healthPoint;
        jumpPower = jumpPower + (speed * 0.01f);
    }

    public override void TakeDamage(int amount)
    {
        // 회피율에 따라 플레이어에게 데미지를 주거나 못주거나.
        if (evasion >= Random.Range(1, 100))
        {
            //Debug.Log("회피");
            //base.TakeDamage(0);
        }
        else
            base.TakeDamage(amount);
    }

    public override void Attack(UnitEntity unit)
    {
        // 명중률에 따라 몬스터를 때리거나 못 때리거나
        if (accuracy >= Random.Range(1, 100))
        {
            //Debug.Log("명중");
            base.Attack(unit);
        }
    }

    // 프로퍼티 get -------------------------------------------
    //public int Evasion { get { return evasion; } }
}
