using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private BoxCollider2D boxc;
    private GameManager gm;

	void Start () {
         boxc = GetComponent<BoxCollider2D>();
	}
	
    protected void LiikuEn(int a, string b, Enemy enemy) {
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

    protected void Liiku(int a, string b, Player player) {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        RaycastHit2D hits;
        Vector2 startpos = player.transform.position;
        Vector2 endpos = player.transform.position;

        if (b == "ho") {
            endpos = startpos + new Vector2(a, 0);
        } else {
            endpos = startpos + new Vector2(0, a);
        }

        hits = Physics2D.Linecast(startpos, endpos);

        if (hits.transform == null) {
            player.transform.position = endpos;
        } else if (hits.transform.tag == "enemy") {
            hits.transform.gameObject.SetActive(false);
            player.transform.position = endpos;
        } else if (hits.transform.tag == "juoma") {
            hits.transform.gameObject.SetActive(false);
            player.transform.position = endpos;
        } else if (hits.transform.tag == "ruoka") {
            hits.transform.gameObject.SetActive(false);
            player.health(10);
            player.transform.position = endpos;
        } else if (hits.transform.tag == "puut") {
            player.transform.position = endpos;
        }

        gm.playerTurn = false;
    }

}
