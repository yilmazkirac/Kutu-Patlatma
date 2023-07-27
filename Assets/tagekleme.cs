using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagekleme : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.gameObject.tag = "Oyuncu_2";
    }
}
