using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using namespace1;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Button btnKaybet;
    public UnityEngine.UI.Text level;
    public int AnlikKarakterSayisi = 1;
    public GameObject VarisNoktasi;
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfektleri;
    [Header("LEVEL VERÝLERÝ")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject _AnaKarakter;
    public bool OyunBittimi;
    bool SonaGeldikmi;
    // Start is called before the first frame update
    void Start()
    {
        DusmanlariOlustur();
    }
    public void DusmanlariOlustur()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }
    public void DusmanlariTetikle()
    {
        foreach (var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SonaGeldikmi = true;
        SavasDurumu();
    }
    // Update is called once per frame
    void Update()
    {

    }
    void SavasDurumu()
    {
        if (SonaGeldikmi)
        {
            if (AnlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittimi = true;
                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                foreach (var item in Karakterler)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);
                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun)
                {
                    Debug.Log("kaybettin");
                    btnKaybet.gameObject.SetActive(true);

                }
                else
                {
                    Debug.Log("kazandýn");
                    btn.gameObject.SetActive(true);
                }
            }
        }
    }
    public void AdamYonetim(string islemturu, int GelenSayi, Transform Pozisyon)
    {
        switch (islemturu)
        {
            case "Carpma":
                {
                    int DonguSayisi = (AnlikKarakterSayisi * GelenSayi) - AnlikKarakterSayisi;
                    int sayi = 0;
                    foreach (var item in Karakterler)
                    {
                        if (sayi < DonguSayisi)
                        {
                            if (!item.activeInHierarchy)
                            {
                                foreach (var item2 in OlusmaEfektleri)
                                {
                                    if (!item2.activeInHierarchy)
                                    {
                                        item2.SetActive(true);
                                        item2.transform.position = Pozisyon.position;
                                        item2.GetComponent<ParticleSystem>().Play();
                                        break;
                                    }
                                }
                                item.transform.position = Pozisyon.position;
                                item.SetActive(true);
                                sayi++;
                            }
                        }
                        else
                        {
                            sayi = 0;
                            break;
                        }
                    }
                    AnlikKarakterSayisi *= GelenSayi;
                }
                //Matematiksel_islemler.Carpma(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;
            case "Toplama":
                {
                    int sayi2 = 0;
                    foreach (var item in Karakterler)
                    {
                        if (sayi2 < GelenSayi)
                        {
                            if (!item.activeInHierarchy)
                            {
                                foreach (var item2 in OlusmaEfektleri)
                                {
                                    if (!item2.activeInHierarchy)
                                    {
                                        item2.SetActive(true);
                                        item2.transform.position = Pozisyon.position;
                                        item2.GetComponent<ParticleSystem>().Play();
                                        break;
                                    }
                                }
                                item.transform.position = Pozisyon.position;
                                item.SetActive(true);
                                sayi2++;
                            }
                        }
                        else
                        {
                            sayi2 = 0;
                            break;
                        }
                    }
                    AnlikKarakterSayisi += GelenSayi;
                }
                //Matematiksel_islemler.Toplama(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;
            case "Cikartma":
                { 
                if (AnlikKarakterSayisi < GelenSayi)
                {
                    foreach (var item in Karakterler)
                    {
                        foreach (var item2 in YokOlmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = item.transform.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                break;
                            }
                        }
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                    AnlikKarakterSayisi = 1;
                }
                else
                {
                    int sayi3 = 0;
                    foreach (var item in Karakterler)
                    {
                        if (sayi3 != GelenSayi)
                        {
                            if (item.activeInHierarchy)
                            {
                                foreach (var item2 in YokOlmaEfektleri)
                                {
                                    if (!item2.activeInHierarchy)
                                    {
                                        Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                                        item2.SetActive(true);
                                        item2.transform.position = yeniPoz;
                                        item2.GetComponent<ParticleSystem>().Play();
                                        break;
                                    }
                                }
                                item.transform.position = Vector3.zero;
                                item.SetActive(false);
                                sayi3++;
                            }
                        }
                        else
                        {
                            sayi3 = 0;
                            break;
                        }
                    }
                    AnlikKarakterSayisi -= 4;
                }
        }
        //Matematiksel_islemler.Cikartma(GelenSayi, Karakterler, YokOlmaEfektleri);
        break;
            case "Bolme":
                {
                    if (AnlikKarakterSayisi <= GelenSayi)
                    {
                        foreach (var item in Karakterler)
                        {
                            foreach (var item2 in YokOlmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                                    item2.SetActive(true);
                                    item2.transform.position = yeniPoz;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                        }
                        AnlikKarakterSayisi = 1;
                    }
                    else
                    {
                        int bolen = AnlikKarakterSayisi / GelenSayi;
                        int sayi3 = 0;
                        foreach (var item in Karakterler)
                        {
                            if (sayi3 != bolen)
                            {
                                if (item.activeInHierarchy)
                                {
                                    foreach (var item2 in YokOlmaEfektleri)
                                    {
                                        if (!item2.activeInHierarchy)
                                        {
                                            Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                                            item2.SetActive(true);
                                            item2.transform.position = yeniPoz;
                                            item2.GetComponent<ParticleSystem>().Play();
                                            break;
                                        }
                                    }
                                    item.transform.position = Vector3.zero;
                                    item.SetActive(false);
                                    sayi3++;
                                }
                            }
                            else
                            {
                                sayi3 = 0;
                                break;
                            }
                        }
                        if (AnlikKarakterSayisi % GelenSayi == 0)
                        {
                            AnlikKarakterSayisi /= GelenSayi;
                        }
                        else if (AnlikKarakterSayisi % GelenSayi == 1)
                        {
                            AnlikKarakterSayisi /= GelenSayi;
                            AnlikKarakterSayisi++;
                        }
                        else if (AnlikKarakterSayisi % GelenSayi == 2)
                        {
                            AnlikKarakterSayisi /= GelenSayi;
                            AnlikKarakterSayisi += 2;
                        }
                    }
                }
                //Matematiksel_islemler.Bolme(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;
        }
    }
    public void YokOlmaEfektiOlustur(Vector3 Pozisyon, bool Balyoz = false, bool Durum = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                if (!Durum)
                {
                    AnlikKarakterSayisi--;
                }
                else
                {
                    KacDusmanOlsun--;
                }
                break;
            }
        }
        if (Balyoz)
        {
            Vector3 yeniPoz = new Vector3(Pozisyon.x, 0.005f, Pozisyon.z);
            foreach (var item in AdamLekesiEfektleri)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = yeniPoz;
                    break;
                }
            }
        }
        if (!OyunBittimi)
        {
            SavasDurumu();
        }
    }

    public void sahne()
    {
        SceneManager.LoadScene(level.text);
    }
    public void sahneKaybet()
    {
        SceneManager.LoadScene("level 1");
    }
}
