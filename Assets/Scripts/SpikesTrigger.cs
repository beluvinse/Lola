using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        GetComponentInChildren<Animation>().Play();
        yield return new WaitForSeconds(1);
    }
}
