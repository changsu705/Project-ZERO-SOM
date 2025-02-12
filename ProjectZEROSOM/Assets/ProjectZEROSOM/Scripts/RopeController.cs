using UnityEngine;

public class RopeController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] ropeNodes; // ������ Rigidbody���� �� �迭

    void Update()
    {
        if (lineRenderer == null || ropeNodes.Length == 0) return;

        // Line Renderer�� ����Ʈ ��ġ�� Rigidbody�� ��ġ�� ������Ʈ
        lineRenderer.positionCount = ropeNodes.Length;
        for (int i = 0; i < ropeNodes.Length; i++)
        {
            lineRenderer.SetPosition(i, ropeNodes[i].position);
        }
    }
}
