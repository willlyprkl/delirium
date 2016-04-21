using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Kenttäskribu pelialueen luontia varten
    public BoardManager kenttaScribu;
    // Pelaajan vuoro bool
    public bool playerTurn = true;
    // Vihollislista
    public List<Enemy> viholliset;
    private bool liiket;
    private float vuoroAika = 0.1f;

    // Pelin käynnistys
	void Awake () {
        // Haetaan kenttäskribu
        kenttaScribu = GetComponent<BoardManager>();
        // Luodaan vihollislista
        viholliset = new List<Enemy>();
        // Ajetaan kentänluonti
        InitGame();
	}

    // Kentänluonti
    void InitGame() {
        kenttaScribu.Setuppi();
    }

    // Tarkistetaan onko pelaajan vuoro ja liikutetaan viholliset, jos ei ole.
    void Update () {
        if (playerTurn || liiket)
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
        liiket = true;
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
        liiket = false;
    }
}
