using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame  
    void Update()
    {
        //Vector3 mousePos = Input.mousePosition;
        //transform.position = mousePos;
    }
    public void ShowCurse(bool _vis) 
    {
        Cursor.visible = _vis;
    }
}
