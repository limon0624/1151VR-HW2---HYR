using UnityEngine;

public class PathMover : MonoBehaviour
{
    public Vector2[] waypoints;   // 路徑點（陣列 + vector）
    public bool[] isJump;         // 對應每個路徑點：true = 用跳的過去
    public float speed = 3f;
    public float jumpHeight = 1.5f;

    int index = 0;
    Vector2 start;
    float t = 0f;

    void Start()
    {
        start = transform.position;
    }

    void Update()
    {
        if (index >= waypoints.Length) return;   // 已到終點

        Vector2 target = waypoints[index];
        float length = Mathf.Max(Vector2.Distance(start, target), 0.01f);

        t += speed * Time.deltaTime / length;
        float p = Mathf.Clamp01(t);

        Vector2 pos = Vector2.Lerp(start, target, p);

        // 跳躍段：疊加一個弧線高度
        if (index < isJump.Length && isJump[index])
            pos.y += Mathf.Sin(p * Mathf.PI) * jumpHeight;

        transform.position = pos;

        if (p >= 1f)
        {
            start = target;
            t = 0f;
            index++;
        }
    }
}