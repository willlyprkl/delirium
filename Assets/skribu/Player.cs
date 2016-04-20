using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : GameController {
    // Pelaajan ominaisuudet
    public int hp;
    public int attack;

    // UI:n tekstit ja näppäimet
    private Button wButton;
    private Button eButton;
    private Button sButton;
    private Button nButton;
    private Text hpText;
    private Text dmgText;


    void Start(){
        // Alustetaan statsit, haetaan napit ja tekstit
        hp = 100;
        attack = 50;
        wButton = (Button)GameObject.Find("wButton").GetComponent<Button>();
        eButton = (Button)GameObject.Find("eButton").GetComponent<Button>();
        sButton = (Button)GameObject.Find("sButton").GetComponent<Button>();
        nButton = (Button)GameObject.Find("nButton").GetComponent<Button>();
        hpText = (Text)GameObject.Find("hpText").GetComponent<Text>();
        dmgText = (Text)GameObject.Find("dmgText").GetComponent<Text>();

        // Asetetaan liikkuminen
        wButton.onClick.AddListener(() => (Liiku(-1, "ho", this)));
        eButton.onClick.AddListener(() => (Liiku(1, "ho", this))); 
        sButton.onClick.AddListener(() => (Liiku(-1, "ve", this)));
        nButton.onClick.AddListener(() => (Liiku(1, "ve", this)));

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

}
