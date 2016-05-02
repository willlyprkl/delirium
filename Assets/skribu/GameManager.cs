using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    private Image gameOverImg;
    private Text gameOverText;
    private Button quitButton;
    private Button mmButton;

    public bool pause = false;

    // Pelin käynnistys
	void Awake () {
        // Haetaan kenttäskribu
        kenttaScribu = GetComponent<BoardManager>();

        gameOverImg = GameObject.Find("gameOverImg").GetComponent<Image>();
        gameOverText = GameObject.Find("gameOverText").GetComponent<Text>();
        quitButton = GameObject.Find("quitButton").GetComponent<Button>();
        mmButton = GameObject.Find("mmButton").GetComponent<Button>();

        quitButton.onClick.AddListener(() => (Quittaa()));
        mmButton.onClick.AddListener(() => (MainMenuun()));
		
        gameOverImg.gameObject.SetActive(false);

        loader = GameObject.Find ("Loader").GetComponent<Loader> ();
        // Luodaan vihollislista
        viholliset = new List<Enemy>();
        // Ajetaan kentänluonti
        InitGame();
	}

    // Kentänluonti
    void InitGame() {
        kenttaScribu.Setuppi(loader.GetKoko(), loader.GetVaikeus());
    }

    // Tarkistetaan onko pelaajan vuoro ja liikutetaan viholliset, jos ei ole.
    void Update () {
        if (playerTurn || enemyMoving || pause)
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

    public void GameOver(bool voitto) {
        gameOverImg.gameObject.SetActive(true);
        if (voitto) {
            gameOverText.text = "You win!";
        } else {
            gameOverText.text = "Game over";
        }
        StartCoroutine(Liikutakuva());
    }

    IEnumerator Liikutakuva() {
        Vector3 endpos = new Vector3(350, 350, 0f);

        float matka = (gameOverImg.transform.position - endpos).sqrMagnitude;
        float speed = 5.0f;

        while(matka > float.Epsilon) {
            Vector2 newPos = Vector2.MoveTowards(gameOverImg.transform.position, endpos, speed);
            gameOverImg.transform.position = newPos;
            matka = (gameOverImg.transform.position - endpos).sqrMagnitude;
            yield return null;
        }

    }

    void MainMenuun() {
        Destroy(GameObject.Find("Loader"));
        Destroy(GameObject.Find("Sounds"));
        SceneManager.LoadScene("Menu");
    }

    void Quittaa() {
        Application.Quit();
    }
}
