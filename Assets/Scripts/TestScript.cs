using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float radius = 5f;
    [Range(0f, 360f)]
    public float arcAngle = 90f;

    void OnDrawGizmos()
    {
        DrawGizmoArc(transform.position, radius, arcAngle);
    }

    void DrawGizmoArc(Vector3 center, float radius, float angle)
    {
        Gizmos.color = Color.green;

        float halfAngle = angle * 0.5f;
        int segments = 50; // Điều chỉnh số lượng đoạn để tăng độ chính xác
        float startAngle = -halfAngle;
        float endAngle = halfAngle;

        float step = angle / segments;

        Vector3 from = center + Quaternion.Euler(0, 0, startAngle) * Vector3.right * radius;

        for (int i = 1; i <= segments; i++)     
        {
            float t = i / (float)segments;
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            Vector3 to = center + Quaternion.Euler(0, 0, currentAngle) * Vector3.right * radius;

            Gizmos.DrawLine(from, to);

            from = to;
        }
    }
}
