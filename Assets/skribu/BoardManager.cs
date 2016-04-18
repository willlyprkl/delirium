using UnityEngine;
using System;
using System.Collections.Generic;  // Listat
using Random = UnityEngine.Random; // Unityn randomi

public class BoardManager : MonoBehaviour {

    // Inspektorille
    [Serializable]  
    public class Count {
        public int minimum; // Minimi- ja maksimiarvot objekteille
        public int maximum;

        // Konstruktori
        public Count(int min, int max){
            minimum = min;
            maximum = max;
        }
    }

    public int sarakkeet = 8;                       // Kentän sarakkeiden määrä
    public int rivit = 8;                           // Kentän rivien määrä

    public Count juomaCount = new Count(2, 4);      // Ylä- ja alarajat erikoistavroille/ruoille
    public Count ruokaCount = new Count(3, 5);
    public Count aseCount = new Count(1, 5);
    public Count metsaCount = new Count(5, 10);
    public Count vihollisCount = new Count(2, 3);

    public GameObject player; 
    public GameObject exit;                         // Exit-prefabi
    public GameObject[] maaTilet;                   // Prefabit maatileistä
    public GameObject[] ruokaTilet;                 //          ruokatileistä
    public GameObject[] juomaTilet;                 //          juomatileistä
    public GameObject[] aseTilet;                   //          asetileistä
    public GameObject[] metsaTilet;                 //          metsätileistä
    public GameObject[] tieTilet;                   //          tietileistä
    public GameObject[] vihollisTilet;              //          vihollisista

    private Transform boardKansio;                  // boardin tilekansio
    private List <Vector3> gridPositiot = new List<Vector3>(); // mahd. tilet

    // Alustaa listan. Luo ruudukon.
    void AlustaLista() {
        gridPositiot.Clear();

        // tarvittava määrä sarakkeita per rivi
        for(int x = 1; x < sarakkeet - 1; x++){
            for(int y = 1; y < rivit - 1; y++) {
                gridPositiot.Add(new Vector3(x, y, 0f));
            }
        }

    }


    void KenttaSetuppi() {
        boardKansio = new GameObject("Board").transform;

        for(int x = -1; x < sarakkeet + 1; x++) {
            for(int y = -1; y < rivit + 1; y++) {
                GameObject alustettava = maaTilet[Random.Range(0, maaTilet.Length)];
                if(x == -1 || x == sarakkeet || y == -1 || y == rivit)
                    alustettava = tieTilet[Random.Range(0, tieTilet.Length)];

                GameObject instanssi = Instantiate(alustettava, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instanssi.transform.SetParent(boardKansio);

            }
        }
    }
        
    Vector3 RandomPositio() {
        int randind = Random.Range(0, gridPositiot.Count);
        Vector3 randompos = gridPositiot[randind];
        gridPositiot.RemoveAt(randind);
        return randompos;
    }

    void Layouttaa(GameObject[] tileArray, int min, int max) {
        int objcnt = Random.Range(min, max + 1);

        for(int i = 0; i < objcnt; i++) {
            Vector3 randpos = RandomPositio();
            GameObject tiili = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tiili, randpos, Quaternion.identity);                
        }
    }

    public void Setuppi() {
        KenttaSetuppi();
        AlustaLista();
        Layouttaa(juomaTilet, juomaCount.minimum, juomaCount.maximum);
        Layouttaa(ruokaTilet, ruokaCount.minimum, ruokaCount.maximum);
        Layouttaa(aseTilet, aseCount.minimum, aseCount.maximum);
        Layouttaa(metsaTilet, metsaCount.minimum, metsaCount.maximum);
        Layouttaa(vihollisTilet, vihollisCount.minimum, vihollisCount.maximum);

        Instantiate(exit, new Vector3(sarakkeet - 1, rivit - 1, 0f), Quaternion.identity);
        //Instantiate(player, new Vector3(sarakkeet - 8, rivit - 8, 0f), Quaternion.identity);
    }

    public void LisaaVihollisia() {
        Layouttaa(vihollisTilet, vihollisCount.minimum, vihollisCount.maximum);

    }
}
