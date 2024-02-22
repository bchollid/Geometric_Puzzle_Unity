using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroyDelay : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
