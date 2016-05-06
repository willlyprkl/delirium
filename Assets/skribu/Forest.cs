using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {
    
    //Puiden kaatuminen parista lyönnistä.

    private int hp = 2;

    public int GetHp() {
        return this.hp;
    }

    public void VahennaHp() {
        hp--;
    }
}

