using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject Collectable;

    void Start()
    {
        Collectable = this.gameObject;
    }

    // Update is called once per frame
    void Update() //continues rotation
    {
        Collectable.transform.rotation = Quaternion.Euler(Collectable.transform.eulerAngles + new Vector3(0, 0.1f, 0));
    }

    private void OnTriggerEnter()
    {
        AudioScript.Instance.PlayerEffect(AudioScript.Effects.Pickup);
        HUDScript.Instance.ObjectCollected();
        this.gameObject.SetActive(false);
    }
}
