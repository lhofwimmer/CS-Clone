using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
	public GameObject recoilMod;
	public GameObject weapon;
	public float maxRecoil_x = -20f;
	public float recoilSpeed = 10f;
	public float recoil = 0.0f;
	private Camera recoilCam;

	void Start()
	{
		recoilCam = recoilMod.GetComponent<Camera> ();
	}

	void Update()
	{
		if(Input.GetButton ("Fire1"))
		{
			recoil += 0.1f;
		}
		recoiling ();
	}

	void recoiling()
	{
		if(recoil > 0)
		{
			Quaternion maxRecoil = Quaternion.Euler (maxRecoil_x, 0, 0);
			recoilCam.transform.rotation = Quaternion.Slerp (recoilCam.transform.rotation, maxRecoil, Time.deltaTime * recoilSpeed);
			//weapon.transform.localEulerAngles.x = recoilCam.transform.localEulerAngles.x;
			recoil -= Time.deltaTime;
		}
		else
		{
			recoil = 0;
			Quaternion minRecoil = Quaternion.Euler (0, 0, 0);
			recoilCam.transform.rotation = Quaternion.Slerp (recoilCam.transform.rotation , minRecoil, Time.deltaTime * recoilSpeed);
			//weapon.transform.localEulerAngles.x = recoilCam.transform.localEulerAngles.x;
		}
	}
}
