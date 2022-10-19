using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    private Transform[] children;
    private float[] dir;
    private GameObject cam;

    private targetCount tCount;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        if (cam == null)
        {
            Debug.LogError("cam is null");
        }
        tCount = FindObjectOfType<targetCount>();
        if (tCount == null)
        {
            Debug.LogError("targetcount is null");
        }

        children = transform.GetComponentsInChildren<Transform>();
        dir = new float[children.Length];
        for (int i = 0; i < dir.Length; i++)
        {
            float x = Random.Range(8f, 15f) * Mathf.Sign(Random.value-0.5f);
            dir[i] = x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < children.Length; i++)
        {
            children[i].Rotate(Vector3.forward*10*Time.deltaTime);
        }
        transform.LookAt(cam.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            tCount.UpdateCount();
            Destroy(gameObject);
        }
        if (other.tag == "Asteroid")
        {
            Resize(2.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            Resize(1);
        }
    }
    private void Resize(float _s) 
    {
        for (int i = 1; i < children.Length; i++)
        {
            children[i].localScale = new Vector3(_s,_s,_s);
        }

    }
}
