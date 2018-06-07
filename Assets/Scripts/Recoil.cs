using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
	private float recoil = 0.0f;
	private float maxRecoil_x = -20f;
	private float maxRecoil_y = 10f;
	private float recoilSpeed = 2f;

	public void StartRecoil (float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
	{
		// in seconds
		recoil = recoilParam;
		maxRecoil_x = maxRecoil_xParam;
		recoilSpeed = recoilSpeedParam;
		maxRecoil_y = Random.Range(-1, 1);
	}

	private void recoiling ()
	{
		if (recoil > 0f) {

		//Quaternion oldrotation = Quaternion.Euler (Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
		//Quaternion newrotation = Quaternion.Euler (Camera.main.transform.eulerAngles.x+10, 0, Camera.main.transform.eulerAngles.z);

		//Camera.main.transform.localRotation = Quaternion.Slerp (oldrotation, newrotation, 0.5f);
			Quaternion maxRecoil = Quaternion.Euler (maxRecoil_x, maxRecoil_y, 0f);
			// Dampen towards the target rotation
			transform.localRotation = Quaternion.Slerp (transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
			recoil -= Time.deltaTime;
			maxRecoil_x += 10;
		} //else {
			//recoil = 0f;
			// Dampen towards the target rotation
			//transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
		//}
	}

	// Update is called once per frame
	void Update ()
	{
		recoiling ();
	}
}
