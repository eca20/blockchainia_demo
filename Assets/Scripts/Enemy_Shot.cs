using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shot : MonoBehaviour {

	public void GotShot() {
		Enemy_Movement behavior = GetComponent<Enemy_Movement> ();
		if (behavior != null) {
			behavior.SetAlive (false);
		}

		StartCoroutine (Die ());
	}

	private IEnumerator Die() {
		this.transform.Rotate (-75, 0, 0);

		yield return new WaitForSeconds (1.5f);

		Destroy (this.gameObject);
	}

}
