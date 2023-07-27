using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class SunucuYönetim : MonoBehaviourPunCallbacks
{
    GameObject serverbilgi;
    GameObject adkaydet;
    GameObject randomgiris;
    GameObject odakur;
  
    void Start()
    {
      
        adkaydet= GameObject.FindWithTag("Adkaydetbuton");
        randomgiris = GameObject.FindWithTag("Randomgirisbuton");
        odakur = GameObject.FindWithTag("Odakur");
        serverbilgi = GameObject.FindWithTag("Serverbilgi");
      
        
        
        
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }
    public override void OnConnectedToMaster()
    {
        serverbilgi.gameObject.GetComponent<Text>().text = "Servere Bağlandı";
    
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        serverbilgi.gameObject.GetComponent<Text>().text = "Lobbye Bağlandı";
        if (!PlayerPrefs.HasKey("kullaniciadi"))
        {
            adkaydet.GetComponent<Button>().interactable = true;
        }
        else
        {
            randomgiris.GetComponent<Button>().interactable = true;
            odakur.GetComponent<Button>().interactable = true;
        }
      
    }
    public void RandomGiris()
    {
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinRandomRoom();
    }
    public void Odakur()
    {
        PhotonNetwork.LoadLevel(1);
        string odaadi = Random.Range(0, 3214).ToString();
        PhotonNetwork.JoinOrCreateRoom(odaadi, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        InvokeRepeating("BilgileriKontrolEt", 0, 1f);
        GameObject objem = PhotonNetwork.Instantiate("Oyuncu",Vector3.zero,Quaternion.identity,0,null);
        objem.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("kullaniciadi");
        if (PhotonNetwork.PlayerList.Length==2)
        {
            objem.gameObject.tag = "Oyuncu_2";
            GameObject.FindWithTag("GameKontrol").gameObject.GetComponent<PhotonView>().RPC("Basla", RpcTarget.All);
         
        }
     

    }
    public override void OnLeftRoom()
    {
      /*  PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac")+1);
        PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet")+1);   */
    }
    public override void OnLeftLobby()
    {
       
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
      

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
      /*  PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
        PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1); 
        PlayerPrefs.SetInt("Toplam_puan", PlayerPrefs.GetInt("Toplam_puan") + 150); */
        InvokeRepeating("BilgileriKontrolEt", 0, 1f);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        serverbilgi.gameObject.GetComponent<Text>().text = "Odaya Girilemedi";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        serverbilgi.gameObject.GetComponent<Text>().text = "Oda Oluşturulamadı";
    }
  void BilgileriKontrolEt()
    {
        if (PhotonNetwork.PlayerList.Length==2)
        {
            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(false);
            GameObject.FindWithTag("Oyuncu1isim").GetComponent<TextMeshProUGUI>().text=PhotonNetwork.PlayerList[0].ToString();
            GameObject.FindWithTag("Oyuncu2isim").GetComponent<TextMeshProUGUI>().text=PhotonNetwork.PlayerList[1].ToString();
            CancelInvoke("BilgileriKontrolEt");
        }
        else
        {
            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(true);
            GameObject.FindWithTag("Oyuncu1isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].ToString();
            GameObject.FindWithTag("Oyuncu2isim").GetComponent<TextMeshProUGUI>().text = "...";
        }
      
    }


}
