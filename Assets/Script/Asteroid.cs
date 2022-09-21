using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    Vector3 _move;
    Vector3 lastp = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
       // _move = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        //_move *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _debug = (transform.position - lastp)/Time.deltaTime;
            lastp= transform.position;
    }
}
