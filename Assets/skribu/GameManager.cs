using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Kenttäskribu pelialueen luontia varten
	public Loader loader;
    public BoardManager kenttaScribu;
    // Pelaajan vuoro bool
    public bool playerTurn = true;
    // Vihollislista
    public List<Enemy> viholliset;
    public bool enemyMoving;
    private float vuoroAika = 0.1f;

    // Pelin käynnistys
	void Awake () {
        // Haetaan kenttäskribu
        kenttaScribu = GetComponent<BoardManager>();

		loader = GameObject.Find ("Loader").GetComponent<Loader> ();
        // Luodaan vihollislista
        viholliset = new List<Enemy>();
        // Ajetaan kentänluonti
        InitGame();
	}

    // Kentänluonti
    void InitGame() {
		int a = 8;
		int b = 8;

		if (loader.GetKoko() == 1) {
			a = 8;
			b = 8;
		} else if(loader.GetKoko() == 2) {
			a = 16;
			b = 16;
		} else if (loader.GetKoko() == 3) {
			a = 32;
			b = 32;
		}

		kenttaScribu.Setuppi(a, b);
    }

    // Tarkistetaan onko pelaajan vuoro ja liikutetaan viholliset, jos ei ole.
    void Update () {
        if (playerTurn || enemyMoving)
            return;

        StartCoroutine(Liikutavihut());
	}

    // Vihollisen lisääminen listaan
    public void LisaaVihuListaan(Enemy x) {
        viholliset.Add(x);
    }

    // Vihollisten liikuttaminen
    IEnumerator Liikutavihut(){

        // Varmistetaan, että ei ole pelaajan vuoro
        playerTurn = false;
        enemyMoving = true;
        yield return new WaitForSeconds(vuoroAika);

        // Jos vihollisia on yksi tai enemmän ajetaan lista läpi
        if (viholliset.Count >= 1){
            for (int i = 0; i < viholliset.Count; i++){
                viholliset[i].LiikuEnemy();
                yield return new WaitForSeconds(vuoroAika);
            }
        // Jos ei, lisätään vihollisia.
        } else {
            kenttaScribu.LisaaVihollisia();
        }

        // Liikkumisen jälkeen on pelaajan vuoro
        playerTurn = true;
        enemyMoving = false;
    }
}
