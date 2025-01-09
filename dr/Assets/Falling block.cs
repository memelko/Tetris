
using UnityEngine;



public class FallingBlock : MonoBehaviour
{
    public float fallSpeed = 1.0f; // Speed at which the block falls
    private float fallTimer;

    void Update()
    {
       
        // Make the block fall over time
        fallTimer += Time.deltaTime;
        if (fallTimer >= fallSpeed)
        {
            transform.position += Vector3.down * 0.5f; // Move down by 0.5 units
            fallTimer = 0;
        }

        
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.position += Vector3.left * 0.65f;

            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.position += Vector3.right * 0.65f;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.position += Vector3.down * 0.5f;
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocks") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            FindObjectOfType<SpawnManager>().PlaceBlock(transform.position, gameObject);
            FindObjectOfType<SpawnManager>().SpawnNewBlock();
            Destroy(this); // Remove this script from the block
            
        }
    }
}
