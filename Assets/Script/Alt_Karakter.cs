using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alt_Karakter : MonoBehaviour
{
    GameObject Target;
    NavMeshAgent _Navmesh;
    public GameManager _GameManager;
    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();
        Target = _GameManager.VarisNoktasi;
    }
    private void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);
    }
    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {  
        if (other.CompareTag("igneliKutu"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Testere"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("PervaneIgneler"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Balyoz"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), true);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Dusman"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), false,false);
            gameObject.SetActive(false);
        }
    }
}
