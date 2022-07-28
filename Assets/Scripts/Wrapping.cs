using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapping : MonoBehaviour
{
    [Header("Scren size")]
    [SerializeField] float screenTop;
    [SerializeField] float screenBottom;
    [SerializeField] float screenLeft;
    [SerializeField] float screeRight;

    void Update()
    {
        Vector2 newPosition = transform.position;

        if (transform.position.y > screenTop)
        {
            newPosition.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPosition.y = screenTop;
        }
        if (transform.position.x > screeRight)
        {
            newPosition.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPosition.x = screeRight;
        }
        transform.position = newPosition;
    }
}
