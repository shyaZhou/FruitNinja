using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class SwordCutter : MonoBehaviour {

	public Material capMaterial;
    //public SteamVR_TrackedController controller;
    public SteamVR_TrackedObject trackObj;
    public CubeTest ct;
    public GameObject[] pieces;
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("<color=blue>OnTriggerEnter:</color>" + other);
        Debug.Log("fruit?" + other.gameObject.GetComponent<Fruit>());
        Debug.Log(this.tag);
        if (other.gameObject.GetComponent<Fruit>() != null && this.tag.Equals("Weapon") && !ct.isCollision)
        {
            Debug.Log(!ct.isCollision);
            Debug.Log("<color=red>OnTriggerEnter With Fruit</color>");
            AudioSource audio = other.gameObject.GetComponent<AudioSource>();
            audio.Play();
            ParticleSystem ps = other.transform.Find("FX").GetComponent<ParticleSystem>();
            ps.Play();
            GameObject victim = other.gameObject;

            pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

            //if (!pieces[1].GetComponent<Rigidbody>())
            //{
            //    pieces[1].AddComponent<Rigidbody>();
            //    MeshCollider temp = pieces[1].AddComponent<MeshCollider>();
            //    temp.convex = true;
            //}
            if (!pieces[1].GetComponent<Rigidbody>())
            {
                pieces[1].AddComponent<Rigidbody>();
            }
            foreach (var item in pieces)
            {
                Destroy(item.GetComponent<MeshCollider>());
                Destroy(item, 3f);
            }
            StartCoroutine(LongVibration(.2f, .2f));
            //Destroy(pieces[1], 1);

        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(this.tag);
    //    if (collision.gameObject.GetComponent<Fruit>() != null && this.tag == "Weapon")
    //    {
    //        Debug.Log("<color=blue>OnTriggerEnter</color>");
    //        AudioSource audio = collision.gameObject.GetComponent<AudioSource>();
    //        audio.Play();
    //        ParticleSystem ps = collision.transform.Find("FX").GetComponent<ParticleSystem>();
    //        ps.Play();
    //        GameObject victim = collision.collider.gameObject;

    //        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

    //        //if (!pieces[1].GetComponent<Rigidbody>())
    //        //{
    //        //    pieces[1].AddComponent<Rigidbody>();
    //        //    MeshCollider temp = pieces[1].AddComponent<MeshCollider>();
    //        //    temp.convex = true;
    //        //}
    //        if (!pieces[1].GetComponent<Rigidbody>())
    //        {
    //            pieces[1].AddComponent<Rigidbody>();
    //        }
    //        foreach (var item in pieces)
    //        {
    //            Destroy(item.GetComponent<MeshCollider>());
    //            Destroy(item, 3f);
    //        }
    //        StartCoroutine(LongVibration(.2f, .2f));
    //        //Destroy(pieces[1], 1);
    //    }
    //}

    //SteamVR_Controller.Input([the index of the controller you want to vibrate]).TriggerHapticPulse([length in microseconds as ushort]);

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)trackObj.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
        }
            yield return null;
    }
}
