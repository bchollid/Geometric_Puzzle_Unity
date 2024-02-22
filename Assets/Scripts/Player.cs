using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 launch;
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private GameObject testPlayer;
    private float spawnTimer = 0f;
    [SerializeField] private float spawnRate = 1f;
    private bool canFling = true;
    public float shotsAbsorbed = 0;
    private GameManager gameManager;
    [SerializeField] private float dashSpeed = 10f;
    private float delayTimer = 0f;
    private bool startCounting = false;
    public bool testLaunchFired = false;
    [SerializeField] private bool dashUnlocked = false;
    [SerializeField] private bool growUnlocked = false;
    [SerializeField] private Vector3 growChangeAmount;
    [SerializeField] private GameObject dashEffect;
    private float dashTimer = 0;
    [SerializeField] private float dashAnimationLimit = 1f;
    private bool startDashCounting = false;
    private bool startGrowCounting = false;
    private float growTimer = 0f;
    [SerializeField] private float growAnimationLimit = 1f;
    private bool canActivateSpecial = false;
    [SerializeField] private bool gravityUnlocked = false;
    private AudioSource audioSource;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Fling();
        Dash(); 
        Grow();
        GravitySwitch();  
        if(startCounting)
        {
            delayTimer+=Time.deltaTime;
        }
    }

    private void Fling()
    {
        if(Input.GetMouseButton(0) && canFling)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            launch = Vector3.ClampMagnitude(transform.position - worldPosition, 5f);
            spawnTimer += Time.deltaTime;
            if(spawnTimer >= spawnRate)
            {
                SpawnTestLaunch();
                spawnTimer = 0;
            }

        }
        if(Input.GetMouseButtonUp(0) && canFling)
        {
            startCounting = true;
            canActivateSpecial = true;
            rb2d.AddForce(new Vector2(launch.x, launch.y) * launchForce, ForceMode2D.Impulse);
            canFling = false;
        }
    }

    private void SpawnTestLaunch()
    {
        testLaunchFired = true;
        var pathTester = Instantiate(testPlayer, transform.position, Quaternion.identity);
        Rigidbody2D pathrb2d = pathTester.GetComponent<Rigidbody2D>();
        pathrb2d.AddForce(new Vector2(launch.x, launch.y) * launchForce, ForceMode2D.Impulse);
    }

    private void Dash()
    {
        if(startDashCounting)
        {
            dashTimer += Time.deltaTime;
            if(dashTimer < dashAnimationLimit)
            {
                Instantiate(dashEffect, transform.position, Quaternion.identity);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && canActivateSpecial && dashUnlocked)
        {
            startDashCounting = true;
            Vector3 worldPositionForDash = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dashDirection = new Vector2(worldPositionForDash.x - transform.position.x, worldPositionForDash.y - transform.position.y);
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.AddForce(dashDirection.normalized * dashSpeed, ForceMode2D.Impulse);
            canActivateSpecial = false;
        }
    }

    private void Grow()
    {    
        if(startGrowCounting)
        {
            growTimer += Time.deltaTime;
            if(growTimer < growAnimationLimit)
            {
                Debug.Log("Grow Timer: " + growTimer);
                Debug.Log("Transform: " + gameObject.transform.localScale);
                Debug.Log("Growth change: " + growChangeAmount);
                gameObject.transform.localScale += growChangeAmount * Time.deltaTime;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && canActivateSpecial && growUnlocked)
        {
            startGrowCounting = true;
            canActivateSpecial = false;
        }
    }

    private void GravitySwitch()
    {
         if(Input.GetKeyDown(KeyCode.Space) && canActivateSpecial && gravityUnlocked)
        {
            rb2d.gravityScale = -1f;
            canActivateSpecial = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        audioSource.volume = rb2d.velocity.sqrMagnitude / 750;
        audioSource.Play();    
    }
}
