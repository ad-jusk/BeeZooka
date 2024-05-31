using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 30f;

    private EdgeCollider2D edgeCollider;
    private Camera mainCamera;
    private GameManager gameEventsManager;

    private void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        mainCamera = Camera.main;
        gameEventsManager = GameManager.Instance;
        CreateEdgeCollider();
    }

    private void CreateEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>
        {
            CameraUtils.ScreenToWorld(mainCamera, Vector2.zero),
            CameraUtils.ScreenToWorld(mainCamera, new Vector2(Screen.width, 0)),
            CameraUtils.ScreenToWorld(mainCamera, new Vector2(Screen.width, Screen.height)),
            CameraUtils.ScreenToWorld(mainCamera, new Vector2(0, Screen.height)),
            CameraUtils.ScreenToWorld(mainCamera, Vector2.zero)
        };

        edgeCollider.SetPoints(edges);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
        ContactPoint2D[] contactPoints = new ContactPoint2D[1];
        collision.GetContacts(contactPoints);

        bool bottomEdgeHit = contactPoints[0].normal.Equals(Vector2.down);

        if (bottomEdgeHit)
        {
            // HITTING BOTTIM EDGE MEANS THE BEE MISSED THE BEEHIVE
            gameEventsManager.NotifyBeehiveMissed();
        }
        else
        {
            collidingRB.AddForce(Vector3.Reflect(collision.relativeVelocity, contactPoints[0].normal) * bounceForce);
            LevelManager.NumberOfEdgeCollisions++;
        }
    }
}
