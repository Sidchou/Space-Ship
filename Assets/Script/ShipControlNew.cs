using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ShipControlNew : MonoBehaviour
{

 
    [SerializeField]
    private float _speed = 50;
    [SerializeField]
    private float _turnSpeed = 5.0f;

    [SerializeField]
    private Thruster engineLeft;
    [SerializeField]
    private Thruster engineRight;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject VC;
    [SerializeField]
    private Camera _camera;

    private Rigidbody _rb;
    private Collider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _camera.cullingMask = 255;
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("_rb is null");
        }
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError("_collider is null");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Wrap();

        Turn();
        Move();
        
        shoot();
        camSwitch();
        
    }
    void Wrap() 
    {
        Vector3 _p = transform.position;
        float _d = Vector3.Magnitude(_p);
        if (_d > 200) {
            _p *= -1;
        }
        transform.position = _p;
    }
    void Turn() 
    {
        Vector3 _dir = new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Rotate(_dir * Time.deltaTime * _turnSpeed);
        if (Input.GetAxis("Horizontal") > 0.1)
        {
            StartCoroutine(boostLeft());
        }
        else if (Input.GetAxis("Horizontal") < -0.1)
        {
            StartCoroutine(boostRight());
        }
    }

    void Move()
    {
        //       Vector3 _dir = Vector3.right * _speed *Time.deltaTime;
        Vector3 _dir = transform.right * _speed;

        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(_dir);
            StartCoroutine(boostLeft());
            StartCoroutine(boostRight());

        }

    }
    void shoot()
    {
        Vector3 _offset = new Vector3(-0.005f, -0.7f, 6.6f);
        GameObject laser = null;
        Quaternion angle = Quaternion.identity;
        if (Input.GetMouseButtonDown(0))
        {
            laser = Instantiate(_laserPrefab, transform.position, _laserPrefab.transform.localRotation);
            angle = _laserPrefab.transform.localRotation;
        }
        if (Input.GetMouseButtonDown(1))
        {
            laser = Instantiate(_laserPrefab, transform.position, _laserPrefab.transform.localRotation);
            angle = _laserPrefab.transform.localRotation;
            _offset.z *= -1;
        }
        if (laser != null)
        {
            Physics.IgnoreCollision(laser.GetComponent<Collider>(), _collider);
            laser.transform.parent = transform;
            laser.transform.localPosition = _offset;
            laser.transform.localRotation = angle;
            laser.transform.parent = null;

        }
    }

    void camSwitch() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            VC.SetActive(!VC.activeSelf);
            if (!VC.activeSelf)
            {
                _camera.cullingMask = 255;
            }
            else
            {
                _camera.cullingMask = 383;
            }

        }
    }
    IEnumerator boostLeft()
    {
        int n = 0;
        while (n < 2)
        {
            engineLeft.Boost(true);

            yield return new WaitForEndOfFrame();
            n++;
        }
        engineLeft.Boost(false);
    }
    IEnumerator boostRight()
    {
        int n = 0;
        while (n < 2)
        {
            engineRight.Boost(true);

            yield return new WaitForEndOfFrame();
            n++;
        }
        engineRight.Boost(false);
    }

}
