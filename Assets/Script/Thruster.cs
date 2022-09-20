using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    private ParticleSystem _system;
    private float _startSize;
    private float _startLife;
    [SerializeField]
    private float _boostedSize = 5.5f;
    [SerializeField]
    private float _boostedLife = 2.25f;

    [SerializeField]

    private bool _boosted = false;
    // Start is called before the first frame update
    void Start()
    {
        _system = GetComponent<ParticleSystem>();
        if (_system == null) 
        {
            Debug.LogError("ParticleSystem does not exist"); 
        } 
        else 
        {
            _startSize = _system.main.startSize.constant;
            _startLife = _system.main.startLifetime.constant;
        }
    }

    // Update is called once per frame
    private void Update()
    {

        var main = _system.main;

        if (_boosted)
        {
            main.startSize = _boostedSize;
            main.startLifetime = _boostedLife;
        }
        else
        {
            main.startSize = _startSize;
            main.startLifetime = _startLife;
        }
    }

    public void Boost(bool _boost)
    {
        _boosted = _boost;
    }
}
