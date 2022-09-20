using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ShipControlNew : MonoBehaviour
{

    [SerializeField]
    private float _maxSpeed = 5.0f;
    private float _speed = 0;
    [SerializeField]
    private float _turnSpeed = 5.0f;

    [SerializeField]
    private float _speedDecay = 0.95f;


    [SerializeField]
    private Thruster engineLeft;
    [SerializeField]
    private Thruster engineRight;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Turn();
        Move();

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
        Vector3 _dir = Vector3.right * _speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            _speed = Mathf.Min(_maxSpeed, _speed + 2);
            StartCoroutine(boostLeft());
            StartCoroutine(boostRight());
        }
        else {
            _speed *= _speedDecay;

        }
        transform.Translate(_dir);
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
