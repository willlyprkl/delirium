using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    // GameManager pelaajan vuoron päivittämiseen
    private GameManager gm;
    private Sounds sounds;
 
    public GameObject[] splat;
    public GameObject[] puuSplat;

    private int keratty = 0;

    private Image joukonHousutImg;
    private Image joukonPaitaImg;
    private Image joukonKengatImg;
    private Image joukonHattuImg;
    private Image joukonLompakkoImg;
    private Image ajatusImg;

	void Start () {
        joukonHattuImg = GameObject.Find("joukonhattuImg").GetComponent<Image>();
        joukonHousutImg = GameObject.Find("joukonhousutImg").GetComponent<Image>();
        joukonKengatImg = GameObject.Find("joukonkengatImg").GetComponent<Image>();
        joukonPaitaImg = GameObject.Find("joukonpaitaImg").GetComponent<Image>();
        joukonLompakkoImg = GameObject.Find("joukonlompakkoImg").GetComponent<Image>();
        ajatusImg = GameObject.Find("ajatusImg").GetComponent<Image>(); 

        sounds = GameObject.Find("Sounds").GetComponent<Sounds>();
        Debug.Log(sounds);
        ajatusImg.gameObject.SetActive(false);

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
        Forest puu;

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
            sounds.PlaySoundY(enemy.enemySound);
            player.VahennaHp(enemy.GetDamage());
            //Debug.Log(enemy.nimi + " hit player for " + enemy.damage + "dmg");
            Logger.Lisaa(enemy.GetNimi() + " hit player for " + enemy.GetDamage() + "dmg");
            enemy.animator.SetTrigger("enemyLyonti");

            if (player.GetHealth() <= 0) {
                gm.gameOver = true;
                gm.GameOver();
            }

        // Jos ruudussa on vihollinen, estetään liike
        } else if (hits.transform.tag == "enemy") {
            move = false;
        } else if (hits.transform.tag == "puut") {
            move = false;
            enemy.animator.SetTrigger("enemyLyonti");
            puu = hits.transform.GetComponent<Forest>();
            puu.VahennaHp();
            if (puu.GetHp() <= 0) {
                GameObject randPuuSplat = puuSplat[Random.Range(0, puuSplat.Length)];
                Instantiate(randPuuSplat, puu.transform.position, Quaternion.identity);
                hits.transform.gameObject.SetActive(false);
            }
        // Jos ruudussa on jotain muuta, esim. itemi, liikutaan siihen
        } else {
            move = true;
        }

        if (move)
            StartCoroutine(SmoothEn(endpos, enemy));
    }


    // Pelaajan liikkuminen
	public void Liiku(int a, string b, Player player) {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gm.enemyMoving || player.playerMoving)
            return;

        if (gm.gameOver) {
            return;
        }

        // Haetaan gm-objekti
        player.playerMoving = true;
        // Raycast, ruutujen tarkistamiseen
        RaycastHit2D hits;
        // Lähtöruutu
        Vector2 startpos = player.transform.position;
        // Loppuruutu
        Vector2 endpos;
        Forest puu;
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
            int i = Random.Range(0, 25);

            if (i == 10) {
                sounds.PlaySound(player.saatana[player.aaniValinta]);
            } else if (i == 5){
                sounds.PlaySound(player.jumalauta[player.aaniValinta]);
            } else if (i == 1) {
                sounds.PlaySound(player.perkele[player.aaniValinta]);
            } else if (i == 15) {
                sounds.PlaySound(player.heng[player.aaniValinta]);
            } else if (i == 24) {
                sounds.PlaySound(player.darra[player.aaniValinta]);
            } else if (i == 20) {
                sounds.PlaySound(player.huhhuh[player.aaniValinta]);
            }

            move = true;

        // Jos ruudussa on vihollinen, tehdään viholliseen vahinkoa
        } else if (hits.transform.tag == "enemy") {
            enemy = hits.transform.GetComponent<Enemy>();
            player.animator.SetTrigger("joukoLyonti");
            int rand = 0;

            if (player.aaniValinta <= 1) {
                rand = Random.Range(0, 4);
            } else if (player.aaniValinta == 2) {
                rand = Random.Range(4, 8);
            } else if (player.aaniValinta  >= 3) {
                rand = Random.Range(8, 12);
            }

            sounds.PlaySound(player.lyonti[rand]);

            enemy.VahennaHp(player.GetDamage());
            //Debug.Log(enemy.ToString());
            // Jos vihollisen hp loppuu, vihollinen tuhotaan
            if ((enemy.GetHealth()) <= 0) {
                gm.viholliset.Remove(enemy);
                // Kuollut vihollinen = veriläikkä
                GameObject randsplat = splat[Random.Range(0, splat.Length)];
				Instantiate (randsplat, enemy.transform.position, Quaternion.identity);
                sounds.PlaySoundZ(enemy.enemySound);
				Destroy(enemy.gameObject);
                // Tappo kill-counteriin
                player.Tappo();

            }
            //Debug.Log(enemy.GetHealth());
            move = false;
            Logger.Lisaa("You hit " + enemy.GetNimi() + " for " + player.GetDamage() + "dmg, " + enemy.GetNimi() + " " + enemy.GetHealth() + "/" + enemy.GetFullHealth() + "hp");


        // Jos ruudussa on itemi se käytetään ja lisätään hp ja damage itemin mukaan

        } else if (hits.transform.tag == "juoma") {
			int rand;
			rand = Random.Range (0, 11);
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
            sounds.PlaySoundX(player.juoma);
			if (rand <= 3) {
				sounds.PlaySoundY (player.royhtays);
			}
			player.LisaaHp (item.GetHp ());
			player.LisaaDmg(item.GetDamage());
            move = true;
			Logger.Lisaa("You drank " + item.GetItemname () + ", gain " + item.GetHp ()+ "hp" + " and " + item.GetDamage () + "dmg");
            // Lisätään juotujen juomien määrää
			player.Juotu ();


        } else if (hits.transform.tag == "ruoka") {
			item = hits.transform.GetComponent<Item> ();
            hits.transform.gameObject.SetActive(false);
            sounds.PlaySound2(player.syonti);
			player.LisaaDmg (item.GetDamage());
			player.LisaaHp (item.GetHp());
			player.syoMetsa ();
            move = true;
			Logger.Lisaa("You ate " + item.GetItemname () + ", gain " + item.GetHp ()+ "hp" + " and " + item.GetDamage () + "dmg");

		} else if (hits.transform.tag == "ase") {
			item = hits.transform.GetComponent<Item> ();
			player.LisaaDmg(item.GetDamage());
            sounds.PlaySound2(player.ase);
			hits.transform.gameObject.SetActive(false);
            move = true;
			Logger.Lisaa("You found " + item.GetItemname () + ", gain " + item.GetDamage () + "dmg");

			// Puista ei välitetä
        } else if (hits.transform.tag == "puut") {           
            player.animator.SetTrigger("joukoLyonti");

            int rand = 0;

            if (player.aaniValinta <= 1) {
                rand = Random.Range(0, 4);
            } else if (player.aaniValinta == 2) {
                rand = Random.Range(4, 8);
            } else if (player.aaniValinta  >= 3) {
                rand = Random.Range(8, 12);
            }

            sounds.PlaySoundX(player.lyonti);
            sounds.PlaySoundZ(player.puu);
            puu = hits.transform.GetComponent<Forest>();
            puu.VahennaHp();
            if (puu.GetHp() <= 0) {
                GameObject randPuuSplat = puuSplat[Random.Range(0, puuSplat.Length)];
                Instantiate(randPuuSplat, puu.transform.position, Quaternion.identity);
				player.puuIsDead ();
                hits.transform.gameObject.SetActive(false);
            }
            move = false;
        // Tiet toimii pelialueen reunana, ei pääse läpi
        } else if (hits.transform.tag == "tie") {
            move = false;
        // Kerattavat ovat objekteja, jotka pelaajan on kerattava
        } else if (hits.transform.tag == "kerattava") {
            sounds.PlaySound2(player.vaatteet);
            if (hits.transform.name == "joukonHousut(Clone)") {
                // Muutetaan UI:ssa oleva image täysin näkyväksi
                joukonHousutImg.color = new Color(255, 255, 255, 255);
                hits.transform.gameObject.SetActive(false);
                move = true;
                // Lisataan keratty objekti
                keratty++;
                Logger.Lisaa("You found your pants! " + keratty + "/5");
            } else if (hits.transform.name == "joukonHattu(Clone)") {
                joukonHattuImg.color = new Color(255, 255, 255, 255);
                hits.transform.gameObject.SetActive(false);
                move = true;
                keratty++;
                Logger.Lisaa("You found your hat! " + keratty + "/5");
            } else if (hits.transform.name == "joukonKengat(Clone)") {
                joukonKengatImg.color = new Color(255, 255, 255, 255);
                hits.transform.gameObject.SetActive(false);
                move = true;
                keratty++;
                Logger.Lisaa("You found your boots! " + keratty + "/5");
            } else if (hits.transform.name == "joukonPaita(Clone)") {
                joukonPaitaImg.color = new Color(255, 255, 255, 255);
                hits.transform.gameObject.SetActive(false);
                move = true;
                keratty++;
                Logger.Lisaa("You found your shirt! " + keratty + "/5");
            } else if (hits.transform.name == "joukonLompakko(Clone)") {
                joukonLompakkoImg.color = new Color(255, 255, 255, 255); 
                hits.transform.gameObject.SetActive(false);
                move = true;
                keratty++;
                Logger.Lisaa("You found your wallet! " + keratty + "/5");

            }
        
        } else if (hits.transform.tag == "exit") {
            if (keratty >= 5) {
                Logger.Lisaa("Voitit pelin....");
                move = true;
            } else {
                StartCoroutine(Ajatus());
                sounds.PlaySound(player.hukassa[player.aaniValinta]);
                move = false;
            }
        } else {
            move = true;
        }

        if (move) {
            StartCoroutine (Smooth(endpos, player));
            sounds.LiikeSound(player.move);
        }

        // Pelaajan vuoro loppuu
        player.playerMoving = false;
        gm.playerTurn = false;
    }

    //Pelaajan ja vihollisen smoothi liikuttaminen 

    IEnumerator Smooth (Vector2 endpos, Player player) {
        // Pyöristetään loppusijainti, jos ruudukosta on "eksytty"
        endpos.x = Mathf.Round(endpos.x);
        endpos.y = Mathf.Round(endpos.y);

        // Vector3 koska syyt
        Vector3 asd = endpos;
        // Haetaan objektin rigidbody
        Rigidbody2D rb2D = player.gameObject.GetComponent<Rigidbody2D>();
        // Liikkumiskohteen ja position välinen neliö
        float sqrRemainingDistance = (player.transform.position - asd).sqrMagnitude;
        // Liikkumisaika
        float speed = 0.1f;

        // Liikutetaan kunnes ollaan oikeassa paikassa
        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPos = Vector3.MoveTowards(rb2D.position, asd, speed);
            rb2D.MovePosition(newPos);
            sqrRemainingDistance = (player.transform.position - asd).sqrMagnitude;
            yield return null;
        }
    }

    IEnumerator SmoothEn (Vector2 endpos, Enemy enemy) {
        endpos.x = Mathf.Round(endpos.x);
        endpos.y = Mathf.Round(endpos.y);
        Vector3 asd = endpos;
        Rigidbody2D rb2D = enemy.gameObject.GetComponent<Rigidbody2D>();
        float sqrRemainingDistance = (enemy.transform.position - asd).sqrMagnitude;
        float speed = 0.1f;

        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPos = Vector3.MoveTowards(rb2D.position, asd, speed);
            rb2D.MovePosition(newPos);
            sqrRemainingDistance = (enemy.transform.position - asd).sqrMagnitude;
            yield return null;
        }
    }
        

    IEnumerator Ajatus() {
        ajatusImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        ajatusImg.gameObject.SetActive(false);
    }
}
