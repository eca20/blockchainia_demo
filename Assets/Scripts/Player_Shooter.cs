using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooter : MonoBehaviour {
	private Camera playerCam;

	void Start () {
		playerCam = GetComponent<Camera> ();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 point = new Vector3 (playerCam.pixelWidth / 2, playerCam.pixelHeight / 2, 0);
			Ray ray = playerCam.ScreenPointToRay (point);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject;

				Enemy_Shot target = hitObject.GetComponent<Enemy_Shot> ();

				if (target != null) {
					target.GotShot ();
				} else {
					StartCoroutine (ShotGen (hit.point));	//Launch a coroutine in response to a hit!
				}

			}
		}
	}

	private IEnumerator ShotGen(Vector3 pos) {
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
		sphere.transform.position = pos;

		yield return new WaitForSeconds (1);

		Destroy (sphere);
	}

	void OnGUI() {
		int size = 12;
		float posX = playerCam.pixelWidth / 2 - size / 4;
		float posY = playerCam.pixelHeight / 2 - size / 2;
		GUI.Label (new Rect (posX, posY, size, size), "*");
	}
}
