using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{

    private GameObject _jojo;

    private HFTInput _hftInput;
    private HFTGamepad _gamepad;
    private Rigidbody _rb;

    public float SPEED;

    // Start is called before the first frame update
    void Start()
    {
        _jojo = transform.Find("JojoGod").gameObject;
        _hftInput = GetComponent<HFTInput>();
        _gamepad = GetComponent<HFTGamepad>();
        _rb = _jojo.GetComponent<Rigidbody>();

        Renderer renderer = _jojo.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_BaseColor", _gamepad.color);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_jojo.transform.position, Vector3.down * 500f, Color.green);

        float dirX = _hftInput.GetAxis("Horizontal");
        _rb.velocity = new Vector3(dirX * SPEED * Time.deltaTime, 0f, 0f);

        if (Input.GetKeyDown("fire1"))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(_jojo.transform.position, Vector3.down, 500f);

            for (int i = 0; i < hits.Length; i++)
            {
                GameObject obj = hits[i].transform.gameObject;

                if (obj.tag == "Player")
                {
                    obj.transform.parent.GetComponent<Player>().Dead();
                }                
            }
        }
    }
}
