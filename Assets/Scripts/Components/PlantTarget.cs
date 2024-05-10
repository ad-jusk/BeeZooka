using UnityEngine;

public class PlantTarget : MonoBehaviour
{
    private GameEventsManager gameEventsManager;

    private void Awake() {
        gameEventsManager = GameEventsManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        gameEventsManager.NotifyNextLevel();
    }
}
