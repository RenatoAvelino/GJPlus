using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] _playerScore = new int[2];

    public Interface interfaceManager;
    void Start()
    {
        _playerScore[0] = 0;
        _playerScore[1] = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScore(int index, int score)
    {
        if (index >= 2)
        {
            Debug.LogError("Invalid Index");
            return;
        }
        _playerScore[index] += score;

        interfaceManager.UpdateScore(_playerScore[0], _playerScore[1]);
        //Debug.Log("Player " + index + " Pontuou");
        //Debug.Log(_playerScore[index]);
    }
}
