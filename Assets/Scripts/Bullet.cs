using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Owner { Player, Ufo }

    [Header("Options")]
    [SerializeField] float speed = 15;
    [Header("Who owner ?")]
    public Owner owner;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (owner == Owner.Player)
        {
            if (collision.collider.tag == "UFO")
                Destroy(gameObject);
        }
        if (owner == Owner.Ufo)
        {
            if (collision.collider.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
        if (collision.collider.tag == "Rock")
        {
            Destroy(gameObject);
        }
    }
}
