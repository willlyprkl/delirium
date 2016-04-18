using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Logger : MonoBehaviour {

    public static void Lisaa(string a) {
        Text logText = GameObject.Find("logText").GetComponent<Text>();
        logText.text = a + "\n" + logText.text;
    }

}
