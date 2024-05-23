using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bee"))
        {
            gameManager.NotifyObstacleEntered();
            Debug.Log("Obstacle hti");

            // Position the bee at the center of the flower
            Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();

            beeRigidbody.velocity = Vector2.zero;

            beeRigidbody.rotation = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

}
