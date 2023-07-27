using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Odul : MonoBehaviour
{
    PhotonView pw;
    void Start()
    {
        pw = GetComponent<PhotonView>();
        StartCoroutine(yokol());
    }

IEnumerator yokol()
    {
        yield return new WaitForSeconds(5);
        if (pw.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
      
    }
}
