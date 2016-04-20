using UnityEngine;
using System.Collections;

public class Enemy : GameController {

    // Vihollistyyppi, eri vihollisia varten
    public int tyyppi;
    // Nimi viholliselle
    public string nimi;
    // GM, jotta viholliset voidaan lisätä listaan
    private GameManager gm;
    // Targetti hahmolle
    private Transform target;
    // Iskuvoima
    public int damage;
    // Hitpointit
    private int hp;

    void Start () {
        // Haetaan gm-objekti
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Haetaan kohde, eli pelaaja
        target = GameObject.FindGameObjectWithTag("Player").transform; 

        // Eri vihollistyypit, tyyppi asetettu prefabissa
        if (tyyppi == 1) {
            // Karhu
            nimi = "Karhu";
            damage = 100;
            hp = 100;
        } else if (tyyppi == 2) {
            // Supikoira
            nimi = "Supikoira";
            damage = 30;
            hp = 20;
        } else if (tyyppi == 3) {
            // Susi
            nimi = "Susi";
            damage = 35;
            hp = 50;
        } else if (tyyppi == 4) {
            // Jänis
            nimi = "Jänis";
            damage = 10;
            hp = 15;
        } else if (tyyppi == 5) {
            // Hirvi
            nimi = "Hirvi";
            damage = 25;
            hp = 80;
		} else if (tyyppi == 6) {
			// Käärme
			nimi = "Käärme";
			damage = 50;
			hp = 25;
		}

        // Lisätään luotu vihollinen listaan
        gm.LisaaVihuListaan(this);
	}

    // Vihollisen liikuttaminen
    public void LiikuEnemy(){
        // Alustetaan liikkuminen
        int x = 0;
        string suunt;

        // Vihollisen ja pelaajan välinen positioero
        float dx = target.position.x - this.transform.position.x;
        float dy = target.position.y - this.transform.position.y;

        // Jos deltat on yli 20, vihollinen ei "aggroa"
        if ((Mathf.Abs(dx) < 20) && (Mathf.Abs(dy) < 20)) {
            // Jos x-suuntainen ero on isompi kuin y-suuntainen, niin liikutaan x-suuntaisesti
            if (Mathf.Abs(dx) > Mathf.Abs(dy)) {
                // Jos x-eroavaisuus on miinuksella, liikutaan miinus-suuntaan
                if (dx < 0)
                    x = -1;
                else
                    x = 1;
                suunt = "ho";

            } else {
                if (dy < 0)
                    x = -1;
                else
                    x = 1;
                suunt = "ve";
            }

            // Liikutetaan vihollista
            LiikuEn(x, suunt, this);

            //Debug.Log(""+ nimi + ": " + x + "" + suunt);
        }
    }

    // HP:n vähennys
    public void VahennaHp(int a) {
        hp -= a;
    }

    public int GetHealth() {
        return this.hp;
    }
}
