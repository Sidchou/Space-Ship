using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class targetCount : MonoBehaviour
{
    [SerializeField]
    private Image targetUI;
    [SerializeField]
    private TargetSpawn ts;
    private int tCount = 10;
    private Image[] targets;
    [SerializeField]
    private GameObject EndSQ;
    [SerializeField]
    private Curser curser;
    [SerializeField]
    private ShipControlNew shipControl;
    // Start is called before the first frame update
    void Start()
    {
        tCount = (int)ts.GetTS();
        targets = new Image[tCount];
        for (int i = 0; i < tCount; i++)
        {
            targets[i] = Instantiate(targetUI, new Vector3(60*(0.5f+i),30,0),Quaternion.identity, transform); 

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (tCount == 0)
        {
            EndSQ.SetActive(true);
            curser.ShowCurse(true);
            shipControl.EndGame();
        }
    }
    public void UpdateCount() {
        Debug.Log("update");
        tCount--;
        targets[tCount].gameObject.SetActive(false);
        Debug.Log(tCount);

    }
}
