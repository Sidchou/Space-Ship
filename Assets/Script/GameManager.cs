using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    private float idleTimer = 0;
    [SerializeField]
    private float idleTime=2;
 
    [SerializeField]
    private GameObject idleSq;
    [SerializeField]
    private GameObject ship;
    private bool playing = false;
    private PlayableDirector sq;

    [SerializeField]
    private GameObject endSq;
    [SerializeField]
    private GameObject endUi;

    [SerializeField]
    private GameObject targetUI;

    [SerializeField]
    private CinemachineBrain cinemachineBrain;
    [SerializeField]
    private Transform worldupShip;
    [SerializeField]
    private Camera cam;
    private int culling;
    // Start is called before the first frame update
    void Start()
    {
        idleTimer = Time.time;
        sq = idleSq.GetComponent<PlayableDirector>();
        if (sq == null)
        {
            Debug.LogError("Timeline SQ is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            CutScene(false);
            idleTimer = Time.time;
        }

        if (Time.time - idleTimer > idleTime)
        {
            if (!playing)
            {
                sq.time = 0;
            }
            CutScene(true);
            culling = cam.cullingMask;
        }
    }
    void CutScene(bool _enter)
    {
        idleSq.SetActive(_enter);
        ship.SetActive(!_enter);
        playing = _enter;
        if (_enter)
        {
            cinemachineBrain.m_WorldUpOverride = null;
            cam.cullingMask = 127;
        }
        else
        {
            cinemachineBrain.m_WorldUpOverride = worldupShip;
            cam.cullingMask = culling;
        }
    }
    public void ResetScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

