using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

    private int hp = 2;

    public int GetHp() {
        return this.hp;
    }

    public void VahennaHp() {
        hp--;
    }
}

