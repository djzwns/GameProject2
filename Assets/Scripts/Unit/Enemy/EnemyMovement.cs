using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    // 리지드바디 사용
    private Rigidbody2D rigid2D;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        if (rigid2D.velocity.y == 0)
            rigid2D.velocity = -(Vector2.right * 2.0f * 0.1f) + rigid2D.velocity;
    }
}
