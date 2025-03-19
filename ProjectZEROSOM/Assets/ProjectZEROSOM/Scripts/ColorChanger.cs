using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer objRenderer;
    private Color[] colors = { Color.red, Color.blue, Color.yellow };
    private int colorIndex = 0;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        // 1�ʸ��� ChangeColor() ����
        InvokeRepeating("ChangeColor", 0f, 1f);
    }

    void ChangeColor()
    {
        objRenderer.material.color = colors[colorIndex]; // ���� ����
        colorIndex = (colorIndex + 1) % colors.Length; // ���� �������� ���� (��ȯ)
    }
}
