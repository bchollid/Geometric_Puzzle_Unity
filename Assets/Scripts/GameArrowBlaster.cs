using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArrowBlaster : MonoBehaviour
{
    [SerializeField] private string direction;
    [SerializeField] private float force = 1f;
    private Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "TestPlayer" || col.gameObject.tag == "Enemy")
        {
            playerRigidbody = col.gameObject.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = Vector3.zero;
            switch(direction)
            {
                case "Left":
                    playerRigidbody.AddForce(new Vector2(-force, 0f), ForceMode2D.Impulse);
                    break;
                case "Right":
                    playerRigidbody.AddForce(new Vector2(force, 0f), ForceMode2D.Impulse);
                    break;
                case "Up":
                    playerRigidbody.AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
                    break;
                case "Down":
                    playerRigidbody.AddForce(new Vector2(0f, -force), ForceMode2D.Impulse);
                    break;
                default:
                    playerRigidbody.AddForce(new Vector2(0f, 0f), ForceMode2D.Impulse);
                    break;
            }
        }
    }
}
