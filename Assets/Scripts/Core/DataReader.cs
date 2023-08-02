using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("AfterStart", 1);
    }
    void AfterStart()
    {
        text = gameObject.GetComponent<TextMeshPro>();
        DataContainer nameToRead = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DataContainer>();
        text.text = nameToRead.savedPlayerName;
    }
}