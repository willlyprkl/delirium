using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    // GameManager pelaajan vuoron päivittämiseen
    private GameManager gm;
	public GameObject splat;

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
            enemy.transform.position = endpos;
        
        // Jos ruudussa on pelaaja, haetaan pelaaja ja vähennetään pelaajan
        // hp:ta vihollisen damagen mukaan.
        } else if (hits.transform.tag == "Player") {
            enemy.transform.position = startpos;
            player = hits.transform.GetComponent<Player>();
            player.VahennaHp(enemy.GetDamage());
            //Debug.Log(enemy.nimi + " hit player for " + enemy.damage + "dmg");
            Logger.Lisaa(enemy.nimi + " hit player for " + enemy.GetDamage() + "dmg");
        
        // Jos ruudussa on vihollinen, estetään liike
        } else if (hits.transform.tag == "Enemy") {
            enemy.transform.position = startpos;
        
        // Jos ruudussa on jotain muuta, esim. itemi, liikutaan siihen
        } else {
            enemy.transform.position = endpos;
        }
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
            player.transform.position = endpos;
        // Jos ruudussa on vihollinen, tehdään viholliseen vahinkoa
        } else if (hits.transform.tag == "enemy") {
            enemy = hits.transform.GetComponent<Enemy>();
            enemy.VahennaHp(player.GetDamage());
            //Debug.Log(enemy.ToString());
            // Jos vihollisen hp loppuu, vihollinen tuhotaan
            if ((enemy.GetHealth()) <= 0) {
                gm.viholliset.Remove(enemy);
				Instantiate (splat, enemy.transform.position, Quaternion.identity);
				Destroy(enemy.gameObject);

            }
            //Debug.Log(enemy.GetHealth());
            player.transform.position = startpos;
            Logger.Lisaa("You hit " + enemy.nimi + " for " + player.GetDamage() + "dmg, " + enemy.nimi + " " + enemy.GetHealth() + "/" + enemy.GetFullHealth());


        // Jos ruudussa on itemi se käytetään ja lisätään hp ja damage itemin mukaan

        } else if (hits.transform.tag == "juoma") {
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
			player.LisaaHp (item.GetHp ());
			player.LisaaDmg(item.GetDamage());
            player.transform.position = endpos;
			Logger.Lisaa("You drank " + item.GetItemname () + ", gain " + item.GetHp ()+ "hp" + " and " + item.GetDamage () + "dmg");


        } else if (hits.transform.tag == "ruoka") {
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
			player.LisaaDmg (item.GetDamage());
			player.LisaaHp (item.GetHp());
            player.transform.position = endpos;
			Logger.Lisaa("You ate " + item.GetItemname () + ", gain " + item.GetHp ()+ "hp" + " and " + item.GetDamage () + "dmg");

		} else if (hits.transform.tag == "ase") {
			item = hits.transform.GetComponent<Item> ();
			player.LisaaDmg(item.GetDamage());
			hits.transform.gameObject.SetActive(false);
			player.transform.position = endpos;
			Logger.Lisaa("You found " + item.GetItemname () + ", gain " + item.GetDamage () + "dmg");

			// Puista ei välitetä
		} else if (hits.transform.tag == "puut") {
            player.transform.position = endpos;
        
        // Tiet toimii pelialueen reunana, ei pääse läpi
        } else if (hits.transform.tag == "tie") {
            player.transform.position = startpos;

        } 

        // Pelaajan vuoro loppuu
        gm.playerTurn = false;
    }
        
}
