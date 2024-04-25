using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public float JumpForce;
    public float MaxVelocityY;
    public Rigidbody2D rb2D;
    public int Points;
    public static bool GameOver;
    public static bool HasStarted;
    public GameObject gameOverScreen;
    public Animator animator;
    public AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip scoreSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        rb2D.gravityScale = 0f;
        GameOver = false;
        HasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (!HasStarted)
            {
                HasStarted = true;
                rb2D.gravityScale = 1f;
            }
            audioSource.clip = jumpSound;
            audioSource.Play();
            animator.SetTrigger("FlapWings");

            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if (rb2D.velocity.y> MaxVelocityY)
            rb2D.velocity = new Vector2(0,MaxVelocityY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Die!");
        GameOver = true;
        gameOverScreen.SetActive(true);
        audioSource.clip = hitSound;
        audioSource.Play();

        if (Points> PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", Points);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PointZone"))
        {
            audioSource.clip = scoreSound;
            audioSource.Play();
            Points++;
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene("flappybird_gameplay");
    }

}
