using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Sounds sounds;
    public Loader loader;
    public BoardManager kenttaScribu;

    public bool playerTurn = true;
    public bool pause = false;
    public bool enemyMoving;

    private float vuoroAika = 0.1f;

    public List<Enemy> viholliset;

    private Image gameOverImg;
	private Image voittoImage;
	private Image havioImage;
    private Text gameOverText;
    private Button quitButton;
    private Button mmButton;

	public AudioClip gameOverSound;

    void Awake () {
        kenttaScribu = GetComponent<BoardManager>();

        gameOverImg = GameObject.Find("gameOverImg").GetComponent<Image>();
		voittoImage = GameObject.Find ("VoittoImg").GetComponent<Image> ();
		havioImage = GameObject.Find ("HavioImg").GetComponent<Image> ();
		voittoImage.gameObject.SetActive (false);
		havioImage.gameObject.SetActive (false);
        gameOverText = GameObject.Find("gameOverText").GetComponent<Text>();
        quitButton = GameObject.Find("quitButton").GetComponent<Button>();
        mmButton = GameObject.Find("mmButton").GetComponent<Button>();

		sounds = GameObject.Find ("Sounds").GetComponent<Sounds> ();

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

    public void LisaaVihuListaan(Enemy x) {
        viholliset.Add(x);
    }

    IEnumerator Liikutavihut(){

        // Varmistetaan, että ei ole pelaajan vuoro
        playerTurn = false;
        enemyMoving = true;
        yield return new WaitForSeconds(vuoroAika);

        // Jos vihollisia on yksi tai enemmän ajetaan lista läpi
        if (viholliset.Count >= 1){
            for (int i = 0; i < viholliset.Count; i++){
                viholliset[i].LiikuEnemy();
                // Jos vihollinen aggroaa, odotetaan vuoronajan
                if (viholliset[i].moves)
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

    // Gameovertilanne
    public void GameOver(bool voitto) {
        gameOverImg.gameObject.SetActive(true);
        // Jos voitetaan valitaan oikea kuva ja pistetään oikea teksti ja vice versa
        if (voitto) {
            gameOverText.text = "You win!";
			voittoImage.gameObject.SetActive (true);
        } else {
            gameOverText.text = "Game over";
			havioImage.gameObject.SetActive (true);
        }
		StartCoroutine(Liikutakuva(voitto));
    }

    // Gameoverkuvan liikutus
	IEnumerator Liikutakuva(bool voitto) {
        Vector3 endpos = new Vector3(-150, 0, 0f);

        float matka = (gameOverImg.transform.localPosition - endpos).sqrMagnitude;
        float speed = 10.0f;

        while(matka > float.Epsilon) {
            Vector2 newPos = Vector2.MoveTowards(gameOverImg.transform.localPosition, endpos, speed);
            gameOverImg.transform.localPosition = newPos;
            matka = (gameOverImg.transform.localPosition - endpos).sqrMagnitude;
            yield return null;
        }

		if (!voitto)
			sounds.PlaySound (gameOverSound);

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
