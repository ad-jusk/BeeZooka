using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Animator ObstacleAnimator;
    public ObstacleType Type;

    private GameManager gameManager;
    private AudioManager audioManager;
    private Collider2D beeCollider;
    private bool hasPlayedSound = false;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        beeCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bee") && !hasPlayedSound)
        {
            hasPlayedSound = true;

            gameManager.NotifyObstacleEntered();
            audioManager.PlaySFX(AudioClipType.ObstacleHit);

            Debug.Log("Obstacle hit");

            Rigidbody2D beeRigidbody = other.GetComponent<Rigidbody2D>();

            beeRigidbody.velocity = Vector2.zero;
            beeRigidbody.rotation = 0;

            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(ObstacleEnteredAnimation(beeRigidbody));
        }
    }
    private IEnumerator ObstacleEnteredAnimation(Rigidbody2D beeRigidbody)
    {
        beeCollider.isTrigger = false;

        switch (Type)
        {
            case ObstacleType.WASP:
            case ObstacleType.MITE:
                yield return HandleWaspOrMiteAttack(beeRigidbody);
                break;

            case ObstacleType.FACTORY:
            case ObstacleType.SMOKER:
                HandleSmokeObstacleInteraction(beeRigidbody);
                break;
        }
    }
    private IEnumerator HandleWaspOrMiteAttack(Rigidbody2D beeRigidbody)
    {
        Animator beeAnimator = beeRigidbody.GetComponent<Animator>();
        if (ObstacleAnimator != null)
        {
            ObstacleAnimator.SetBool("isAttacking", true);
            beeRigidbody.position = new Vector2(
                transform.position.x - 0.35f,
                transform.position.y - 0.2f
            );
            yield return new WaitForSeconds(ObstacleAnimator.GetCurrentAnimatorStateInfo(0).length * 0.8f);

            beeAnimator.SetBool("isDefeated", true);

            ObstacleAnimator.SetBool("isAttacking", false);
        }
    }

    private void HandleSmokeObstacleInteraction(Rigidbody2D beeRigidbody)
    {
        Animator beeAnimator = beeRigidbody.GetComponent<Animator>();
        beeAnimator.SetBool("isDefeated", true);
        SpriteRenderer beeBodyRenderer = GameObject.Find("bee_body").GetComponent<SpriteRenderer>();
        if (beeBodyRenderer != null)
        {
            StartCoroutine(ChangeObstacleColorGradually(beeBodyRenderer, Color.gray, 1f));
        }
        else
        {
            Debug.LogError("bee_body SpriteRenderer not found");
        }
    }

    private IEnumerator ChangeObstacleColorGradually(SpriteRenderer spriteRenderer, Color targetColor, float duration)
    {
        Color initialColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = targetColor; 
    }
}
