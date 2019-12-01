using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMoais : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer==9)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-8, 8)*1000, Random.Range(-8, 8)*1300, 0));
        }

    }
}
