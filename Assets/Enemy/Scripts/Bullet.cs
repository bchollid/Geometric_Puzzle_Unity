using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private GameObject prince;
    private GameObject player;
    private GameManager gameManager;
    private Vector3 currentPrincePosition;
    private bool hasHit = false;
    private float forceAmount;
    // Start is called before the first frame update
    void Start()
    {
        forceAmount = GameObject.Find("Enemy").GetComponent<Enemy>().forceAmount;
        prince = GameObject.Find("Prince");
        player = GameObject.Find("Player");
        speed = GameObject.Find("Enemy").GetComponent<Enemy>().bulletSpeed;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentPrincePosition = GameObject.Find("Prince").transform.position;
        // Vector3 targ = currentPrincePosition;
        //     targ.z = 0f;

        //     Vector3 objectPos = transform.position;
        //     targ.x = targ.x - objectPos.x;
        //     targ.y = targ.y - objectPos.y;

        //     float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        GetComponent<Rigidbody2D>().AddForce((currentPrincePosition - transform.position)* forceAmount) ;
    }

    // Update is called once per frame
    void Update()
    {
        // float step = speed * Time.deltaTime;
        // transform.position = Vector2.MoveTowards(transform.position, currentPrincePosition, step);    
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
        }
    }
}
