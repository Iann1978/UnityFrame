using UnityEngine;
using System.Collections;
using Fps;

public class BaseAIParameters : MonoBehaviour {

    public enum Character
    {
        Moderate,
        Cowardly,
    };

    public Character character = Character.Moderate;


    // 目标和掩体
    public ITarget target;
    public Vector3 memTargetPosition;   // 记忆中的目标位置
    public float disToMemeyTarget;      // 到记忆中的目标的位置




    public ICover cover;
    
    // 当前要去的地点
    public bool destinationEnable = true;
    public Vector3 destination = Vector3.zero;

    public bool shotting = false;
    public bool crouching = false;

    public bool fire = false;


    public float endSeekingDistance = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
