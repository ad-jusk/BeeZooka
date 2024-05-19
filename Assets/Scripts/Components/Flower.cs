using UnityEngine;

public class Flower : MonoBehaviour
{
    private enum FlowerState {
        ACTIVE, INACTIVE
    };

    [SerializeField]
    private FlowerColor flowerColor = FlowerColor.RED; // RED BY DEFAULT
    private FlowerState flowerState = FlowerState.ACTIVE;

    private GameManager gameManager;

    private void Awake() {
        gameManager = GameManager.Instance;
    }

    // Using OnTriggerEnter2D instead of OnCollisionEnter2D for entering flower
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (flowerState == FlowerState.INACTIVE)
        {
            Debug.Log("Inactive flower hit - ignoring collision");
            return;
        }

        if (other.CompareTag("Bee"))
        {
            gameManager.NotifyFlowerEntered(flowerColor);

            //Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();

            // Position the bee at the center of the flower
            Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();

            beeRigidbody.velocity = Vector2.zero;
            beeRigidbody.position = transform.GetComponent<Renderer>().bounds.center;

            beeRigidbody.rotation = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If the bee exits the flower collider trigger, set the flower inactive
        if (other.CompareTag("Bee"))
        {
            SetFlowerInactive();
        }
    }

    private void SetFlowerInactive() {
        flowerState = FlowerState.INACTIVE;
        // ALSO DISABLE COLLIDER
        GetComponent<Collider2D>().enabled = false;
    }
}
