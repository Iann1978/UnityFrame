using UnityEngine;
using System.Collections;
using Fps;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AnimationScript : MonoBehaviour {

    float animationDampTime = 0.15f;
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    BaseAIParameters param;

    public Transform bulletAnchor;
    public GameObject bulletPrefab;
	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        param = GetComponent<BaseAIParameters>();
        //agent.updateRotation = false;
    }
	
    public void Shoot()
    {
        Shooter shooter = GetComponent<Shooter>();
        if (shooter)
        {
            shooter.Fire();
        }

    }
	// Update is called once per frame
	void Update ()
    {
        float forward = Vector3.Dot(agent.velocity, transform.forward);
        float horizontal = Vector3.Dot(agent.velocity, transform.right);
        animator.SetFloat("Forwards", forward);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetBool("Engaging", param.shotting);
        animator.SetBool("Crouching", param.crouching);

        GameObject mechineGun = GameObject.Find("MachineGun");
        RotateToAimGunScript rotateToAimGunScript = GetComponent<RotateToAimGunScript>();
        rotateToAimGunScript.isEnabled = param.shotting;
        rotateToAimGunScript.targetTransform = mechineGun.transform;

        if (param.fire)
        {
            //animator.SetTrigger("Fire");
            param.fire = false;



            Invoke("Shoot", 1.0f);
            //GameObject bullet = Instantiate<GameObject>(bulletPrefab);
            //bullet.transform.position = bulletAnchor.position;
            //bullet.transform.rotation = bulletAnchor.rotation;
            //Rigidbody body = bullet.GetComponent<Rigidbody>();
            //body.AddForce(bulletAnchor.forward * 10.0f, ForceMode.VelocityChange);
        }

        //animator.SetFloat("Forwards", Vector3.Dot(myAIBodyTransform.forward, agent.speed) / maxMovementSpeed, animationDampTime, Time.deltaTime);


    }
}
