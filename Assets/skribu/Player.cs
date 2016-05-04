using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    
    // Pelaajan ominaisuudet
    private int hp;
    private int attack;
    private int juotuMaara = 0;
    private int tapot = 0;
	private int puutkaatuu = 0;
	private int syotyMetsa = 0;
    public int aaniValinta;
    public int askeleet = 0;

    // UI:n tekstit ja näppäimet
    private Button wButton;
    private Button eButton;
    private Button sButton;
    private Button nButton;
    private Text hpText;
    private Text dmgText;
    private Text tapotText;

    // Äänet
    public AudioClip[] puu;
    public AudioClip[] lyonti;
    public AudioClip[] kuole;
    public AudioClip[] royhtays;
    public AudioClip[] heng;
    public AudioClip[] darra;
    public AudioClip[] huhhuh;
    public AudioClip[] jumalauta;
    public AudioClip[] saatana;
    public AudioClip[] perkele;
    public AudioClip[] juoma;
    public AudioClip[] hukassa;
    public AudioClip syonti;
    public AudioClip vaatteet;
    public AudioClip move;
    public AudioClip ase;

    // Pelaajan animaattori, gamecontroller liikuttamista varten
    public Animator animator;
	private GameController gc;

    // Estää pelaajan liiaallisen liikkumisen
    public bool playerMoving = false;


    void Start(){
        // Alustetaan statsit, haetaan napit ja tekstit
        hp = 50;
        attack = 20;
        aaniValinta = Random.Range(0, 3);
        animator = GetComponent<Animator>();
		gc = GameObject.Find ("GameController").GetComponent<GameController> ();

        wButton = (Button)GameObject.Find("wButton").GetComponent<Button>();
        eButton = (Button)GameObject.Find("eButton").GetComponent<Button>();
        sButton = (Button)GameObject.Find("sButton").GetComponent<Button>();
        nButton = (Button)GameObject.Find("nButton").GetComponent<Button>();
        hpText = (Text)GameObject.Find("hpText").GetComponent<Text>();
        dmgText = (Text)GameObject.Find("dmgText").GetComponent<Text>();
        tapotText = (Text)GameObject.Find("tapotText").GetComponent<Text>();

        // Asetetaan liikkuminen
		wButton.onClick.AddListener(() => (gc.Liiku(-1, "ho", this)));
		eButton.onClick.AddListener(() => (gc.Liiku(1, "ho", this))); 
		sButton.onClick.AddListener(() => (gc.Liiku(-1, "ve", this)));
		nButton.onClick.AddListener(() => (gc.Liiku(1, "ve", this)));

        HpText();
        DmgText();
    }

    // HP:n lisäys
    public void LisaaHp(int a) {
        hp += a;
        HpText();
    }

    // HP:n vähennys
    public void VahennaHp(int a){
        hp -= a;
        if (hp <= 0)
            hp = 0;
        HpText();
    }

    // Damagen kasvatus (itemit)
    public void LisaaDmg(int a) {
        attack += a;
        DmgText();
    }

    // HP-textin päivitys
    void HpText() {
        hpText.text = "HP: " + hp;
    }

    // DMG-textin päivitys
    void DmgText() {
        dmgText.text = "DMG: " + attack;
    }

    public int GetHealth() {
        return this.hp;
    }

    public int GetDamage() {
        return this.attack;
    }

    // Juotujen juomien laskuri, achievementit
	public void Juotu () {
		juotuMaara++;
		if (juotuMaara == 5) {
			LisaaHp (10);
			LisaaDmg (5);
			Logger.Lisaa ("+++++ DRUNKENMASTER SURVIVOR ACHIEVEMENT: STAGE POWER FISTS +++++");
		}
		if (juotuMaara == 10) {
			LisaaHp (20);
			LisaaDmg (5);
			Logger.Lisaa ("+++++ DRUNKENMASTER SURVIVOR ACHIEVEMENT: UNLEASHED INNER BEAST +++++");
		}
	}

    public void Tappo() {
        tapot++;
        tapotText.text = "Kills: " + tapot;
    }
	public void puuIsDead() {
		puutkaatuu++;
		if (puutkaatuu == 20) {
			LisaaDmg (10);
			Logger.Lisaa ("+++++ WOODCUTTER SURVIVOR ACHIEVEMENT: WOODY WOODPECKER +++++");
		}
	}
	public void syoMetsa() {
		syotyMetsa++;
		if (syotyMetsa == 10) {
			LisaaHp (10);
			LisaaDmg (5);
			Logger.Lisaa ("+++++ MUSHROOMS SURVIVOR ACHIEVEMENT: SECRET PSYCHEDELIC TRIP +++++");
		}
	}
}
