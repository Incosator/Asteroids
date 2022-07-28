using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player options")]
    [SerializeField] float speed = 5;
    [SerializeField] float rotationSpeed = 150;
    [SerializeField] bool mouseOn;

    [Header("Variables")]
    [SerializeField] int lives;
    [SerializeField] float timeToRespawn = 3;
    [SerializeField] float invulnerabilityTimer = 3;
    [SerializeField] float flashingTime = 0.5f;

    [Header("UI")]
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject gameOverPanel;

    public GameObject exploid;
    public Weapon weapon;
    public int score;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    private void Start()
    {
        score = 0;
        lives = 3;

        scoreText.text = "Score " + score;
        livesText.text = "Lives " + lives;

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    public void Score(int points)
    {
        score += points;
        scoreText.text = "Score " + score;
    }

    void Update()
    {
        Moveing();
    }
    private void Moveing()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }

        if (!mouseOn)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            }
        }

        if (mouseOn)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 moveDirection = (worldPosition - transform.position).normalized;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle - 90);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            weapon.Fire();
        }
    }
    private void Respawn()
    {
        transform.position = Vector2.zero;
        StartCoroutine(nameof(Invulnerability));
    }

    IEnumerator Invulnerability()
    {
        var invuTime = invulnerabilityTimer;
        while (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer--;
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashingTime);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashingTime);
            spriteRenderer.enabled = true;
        }
        invulnerabilityTimer = invuTime;
        collider2D.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerBullet")
        {
            return;
        }
        var obj = Instantiate(exploid, transform.position, Quaternion.identity);
        Destroy(obj, 4f);

        lives--;
        livesText.text = "Lives " + lives;

        spriteRenderer.enabled = false;
        collider2D.enabled = false;

        Invoke(nameof(Respawn), timeToRespawn);

        if (lives < 0)
        {
            Time.timeScale = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        CancelInvoke(nameof(Respawn));
        gameOverPanel.SetActive(true);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene("Asteroids");
        Time.timeScale = 1;
        Spawner.asteroidCounter = 0;
    }

    private void GoToMenue()
    {
        SceneManager.LoadScene("Menue");
        Time.timeScale = 1;
        Spawner.asteroidCounter = 0;
    }
}
