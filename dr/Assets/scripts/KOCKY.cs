

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] kocky; // Pole pre tri rôzne prefaby kociek
    public Vector3 spawnPozicia = new Vector3(-0.22f, 4.28f, 0f); // Pozícia spawnu
    public float spawnInterval = 5f; // Interval spawnu
    public float rychlostPadania = 1.5f; // Normálna rýchlosť padania
    public float zrychlenaRychlost = 2f; // Rýchlosť pri držaní Shift
    public float pohybHorizontalny = 1f; // Rýchlosť pohybu do strán

    void Start()
    {
        // Spustenie opakovanej funkcie spawnu
        InvokeRepeating("SpawnKocku", 0f, spawnInterval);
    }

    void SpawnKocku()
    {
        if (kocky.Length > 0)
        {
            // Náhodne vyberie jednu kocku z poľa
            int index = Random.Range(0, kocky.Length);
            GameObject novaKocka = Instantiate(kocky[index], spawnPozicia, Quaternion.identity);

            // Pridanie pohybového skriptu k novej kocke
            novaKocka.AddComponent<MovingCube>().Initialize(rychlostPadania, zrychlenaRychlost, pohybHorizontalny);

            // Zničenie kocky po 8 sekundách
            Destroy(novaKocka, 8f);
        }
    }
}

public class MovingCube : MonoBehaviour
{
    private float rychlostPadania;
    private float zrychlenaRychlost;
    private float pohybHorizontalny;

    public void Initialize(float rychlost, float zrychlena, float pohyb)
    {
        rychlostPadania = rychlost;
        zrychlenaRychlost = zrychlena;
        pohybHorizontalny = pohyb;
    }

    void Update()
    {
        // Pohyb nadol
        float aktualnaRychlost = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? zrychlenaRychlost : rychlostPadania;
        transform.Translate(Vector3.down * aktualnaRychlost * Time.deltaTime);

        // Pohyb doľava
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.left * pohybHorizontalny);
        }

        // Pohyb doprava
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.right * pohybHorizontalny);
        }
    }
}
