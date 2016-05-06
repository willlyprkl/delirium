using UnityEngine;
using System;
using System.Collections.Generic;  
using Random = UnityEngine.Random; 

public class BoardManager : MonoBehaviour {

    // Inspektorille
    [Serializable]  
    public class Count {
        public int minimum; // Minimi- ja maksimiarvot objekteille
        public int maximum;

        public Count(int min, int max){
            minimum = min;
            maximum = max;
        }
    }

    public int sarakkeet = 8;                       // Kentän sarakkeiden määrä
    public int rivit = 8;                           // Kentän rivien määrä

    public Count juomaCount = new Count(2, 4);      // Ylä- ja alarajat
    public Count ruokaCount = new Count(3, 5);
    public Count aseCount = new Count(1, 5);
    public Count metsaCount = new Count(5, 10);
    public Count metsapropCount = new Count(3, 5);
    public Count vihollisCount = new Count(2, 3);

    public GameObject joukonHousut;                 // Vaatteet
    public GameObject joukonPaita;
    public GameObject joukonHattu;
    public GameObject joukonKengat;
    public GameObject joukonLompakko;
    public GameObject player;                       // Pelaaja
    public GameObject exit;                         // Exit-prefabi
                                                    
    public GameObject[] maaTilet;                   // Prefabit maatileistä
    public GameObject[] ruokaTilet;                 //          ruokatileistä
    public GameObject[] juomaTilet;                 //          juomatileistä
    public GameObject[] aseTilet;                   //          asetileistä
    public GameObject[] metsaTilet;                 //          metsätileistä
    public GameObject[] metsapropTilet;             //          metsäproptileistä
    public GameObject[] tieTilet;                   //          tietileistä
    public GameObject[] vihollisTilet;              //          vihollisista

    private Transform boardKansio;                  // boardin "tilekansio"
    private List <Vector3> gridPositiot = new List<Vector3>(); // mahd. tilet

    // Luo ruudukon.
    void AlustaLista() {
        // Varmistetaan, että lista on tyhjä
        gridPositiot.Clear();

        // tarvittava määrä sarakkeita per rivi
        for(int x = 0; x <= sarakkeet - 1; x++) {
            for(int y = 0; y <= rivit - 1; y++) {
                if ((x == (sarakkeet - 1)) && (y == (rivit - 1))) {
                    //Debug.Log("Jou");
                } else {
                    gridPositiot.Add(new Vector3(x, y, 0f));
                }
            }
        }

    }

