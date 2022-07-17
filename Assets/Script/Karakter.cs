using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public GameObject _Kamera;
    public bool SonaGeldikmi;
    public GameObject Gidecegiyer;
    float ParmakPozX;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        if (!SonaGeldikmi)
        {
            transform.Translate(Vector3.forward * .5f * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SonaGeldikmi)
        {
            transform.position = Vector3.Lerp(transform.position, Gidecegiyer.transform.position, .015f);
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch parmak = Input.GetTouch(0);
                if (parmak.deltaPosition.x > 25f)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                }
                if (parmak.deltaPosition.x < -25f)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                }
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Toplama") || other.CompareTag("Cikartma") || other.CompareTag("Bolme"))
        {
            int sayi = int.Parse(other.name);
            _GameManager.AdamYonetim(other.tag, sayi, other.transform);
        }
        else if (other.CompareTag("Sontetikleyici"))
        {
            _Kamera.GetComponent<Kamera>().SonaGeldikmi = true;
            _GameManager.DusmanlariTetikle();
            SonaGeldikmi = true;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direk"))
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
            }

        }
    }
}
