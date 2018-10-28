using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float mass = 1.0f;
    public float velocity = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, velocity * Time.deltaTime);	
	}

    public void Fire(float impulse)
    {
        velocity = impulse / mass;
    }
}
