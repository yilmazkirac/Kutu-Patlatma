using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncu : MonoBehaviour
{
    public GameObject Atesnoktasi;
    public GameObject Top;
    public ParticleSystem TopAtisEfect;
    public AudioSource TopAtmaSesi;
    float atisyonu;

    [Header("GucBari")]
     Image powerbar;
    float powersayi;
    bool sonageldimi = false;
    Coroutine powerdongu;
    PhotonView pw;
    void Start()
    {

        pw = GetComponent<PhotonView>();
      
      
        if (pw.IsMine)
        {
         
            powerbar = GameObject.FindWithTag("powerbar").GetComponent<Image>();
            // GetComponent<Oyuncu>().enabled = true;
            if (PhotonNetwork.IsMasterClient)
            {
         gameObject.tag = "Oyuncu_1";
                transform.position = GameObject.FindWithTag("OlusacakNokta1").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta1").transform.rotation;
                atisyonu = 2f;
            }
            else
            {
       gameObject.tag = "Oyuncu_2";
                transform.position = GameObject.FindWithTag("OlusacakNokta2").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta2").transform.rotation;
                atisyonu = -2f;
            }
        }
        InvokeRepeating("Oyunbasladimi", 0, .5f);

    }
    public void Oyunbasladimi()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (pw.IsMine)
            {

                powerdongu = StartCoroutine(powerbarx());
                CancelInvoke("Oyunbasladimi");

            }
            else
            {
                StopAllCoroutines();
            }
         
        }

    }
    IEnumerator powerbarx()
    {
        powerbar.fillAmount = 0;
        sonageldimi = false;
        while (true)
        {

            if (powerbar.fillAmount < 1 && !sonageldimi)
            {
                powersayi = 0.01f;
                powerbar.fillAmount += powersayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
            else
            {
                sonageldimi = true;
                powersayi = 0.01f;
                powerbar.fillAmount -= powersayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
                if (powerbar.fillAmount == 0)
                {
                    sonageldimi = false;
                }
            }
        }
    }
    private void Update()
    {
        if (pw.IsMine)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
            
                PhotonNetwork.Instantiate("TopCikis", Atesnoktasi.transform.position, Atesnoktasi.transform.rotation, 0, null);
                TopAtmaSesi.Play();             
                GameObject topobjesi = PhotonNetwork.Instantiate("Top", Atesnoktasi.transform.position, Atesnoktasi.transform.rotation, 0, null);
                
                topobjesi.GetComponent<PhotonView>().RPC("Tagaktar", RpcTarget.All, gameObject.tag);
                Rigidbody2D rb = topobjesi.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(atisyonu, 0f) * powerbar.fillAmount * 30f, ForceMode2D.Impulse);
                StopCoroutine(powerdongu);

            }



        }

    }

    public void sonuc(int deger)
    {
        if (pw.IsMine) 
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (deger==1)
                {

                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
                    PlayerPrefs.SetInt("Toplam_puan", PlayerPrefs.GetInt("Toplam_puan") + 150);
                }
                else
                {
                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);
                }
            }
            else
            {

                if (deger == 2)
                {

                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
                    PlayerPrefs.SetInt("Toplam_puan", PlayerPrefs.GetInt("Toplam_puan") + 150);
                }
                else
                {
                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);
                }


            }
        }
        
    }
    public void poweroynasin()
    {

        powerdongu = StartCoroutine(powerbarx());

    }
}
