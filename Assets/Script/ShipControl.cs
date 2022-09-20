using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{

    private float[] _camClampX = { 30,150 };
    private float[] _camClampY = { -50 , 40 };

    [SerializeField]
    private Vector3 _momentum = Vector3.zero;
    private Vector3 _AngularMomentum = Vector3.zero;

    [SerializeField]
    private float _maxSpeed = 5.0f;
    [SerializeField]
    private float _speedDecay = 0.95f;

    private bool _rollToggle = false;

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
        Accelerate();
        Redirect();
        Roll();
        //Turn();

    }
    void Accelerate() 
    {
        Vector3 _dir = new Vector3( Input.GetAxis("Vertical"), 0, 0);
        if (_dir.x > 0)
        {
            _momentum += transform.TransformDirection(_dir);
            _momentum = Vector3.ClampMagnitude(_momentum, 360);
            StartCoroutine(boostLeft());
            StartCoroutine(boostRight());
        }
        _momentum.y = 0;
        transform.Translate(_momentum * Time.deltaTime, Space.World);
        _momentum *= _speedDecay;
    }
    void Redirect() {
        Vector3 _dir = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        _AngularMomentum += _dir;
        _momentum = Vector3.ClampMagnitude(_momentum, _maxSpeed);

        transform.Rotate(_AngularMomentum* Time.deltaTime);
        _AngularMomentum *= _speedDecay;
        if (Input.GetAxis("Horizontal")>0.1) {
            StartCoroutine(boostLeft()); 
        }
        else if (Input.GetAxis("Horizontal")< -0.1)
            {
            StartCoroutine(boostRight()); }
        }
    void Roll()
    {
        //Vector3 _dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("Jump"));
        //Vector3 _dir = new vector3(input.getaxis("pitch"), input.getaxis("horizontal"), input.getaxis("vertical"));
        //transform.Rotate(_dir);
        if (Mathf.Abs(Input.GetAxis("Pitch")) > 0.5f && !_rollToggle)
        {
            StartCoroutine(RollRoutine(Input.GetAxis("Pitch")));
            _rollToggle = true;
        }

    }


    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    IEnumerator RollRoutine(float _dir) {
        float n = 0;
        Quaternion rot = Quaternion.identity;
        while ( n<360 ) {
            float tScale = Time.deltaTime;
            float _r = 720f;
            _r = 360;
            transform.Rotate(Vector3.right * Mathf.Sign(_dir) * tScale* _r);
            n += _r * tScale;
            yield return new WaitForEndOfFrame();
            rot = transform.rotation;
        }
        rot.x = 0;
        rot.z = 0;
        transform.rotation = rot;
        Vector3 fixY = transform.position;
        fixY.y = 0;
        transform.position = fixY;
        _rollToggle = false;
    }
    IEnumerator boostLeft() {
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
        while (n<2) {
            engineRight.Boost(true);

            yield return new WaitForEndOfFrame();
            n++;
        }
            engineRight.Boost(false);
    }
}
