using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Anamenukontrol : MonoBehaviour
{
    public GameObject ilkpanel;
    public GameObject ikincipanel;
    public TMP_InputField kullaniciadi;
    public Text varolankullaniciadi;
    public TextMeshProUGUI[] istatistik;

    void Start()
    {
       
        if (!PlayerPrefs.HasKey("kullaniciadi"))
        {          
            PlayerPrefs.SetInt("Toplam_mac",0);
            PlayerPrefs.SetInt("Maglubiyet",0);
            PlayerPrefs.SetInt("Galibiyet",0);
            PlayerPrefs.SetInt("Toplam_puan",0);
            ilkpanel.SetActive(true);
            DegerleriYaz();
        }
        else
        {
            ikincipanel.SetActive(true);
            varolankullaniciadi.text = PlayerPrefs.GetString("kullaniciadi");
            DegerleriYaz();
        }
    }

    public void kullaniciadikaydet()
    {
        PlayerPrefs.SetString("kullaniciadi", kullaniciadi.text);
        ilkpanel.SetActive(false);
        ikincipanel.SetActive(true);
        varolankullaniciadi.text = PlayerPrefs.GetString("kullaniciadi");
       GameObject.FindWithTag("Randomgirisbuton").GetComponent<Button>().interactable = true; 
        GameObject.FindWithTag("Odakur").GetComponent<Button>().interactable = true;    
    }
    void DegerleriYaz()
    {
        istatistik[0].text = PlayerPrefs.GetInt("Toplam_mac").ToString();
        istatistik[1].text = PlayerPrefs.GetInt("Maglubiyet").ToString();
        istatistik[2].text = PlayerPrefs.GetInt("Galibiyet").ToString();
        istatistik[3].text = PlayerPrefs.GetInt("Toplam_puan").ToString();
    }
}
