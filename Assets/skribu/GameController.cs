using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private GameManager gm;
    public Logger logger;

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

        if (hits.transform == null){
            enemy.transform.position = endpos;

        } else if (hits.transform.tag == "Player") {
            enemy.transform.position = startpos;
            player = hits.transform.GetComponent<Player>();
            player.VahennaHp(enemy.damage);
            //Debug.Log(enemy.nimi + " hit player for " + enemy.damage + "dmg");
            Logger.Lisaa(enemy.nimi + " hit player for " + enemy.damage + "dmg");
        } else if (hits.transform.tag == "Enemy") {
            enemy.transform.position = startpos;
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
		Item item;

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
            Logger.Lisaa("You hit " + enemy.nimi + " for " + player.attack + "dmg");
        } else if (hits.transform.tag == "juoma") {
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
			player.LisaaHp (item.GetHp ());
			player.LisaaDmg(item.GetDamage());
            player.transform.position = endpos;
        } else if (hits.transform.tag == "ruoka") {
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
			player.LisaaDmg (item.GetDamage());
			player.LisaaHp (item.GetHp());
            player.transform.position = endpos;
        } else if (hits.transform.tag == "puut") {
            player.transform.position = endpos;
        } else if (hits.transform.tag == "tie") {
            player.transform.position = startpos;
        } else if (hits.transform.tag == "ase") {
			item = hits.transform.GetComponent<Item> ();
			player.LisaaDmg(item.GetDamage());
			hits.transform.gameObject.SetActive(false);
			player.transform.position = endpos;
        }

        gm.playerTurn = false;
    }

}
