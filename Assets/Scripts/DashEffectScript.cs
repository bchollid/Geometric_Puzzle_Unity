using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectScript : MonoBehaviour
{
    [SerializeField] private float dashAnimationDeleteTime = 0.1f;
    void Start()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 50f;
        GetComponent<SpriteRenderer>().color = tmp;
        StartCoroutine(DashDelete());
    }

    void Update()
    {
        
    }

    private IEnumerator DashDelete()
    {
        yield return new WaitForSeconds(dashAnimationDeleteTime);
        Destroy(gameObject);
    }
}
