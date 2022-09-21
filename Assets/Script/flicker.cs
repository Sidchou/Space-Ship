using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] flickerPrefab;
    private int _idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateFlicker());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator UpdateFlicker() {
        float r = Random.Range(0.2f,0.5f);
        for (int i = 0; i < flickerPrefab.Length; i++) {
            if (i <=_idx)
            {
                flickerPrefab[i].SetActive(true);

            }
            else 
            { 
                flickerPrefab[i].SetActive(false);
            }
        }
        _idx = (_idx +1 )% flickerPrefab.Length; 
        yield return new WaitForSeconds(r);
        StartCoroutine(UpdateFlicker());
    }
}
