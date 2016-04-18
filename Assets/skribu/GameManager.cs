using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public BoardManager kenttaScribu;
    public bool playerTurn = true;

    public List<Enemy> viholliset;

	// Use this for initialization
	void Awake () {
        kenttaScribu = GetComponent<BoardManager>();
        viholliset = new List<Enemy>();
        InitGame();
	}

    void InitGame() {
        kenttaScribu.Setuppi();
    }

	// Update is called once per frame
	void Update () {
        if (playerTurn)
            return;

        Liikutavihut();
	}

    public void LisaaVihuListaan(Enemy x) {
        viholliset.Add(x);
    }

    void Liikutavihut(){
        playerTurn = false;

        if (viholliset.Count >= 1){
            for (int i = 0; i < viholliset.Count; i++){
                viholliset[i].LiikuEnemy();
            }
        } else {
            kenttaScribu.LisaaVihollisia();
        }

        playerTurn = true;
    }
}
