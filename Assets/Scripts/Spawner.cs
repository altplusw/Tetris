using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] pieces;
    public Transform previewPosition;

    int nextIndex;
    GameObject previewPiece;

    void Start()
    {
        nextIndex = Random.Range(0, pieces.Length);
        ShowPreview();
        SpawnPiece();
    }

    void ShowPreview()
    {
        if (previewPiece != null)
            Destroy(previewPiece);

        previewPiece = Instantiate(pieces[nextIndex],
            previewPosition.position, Quaternion.identity);

        // Disable movement on preview
        previewPiece.GetComponent<Tetromino>().enabled = false;
    }

    public void SpawnPiece()
    {
        if (GameManager.instance.IsGameOver()) return;

        Vector3 spawnPos = new Vector3(5, 20, 0);

        Vector2Int gridPos = Grid.WorldToGrid(spawnPos);
        if (!Grid.IsEmpty(gridPos))
        {
            GameManager.instance.GameOver();
            return;
        }

        // Spawn the next piece
        Instantiate(pieces[nextIndex], spawnPos, Quaternion.identity);

        // Pick new next piece
        nextIndex = Random.Range(0, pieces.Length);
        ShowPreview();
    }
            public int GetPieceIndex(GameObject piece)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if (piece.name.Contains(pieces[i].name))
                    return i;
            }
            return 0;
        }
}