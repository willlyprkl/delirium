using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Logger : MonoBehaviour {
    // Combat-logi
    public static void Lisaa(string a) {
        Text logText = GameObject.Find("logText").GetComponent<Text>();

        if (logText.text.Length > 100) {
            logText.text.Remove(100);
        }

        logText.text = a + "\n" + logText.text;
        //Debug.Log(logText.text);
    }

}
