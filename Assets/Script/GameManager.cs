using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
        }
    }
    void CutScene(bool _enter)
    {
        idleSq.SetActive(_enter);
        ship.SetActive(!_enter);
        playing = _enter;
    }
}

