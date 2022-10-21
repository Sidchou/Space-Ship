using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private Vector3 _move;
    private Rigidbody _rb;
    private Vector3 pos;
    private Vector3 rt;
    private float maxSpeed = 5;
    [SerializeField]
    private MeshRenderer go;
    [SerializeField]
    private Material mat;
    [SerializeField]
    private Material mat_h;

    // Start is called before the first frame update
    void Start()
    {
        _move = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("rb is null");
        }
        _rb.velocity = _move.normalized*0;
        pos = transform.position;
        rt = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 vel = pos - transform.position;
        //Vector3 ang = rt - transform.rotation.eulerAngles;

        Wrap();
        //_rb.AddForce(vel*Time.deltaTime);
        //_rb.angularVelocity = ang * Time.deltaTime;
        //pos = transform.position;
        //rt = transform.rotation.eulerAngles;
        if (_rb.velocity.magnitude < maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * maxSpeed;
        }
        //Debug.Log(_rb.velocity.magnitude);

    }
    private void OnCollisionEnter(Collision collision)
    {
        //_rb.AddForce(collision.GetContact(0).normal) ;
        //_rb.AddTorque(collision.transform.rotation.eulerAngles) ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            go.material = mat_h;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Target")
        {
            go.material = mat;
        }
    }
    void Wrap()
    {
        Vector3 _p = transform.position;
        float _d = Vector3.Magnitude(_p);
        if (_d > 200)
        {
            _p *= -0.9f;
        }
        transform.position = _p;
    }
}
