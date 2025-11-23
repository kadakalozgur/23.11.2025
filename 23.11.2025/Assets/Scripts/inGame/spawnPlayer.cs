using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu script karkaterin enrde spawn oclaðýný belirlemek içindir.
//Örneðin oyuncu evden çýktýðýdna evin kapýsýnýn önüde spawn olamlý köyün ortasýnda deðil.

public class spawnPlayer : MonoBehaviour
{

    public static string wherePlayer; //Diðer scriptlerden eriþirken hata almamak için static tanýmladýk.Yoksa hata veriyor.

    void Start()
    {

        if(wherePlayer == "outHouse")
        {

            transform.position = new Vector2(406, 203);

        }

        else if (wherePlayer == "outBar")
        {

            transform.position = new Vector2(405, 183);

        }

    }

}
