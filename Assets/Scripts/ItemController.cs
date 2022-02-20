using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    GameManager gm;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    bool triger = true;

    void Update()
    {
        if (triger && gm.step >= 2)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().isTrigger = true;
            triger = false;
        }
    }

    public void Point()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().isTrigger = false;

        // ‰¹’Ç‰Á


        if (gm.GetPlayerName() == "Player_1")
        {
            transform.position = new Vector3(-5, 10, 8);
            GameObject.Find("Pt_1").GetComponent<Counter>().Point();
        }
        else
        {
            transform.position = new Vector3(19, 10, 8);
            GameObject.Find("Pt_2").GetComponent<Counter>().Point();
        }
    }
}
