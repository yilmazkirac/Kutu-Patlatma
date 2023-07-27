using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    float Darbegucu;
    public ParticleSystem TopYokolmaEfect;
    public AudioSource Topyokolma;

    GameObject gamekontrol;
    GameObject Oyuncu;
    PhotonView pw;
     AudioSource Topyokolmases;
    int benkimim;

    void Start()
    {
        pw = GetComponent<PhotonView>();
        Darbegucu = 20;
        gamekontrol = GameObject.FindWithTag("GameKontrol");
        Topyokolmases=GetComponent<AudioSource>();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ortadaki_Kutu"))
        {
            collision.gameObject.GetComponent<PhotonView>().RPC("DarbeAl", RpcTarget.All, Darbegucu);
       
            Oyuncu.GetComponent<Oyuncu>().poweroynasin();
                     
                PhotonNetwork.Instantiate("Kutu_kýrýlma", collision.transform.position, collision.transform.rotation, 0, null);
                Topyokolmases.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);
           
    
           
        }
        if (collision.gameObject.CompareTag("Oyuncu_2_Kule") ||collision.gameObject.CompareTag("Oyuncu_2"))
        {
            Debug.Log(collision.gameObject.tag);
            gamekontrol.GetComponent<PhotonView>().RPC("Darbe_ver", RpcTarget.All,2, Darbegucu);

            Oyuncu.GetComponent<Oyuncu>().poweroynasin();
           
                PhotonNetwork.Instantiate("Kutu_kýrýlma", collision.transform.position, collision.transform.rotation, 0, null);
                Topyokolmases.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);
          
        }
        if (collision.gameObject.CompareTag("Oyuncu_1_Kule") || collision.gameObject.CompareTag("Oyuncu_1"))
        {
            Debug.Log(collision.gameObject.tag);
            gamekontrol.GetComponent<PhotonView>().RPC("Darbe_ver", RpcTarget.All, 1, Darbegucu);

            Oyuncu.GetComponent<Oyuncu>().poweroynasin();
 
                PhotonNetwork.Instantiate("Kutu_kýrýlma", collision.transform.position, collision.transform.rotation, 0, null);
                Topyokolmases.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);
           

        }
        if (collision.gameObject.CompareTag("Zemin"))
        {          
            Oyuncu.GetComponent<Oyuncu>().poweroynasin();
     
                PhotonNetwork.Instantiate("Kutu_kýrýlma",transform.position, transform.rotation, 0, null);
                Topyokolmases.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);
           

        }
        if (collision.gameObject.CompareTag("Tahta"))
        {
            Oyuncu.GetComponent<Oyuncu>().poweroynasin();
    
                PhotonNetwork.Instantiate("Kutu_kýrýlma", transform.position, transform.rotation, 0, null);
            Topyokolmases.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);


        }
        if (collision.gameObject.CompareTag("Odul"))
        {
            gamekontrol.GetComponent<PhotonView>().RPC("CanDoldur", RpcTarget.All, benkimim);
            Oyuncu.GetComponent<Oyuncu>().poweroynasin();
            PhotonNetwork.Destroy(collision.gameObject);
            PhotonNetwork.Instantiate("Kutu_kýrýlma", transform.position, transform.rotation, 0, null);
            Topyokolmases.Play();
            if (pw.IsMine)             
            PhotonNetwork.Destroy(gameObject);

        }
    }
    [PunRPC]
 public void Tagaktar(string gelentag)
    {      
            Oyuncu = GameObject.FindWithTag(gelentag);
        if (gelentag=="Oyuncu_1")
            benkimim = 1;
        
        else
            benkimim =2;
    }
}
