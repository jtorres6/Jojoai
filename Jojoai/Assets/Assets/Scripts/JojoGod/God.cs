using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{

    private GameObject _jojo;

    private HFTInput _hftInput;
    private HFTGamepad _gamepad;
    private Rigidbody _rb;
    
    private bool ray_active = false;
    private bool shooting = false;

    public float SPEED;

    private Animator _animRay, _animParticle;
    // Start is called before the first frame update
    void Start()
    {
        
        _jojo = transform.Find("JojoGod").gameObject;
        _hftInput = GetComponent<HFTInput>();
        _gamepad = GetComponent<HFTGamepad>();
        _rb = _jojo.GetComponent<Rigidbody>();
        _animParticle = _jojo.transform.GetChild(3).GetComponent<Animator>();

        Renderer renderer = _jojo.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_BaseColor", _gamepad.color);
    }

    
    private IEnumerator WaitForShoot() {
        
        shooting = true;
        gameObject.GetComponent<CameraShake>().enabled = false;
        gameObject.GetComponent<CameraShake>().shakeDuration = 0.0f;
        yield return new WaitForSeconds(1.46f);
        gameObject.GetComponent<CameraShake>().camTransform = Camera.main.transform;
        gameObject.GetComponent<CameraShake>().shakeDuration = 0.5f;
        gameObject.GetComponent<CameraShake>().enabled = true;
        StartCoroutine(ShootDelay());
        

    }
    private IEnumerator ShootDelay() {
        ray_active = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<CameraShake>().enabled = false;
        gameObject.GetComponent<CameraShake>().shakeDuration = 0.0f;
        ray_active = false;
        shooting = false;

    }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_jojo.transform.position, Vector3.down * 500f, Color.green);

        float dirX = _hftInput.GetAxis("Horizontal");
        _rb.velocity = new Vector3(dirX * SPEED * Time.deltaTime, 0f, 0f);
        
        if (_hftInput.GetButtonDown("fire1"))
        {
            
            if(!shooting)
            {
                _animParticle.SetTrigger("Trigger");
                StartCoroutine(WaitForShoot());

            }
            
            RaycastHit[] hits;
            hits = Physics.RaycastAll(_jojo.transform.position, Vector3.down, 500f);
            if(ray_active){
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
}
