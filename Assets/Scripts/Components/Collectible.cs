using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
    }

    // Using OnTriggerEnter2D instead of OnCollisionEnter2D for entering flower
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bee"))
        {
            Debug.Log("collectible");
            audioManager.PlaySFX(AudioClipType.FlowerEntered);
            //Transform particleTransform = transform.Find("ParticlesPollinated");
            gameObject.SetActive(false);
            gameManager.NotifyCollectibleCollected();
        }
    }

}
