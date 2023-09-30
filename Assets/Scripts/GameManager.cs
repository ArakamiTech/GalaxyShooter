using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool _gameOver = true;
    [SerializeField]
    private GameObject player;
    private UIManager _uIManager;

    public void Start(){
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void Update()
    {
        if (_gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                _gameOver = false;
                _uIManager.HideTitleScreen();
            }
        }
    }
}
