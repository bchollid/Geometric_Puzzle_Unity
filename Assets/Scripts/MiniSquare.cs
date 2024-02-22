using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSquare : MonoBehaviour
{
    [SerializeField] private float delayTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
