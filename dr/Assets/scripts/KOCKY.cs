

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

    private GameObject aktualnaKocka; // Aktuálna pohybujúca sa kocka
    private bool zastavena = false;  // Indikátor, či sa aktuálna kocka zastavila

    void Start()
    {
        // Spustenie opakovanej funkcie spawnu
        InvokeRepeating("SpawnKocku", 0f, spawnInterval);
    }

    void Update()
    {
        if (aktualnaKocka != null && !zastavena)
        {
            // Pohyb nadol
            float aktualnaRychlost = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? zrychlenaRychlost : rychlostPadania;
            aktualnaKocka.transform.Translate(Vector3.down * aktualnaRychlost * Time.deltaTime);

            // Pohyb doľava
            if (Input.GetKeyDown(KeyCode.A))
            {
                aktualnaKocka.transform.Translate(Vector3.left * pohybHorizontalny);
            }

            // Pohyb doprava
            if (Input.GetKeyDown(KeyCode.D))
            {
                aktualnaKocka.transform.Translate(Vector3.right * pohybHorizontalny);
            }
        }
    }

    void SpawnKocku()
    {
        if (kocky.Length > 0)
        {
            // Náhodne vyberie jednu kocku z poľa
            int index = Random.Range(0, kocky.Length);
            aktualnaKocka = Instantiate(kocky[index], spawnPozicia, Quaternion.identity);

            // Pridanie komponentov pre fyziku a kolízie
            Rigidbody2D rb = aktualnaKocka.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = aktualnaKocka.AddComponent<Rigidbody2D>();
            }
            rb.bodyType = RigidbodyType2D.Dynamic; // Nastavenie na dynamický objekt
            rb.gravityScale = 0; // Deaktivácia gravitácie (padanie riadené kódom)

            if (!aktualnaKocka.TryGetComponent(out BoxCollider2D _))
            {
                aktualnaKocka.AddComponent<BoxCollider2D>();
            }

            zastavena = false; // Kocka nie je zastavená
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Boom");
        zastavena = true;
        if (aktualnaKocka != null && collision.gameObject != null)
        {
            // Zastavenie aktuálnej kocky pri dotyku s iným objektom
            zastavena = true;

            // Fixácia aktuálnej kocky na pozícii
            Rigidbody2D rb = aktualnaKocka.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static; // Zastavenie pohybu kocky
            }
        }
    }
}
