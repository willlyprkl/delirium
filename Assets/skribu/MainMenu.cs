using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private Sounds sounds;
    public AudioClip nappi;

	private Loader loader;

	private Button storyButton;
	private Button optionsButton;
	private Button startButton;
	private Button howToButton;

	private Button helppoButton;
	private Button normiButton;
	private Button vaikeaButton;

	private Button pieniButton;
	private Button mediumButton;
	private Button suuriButton;

	private Button diffiButton;
	private Button sizeButton;

	private Button backButton;
	private Button backButton2;
	//private Scrollbar skrolli;

	private GameObject paaKansio;
	private GameObject optionKansio;
	private GameObject storyKansio;
	private GameObject sizeKansio;
	private GameObject vaikeusKansio;
	private GameObject howToKansio;

	private int vaikeus = 2;
	private int koko = 2;


	void Start() {
        sounds = GameObject.Find("Sounds").GetComponent<Sounds>();

		loader = GameObject.Find ("Loader").GetComponent<Loader> ();

		paaKansio = GameObject.Find ("PaaMenu");
		startButton = GameObject.Find ("StartGameButton").GetComponent<Button> ();
		optionsButton = GameObject.Find ("OptionsButton").GetComponent<Button> ();
		storyButton = GameObject.Find ("StoryButton").GetComponent<Button> ();
		howToButton = GameObject.Find ("HowToButton").GetComponent<Button> ();

		optionKansio = GameObject.Find ("OptionsMenu");
		diffiButton = GameObject.Find ("DifficultyButton").GetComponent<Button> ();
		sizeButton = GameObject.Find ("SizeButton").GetComponent<Button> ();

		storyKansio = GameObject.Find ("StoryMenu");
		backButton = GameObject.Find ("BackButton").GetComponent<Button> ();
		//skrolli = GameObject.Find ("Scrollbar").GetComponent<Scrollbar> ();

		sizeKansio = GameObject.Find ("SizeMenu");
		pieniButton = GameObject.Find ("PieniButton").GetComponent<Button> ();
		mediumButton = GameObject.Find ("MediumButton").GetComponent<Button> ();
		suuriButton = GameObject.Find ("HugeButton").GetComponent<Button> ();

		vaikeusKansio = GameObject.Find ("VaikeusMenu");
		helppoButton = GameObject.Find ("HelppoButton").GetComponent<Button>();
		normiButton = GameObject.Find ("NormiButton").GetComponent<Button> ();
		vaikeaButton = GameObject.Find ("VaikeaButton").GetComponent<Button> ();

		howToKansio = GameObject.Find ("HowToMenu");
		backButton2 = GameObject.Find ("BackButton2").GetComponent<Button> ();

		helppoButton.onClick.AddListener (() => (VaikeusAste (1)));
		normiButton.onClick.AddListener (() => (VaikeusAste (2)));
		vaikeaButton.onClick.AddListener (() => (VaikeusAste (3)));
		startButton.onClick.AddListener(() => (AlotaPeli(loader)));
		optionsButton.onClick.AddListener(() => (Valinnat()));
		storyButton.onClick.AddListener (() => (Tarina ()));	
		howToButton.onClick.AddListener (() => (MitenPelata ()));
		diffiButton.onClick.AddListener (() => (Vaikeus ()));
		sizeButton.onClick.AddListener (() => (Koko ()));
		backButton.onClick.AddListener (() => {
			storyKansio.gameObject.SetActive (false);
			paaKansio.gameObject.SetActive (true);
		});
		backButton2.onClick.AddListener (() => {
			howToKansio.gameObject.SetActive (false);
			paaKansio.gameObject.SetActive (true);
		});
		pieniButton.onClick.AddListener (() => (SetKoko (1)));
		mediumButton.onClick.AddListener (() => (SetKoko (2)));
		suuriButton.onClick.AddListener (() => (SetKoko (3)));

		storyKansio.gameObject.SetActive (false);
		optionKansio.gameObject.SetActive (false);
		sizeKansio.gameObject.SetActive (false);
		vaikeusKansio.gameObject.SetActive (false);
		howToKansio.gameObject.SetActive (false);



	}

	void AlotaPeli(Loader loader) {
		loader.SetKoko (koko);
		loader.SetVaikeus (vaikeus);

		SceneManager.LoadScene ("main");

        Aani();
	}

	void Valinnat() {
		paaKansio.gameObject.SetActive (false);
		optionKansio.gameObject.SetActive (true);
        Aani();
	
	}

	void Tarina() {
		paaKansio.gameObject.SetActive (false);
		storyKansio.gameObject.SetActive (true);

        Aani();
	}

	void VaikeusAste(int a) {
		vaikeusKansio.gameObject.SetActive (false);
		paaKansio.gameObject.SetActive (true);
		this.vaikeus = a;
        Aani();
    }

	void MitenPelata() {
		paaKansio.gameObject.SetActive (false);
		howToKansio.gameObject.SetActive (true);

        Aani();
	}

	void Vaikeus () {
		optionKansio.gameObject.SetActive (false);
		vaikeusKansio.gameObject.SetActive (true);
        Aani();
	}

	void Koko () {
		optionKansio.gameObject.SetActive (false);
		sizeKansio.gameObject.SetActive (true);
        Aani();
	}
	void SetKoko (int a) {
		sizeKansio.gameObject.SetActive (false);
		paaKansio.gameObject.SetActive (true);
		this.koko = a;
        Aani();
	}
   
    void Aani() {
        sounds.PlaySound(nappi);
    }
		

}