using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fallTime = 1.0f;
    float previousTime;
    bool hasHeld = false;

    // DAS settings
    float dasDelay = 0.15f;
    float dasInterval = 0.05f;
    float dasTimerLeft = 0f;
    float dasTimerRight = 0f;

    void Update()
    {
        // Hold piece
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!hasHeld)
            {
                hasHeld = true;
                FindObjectOfType<HoldManager>().HoldPiece(this);
            }
        }

        // LEFT
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
            dasTimerLeft = dasDelay;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dasTimerLeft -= Time.deltaTime;
            if (dasTimerLeft <= 0)
            {
                dasTimerLeft = dasInterval;
                Move(Vector3.left);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            dasTimerLeft = 0;
        }

        // RIGHT
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
            dasTimerRight = dasDelay;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dasTimerRight -= Time.deltaTime;
            if (dasTimerRight <= 0)
            {
                dasTimerRight = dasInterval;
                Move(Vector3.right);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            dasTimerRight = 0;
        }

        // Rotate
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }

        // Soft drop
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Time.time - previousTime > fallTime / 10)
            {
                Move(Vector3.down);
                previousTime = Time.time;
            }
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