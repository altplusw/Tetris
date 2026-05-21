using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    public Color gridColor = new Color(1f, 1f, 1f, 0.1f);
    public Color borderColor = Color.white;

    void OnDrawGizmos()
    {
        // Draw border
        Gizmos.color = borderColor;
        
        // Bottom
        Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(10, 0, 0));
        // Top
        Gizmos.DrawLine(new Vector3(0, 20, 0), new Vector3(10, 20, 0));
        // Left
        Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 20, 0));
        // Right
        Gizmos.DrawLine(new Vector3(10, 0, 0), new Vector3(10, 20, 0));

        // Draw grid lines
        Gizmos.color = gridColor;

        // Vertical lines
        for (int x = 1; x < 10; x++)
        {
            Gizmos.DrawLine(new Vector3(x, 0, 0), new Vector3(x, 20, 0));
        }

        // Horizontal lines
        for (int y = 1; y < 20; y++)
        {
            Gizmos.DrawLine(new Vector3(0, y, 0), new Vector3(10, y, 0));
        }
    }
}