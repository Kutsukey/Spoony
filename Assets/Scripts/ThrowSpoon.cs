using System.Collections.Generic;
using UnityEngine;

public class ThrowSpoon : MonoBehaviour
{
    [SerializeField] float force = 100;
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
        Destroy(gameObject, 7);
    }


}