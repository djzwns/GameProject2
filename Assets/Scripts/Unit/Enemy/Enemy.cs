using UnityEngine;
using System.Collections;

public class Enemy : UnitEntity
{
    void Awake()
    {
        unitName = "enemy";
        LoadData();
    }
    // 함수 재정의 -----------------------------
    public override void SaveData()
    {
        base.SaveData();
    }
    public override void LoadData()
    {
        base.LoadData();
    }

    public override void TakeDamage(int amount)
    {
        // 회피율에 따라 데미지를 주거나 못주거나.
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
        base.Attack(unit);
    }
}
