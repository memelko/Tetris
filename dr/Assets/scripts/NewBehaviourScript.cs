using UnityEngine;

public class SimplifiedTetris : MonoBehaviour
{
    public GameObject r, b, y, e;
    public GameObject[,] pole = new GameObject[16, 8]; // The game field (16x8)
    private GameObject currentBlock;  // The currently falling block
    private int currentX, currentY;   // The position of the current block

    void Start()
    {
        InitializeField();
        SpawnNewBlock();
    }

    void Update()
    {
        // Let the current block fall
        if (currentY > 0 && pole[currentY - 1, currentX] == null) // Check if there's space to fall
        {
            currentBlock.transform.position = new Vector3(3 - currentX * 0.65f, 4.5f - (currentY - 1) * 0.5f, 0);
            pole[currentY, currentX] = null;  // Clear previous position
            currentY--;
            pole[currentY, currentX] = currentBlock;
        }
        else
        {
            // Block has landed; Spawn a new one
            SpawnNewBlock();
            CheckForFourBlocks();
        }
    }

    void InitializeField()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i < 8)
                {
                    pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    int typ = Random.Range(1, 5);
                    if (typ == 1)
                    {
                        pole[i, j] = Instantiate(r, Vector3.zero, Quaternion.identity);
                    }
                    else if (typ == 2)
                    {
                        pole[i, j] = Instantiate(b, Vector3.zero, Quaternion.identity);
                    }
                    else if (typ == 3)
                    {
                        pole[i, j] = Instantiate(y, Vector3.zero, Quaternion.identity);
                    }
                    else
                    {
                        pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    }
                }
            }
        }

        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                pole[i, j].transform.position = new Vector3(3 - j * 0.65f, 4.5f - i * 0.5f, 0);
            }
        }
    }

    void SpawnNewBlock()
    {
        // Randomly select a block type (r, b, y)
        int typ = Random.Range(1, 4);
        GameObject block = null;

        if (typ == 1)
            block = Instantiate(r, Vector3.zero, Quaternion.identity);
        else if (typ == 2)
            block = Instantiate(b, Vector3.zero, Quaternion.identity);
        else
            block = Instantiate(y, Vector3.zero, Quaternion.identity);

        // Set its position at the top of the grid
        currentX = 3;  // Spawning at the middle
        currentY = 7;  // Spawning at the top row

        currentBlock = block;
        pole[currentY, currentX] = currentBlock;

        // Update position
        currentBlock.transform.position = new Vector3(3 - currentX * 0.65f, 4.5f - currentY * 0.5f, 0);
    }

    void CheckForFourBlocks()
    {
        // Check horizontal and vertical for four blocks in a row
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (pole[i, j] != null)
                {
                    GameObject currentBlock = pole[i, j];
                    CheckDirection(i, j, currentBlock);
                }
            }
        }
    }

    void CheckDirection(int x, int y, GameObject block)
    {
        int count = 1;

        // Check right
        for (int i = x + 1; i < 16; i++)
        {
            if (pole[i, y] != null && pole[i, y] == block)
            {
                count++;
                if (count == 4)
                {
                    DestroyBlocks(x, y, "right");
                    return;
                }
            }
            else break;
        }

        // Check down
        count = 1;
        for (int i = y + 1; i < 8; i++)
        {
            if (pole[x, i] != null && pole[x, i] == block)
            {
                count++;
                if (count == 4)
                {
                    DestroyBlocks(x, y, "down");
                    return;
                }
            }
            else break;
        }
    }

    void DestroyBlocks(int x, int y, string direction)
    {
        // Destroy the blocks in the line
        if (direction == "right")
        {
            for (int i = x; i < x + 4; i++)
            {
                Destroy(pole[i, y]);
                pole[i, y] = e;
            }
        }
        else if (direction == "down")
        {
            for (int i = y; i < y + 4; i++)
            {
                Destroy(pole[x, i]);
                pole[x, i] = e;
            }
        }
    }
}