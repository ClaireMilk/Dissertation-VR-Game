using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKB;
using UnityEngine.SceneManagement;

public class DataContainer : MonoBehaviour
{
    public string savedPlayerName;
    public KeyboardBehaviour keyboard;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SavePlayerName()
    {
        savedPlayerName = keyboard.Text;
        SceneManager.LoadScene(1);
    }
}
