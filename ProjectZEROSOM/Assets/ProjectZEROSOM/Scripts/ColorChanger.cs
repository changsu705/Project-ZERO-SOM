using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer objRenderer;
    private Color[] colors = { Color.red, Color.blue, Color.yellow };
    private int colorIndex = 0;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        // 1초마다 ChangeColor() 실행
        InvokeRepeating("ChangeColor", 0f, 1f);
    }

    void ChangeColor()
    {
        objRenderer.material.color = colors[colorIndex]; // 색상 변경
        colorIndex = (colorIndex + 1) % colors.Length; // 다음 색상으로 변경 (순환)
    }
}
