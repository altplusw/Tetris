using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fallTime = 1.0f;
    float previousTime;
    bool hasHeld = false;

    void Update()
    {
        // Hold piece
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!hasHeld)
            {
                Debug.Log("C pressed - trying to hold");
                hasHeld = true;
                FindObjectOfType<HoldManager>().HoldPiece(this);
            }
        }

        // Move left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        // Move right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }
        // Soft drop
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.down);
        }

        // Hard drop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            while (IsValidPosition())
            {
                transform.position += Vector3.down;
            }
            transform.position += Vector3.up;
            LandPiece();
        }

        // Auto fall
        if (Time.time - previousTime > fallTime)
        {
            Move(Vector3.down);
            previousTime = Time.time;
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction;

        if (!IsValidPosition())
        {
            transform.position -= direction;

            if (direction == Vector3.down)
            {
                LandPiece();
            }
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 0, -90);

        if (!IsValidPosition())
        {
            transform.Rotate(0, 0, 90);
        }
    }

    bool IsValidPosition()
    {
        foreach (Transform block in transform)
        {
            Vector2Int pos = Grid.WorldToGrid(block.position);
            if (!Grid.IsInsideGrid(pos)) return false;
            if (!Grid.IsEmpty(pos)) return false;
        }
        return true;
    }

    void LandPiece()
{
    foreach (Transform block in transform)
    {
        Grid.AddToGrid(block);
    }

    int lines = Grid.ClearLines();
    
    // Play correct sound
    if (lines > 0)
        AudioManager.instance.PlayLineClear();
    else
        AudioManager.instance.PlayBlockLand();

    transform.DetachChildren();
    Destroy(gameObject);
    FindObjectOfType<Spawner>().SpawnPiece();
}

    public bool IsValidPositionPublic()
    {
        return IsValidPosition();
    }
}