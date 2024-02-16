using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 launch;
    private float horizontal;
    private float vertical;
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private GameObject miniSquare;
    private float spawnTimer = 0f;
    [SerializeField] private float spawnRate = 1f;
    private bool canFling = true;
    public float shotsAbsorbed = 0;
    private GameManager gameManager;
    [SerializeField] private float dashSpeed = 10f;
    private bool canDash = true;
    // public Animator anim;
    // private float delayCheck = 1f;
    private float delayTimer = 0f;
    private bool startCounting = false;
    [SerializeField] private GameObject wallPart;
    private bool canBuild = true;
    [SerializeField] private float wallCooldownSeconds = 0.5f;
    public bool testLaunchFired = false;
    // [SerializeField] private float currentSpeed = 5f;
    // [SerializeField] private float normalSpeed = 5f;
    // [SerializeField] private float dashSpeed = 10f;
    // [SerializeField] private float jumpingPower = 5f;
    // [SerializeField] private float groundCheckBoxDistance = 1f;
    // [SerializeField] private Vector3 groundCheckBoxSize;
    // [SerializeField] private LayerMask groundLayerMask;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        Fling();
        Dash();   
        CreateWall();
        if(startCounting)
        {
            delayTimer+=Time.deltaTime;
        }
        // if(delayTimer >= delayCheck)
        // {
            //  if(rb2d.velocity.magnitude < 1f)
            // {    
            //     anim.SetBool("isDying", true);
            // }
        // }

    }

    private void Fling()
    {
        if(Input.GetMouseButton(0) && canFling)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            launch = transform.position - worldPosition;
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
            // anim.SetBool("isFlinging", true);
            // StartCoroutine(DetectMovementDelay());
            rb2d.AddForce(new Vector2(launch.x, launch.y) * launchForce, ForceMode2D.Impulse);
            canFling = false;
        }
    }

    private void SpawnTestLaunch()
    {
        testLaunchFired = true;
        var pathTester = Instantiate(miniSquare, transform.position, Quaternion.identity);
        Rigidbody2D pathrb2d = pathTester.GetComponent<Rigidbody2D>();
        pathrb2d.AddForce(new Vector2(launch.x, launch.y) * launchForce, ForceMode2D.Impulse);
    }

    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Vector3 worldPositionForDash = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dashDirection = new Vector2(worldPositionForDash.x - transform.position.x, worldPositionForDash.y - transform.position.y);
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.AddForce(dashDirection.normalized * dashSpeed, ForceMode2D.Impulse);
            canDash = false;
        }
    }

    private void CreateWall()
    {
        if(Input.GetKey(KeyCode.LeftShift) && canBuild)
        {
            Instantiate(wallPart, transform.position, Quaternion.identity);
            StartCoroutine(WallCooldown());
        }
    }

    private IEnumerator WallCooldown()
    {
        yield return new WaitForSeconds(wallCooldownSeconds);
        canBuild = false;
    }

    // private IEnumerator DetectMovementDelay()
    // {
    //     yield return new WaitForSeconds(1f);
    //      if(rb2d.velocity.magnitude < 2f)
    //     {
    //         anim.SetBool("isDying", true);
    //     }
    // }
}
