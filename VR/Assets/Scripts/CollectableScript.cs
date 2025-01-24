using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{

    // Reference of the Collectable
    private GameObject collectable;

    // Start is called before the first frame update
    void Start()
    {
        collectable = this.gameObject;
    }

    // Update is called once per frame
    void Update() //continues rotation
    {
        // Make the Collectable Spin on himself, i used localRotation because the Collecatbles have a parent that change rotation
        collectable.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0.1f, 0));
    }

    private void OnTriggerEnter()
    {
        //Play the PickUp Sound Effect
        AudioScript.Instance.PlayEffect(AudioScript.Effects.Pickup);

        // Update the Points and the Collectable Counter
        HUDScript.Instance.ObjectCollected();

        // Hide the Collectable without destroying the GameObject, because reusing it is less expensive than creating a new one.
        this.gameObject.SetActive(false);
    }
}
