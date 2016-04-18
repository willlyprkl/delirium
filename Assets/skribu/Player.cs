using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : GameController {

    public int hp;
    public int attack;

    private Button wButton;
    private Button eButton;
    private Button sButton;
    private Button nButton;
    private Text hpText;
    private Text dmgText;


    void Start(){
        hp = 100;
        attack = 50;
        wButton = (Button)GameObject.Find("wButton").GetComponent<Button>();
        eButton = (Button)GameObject.Find("eButton").GetComponent<Button>();
        sButton = (Button)GameObject.Find("sButton").GetComponent<Button>();
        nButton = (Button)GameObject.Find("nButton").GetComponent<Button>();
        hpText = (Text)GameObject.Find("hpText").GetComponent<Text>();
        dmgText = (Text)GameObject.Find("dmgText").GetComponent<Text>();

        wButton.onClick.AddListener(() => (Liiku(-1, "ho", this)));
        eButton.onClick.AddListener(() => (Liiku(1, "ho", this))); 
        sButton.onClick.AddListener(() => (Liiku(-1, "ve", this)));
        nButton.onClick.AddListener(() => (Liiku(1, "ve", this)));

    }

    public void LisaaHp(int a) {
        hp += a;
        HpText();
    }

    public void VahennaHp(int a){
        hp -= a;
        HpText();
    }

    public void LisaaDmg(int a) {
        attack += a;
        DmgText();
    }

    void HpText() {
        hpText.text = "HP: " + hp;
    }

    void DmgText() {
        dmgText.text = "DMG: " + attack;
    }

}
