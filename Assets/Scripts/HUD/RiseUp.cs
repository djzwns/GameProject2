using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RiseUp : MonoBehaviour {
    void Start()
    {
        StartCoroutine(RiseAndDestroy());
    }

    // 위로 올라가다가 소멸
    IEnumerator RiseAndDestroy()
    {
        float startY = transform.position.y;
        yield return new WaitForSeconds(0.5f);

        while (Mathf.Abs(startY - transform.position.y) < 50f)
        {
            yield return new WaitForSeconds(0.001f);

            transform.position += (Vector3.up/* * 0.01f*/);

            // 점점 투명하게. 갑자기 사라지면 부자연스러워보여서 추가
            GetComponent<Text>().color += new Color(0, 0, 0, -0.01f);
        }
        Destroy(gameObject);
        StopCoroutine(RiseAndDestroy());
    }
}
