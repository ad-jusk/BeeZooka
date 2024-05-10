using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 30f;

    private EdgeCollider2D edgeCollider;
    private Camera mainCamera;

    private void Awake() {
        edgeCollider = GetComponent<EdgeCollider2D>();
        mainCamera = Camera.main;
        CreateEdgeCollider();
    }

    private void CreateEdgeCollider() {
        
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

    private void OnCollisionEnter2D(Collision2D collision) {
       
        Rigidbody2D collidingRB = collision.transform.GetComponent<Rigidbody2D>();
        ContactPoint2D[] contactPoints = new ContactPoint2D[1];
        collision.GetContacts(contactPoints);

        bool topEdgeHit = contactPoints[0].normal.Equals(Vector2.up);

        if(topEdgeHit) {
            collidingRB.velocity = Vector2.zero;
        } else {
            collidingRB.AddForce(Vector3.Reflect(collision.relativeVelocity, contactPoints[0].normal) * bounceForce);
        }
    }
}
