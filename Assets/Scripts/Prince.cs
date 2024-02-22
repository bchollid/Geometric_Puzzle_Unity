using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince : MonoBehaviour
{

    public bool death = false;
    private GameManager gameManager;
    private Rigidbody2D rb2d;
    [SerializeField] private GameObject deathEffect;
    private AudioSource audioSource;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        audioSource.volume= rb2d.velocity.sqrMagnitude / 100;
        audioSource.Play();  
        if(col.tag == "Enemy" || col.tag == "OutOfBounds")
        {
            death = true;
            gameManager.CheckForWin();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
