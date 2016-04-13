using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public GameObject player;

    protected GameManager gm;
    private Button wButton;
    private Button eButton;
    private Button sButton;
    private Button nButton;


	void Start () {
        wButton = (Button)GameObject.Find("wButton").GetComponent<Button>();
        eButton = (Button)GameObject.Find("eButton").GetComponent<Button>();
        sButton = (Button)GameObject.Find("sButton").GetComponent<Button>();
        nButton = (Button)GameObject.Find("nButton").GetComponent<Button>();

        wButton.onClick.AddListener(() => (Liiku(-1, "ho")));
        eButton.onClick.AddListener(() => (Liiku(1, "ho"))); 
        sButton.onClick.AddListener(() => (Liiku(-1, "ve")));
        nButton.onClick.AddListener(() => (Liiku(1, "ve")));

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

	}
	
    public void LiikuEn(int a, string b, Enemy enemy) {
        if (b == "ho") {
            Vector2 startpos = enemy.transform.position;
            Vector2 endpos = startpos + new Vector2(a, 0);
            enemy.transform.position = endpos;
        } else {
            Vector2 startpos = enemy.transform.position;
            Vector2 endpos = startpos + new Vector2(0, a);
            enemy.transform.position = endpos;
        }
    }

    public void Liiku(int a, string b) {
        if (b == "ho") {
            Vector2 startpos = player.transform.position;
            Vector2 endpos = startpos + new Vector2(a, 0);
            player.transform.position = endpos; 
        } else {
            Vector2 startpos = player.transform.position;
            Vector2 endpos = startpos + new Vector2(0, a);
            player.transform.position = endpos;
        }

        gm.playerTurn = false;
    }
}
