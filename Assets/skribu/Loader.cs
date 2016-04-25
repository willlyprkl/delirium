using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	private int vaikeus;
	private int koko;

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
