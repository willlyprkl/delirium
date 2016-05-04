using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {


	private string nimi;

	private int hp;
	private int damage;
	private int juotuMaara;

	public int tyyppi;



	// Use this for initialization
	void Start () {

		// Ruoat
		if (tyyppi == 1) {
			//1 = kalja
			nimi = "Kaljan";
			hp = 5;
			damage = 5;
			juotuMaara += 1;
		} else if (tyyppi == 2) {
			//2 = viina
			nimi = "Viinaa";
			hp = 10;
			damage = 5;
			juotuMaara += 1;
		} else if (tyyppi == 3) {
			//3 = marjat
			nimi = "Marjoja";
			hp = 10;
		} else if (tyyppi == 4) {
			//4 = tatit
			nimi = "Tatteja";
			hp = 10;
			damage = 5;
		} else if (tyyppi == 5) {
			//5 = kärpässienet
			nimi = "Kärpässieniä";
			hp = 5;
			damage = 15;

			// Aseet
		} else if (tyyppi == 6) {
			//6 = kirves
			nimi = "Kirves";
			damage = 10;
		} else if (tyyppi == 7) {
			//7 = keppi
			nimi = "Keppi";
			damage = 5;
		} else if (tyyppi == 8) {
			//8 = vasara
			nimi = "Vasara";
			damage = 5;
		} else if (tyyppi == 9) {
			//9 = putki
			nimi = "Putki";
			damage = 10;

        } else if (tyyppi == 10) {
            //10 = munat
            nimi = "Munat";
            hp = 5;
            damage = 5;
		} else if (tyyppi == 11) {
			//10 = munat
			nimi = "Marjoja";
			hp = 10;
		}



		if (juotuMaara == 20) {
			// DRUNKENMASTER ACHIVEMENT
			hp += 75;
			damage += 25;
		}
	}


	public int GetDamage() {
		return this.damage;
	}
	public int GetHp() {
		return this.hp;
	}

	public string GetItemname () {
		return this.nimi;
	}
}
