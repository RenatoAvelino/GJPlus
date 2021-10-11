using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interface : MonoBehaviour
{
    public Transform pedrasJogador1;
    public Transform pedrasJogador2;

    public TMP_Text pontosJogador1;
    public TMP_Text pontosJogador2;

    void Start()
    {
        pontosJogador1.text = 0.ToString();
        pontosJogador2.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int score1, int score2)
    {
        pontosJogador1.text = score1.ToString();
        pontosJogador2.text = score2.ToString();
    }

    public void UpdatePedras()
    {
        for (int i = 0; i < GameManager.Instance.Pedras1.Length; i++)
        {
            if (GameManager.Instance.Pedras1[i].tipo != Pedra.Tipos.Nada)
            {
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().sprite = GameManager.Instance.GetPedraSprite(GameManager.Instance.Pedras1[i].tipo);
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
                pedrasJogador1.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
            }

            if(GameManager.Instance.Pedras2[i].tipo != Pedra.Tipos.Nada)
            {
                pedrasJogador2.GetChild(i).gameObject.GetComponent<Image>().sprite = GameManager.Instance.GetPedraSprite(GameManager.Instance.Pedras2[i].tipo);
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
