using UnityEngine;

public class Beehive : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake() {
        gameManager = GameManager.Instance;    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(gameManager.AllFlowersCollected() ? "LEVEL CLEARED" : "LEVEL FAILED");
    }
}
