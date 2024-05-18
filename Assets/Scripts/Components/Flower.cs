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

    private void OnCollisionEnter2D(Collision2D collision) {

        if(flowerState == FlowerState.INACTIVE) {
            Debug.Log("Inactive flower hit - ignoring collision");
            return;
        }

        gameManager.NotifyFlowerEntered(flowerColor);

        Rigidbody2D beeRigidbody = collision.transform.GetComponent<Rigidbody2D>();
        beeRigidbody.velocity = Vector2.zero;
        beeRigidbody.position = transform.GetComponent<Renderer>().bounds.center;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        SetFlowerInactive();
    }

    private void SetFlowerInactive() {
        flowerState = FlowerState.INACTIVE;
        // ALSO DISABLE COLLIDER
        GetComponent<Collider2D>().enabled = false;
    }
}
