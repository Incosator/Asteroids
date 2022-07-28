using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [Header("UFO variables")]

    [SerializeField, Range(0, 10)] float speed = 5;
    [SerializeField, Range(0, 100)] public int points;

    public Weapon weapon;
    public GameObject exploid;

    private Vector2 spawnPosition;
    private GameObject player;

    void Start()
    {
        spawnPosition = new Vector2(Random.Range(0, 2) > 0 ? 10 : -10, Random.Range(4f, -4f));
        transform.position = spawnPosition;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (spawnPosition.x < 0)
            transform.position += transform.right * speed * Time.deltaTime;
        else if (spawnPosition.x > 0)
            transform.position -= transform.right * speed * Time.deltaTime;

        Attack();
    }
    private void Attack() => weapon.Fire();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "UFOBullet")
            return;
        if ((collision.collider.tag == "Player") || (collision.collider.tag == ("Rock")) || (collision.collider.tag == "PlayerBullet"))
        {
            player.GetComponent<Player>().Score(points);
            var obj = Instantiate(exploid, transform.position, Quaternion.identity);
            Destroy(obj, 4f);
            Destroy(gameObject);
        }
    }
}