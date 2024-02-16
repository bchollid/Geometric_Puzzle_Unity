using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince : MonoBehaviour
{

    public bool death = false;
    private GameManager gameManager;
    // public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // if(col.tag == "Bullet")
        // {
        //     anim.SetBool("isDying", true);
        // }
        // if(col.tag == "Player")
        // {
        //     StartCoroutine(BumpReset());
        // }
    }

    // private IEnumerator BumpReset()
    // {
    //     anim.SetBool("gotBumped", true);
    //     yield return new WaitForSeconds(0.5f);
    //     anim.SetBool("gotBumped", false);
    // }
}
