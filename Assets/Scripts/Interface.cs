using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Transform pedrasJogador1;
    public Transform pedrasJogador2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePontos()
    {
       
    }

    public void UpdatePedras()
    {
        for (int i = 0; i < GameManager.Instance.pedrasJogador1.Length; i++)
        {
            if (GameManager.Instance.pedrasJogador1[i] != Pedra.Tipos.Nada)
            {
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().sprite = GameManager.Instance.GetPedraSprite(GameManager.Instance.pedrasJogador1[i]);
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            }

            if(GameManager.Instance.pedrasJogador2[i] != Pedra.Tipos.Nada)
            {
                pedrasJogador2.GetChild(i).gameObject.GetComponent<Image>().sprite = GameManager.Instance.GetPedraSprite(GameManager.Instance.pedrasJogador2[i]);
                pedrasJogador2.GetChild(i).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                pedrasJogador2.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
                pedrasJogador2.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
    }
}
