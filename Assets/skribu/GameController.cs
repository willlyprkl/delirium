using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private GameManager gm;

	void Start () {
	}
	
    protected void LiikuEn(int a, string b, Enemy enemy) {
        RaycastHit2D hits;
        Vector2 startpos = enemy.transform.position;
        Vector2 endpos = enemy.transform.position;;
        Player player;

        if (b == "ho") {
            endpos = startpos + new Vector2(a, 0);
        } else {
            endpos = startpos + new Vector2(0, a);
        }

        hits = Physics2D.Linecast(startpos, endpos);

        if (hits.transform == null) {
            enemy.transform.position = endpos;

        } else if (hits.transform.tag == "Player") {
            enemy.transform.position = startpos;
            Debug.Log("player");
            player = hits.transform.GetComponent<Player>();
            player.VahennaHp(enemy.damage);
        } else {
            enemy.transform.position = endpos;
        }
    }

    protected void Liiku(int a, string b, Player player) {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        RaycastHit2D hits;
        Vector2 startpos = player.transform.position;
        Vector2 endpos;
        Enemy enemy;

        if (b == "ho") {
            endpos = startpos + new Vector2(a, 0);
        } else {
            endpos = startpos + new Vector2(0, a);
        }

        hits = Physics2D.Linecast(startpos, endpos);

        if (hits.transform == null) {
            player.transform.position = endpos;
        } else if (hits.transform.tag == "enemy") {
            enemy = hits.transform.GetComponent<Enemy>();
            enemy.VahennaHp(player.attack);
            //Debug.Log(enemy.ToString());
            if ((enemy.GetHealth()) <= 0) {
                gm.viholliset.Remove(enemy);
                Destroy(enemy.gameObject);
            }
            Debug.Log(enemy.GetHealth());
            player.transform.position = startpos;
        } else if (hits.transform.tag == "juoma") {
            hits.transform.gameObject.SetActive(false);
            player.LisaaDmg(10);
            player.transform.position = endpos;
        } else if (hits.transform.tag == "ruoka") {
            hits.transform.gameObject.SetActive(false);
            player.LisaaHp(10);
            player.transform.position = endpos;
        } else if (hits.transform.tag == "puut") {
            player.transform.position = endpos;
        }

        gm.playerTurn = false;
    }

}
