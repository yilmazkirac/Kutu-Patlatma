using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameKonrtol : MonoBehaviour
{

    [Header("OYUNCU AYARLARI")]
   public Image Oyuncu_1_healt_bar;
    public Image Oyuncu_2_healt_bar;
    float oyuncu_1_healt=100;
    float oyuncu_2_healt=100;

    PhotonView pw;
     bool basladikmi;
    int limit;
    int olusturmasayisi;
    public GameObject[] Noktalar;

    GameObject oyuncu1;
    GameObject oyuncu2;

    bool oyunbittimi=false;


    private void Start()
    {
        pw=GetComponent<PhotonView>();
        basladikmi=false;
        olusturmasayisi=0;
        limit = 5;
    }

    IEnumerator OlusturmayaBasla()
    {
     
        while (true&& basladikmi)
        {               
            if (limit== olusturmasayisi)
            {
                basladikmi=false ;
            }
            yield return new WaitForSeconds(5);
            int i = Random.Range(0, 6);
            PhotonNetwork.Instantiate("Odul", Noktalar[i].transform.position, Noktalar[i].transform.rotation, 0, null);
            olusturmasayisi++;
        }
    }
    [PunRPC]
    public void Basla()
    {
        if (PhotonNetwork.IsMasterClient)   
            basladikmi=true;
            StartCoroutine(OlusturmayaBasla());



    }
    [PunRPC]
    public void Darbe_ver(int kriter,float darbegucu)
    {
        if (kriter==1)
        {
            oyuncu_1_healt -= darbegucu;
            Oyuncu_1_healt_bar.fillAmount = oyuncu_1_healt / 100;
            if (oyuncu_1_healt <= 0)
            {
                foreach (GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject))as GameObject[])
                {
                    if (objem.gameObject.CompareTag("Oyunsonupanel"))
                    {
                        objem.gameObject.SetActive(true);
                        GameObject.FindWithTag("Oyunsonubilgi").GetComponent<TextMeshProUGUI>().text = "2.Oyuncu galip";
                    }
                }
                kazanan(2);
                /*  oyuncu1 = GameObject.FindWithTag("Oyuncu_1");
                  oyuncu2 = GameObject.FindWithTag("Oyuncu_2");
                  oyuncu1.GetComponent<PhotonView>().RPC("maglup", RpcTarget.All);
                  oyuncu2.GetComponent<PhotonView>().RPC("galip", RpcTarget.All);*/

            }
        }
        else if (kriter==2)
        {
            oyuncu_2_healt -= darbegucu;
            Oyuncu_2_healt_bar.fillAmount = oyuncu_2_healt / 100;
            if (oyuncu_2_healt <= 0)
            {
                foreach (GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (objem.gameObject.CompareTag("Oyunsonupanel"))
                    {
                        objem.gameObject.SetActive(true);
                        GameObject.FindWithTag("Oyunsonubilgi").GetComponent<TextMeshProUGUI>().text = "1.Oyuncu galip";
                    }
                }
                kazanan(1);
              /*  oyuncu1 = GameObject.FindWithTag("Oyuncu_1");
                oyuncu2 = GameObject.FindWithTag("Oyuncu_2");
                oyuncu2.GetComponent<PhotonView>().RPC("maglup", RpcTarget.All);
                oyuncu1.GetComponent<PhotonView>().RPC("galip", RpcTarget.All);*/

            }
        }
        /* switch (kriter)
         {
            case 1:
                 oyuncu_1_healt -= darbegucu;
                 Oyuncu_1_healt_bar.fillAmount = oyuncu_1_healt / 100;
                 if (oyuncu_1_healt <= 0)
                 {

                     Debug.Log("1yewnildi");
                 }
                 break;

             case 2:
                 oyuncu_2_healt -= darbegucu;
                 Oyuncu_2_healt_bar.fillAmount = oyuncu_2_healt / 100;
                 if (oyuncu_2_healt <= 0)
                 {

                     Debug.Log("2yewnildi");
                 }
                 break;
         }*/
    
    }
    public void anamenu()
    {
        SceneManager.LoadScene(0);
    }
    void kazanan(int deger)
    {
        if (!oyunbittimi)
        {
            GameObject.FindWithTag("Oyuncu_1").GetComponent<Oyuncu>().sonuc(deger);
            GameObject.FindWithTag("Oyuncu_2").GetComponent<Oyuncu>().sonuc(deger);
            oyunbittimi=true;
        }
       
    }
    [PunRPC]
    public void CanDoldur(int hangioyuncu)
    {
        switch (hangioyuncu)
        {
            case 1:
                oyuncu_1_healt += 30;
                Oyuncu_1_healt_bar.fillAmount = oyuncu_1_healt / 100;
                if (oyuncu_1_healt >= 100)
                {

                    oyuncu_1_healt = 100;
                    Oyuncu_1_healt_bar.fillAmount = oyuncu_1_healt / 100;
                }
                break;
            case 2:
                oyuncu_2_healt += 30;
                Oyuncu_2_healt_bar.fillAmount = oyuncu_2_healt / 100;
                if (oyuncu_2_healt >= 100)
                {

                    oyuncu_2_healt = 100;
                    Oyuncu_2_healt_bar.fillAmount = oyuncu_2_healt / 100;
                }
                break;
        }
    }
}
