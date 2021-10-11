using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainCanvas;
    public GameObject InstrCanvas;
    public GameObject CreditCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPlay()
    {
        SceneManager.LoadScene("Scene01", LoadSceneMode.Single);
    }

    public void ButtonInst()
    {
        MainCanvas.SetActive(false);
        InstrCanvas.SetActive(true);

    }
    public void ButtonCredits()
    {
        MainCanvas.SetActive(false);
        CreditCanvas.SetActive(true);
    }
    public void ButtonExit()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        MainCanvas.SetActive(true);
        InstrCanvas.SetActive(false);
        CreditCanvas.SetActive(false);
    }
}
