using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private GameObject _explosion;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _move = Vector3.down * _speed * Time.deltaTime;
        transform.Translate(_move);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Asteroid")
        {
            Explode();
        }
        Wrap();

    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.transform.tag == "Asteroid")
    //    {
    //        Explode();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Explode();
        }
    }
    private void Explode()
    {
        Instantiate(_explosion, transform.position + transform.up * _speed * Time.deltaTime * 2, transform.rotation);
        Destroy(gameObject, 0.05f);
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
