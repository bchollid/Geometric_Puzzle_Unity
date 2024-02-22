using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEffect : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float randomX;
    private float randomY;
    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-20f, 20f);
        randomY = Random.Range(-20f, 20f);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
