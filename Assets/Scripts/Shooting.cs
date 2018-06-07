using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {

	public int damage = 1;
	public float fireRate = 0.016f;
	public float weaponRange = 100000000f;
	public float hitForce = 100f;

	private Camera fpsCamera;

	public AudioSource gunAudio;

	private float nextFire;

	public int magazineSize = 30;
	private int bulletsRemaining = 30;
	public AudioSource reloadAudio;

	public Text magazineStatus; 

	private bool reloading = false;



	public Recoil recoilComponent;
	private Recoil recoilref = null;

	private float recoil = 0.0f;
	private float maxRecoil_x = -20f;
	private float maxRecoil_y = 10f;
	private float recoilSpeed = 2f;

	// Use this for initialization
	void Start () {
		gunAudio = GetComponent<AudioSource> ();
		fpsCamera = GetComponentInParent<Camera> ();

		//recoilref = recoilComponent.GetComponent<Recoil> ();
		//recoilref.SetRecoil (0.2f,-10f,10f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire && magazineSize > 0 && reloading == false) 
		{

			nextFire = Time.time + fireRate;
			gunAudio.Play ();
			Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint (new Vector3(0.5f,0.5f,0.0f));
			RaycastHit hit;

			updateMagazine ();


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

		if(Input.GetKeyDown(KeyCode.R) && bulletsRemaining < magazineSize)
		{
			StartCoroutine(reloadGun ());
		}

	}



	void updateMagazine()
	{
		bulletsRemaining--;

		if(bulletsRemaining <= 0)
		{
			StartCoroutine (reloadGun());
		}
		updateMagazineSize ();
	}

	IEnumerator reloadGun()
	{	
		reloading = true;
		reloadAudio.Play ();
		yield return new WaitForSeconds (1);
		bulletsRemaining = magazineSize;
		updateMagazineSize ();
		reloading = false;
	}

	void updateMagazineSize()
	{
		magazineStatus.text = bulletsRemaining + "/" + magazineSize;
	}
}
