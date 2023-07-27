using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ortadaki_kutu : MonoBehaviour
{
    float Healt = 100;
    public Image Healtbar;
    public GameObject HealCanvas;
    GameObject gamekontrol;
    PhotonView pw;
    AudioSource Kutuyokolmasesi;
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        Kutuyokolmasesi=GetComponent<AudioSource>();
        gamekontrol = GameObject.FindWithTag("GameKontrol");
    }
    [PunRPC]
    public void DarbeAl(float Darbegucu)
    {
        if (pw.IsMine)
        {
            Healt -= Darbegucu;
        Healtbar.fillAmount = Healt / 100;


        if (Healt <= 0)
        {
         
                PhotonNetwork.Instantiate("boom", transform.position, transform.rotation, 0, null);
                Kutuyokolmasesi.Play();
                PhotonNetwork.Destroy(gameObject);
           
        }
        else
            StartCoroutine(CanvasCikar());
        }

    }


    IEnumerator CanvasCikar()
    {
        if (!HealCanvas.activeInHierarchy)
        {
            HealCanvas.SetActive(true);
            yield return new WaitForSeconds(2);
            HealCanvas.SetActive(false);
        }
    }
}
