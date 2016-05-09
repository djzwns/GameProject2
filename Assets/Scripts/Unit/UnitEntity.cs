using UnityEngine;
using System.Collections;

[System.Serializable]   // 인스펙터 창에서 보이도록 사용
public class UnitEntity : ScriptableObject {
    public int defence = 0;            // 방어력
    public int strength = 500;         // 공격력
    public int power = 100;            // 힘
    public float healthPoint = 5000f;  // 최대 체력
    public float speed = 2f;           // 이동속도
    [SerializeField]
    protected float currentHealthPoint;// 현재 체력
    protected float jumpPower = 3f;    // 점프력
    protected float attackSpeed = 2f;

    void Awake()
    {
        currentHealthPoint = healthPoint;
    }

    // 데미지를 0부터 최대 값까지.
    public virtual void TakeDamage(int amount)
    {
        // 방어력에 의해 데미지를 감소시켜 깎음. 방어력 1당 1% 데미지 감소
        currentHealthPoint -= Mathf.Clamp(amount - ( amount * defence * 0.01f ), 0, int.MaxValue );
        //Debug.Log("현재 체력 : " + currentHelthPoint);
    }

    // unit을 공격 한다.
    public virtual void Attack(UnitEntity unit)
    {
        unit.TakeDamage(strength);
    }

    public bool IsDead()
    {
        if (currentHealthPoint <= 0)
            return true;

        return false;
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