    // Luo kenttäpohjan
	void KenttaSetuppi() {
        // Luo "kansion"
        boardKansio = new GameObject("Board").transform;

        // Loop jossa käydään kaikki ruudut läpi
        for(int x = -1; x < sarakkeet + 1; x++) {
            for(int y = -1; y < rivit + 1; y++) {
                // Random maatile jokaiseen ruutuun
                GameObject alustettava = maaTilet[Random.Range(0, maaTilet.Length)];
                // Teistä reunat kartalle
                if (x == -1 && y >= 0 && y < sarakkeet) {
                    alustettava = tieTilet[1];
                } else if (x == rivit && y >= 0 && y < sarakkeet) {
                    alustettava = tieTilet[2];
                } else if (y == -1 && x >= 0 && x < rivit) {
                    alustettava = tieTilet[4];
                } else if (y == rivit && x >= 0 && x < rivit) {
                    alustettava = tieTilet[3];
                } else if (x <= -1 || x == sarakkeet || y <= -1 || y == rivit){
                    alustettava = tieTilet[0];
                }

                // Instantiatetaan annettu tile
                GameObject instanssi = Instantiate(alustettava, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                // Asetetaan "kansioon"
                instanssi.transform.SetParent(boardKansio);

            }
        }
    }
        
    // Random-positioita
    Vector3 RandomPositio() {
        if (gridPositiot.Count < 1){
            //Debug.Log("Lista tyhjä");
            AlustaLista();
        }
        int randind = Random.Range(0, gridPositiot.Count);
        Vector3 randompos = gridPositiot[randind];
        // Poistetaan listasta, jotta tavarat eivät syntyisi päällekkäin
        gridPositiot.RemoveAt(randind);
        return randompos;
    }

    // Tavaroiden, puiden, vihollisten layouttaus. (GameObject-Array, montako halutaan)
    void Layouttaa(GameObject[] tileArray, int min, int max) {
        int objcnt = Random.Range(min, max + 1);

        for(int i = 0; i < objcnt; i++) {
            // Haetaan randompositio
            Vector3 randpos = RandomPositio();
            // Valitaan satunnaisesti prefabi arraysta
            GameObject tiili = tileArray[Random.Range(0, tileArray.Length)];
            // Instantiatetaan kyseinen tiili
            Instantiate(tiili, randpos, Quaternion.identity);                
        }
    }


    // Setuppi
	public void Setuppi(int koko, int vaikeus) {
        if (koko <= 1) {
            sarakkeet = 8;
            rivit = 8;
            // 8^2 = 64;

            juomaCount.minimum = 2;
            juomaCount.maximum = 3;
            ruokaCount.minimum = 2;
            ruokaCount.maximum = 4;
            aseCount.minimum = 1;
            aseCount.maximum = 3;
            metsaCount.minimum = 15;
            metsaCount.maximum = 20;
            metsapropCount.minimum = 8;
            metsapropCount.maximum = 10;

        } else if (koko == 2) {
            sarakkeet = 16;
            rivit = 16;
            // 16^2 = 256;

            juomaCount.minimum = 12;
            juomaCount.maximum = 20;
            //min 4%
            //max 8%
            ruokaCount.minimum = 12;
            ruokaCount.maximum = 20;
            //min 4%
            //max 8%
            aseCount.minimum = 4;
            aseCount.maximum = 8;
            // min 2%
            // max 3%
            metsaCount.minimum = 80;
            metsaCount.maximum = 100;
            // min 31%
            // max 39%
            metsapropCount.minimum = 15;
            metsapropCount.maximum = 30;
            // min 6%
            // max 12%

        } else if (koko >= 3) {
            sarakkeet = 25;
            rivit = 25;
            // 625;

            juomaCount.minimum = 25;
            juomaCount.maximum = 50;

            ruokaCount.minimum = 25;
            ruokaCount.maximum = 50;

            aseCount.minimum = 12;
            aseCount.maximum = 19;

            metsaCount.minimum = 190;
            metsaCount.maximum = 240;

            metsapropCount.minimum = 38;
            metsapropCount.maximum = 75;
        }

        if (vaikeus <= 1 && koko <= 1) {
            vihollisCount.minimum = 1;
            vihollisCount.maximum = 2;
        } else if (vaikeus == 2 && koko <= 1) {
            vihollisCount.minimum = 2;
            vihollisCount.maximum = 3;
        } else if (vaikeus >= 3 && koko <= 1) {
            vihollisCount.minimum = 4;
            vihollisCount.maximum = 5;

        } else if (vaikeus <= 1 && koko == 2) {
            vihollisCount.minimum = 2;
            vihollisCount.maximum = 3;
        } else if (vaikeus == 2 && koko == 2) {
            vihollisCount.minimum = 4;
            vihollisCount.maximum = 7;
        } else if (vaikeus >= 3 && koko == 2) {
            vihollisCount.minimum = 7;
            vihollisCount.maximum = 10;

        } else if (vaikeus <= 1 && koko >= 3) {
            vihollisCount.minimum = 10;
            vihollisCount.maximum = 12;
        } else if (vaikeus == 2 && koko >= 3) {
            vihollisCount.minimum = 13;
            vihollisCount.maximum = 15;
        } else if (vaikeus >= 3 && koko >= 3) {
            vihollisCount.minimum = 18;
            vihollisCount.maximum = 20;
        }

        // Luodaan kentän pohja
        KenttaSetuppi();
        // Alustetaan vektorilista
        AlustaLista();
        // Layoutataan kaikki mahdolliset itemit yms.(tilet, min, max)
        Layouttaa(juomaTilet, juomaCount.minimum, juomaCount.maximum);
        Layouttaa(ruokaTilet, ruokaCount.minimum, ruokaCount.maximum);
        Layouttaa(aseTilet, aseCount.minimum, aseCount.maximum);
        Layouttaa(metsaTilet, metsaCount.minimum, metsaCount.maximum);
        Layouttaa(metsapropTilet, metsapropCount.minimum, metsapropCount.maximum);
        Layouttaa(vihollisTilet, vihollisCount.minimum, vihollisCount.maximum);

        Instantiate(joukonHattu, RandomPositio(), Quaternion.identity);
        Instantiate(joukonHousut, RandomPositio(), Quaternion.identity);
        Instantiate(joukonKengat, RandomPositio(), Quaternion.identity);
        Instantiate(joukonPaita, RandomPositio(), Quaternion.identity);
        Instantiate(joukonLompakko, RandomPositio(), Quaternion.identity);

        // Exitti kartan yläkulmaan
        Instantiate(exit, new Vector3(sarakkeet - 1, rivit - 1, 0f), Quaternion.identity);
        player.transform.position = RandomPositio();
    }

    // Vihollisten respawnaus
    public void LisaaVihollisia() {
        Layouttaa(vihollisTilet, vihollisCount.minimum, vihollisCount.maximum);

    }
}
