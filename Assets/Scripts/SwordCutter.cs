using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class SwordCutter : MonoBehaviour {

	public Material capMaterial;
    public SteamVR_TrackedController controller;
    public SteamVR_TrackedObject trackObj;

	// Use this for initialization
	//void Start () {

		
	//}
	
	//void Update(){

	//	if(Input.GetMouseButtonDown(0)){
	//		RaycastHit hit;

	//		if(Physics.Raycast(transform.position, transform.forward, out hit)){

	//			GameObject victim = hit.collider.gameObject;

	//			GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

	//			if(!pieces[1].GetComponent<Rigidbody>())
	//				pieces[1].AddComponent<Rigidbody>();

	//			Destroy(pieces[1], 1);
	//		}

	//	}
	//}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.5f);

	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;

        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

        if (!pieces[1].GetComponent<Rigidbody>())
        {
            pieces[1].AddComponent<Rigidbody>();
            MeshCollider temp= pieces[1].AddComponent<MeshCollider>();
            temp.convex = true;
        }
        StartCoroutine(LongVibration(.2f,.2f));
        //Destroy(pieces[1], 1);

    }

    //SteamVR_Controller.Input([the index of the controller you want to vibrate]).TriggerHapticPulse([length in microseconds as ushort]);

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)trackObj.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
        yield return null;
    }
}
}
