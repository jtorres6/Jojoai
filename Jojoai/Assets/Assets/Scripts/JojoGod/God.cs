using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    private HFTInput _hftInput;
    private HFTGamepad _gamepad;
    private Rigidbody _rb;

    public float SPEED;

    // Start is called before the first frame update
    void Start()
    {
        _hftInput = GetComponent<HFTInput>();
        _gamepad = GetComponent<HFTGamepad>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
                    Debug.DrawRay(transform.position, Vector3.down * 500f, Color.green);

        float dirX = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector3(dirX * SPEED * Time.deltaTime, 0f, 0f);

        if (Input.GetKeyDown("space"))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, Vector3.down, 500f);

            for (int i = 0; i < hits.Length; i++)
            {
                GameObject obj = hits[i].transform.gameObject;

                if (obj.tag == "Player")
                {
                    obj.GetComponent<Player>().Dead();
                }                
            }
        }
    }
}
