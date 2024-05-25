using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bee"))
        {
            gameManager.NotifyObstacleEntered();
            audioManager.PlaySFX(AudioClipType.ObstacleHit);

            Debug.Log("Obstacle hit");

            // Position the bee at the center of the flower
            Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();

            beeRigidbody.velocity = Vector2.zero;

            beeRigidbody.rotation = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
