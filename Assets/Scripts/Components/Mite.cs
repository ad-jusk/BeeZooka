using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Mite : MonoBehaviour
{
    public float speed = 3f;

    [SerializeField]
    private bool movingRight = false; 

    private float leftScreenBorder;
    private float rightScreenBorder;
    private GameManager gameManager;

    private void Start()
    {
        CalculateScreenBorders();
        gameManager = GameManager.Instance;
        gameManager.OnObstacleEntered += HandleObstacleEntered;

    }

    void FixedUpdate()
    {
        Move();
    }
    private void HandleObstacleEntered()
    {
        speed = 0;
    }
    private void CalculateScreenBorders()
    {
        Camera mainCamera = Camera.main;
        leftScreenBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x + 0.8f;
        rightScreenBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x - 0.8f;
    }

    private void Move()
    {
        Vector3 newPosition = transform.position;

        if (movingRight)
        {
            // Move to the right
            newPosition.x += speed * Time.deltaTime;

            if (newPosition.x >= rightScreenBorder)
            {
                movingRight = false;
                Rotate();
            }
        }
        else
        {
            // Move to the left
            newPosition.x -= speed * Time.deltaTime;

            if (newPosition.x <= leftScreenBorder)
            {
                movingRight = true;
                Rotate();
            }
        }

        transform.position = newPosition;
    }

    private void Rotate()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}