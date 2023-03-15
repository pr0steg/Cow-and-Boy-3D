using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    private Rigidbody rb;
    public Rigidbody Rb => rb;

    public bool TimerOn = false;
    public float TimeToDestroy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        TimerOn = true;
        StartCoroutine(SelfDestruct(TimeToDestroy));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wolf")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator SelfDestruct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
