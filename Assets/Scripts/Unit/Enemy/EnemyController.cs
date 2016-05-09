using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    // 적 정보를 담고 있음 private 이지만 인스펙터창에 노출
    [SerializeField]
    private Enemy enemy;
    // 플레이어 정보를 받아옴
    private Player player;
    // 공격 가능한 시간
    private float attackTime = 0f;

	// Use this for initialization
	void Awake () {
        enemy = ScriptableObject.CreateInstance<Enemy>() as Enemy;
        player = Player.Instance;
	}

    void Update()
    {
        attackTime += Time.deltaTime;
        if (enemy.IsDead())
            gameObject.SetActive(false);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            if (attackTime >= enemy.AttackSpeed)
            {
                attackTime = 0;
                enemy.Attack(player);
            }
        }
    }

    // 적 정보 보내줌
    public UnitEntity Enemy { get { return enemy; }}
}
