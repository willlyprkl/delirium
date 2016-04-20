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
			nimi = "Kalja";
			hp = 10;
			damage = 10;
			juotuMaara += 1;
		} else if (tyyppi == 2) {
			//2 = viina
			nimi = "Viina";
			hp = 25;
			damage = 15;
			juotuMaara += 1;
		} else if (tyyppi == 3) {
			//3 = marjat
			nimi = "Marjat";
			hp = 15;
		} else if (tyyppi == 4) {
			//4 = tatit
			nimi = "Tatteja";
			hp = 15;
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
			damage = 25;
		} else if (tyyppi == 7) {
			//7 = keppi
			nimi = "Keppi";
			damage = 10;
		} else if (tyyppi == 8) {
			//8 = vasara
			nimi = "Vasara";
			damage = 10;
		} else if (tyyppi == 9) {
			//9 = putki
			nimi = "Putki";
			damage = 15;

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
