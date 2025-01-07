using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject r, b, y, e;
    public GameObject[,] pole = new GameObject[16, 8];
    private GameObject currentBlock;

    void Start()
    {
        SpawnNewBlock();
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // Fill the first 8 rows with empty objects
                if (i < 8)
                {
                    pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    // Randomly spawn r, b, y, or e for rows 8 to 15
                    int typ = Random.Range(1, 10);
                    if (typ == 1)
                    {
                        pole[i, j] = Instantiate(r, Vector3.zero, Quaternion.identity);
                    }
                    else if (typ == 2)
                    {
                        pole[i, j] = Instantiate(r, Vector3.zero, Quaternion.identity);
                    }
                    else if (typ == 3)
                    {
                        pole[i, j] = Instantiate(r, Vector3.zero, Quaternion.identity);
                    }
                    else
                    {
                        pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    }
                }
            }
        }

        // Set the positions of the instantiated objects
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                pole[i, j].transform.position = new Vector3(3 - j * 0.65f, 4.5f - i * 0.5f, 0);
            }
        }
    }
    private void Update()
    {
        
    }
    public void checker()
    {
        // Check rows for matching 4 objects
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                if (pole[i, j].tag != "Empty" &&
                    pole[i, j].tag == pole[i, j + 1].tag &&
                    pole[i, j].tag == pole[i, j + 2].tag &&
                    pole[i, j].tag == pole[i, j + 3].tag)
                {
                    

                    Destroy(pole[i, j]);
                    Destroy(pole[i, j + 1]);
                    Destroy(pole[i, j + 2]);
                    Destroy(pole[i, j + 3]);

                    pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i, j + 1] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i, j + 2] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i, j + 3] = Instantiate(e, Vector3.zero, Quaternion.identity);
                }
            }
        }

        // Check columns for matching 4 objects
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i <= 12; i++)
            {
                if (pole[i, j].tag != "Empty" &&
                    pole[i, j].tag == pole[i + 1, j].tag &&
                    pole[i, j].tag == pole[i + 2, j].tag &&
                    pole[i, j].tag == pole[i + 3, j].tag)
                {
                    Debug.Log($"Match found at column ({j}): {i}, {i + 1}, {i + 2}, {i + 3}");

                    Destroy(pole[i, j]);
                    Destroy(pole[i + 1, j]);
                    Destroy(pole[i + 2, j]);
                    Destroy(pole[i + 3, j]);

                    pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i + 1, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i + 2, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i + 3, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                }
            }
        }
    }
    public void SpawnNewBlock()
    {
        // Randomly choose a color for the new block
        int randomColor = Random.Range(1, 4);
        GameObject blockPrefab = r; // Default to red

        if (randomColor == 2) blockPrefab = b;
        else if (randomColor == 3) blockPrefab = y;

        // Spawn the block at the top of the field
        currentBlock = Instantiate(blockPrefab, new Vector3(0.4f, 4.5f, 0), Quaternion.identity);
        currentBlock.AddComponent<FallingBlock>(); // Attach the FallingBlock script
    }
    public void PlaceBlock(Vector3 position, GameObject block)
    {
        int row = Mathf.RoundToInt((4.5f - position.y) / 0.5f);
        int col = Mathf.RoundToInt((3 - position.x) / 0.65f);

        // Place the block in the grid
        pole[row, col] = block;

        // Check for matches after placing the block
        checker();
    }

}




