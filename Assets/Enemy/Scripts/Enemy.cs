using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject testBullet;
    private GameObject prince;
    private float testFireTimer = 0;
    [SerializeField] private float testFireCooldown = 1f;
    private bool willTestFire = true;
    public float bulletSpeed = 5f;
    private bool canFire = true;
    public float forceAmount = 10f;
    private Vector3 currentPrincePosition;
    private GameManager gameManager;
    [SerializeField] private Rigidbody2D rb2d;
    private bool hasHit = false;
    private Player player;
    // public Animator anim;

    void Start()
    {
        prince = GameObject.Find("Prince");   
        currentPrincePosition = prince.transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if(willTestFire && Input.GetMouseButton(0))
        {
            testFireTimer += Time.deltaTime;
            if(testFireTimer >= testFireCooldown)
            {
                TestFire();
                testFireTimer = 0;
            }
        }
        if(Input.GetMouseButtonUp(0) && canFire)
        {
            // anim.SetBool("isAttacking", true);
            willTestFire = false;
            Fire();
            canFire = false;
        }
    }

    void Fire()
    {
        rb2d.AddForce((currentPrincePosition - transform.position)* forceAmount);
    }

    void TestFire()
    {
        var testShot = Instantiate(testBullet, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !hasHit|| col.gameObject.tag == "Environment" && !hasHit)
        {
            player.GetComponent<Player>().shotsAbsorbed++;
            gameManager.CheckForWin();
            hasHit = true;
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Prince" && !hasHit)
        {
            hasHit = true;
            prince.GetComponent<Prince>().death = true;
            gameManager.CheckForWin();
            Destroy(gameObject);
        }
    }
}

