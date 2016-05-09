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
        base.TakeDamage(amount);
    }

    public override void Attack(UnitEntity unit)
    {
        base.Attack(unit);
    }
}
