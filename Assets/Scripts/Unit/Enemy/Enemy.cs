using UnityEngine;
using System.Collections;

public class Enemy : UnitEntity
{
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }

    public override void Attack(UnitEntity unit)
    {
        base.Attack(unit);
    }
}
