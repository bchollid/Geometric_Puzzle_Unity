using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private string direction;
    [SerializeField] private float force = 1f;
    private Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Working!");
            switch(direction)
            {
                case "Left":
                    playerRigidbody.AddForce(new Vector2(-force, 0f), ForceMode2D.Impulse);
                    Debug.Log("Working left!");
                    break;
                default:
                    playerRigidbody.AddForce(new Vector2(0f, 0f), ForceMode2D.Impulse);
                    break;
            }
        }
    }
}
