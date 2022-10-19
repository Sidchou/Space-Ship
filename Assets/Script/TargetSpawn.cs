using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float targetCount;
    // Start is called before the first frame update
    void Start()
    {
        float r = 100;
        for (int i = 0; i < targetCount; i++)
        {
            Vector3 _p = new Vector3(Random.Range(-r, r), Random.Range(-r, r), Random.Range(-r, r));

            Instantiate(target, _p, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetTS() 
    {
        return targetCount;
    }
}
