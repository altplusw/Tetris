using UnityEngine;

public class HoldManager : MonoBehaviour
{
    public Transform holdPosition;
    
    GameObject heldPiece;
    int heldIndex = -1;

    public void HoldPiece(Tetromino currentPiece)
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        
        // Get current piece index
        int currentIndex = spawner.GetPieceIndex(currentPiece.gameObject);

        if (heldPiece == null)
        {
            // No held piece yet — just hold and spawn next
            heldIndex = currentIndex;
            heldPiece = Instantiate(spawner.pieces[heldIndex], 
                holdPosition.position, Quaternion.identity);
            heldPiece.GetComponent<Tetromino>().enabled = false;
            
            Destroy(currentPiece.gameObject);
            spawner.SpawnPiece();
        }
        else
        {
            // Swap held piece with current
            int tempIndex = heldIndex;
            heldIndex = currentIndex;

            Destroy(heldPiece);
            heldPiece = Instantiate(spawner.pieces[heldIndex],
                holdPosition.position, Quaternion.identity);
            heldPiece.GetComponent<Tetromino>().enabled = false;

            Destroy(currentPiece.gameObject);
            
            // Spawn the previously held piece
            Instantiate(spawner.pieces[tempIndex], 
                new Vector3(5, 20, 0), Quaternion.identity);
        }
    }
}