using UnityEngine;

public class ExplosionForce : MonoBehaviour {

	public GameObject explosion;	
	public Camera camera;

	public void fire ()
	{
		RaycastHit raycast;

		if (Physics.Raycast(camera.transform.position, camera.transform.forward, out raycast))
			Instantiate(explosion, raycast.point, Quaternion.LookRotation(raycast.normal));
	}

}
