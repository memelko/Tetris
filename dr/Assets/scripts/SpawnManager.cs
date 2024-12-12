using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject r, b, y,e;
    public GameObject[,] pole = new GameObject[16,8];
    void Start()
    {
        for(int i = 0; 1 < 16; i++) 
        {
          for ( int j=0; j < 8; j++)
          {
                int typ = Random.Range(1, 10);
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
                else if (typ > 3)
                {
                    pole[i, j] = Instantiate(e, Vector3.zero, Quaternion.identity);
                }
          }
        }
        for (int i = 0; 1 < 16; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                pole[i, j].transform.position = new Vector3(3 - j * 0.65f, 4.5f - i * 0.65f, 0); 
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
