using UnityEngine;

public class Beehive : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake() {
        gameManager = GameManager.Instance;    
    }

    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log(gameManager.AllFlowersCollected() ? "LEVEL CLEARED" : "LEVEL FAILED");
        if (gameManager.AllFlowersCollected())
        {
            gameManager.WinMenu.SetActive(true);
            Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();
            beeRigidbody.velocity = Vector2.zero;
            beeRigidbody.position = transform.GetComponent<Renderer>().bounds.center;
        }


    }
}
