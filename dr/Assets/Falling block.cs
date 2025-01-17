
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


        // Handle horizontal and fast downward movement
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove(Vector2.left))
            transform.position += Vector3.left * 0.65f;

        if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove(Vector2.right))
            transform.position += Vector3.right * 0.65f;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.position += Vector3.down * 0.5f;
    }

    private bool CanMove(Vector2 direction)
    {
        // Define the raycast length based on block size
        float rayLength = 0.5f; // Adjust this to match your grid cell size
        Debug.DrawRay(transform.position, direction * rayLength, Color.red); // doesnt work for some reason
        // Cast a ray in the desired direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, LayerMask.GetMask("Blocks", "Ground", "Dumb blocks"));

        // If the raycast hits something, block the movement
        return hit.collider == null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocks") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Falling blocks") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Dumb blocks"))
        {
            // Set the layer directly to "Dumb blocks"
            gameObject.layer = LayerMask.NameToLayer("Dumb blocks");

            // Make sure the BoxCollider2D is set to trigger
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.isTrigger = true;
            }

            // Place the block and spawn a new one
            FindObjectOfType<SpawnManager>().PlaceBlock(transform.position, gameObject);
            FindObjectOfType<SpawnManager>().SpawnNewBlock();

            // Destroy this script
            Destroy(this);
        }
    }
}
