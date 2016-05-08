using UnityEngine;
using System.Collections;

public class UnitEntity : ScriptableObject {
    public int defence = 10;            // 방어력
    public int strength = 500;         // 공격력
    public int power = 100;            // 힘
    public float helthPoint = 5000f;   // 최대 체력
    public float speed = 2f;           // 이동속도
    protected float currentHelthPoint; // 현재 체력
    protected float jumpPower = 3f;    // 점프력
    protected float attackSpeed = 2f;

    // 데미지를 0부터 최대 값까지.
    public virtual void TakeDamage(int amount)
    {
        // 방어력에 의해 데미지를 감소시켜 깎음. 방어력 1당 1% 데미지 감소
        currentHelthPoint -= Mathf.Clamp(amount - ( amount * defence * 0.01f ), 0, int.MaxValue );
        //Debug.Log("현재 체력 : " + currentHelthPoint);
    }

    // unit을 공격 한다.
    public void Attack(UnitEntity unit)
    {
        unit.TakeDamage(strength);
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
    }

    public float JumpPower
    {
        get { return jumpPower; }
    }
}
