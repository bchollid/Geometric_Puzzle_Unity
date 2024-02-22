using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private GameObject prince;
    [SerializeField] private float delay = 1f;
    private Vector3 currentPrincePosition;
    private float forceAmount;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = GameObject.Find("Enemy");
        // float closestDistance = 100f;
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if(distance == 0f)
            {
                closestEnemy = enemy;
            }
        }
        forceAmount = closestEnemy.GetComponent<Enemy>().forceAmount;
        prince = GameObject.Find("Prince");
        StartCoroutine(DestroyDelay());
        currentPrincePosition = GameObject.Find("Prince").transform.position;
        GetComponent<Rigidbody2D>().AddForce((currentPrincePosition - transform.position)* forceAmount) ;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
        // float step = speed * Time.deltaTime;
        // transform.position = Vector2.MoveTowards(transform.position, currentPrincePosition.position, step);

        // Vector3 targ = currentPrincePosition.position;
        //     targ.z = 0f;

        //     Vector3 objectPos = transform.position;
        //     targ.x = targ.x - objectPos.x;
        //     targ.y = targ.y - objectPos.y;

        //     float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
