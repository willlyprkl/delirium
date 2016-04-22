using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    // GameManager pelaajan vuoron päivittämiseen
    private GameManager gm;
	public GameObject[] splat;


	void Start () {
		
	}
	
    // Vihollisen liikkuminen(suunta, hor/ver, vihollinen)
    public void LiikuEn(int a, string b, Enemy enemy) {
        // Raycast, ruutujen tarkistamiseen
        RaycastHit2D hits;

        // Lähtöruutu
        Vector2 startpos = enemy.transform.position;
        // Loppuruutu
        Vector2 endpos;
        // Pelaaja
        Player player;

        bool move = false;

        // Tarkistaa liikeen horisontaalisuuden/vertikaalisuuden
        if (b == "ho") {
            endpos = startpos + new Vector2(a, 0);
        } else {
            endpos = startpos + new Vector2(0, a);
        }

        // Linecast ruutun johon liikutaan
        hits = Physics2D.Linecast(startpos, endpos);
        // Jos ruutu on tyhjä, ruutuun liikutaan
        if (hits.transform == null){
            move = true;
        
        // Jos ruudussa on pelaaja, haetaan pelaaja ja vähennetään pelaajan
        // hp:ta vihollisen damagen mukaan.
        } else if (hits.transform.tag == "Player") {
            move = false;
            player = hits.transform.GetComponent<Player>();
            enemy.animator.SetTrigger("enemyLyonti");
            player.VahennaHp(enemy.GetDamage());
            //Debug.Log(enemy.nimi + " hit player for " + enemy.damage + "dmg");
            Logger.Lisaa(enemy.nimi + " hit player for " + enemy.GetDamage() + "dmg");
        
        // Jos ruudussa on vihollinen, estetään liike
        } else if (hits.transform.tag == "enemy") {
            move = false;
        
        // Jos ruudussa on jotain muuta, esim. itemi, liikutaan siihen
        } else {
            move = true;
        }

        if (move)
            StartCoroutine(SmoothEn(endpos, enemy));
    }


    // Pelaajan liikkuminen
	public void Liiku(int a, string b, Player player) {
        // Haetaan gm-objekti
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Raycast, ruutujen tarkistamiseen
        RaycastHit2D hits;
        // Lähtöruutu
        Vector2 startpos = player.transform.position;
        // Loppuruutu
        Vector2 endpos;
        // Vihollinen
        Enemy enemy;
        // Itemi
		Item item;
        // Liikkumisen tarkistusta varten
        bool move = false;

        // Tarkistaa liikeen horisontaalisuuden/vertikaalisuuden
        if (b == "ho") {
            endpos = startpos + new Vector2(a, 0);
        } else {
            endpos = startpos + new Vector2(0, a);
        }

        // Linecast ruutun johon liikutaan
        hits = Physics2D.Linecast(startpos, endpos);

        // Jos ruutu on tyhjä, liikutaan
        if (hits.transform == null) {
            move = true;

        // Jos ruudussa on vihollinen, tehdään viholliseen vahinkoa
        } else if (hits.transform.tag == "enemy") {
            enemy = hits.transform.GetComponent<Enemy>();
            player.animator.SetTrigger("joukoLyonti");
            enemy.VahennaHp(player.GetDamage());
            //Debug.Log(enemy.ToString());
            // Jos vihollisen hp loppuu, vihollinen tuhotaan
            if ((enemy.GetHealth()) <= 0) {
                gm.viholliset.Remove(enemy);
                GameObject randsplat = splat[Random.Range(0, splat.Length)];
				Instantiate (randsplat, enemy.transform.position, Quaternion.identity);
				Destroy(enemy.gameObject);

            }
            //Debug.Log(enemy.GetHealth());
            move = false;
            Logger.Lisaa("You hit " + enemy.nimi + " for " + player.GetDamage() + "dmg, " + enemy.nimi + " " + enemy.GetHealth() + "/" + enemy.GetFullHealth() + "hp");


        // Jos ruudussa on itemi se käytetään ja lisätään hp ja damage itemin mukaan

        } else if (hits.transform.tag == "juoma") {
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
			player.LisaaHp (item.GetHp ());
			player.LisaaDmg(item.GetDamage());
            move = true;
			Logger.Lisaa("You drank " + item.GetItemname () + ", gain " + item.GetHp ()+ "hp" + " and " + item.GetDamage () + "dmg");


        } else if (hits.transform.tag == "ruoka") {
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
			player.LisaaDmg (item.GetDamage());
			player.LisaaHp (item.GetHp());
            move = true;
			Logger.Lisaa("You ate " + item.GetItemname () + ", gain " + item.GetHp ()+ "hp" + " and " + item.GetDamage () + "dmg");

		} else if (hits.transform.tag == "ase") {
			item = hits.transform.GetComponent<Item> ();
			player.LisaaDmg(item.GetDamage());
			hits.transform.gameObject.SetActive(false);
            move = true;
			Logger.Lisaa("You found " + item.GetItemname () + ", gain " + item.GetDamage () + "dmg");

			// Puista ei välitetä
		} else if (hits.transform.tag == "puut") {
            move = true;
        // Tiet toimii pelialueen reunana, ei pääse läpi
        } else if (hits.transform.tag == "tie") {
            move = false;

        } 

        if (move) 
            StartCoroutine (Smooth(endpos, player));
        
        // Pelaajan vuoro loppuu
        gm.playerTurn = false;
    }

    IEnumerator Smooth (Vector2 endpos, Player player) {
        Vector3 asd = endpos;
        Rigidbody2D rb2D = player.gameObject.GetComponent<Rigidbody2D>();
        float sqrRemainingDistance = (player.transform.position - asd).sqrMagnitude;
        float speed = Time.deltaTime * 10;

        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPos = Vector3.MoveTowards(rb2D.position, asd, speed);
            rb2D.MovePosition(newPos);
            sqrRemainingDistance = (player.transform.position - asd).sqrMagnitude;
            yield return null;
        }
    }

    IEnumerator SmoothEn (Vector2 endpos, Enemy enemy) {
        Vector3 asd = endpos;
        Rigidbody2D rb2D = enemy.gameObject.GetComponent<Rigidbody2D>();
        float sqrRemainingDistance = (enemy.transform.position - asd).sqrMagnitude;
        float speed = Time.deltaTime * 10;

        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPos = Vector3.MoveTowards(rb2D.position, asd, speed);
            rb2D.MovePosition(newPos);
            sqrRemainingDistance = (enemy.transform.position - asd).sqrMagnitude;
            yield return null;
        }
    }
        
}
