using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public PlayerConfig pc;

	public Vector3 direction;
	public Vector3 rotation;
	public float yaw;
	public float pitch;

	void Update () {
		this.SetDirection();
	}
	//player movement inputs
	void SetDirection () {
		this.direction = Vector3.zero;

		if (Input.GetKey(this.pc.forward)) {
			this.direction += Vector3.forward;
		} else if (Input.GetKey(this.pc.backward)) {
			this.direction += Vector3.back;
		}

		if (Input.GetKey(this.pc.left)) {
			this.direction += Vector3.left;
		} else if (Input.GetKey(this.pc.right)) {
			this.direction += Vector3.right;
		}

		this.direction = this.direction.normalized;
	}
	//camera rotation inputs
	void SetRotation () {
		yaw = Input.GetAxis("Mouse X") * this.pc.mouseSensitivity;

		pitch = Input.GetAxis ("Mouse Y") * this.pc.mouseSensitivity;

		if (this.pc.invertY) {
			pitch *= -1;
		}

		this.rotation = new Vector3(yaw, 0f, 0f);
		Camera.main.transform.localRotation *= Quaternion.Euler (-pitch, 0, 0);
	}
}