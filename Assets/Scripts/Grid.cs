using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];

    // Convert world position to grid position
    public static Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x),
            Mathf.RoundToInt(worldPos.y)
        );
    }

    // Check if position is inside the grid
    public static bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0;
    }

    // Check if position is empty
    public static bool IsEmpty(Vector2Int pos)
    {
        if (pos.y >= height) return true;
        return grid[pos.x, pos.y] == null;
    }

    // Add block to grid
    public static void AddToGrid(Transform block)
{
    Vector2Int pos = WorldToGrid(block.position);
    if (pos.y < height)
    {
        grid[pos.x, pos.y] = block;
        
        // Check if block was added near the top
        if (pos.y >= height - 1)
        {
            GameManager.instance.GameOver();
        }
    }
}

    // Check and clear full lines
    public static int ClearLines()
    {
        int linesCleared = 0;

        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                DeleteLine(y);
                MoveDownAbove(y);
                y--;
                linesCleared++;
            }
        }
        if (linesCleared > 0)
            ScoreManager.instance.AddScore(linesCleared);
        return linesCleared;
    }

    static bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null) return false;
        }
        return true;
    }

    static void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    static void MoveDownAbove(int clearedY)
    {
        for (int y = clearedY + 1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }
}