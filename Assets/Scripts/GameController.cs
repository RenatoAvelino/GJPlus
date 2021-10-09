using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] _playerScore = new int[2];
    void Start()
    {
        _playerScore[0] = 0;
        _playerScore[1] = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScoreUp(int index)
    {
        if (index >= 2)
        {
            Debug.LogError("Invalid Index");
            return;
        }
        _playerScore[index]++;
        //Debug.Log("Player " + index + " Pontuou");
        //Debug.Log(_playerScore[index]);
    }
    public void SetScoreDown(int index)
    {
        if (index >= 2)
        {
            Debug.LogError("Invalid Index");
            return;
        }
        _playerScore[index]--;

    }
}
