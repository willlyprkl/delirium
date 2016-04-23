using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    
    // Pelaajan ominaisuudet
    private int hp;
    private int attack;
    private int juotuMaara = 0;
    private int tapot = 0;

    // UI:n tekstit ja näppäimet
    private Button wButton;
    private Button eButton;
    private Button sButton;
    private Button nButton;
    private Text hpText;
    private Text dmgText;
    private Text tapotText;

    // Pelaajan animaattori, gamecontroller liikuttamista varten
    public Animator animator;
	private GameController gc;

    // Estää pelaajan liiaallisen liikkumisen
    public bool playerMoving = false;


    void Start(){
        // Alustetaan statsit, haetaan napit ja tekstit
        hp = 100;
        attack = 50;
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

    }

    // HP:n lisäys
    public void LisaaHp(int a) {
        hp += a;
        HpText();
    }

    // HP:n vähennys
    public void VahennaHp(int a){
        hp -= a;
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
			LisaaHp (50);
			LisaaDmg (10);
			Logger.Lisaa ("+++++ DRUNKENMASTER SURVIVOR ACHIEVEMENT: STAGE POWER FISTS +++++");
		}
		if (juotuMaara == 10) {
			LisaaHp (50);
			LisaaDmg (10);
			Logger.Lisaa ("+++++ DRUNKENMASTER SURVIVOR ACHIEVEMENT: UNLEASHED INNER BEAST +++++");
		}
	}

    public void Tappo() {
        tapot++;
        tapotText.text = "Kills: " + tapot;
    }
}
