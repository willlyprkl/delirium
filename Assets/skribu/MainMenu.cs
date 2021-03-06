                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private Loader loader;

    private Sounds sounds;
    public AudioClip nappi;

    private Image sade2;

    public Vector2 alku;
    public Vector2 loppu;

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


    private Button exitGameButton;

    private GameObject paaKansio;
    private GameObject optionKansio;
    private GameObject storyKansio;
    private GameObject sizeKansio;
    private GameObject vaikeusKansio;
    private GameObject howToKansio;

    private int vaikeus = 2;
    private int koko = 2;

    float aika;

    void Start() {
        sade2 = GameObject.Find ("Sade2").GetComponent<Image>();

        sounds = GameObject.Find("Sounds").GetComponent<Sounds>();

        loader = GameObject.Find ("Loader").GetComponent<Loader> ();
		exitGameButton = GameObject.Find ("ExitButton").GetComponent<Button> ();

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
            Aani();
        });
        backButton2.onClick.AddListener (() => {
            howToKansio.gameObject.SetActive (false);
            paaKansio.gameObject.SetActive (true);
            Aani();
        });
        pieniButton.onClick.AddListener (() => (SetKoko (1)));
        mediumButton.onClick.AddListener (() => (SetKoko (2)));
        suuriButton.onClick.AddListener (() => (SetKoko (3)));
        exitGameButton.onClick.AddListener (() => (Application.Quit()));

        storyKansio.gameObject.SetActive (false);
        optionKansio.gameObject.SetActive (false);
        sizeKansio.gameObject.SetActive (false);
        vaikeusKansio.gameObject.SetActive (false);
        howToKansio.gameObject.SetActive (false);

        alku = sade2.transform.localPosition;
        loppu = new Vector2 (0, -700);

    }

    void Update() {
        // Viinasadeanimaatio

        if (aika >= 1.0f) {
            aika = 0f;
            sade2.transform.position = alku;
        }
        aika += (Time.deltaTime / 10);

        sade2.transform.localPosition = Vector3.Lerp (alku, loppu, aika);
    }
        
    void AlotaPeli(Loader loader) {
        loader.SetKoko (koko);
        loader.SetVaikeus (vaikeus);
        Aani();
        SceneManager.LoadScene ("main");
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
        sounds.LiikeSound(nappi);
    }


}