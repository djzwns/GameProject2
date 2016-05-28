using UnityEngine;
using System.Collections;

[System.Serializable]   // 인스펙터 창에서 보이도록 사용
public class UnitEntity : ScriptableObject {
    public string unitName;
    public int defence = 0;            // 방어력
    public int strength = 50;         // 공격력
    public int power = 20;            // 힘
    public float healthPoint = 1000;  // 최대 체력
    public float speed = 2f;           // 이동속도
    [SerializeField]
    protected float currentHealthPoint;// 현재 체력
    public float CurrentHealthPoint { get { return currentHealthPoint; } }
    protected float jumpPower = 3f;    // 점프력
    protected float attackSpeed = 1f;
    public int evasion = 0;     // 회피율

    void Awake()
    {
        //LoadData();
    }

    // 데미지를 0부터 최대 값까지.
    public virtual void TakeDamage(float amount)
    {
        // 회피율에 따라 데미지를 주거나 못주거나.
        if (evasion >= Random.Range(1, 100))
        {
            //Debug.Log("회피");
            //base.TakeDamage(0);
        }
        else // 방어력에 의해 데미지를 감소시켜 깎음. 방어력 1당 1% 데미지 감소
            currentHealthPoint -= Mathf.Clamp(amount - ( amount * defence * 0.01f ), 0, int.MaxValue );
        //Debug.Log("현재 체력 : " + currentHelthPoint);
    }

    // unit을 공격 한다.
    public virtual void Attack(UnitEntity unit)
    {
        // 데미지를 80~120% 랜덤하게 줌
        float damage = strength * Random.Range(0.8f, 1.2f);

        unit.TakeDamage(damage);
    }

    // 죽었는지 체크함, 죽으면 true 반환
    public bool IsDead()
    {
        if (currentHealthPoint <= 0)
            return true;

        return false;
    }

    // 속성들 저장
    public virtual void SaveData()
    {
        PlayerPrefs.SetInt(unitName + "Defence", defence);
        PlayerPrefs.SetInt(unitName + "Strength", strength);
        PlayerPrefs.SetInt(unitName + "Power", power);
        PlayerPrefs.SetFloat(unitName + "HelthPoint", healthPoint);
        PlayerPrefs.SetFloat(unitName + "Speed", speed);
    }

    // 속성들 불러오기
    public virtual void LoadData()
    {
        defence = PlayerPrefs.GetInt(unitName + "Defence", defence);
        strength = PlayerPrefs.GetInt(unitName + "Strength", strength);
        power = PlayerPrefs.GetInt(unitName + "Power", power);
        healthPoint = PlayerPrefs.GetFloat(unitName + "HelthPoint", healthPoint);
        speed = PlayerPrefs.GetFloat(unitName + "Speed", speed);
        currentHealthPoint = healthPoint;
    }

    // 프로퍼티 get
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }

    public float JumpPower
    {
        get { return jumpPower; }
    }
}
