using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] SpawnPointsArray;
    [SerializeField]
    private GameObject[] CollectablesArray;
    private bool CollectablePossitionated = false;
    void Start()
    {
        for (int i = 0; i < CollectablesArray.Length; i++)
        {
            do {
                int RandomInt = Random.Range(0, SpawnPointsArray.Length);

                if (SpawnPointsArray[RandomInt] != null)
                {
                    CollectablesArray[i].transform.position = SpawnPointsArray[RandomInt].transform.position;
                    SpawnPointsArray[RandomInt] = null;
                    CollectablePossitionated = true;
                }
            } while (!CollectablePossitionated);
            CollectablePossitionated = false ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
