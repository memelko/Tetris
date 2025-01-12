using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject r, b, y, e, TR, TB, TY;
    public GameObject[,] pole = new GameObject[16, 8];
    private GameObject currentBlock;
    int score = 0;//tono
    public TMP_Text Score;//tono
    public int pocetV = 0;//tono
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
                        pocetV ++; //tono
                    }
                    else if (typ == 2)
                    {
                        pole[i, j] = Instantiate(b, Vector3.zero, Quaternion.identity);
                        pocetV ++;//tono
                    }
                    else if (typ == 3)
                    {
                        pole[i, j] = Instantiate(y, Vector3.zero, Quaternion.identity);
                        pocetV ++;//tono
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
        UpdateScoreText();//tono
    }
    private void Update()
    {
        UpdateScoreText();//tono
        if (pocetV == 0) 
        {
            konecnaVScena();
        }
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
                    score += 400;//tono
                    pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i + 1, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i + 2, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pole[i + 3, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                    pocetV = -4;//tono
                    
                }
            }
        }
    }
    public void SpawnNewBlock()
    {
        // Randomly color
        int randomColor = Random.Range(1, 4);
        GameObject blockPrefab = TR; // Default to red

        if (randomColor == 2) blockPrefab = TB;
        else if (randomColor == 3) blockPrefab = TY;

        // Spawn block 
        currentBlock = Instantiate(blockPrefab, new Vector3(0.4f, 4.5f, 0), Quaternion.identity);
        currentBlock.AddComponent<FallingBlock>(); // click the scirpt to the block
    }

    public void PlaceBlock(Vector3 position, GameObject block)
    {
        int row = Mathf.RoundToInt((4.5f - position.y) / 0.5f);
        int col = Mathf.RoundToInt((3 - position.x) / 0.65f);

        pole[row, col] = block;

        checker(); // Skontrolovať zápasy
        CheckForFullRows(); // Skontrolovať plné riadky
    }
    void UpdateScoreText()
    {
        Score.text =  score.ToString(); //tono
    }
    public void konecnaVScena() //tono
    {
        SceneManager.LoadScene(2);
    }
    public void konecnaPScena() //tono
    {
        SceneManager.LoadScene(3);
    }
    private bool IsRowFull(int row)
    {
        for (int col = 0; col < pole.GetLength(1); col++)
        {
            if (pole[row, col].tag == "Empty")
            {
                return false; // Ak nájde prázdnu bunku, riadok nie je plný
            }
        }
        return true; // Ak sú všetky bunky plné, riadok je plný
    }
    private void CheckForFullRows()
    {
        for (int i = 0; i < pole.GetLength(0); i++) // Prechádza všetky riadky
        {
            if (IsRowFull(i))
            {
                konecnaPScena();
            }
        }
    }

    

}




