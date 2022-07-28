using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Asteroid : MonoBehaviour
{
    [Header("Asteroids param")]
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] public int points;

    private Vector2 move;
    private Vector2 spawnPosition;

    [Header("Asteroids variables")]
    public GameObject mediumAsteroid;
    public GameObject smallAsteroid;
    public int asteroidSize;   // 3 big, 2 medium , 1 small

    private GameObject player;
    public GameObject exploid;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        move = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)); ;
        spawnPosition = new Vector2(Random.Range(-9.5f, 9.5f), Random.Range(0, 3) > 1 ? 5.5f : -5.5f);

        if (asteroidSize == 3)
        {
            transform.position = spawnPosition;
        }
    }

    void Update()
    {
        transform.position += (Vector3)move * speed * Time.deltaTime;
        transform.Rotate(0, 0, rotSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Rock")
        {
            return;
        }

        if ((collision.collider.tag == "PlayerBullet") || (collision.collider.tag == "UFOBullet") ||
            (collision.collider.tag == "Player") || (collision.collider.tag == "UFO"))
        {
            Spawner.asteroidCounter -= 1;
            Destroy(gameObject);

            if (asteroidSize == 3)
            {
                Instantiate(mediumAsteroid, transform.position + transform.right, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
                Instantiate(mediumAsteroid, transform.position - transform.right, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
                Spawner.asteroidCounter += 2;
            }
            else if (asteroidSize == 2)
            {
                Instantiate(smallAsteroid, transform.position + transform.right, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
                Instantiate(smallAsteroid, transform.position - transform.right, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
                Spawner.asteroidCounter += 2;
            }

            player.GetComponent<Player>().Score(points);
            var obj = Instantiate(exploid, transform.position, transform.rotation);
            Destroy(obj, 3f);
            Destroy(gameObject);
        }
    }
}
