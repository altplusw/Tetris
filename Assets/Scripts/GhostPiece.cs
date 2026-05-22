using UnityEngine;

public class GhostPiece : MonoBehaviour
{
    public GameObject blockPrefab;
    public Color ghostColor = new Color(1f, 1f, 1f, 0.5f);

    GameObject[] ghostBlocks;
    Tetromino currentPiece;

  void Update()
{
    // Find ACTIVE piece only — not preview or hold
    Tetromino[] allPieces = FindObjectsOfType<Tetromino>();
    currentPiece = null;
    
    foreach (Tetromino t in allPieces)
    {
        if (t.enabled)
        {
            currentPiece = t;
            break;
        }
    }

    ClearGhost();

    if (currentPiece != null)
    {
        DrawGhost();
    }
}

    void DrawGhost()
    {
        // Get current piece block positions
        Transform[] blocks = GetChildBlocks(currentPiece.transform);
        Debug.Log("Blocks found: " + blocks.Length + " Drop distance: ");
        // Find how far down the piece can go
        int dropDistance = 0;
        while (CanMoveDown(blocks, dropDistance + 1))
        {
            dropDistance++;
        }

        // Create ghost blocks at drop position
        ghostBlocks = new GameObject[blocks.Length];
        for (int i = 0; i < blocks.Length; i++)
{
    Vector3 ghostPos = blocks[i].position + 
        Vector3.down * dropDistance;
    ghostBlocks[i] = Instantiate(blockPrefab, ghostPos, 
        Quaternion.identity);
    
    Debug.Log("Ghost block spawned at: " + ghostPos);
    
    SpriteRenderer sr = ghostBlocks[i].GetComponent<SpriteRenderer>();
    sr.color = new Color(1f, 1f, 1f, 0.5f);
    sr.sortingOrder = 1;
}
    }

    bool CanMoveDown(Transform[] blocks, int distance)
{
    foreach (Transform block in blocks)
    {
        Vector3 newPos = block.position + Vector3.down * distance;
        Vector2Int pos = Grid.WorldToGrid(newPos);

        // Check X bounds and bottom
        if (pos.x < 0 || pos.x >= Grid.width || pos.y < 0)
            return false;

        // Only check grid if within height
        if (pos.y < Grid.height)
        {
            if (Grid.grid[pos.x, pos.y] != null)
                return false;
        }
    }
    return true;
}

    void ClearGhost()
    {
        if (ghostBlocks != null)
        {
            foreach (GameObject block in ghostBlocks)
            {
                if (block != null)
                    Destroy(block);
            }
        }
    }

    Transform[] GetChildBlocks(Transform piece)
    {
        Transform[] blocks = new Transform[piece.childCount];
        for (int i = 0; i < piece.childCount; i++)
        {
            blocks[i] = piece.GetChild(i);
        }
        return blocks;
    }
}