using UnityEngine;

public class RopeController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] ropeNodes; // 밧줄의 Rigidbody들이 들어갈 배열

    void Update()
    {
        if (lineRenderer == null || ropeNodes.Length == 0) return;

        // Line Renderer의 포인트 위치를 Rigidbody의 위치로 업데이트
        lineRenderer.positionCount = ropeNodes.Length;
        for (int i = 0; i < ropeNodes.Length; i++)
        {
            lineRenderer.SetPosition(i, ropeNodes[i].position);
        }
    }
}
