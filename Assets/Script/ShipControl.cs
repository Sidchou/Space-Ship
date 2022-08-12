using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _cam;
    private Vector3 _camAim;
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

    // Start is called before the first frame update
    void Start()
    {
        _camAim = _cam.transform.eulerAngles;
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
           
        }
        transform.Translate(_momentum * Time.deltaTime, Space.World);
        _momentum *= _speedDecay;
    }
    void Redirect() {
        Vector3 _dir = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        _AngularMomentum += _dir;
        _momentum = Vector3.ClampMagnitude(_momentum, _maxSpeed);

        transform.Rotate(_AngularMomentum* Time.deltaTime);
        _AngularMomentum *= _speedDecay;

    }
    void Roll()
    {
        //Vector3 _dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("Jump"));
        //Vector3 _dir = new vector3(input.getaxis("pitch"), input.getaxis("horizontal"), input.getaxis("vertical"));
        //transform.Rotate(_dir);
        if (Mathf.Abs(Input.GetAxis("Pitch")) > 0.5f && !_rollToggle)
        {
            Debug.Log("input "+Input.GetAxis("Pitch"));
            StartCoroutine(RollRoutine(Input.GetAxis("Pitch")));
            _rollToggle = true;
        }

    }
    void Turn() 
    {
        Vector3 _mouseXY = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"),0);
        _camAim += _mouseXY * Time.deltaTime* 200;
        _camAim.y = ClampAngle(_camAim.y, _camClampX[0], _camClampX[1]);
        _camAim.x = ClampAngle(_camAim.x, _camClampY[0], _camClampY[1]);

        _cam.transform.localRotation = Quaternion.Euler(_camAim);
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
            transform.Rotate(Vector3.right * Mathf.Sign(_dir) * tScale* _r);
            n += _r * tScale;
            yield return new WaitForEndOfFrame();
            rot = transform.rotation;
        }
        rot.x = 0;
        transform.rotation = rot;

        _rollToggle = false;
    }
}
