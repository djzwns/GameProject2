using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    // 리지드바디 사용
    private Rigidbody2D rigid2D;

    public float speed = 2.0f;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        if (rigid2D.velocity.y == 0)
            rigid2D.velocity = new Vector2(-speed, rigid2D.velocity.y);
    }
}
