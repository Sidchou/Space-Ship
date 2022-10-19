using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] asteroids;
    private float r = 150;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            Vector3 _p = new Vector3(Random.Range(50,r)* Mathf.Sign(Random.value-0.5f), Random.Range(-r, r), Random.Range(-r, r));
            Instantiate(asteroids[i], _p, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
