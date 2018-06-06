using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public int damage = 1;
	public float fireRate = 0.016f;
	public float weaponRange = 100000000f;
	public float hitForce = 100f;

	private Camera fpsCamera;

	public AudioSource gunAudio;

	private float nextFire;

	public int magazineSize = 30;
	public AudioSource reloadAudio;

	// Use this for initialization
	void Start () {
		gunAudio = GetComponent<AudioSource> ();
		fpsCamera = GetComponentInParent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			if (magazineSize <= 0) {

			}

			nextFire = Time.time + fireRate;
			gunAudio.Play ();
			Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint (new Vector3(0.5f,0.5f,0.0f));
			RaycastHit hit;
			magazineSize -= 1;



			if (Physics.Raycast (rayOrigin, fpsCamera.transform.forward, out hit, weaponRange)) 
			{
				ShootableObj health = hit.collider.GetComponent<ShootableObj> ();

				if (health != null) {
					health.Damage (damage);
				}

				if (hit.rigidbody != null) {
					hit.rigidbody.AddForce (-hit.normal * hitForce);
				}
			}
		}
	}


}
