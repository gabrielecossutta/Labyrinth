using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceUIScript : MonoBehaviour
{
    // Reference of the Collectables Text Counter
    private GameObject collectableTextCounter;

    // Reference of the Parent(Player)
    private GameObject parent;

    // Reference of the Main Camera
    [SerializeField]
    private Camera cameras;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Reference of the TextCounter
        collectableTextCounter = this.gameObject;

        // Get the Reference of the Player by traversing up the hierarchy
        parent = collectableTextCounter.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the transform of the TextCounter to follow the parent transform with an added offset
        collectableTextCounter.transform.position = parent.transform.position + new Vector3(0, 0.5f, 0);

        // Update the rotation of the TextCounter to always look at the Main Camera
        collectableTextCounter.transform.rotation = Quaternion.LookRotation(cameras.transform.forward, Vector3.up);
    }
}
