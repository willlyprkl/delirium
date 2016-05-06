using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	private int vaikeus;
	private int koko;

    /*
     * Loader säilyttää vaikeusasteen ja mapin koon tiedot,
     *  jotka annetaan gamemanagerille mapin generoimista varten.
     */
	void Start () {
		DontDestroyOnLoad (this);

	}

	public void SetVaikeus(int a) {
		vaikeus = a;
	}

	public void SetKoko(int a){
		koko = a;
	}

	public int GetVaikeus() {
		return vaikeus;
	}

	public int GetKoko() {
		return koko;
	}
}
