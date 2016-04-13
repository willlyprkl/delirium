using UnityEngine;
using System.Collections;

public class Enemy : GameController {
   
    public int tyyppi;

    private GameManager gm;
    private Transform target;
    private int damage;
    private int hp;

    void Start () {
        gm = (GameManager)GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform; 

        if (tyyppi == 1) {
            damage = 100;
            hp = 100;
        } else if (tyyppi == 2) {
            // Supikoira
            damage = 30;
            hp = 20;
        } else if (tyyppi == 3) {
            // Susi
            damage = 35;
            hp = 50;
        } else if (tyyppi == 4) {
            // Jänis
            damage = 10;
            hp = 15;
        } else if (tyyppi == 5) {
            // Hirvi
            damage = 25;
            hp = 80;
        }

        gm.LisaaVihuListaan(this);
	}

    public void LiikuEnemy(){
        int x = 0;
        string suunt;

        float dx = target.position.x - this.transform.position.x;
        float dy = target.position.y - this.transform.position.y;

        if (Mathf.Abs(dx) > Mathf.Abs(dy)) {
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


        LiikuEn(x, suunt, this);

        Debug.Log("" + x + "" + suunt);
    }
}
