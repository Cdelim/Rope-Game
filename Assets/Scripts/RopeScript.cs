using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
//[RequireComponent (typeof (LineRenderer))]
 
public class RopeScript : MonoBehaviour {
	
 
	public Transform target;
	public float resolution = 0.5F;							  
	public float ropeDrag = 0.1F;								 
	public float ropeMass = 0.1F;							
	public float ropeColRadius = 0.3F;					
	//public float ropeBreakForce = 25.0F;					 
	private Vector3[] segmentPos;
    [HideInInspector]
	public GameObject[] joints;
    public LayerMask ropeMask;
	//private LineRenderer line;							
	private int segments = 0;
    private bool rope = false;					 
    		

    [SerializeField] GameObject prefab;
    [SerializeField] Material matOfPrefab;

    void Awake()
	{
        BuildRope();
	}
 
 
 
 
	void BuildRope()
	{
        segments = (int)(Vector3.Distance(transform.position,target.position)*resolution);
		segmentPos = new Vector3[segments];
		joints = new GameObject[segments];
		segmentPos[0] = transform.position;
		segmentPos[segments-1] = target.position;
 
		var segs = segments-1;
		var seperation = ((target.position - transform.position)/segs);
 
		for(int s=1;s < segments;s++)
		{
			Vector3 vector = (seperation*s) + transform.position;	
			segmentPos[s] = vector;
            AddJointPhysics(s);
		}
 
		SpringJoint end = target.gameObject.AddComponent<SpringJoint>();
        end.anchor = Vector3.zero;
		end.connectedBody = joints[joints.Length-1].transform.GetComponent<Rigidbody>();
		target.parent = transform;
 
		rope = true;
	}
 
	void AddJointPhysics(int n)
	{
		joints[n] = Instantiate(prefab, segmentPos[n], Quaternion.identity);
        joints[n].name= transform.name+"Joint_" + n;
        joints[n].GetComponent<MeshRenderer>().material = matOfPrefab;
        joints[n].layer = (int)Mathf.Log(ropeMask.value, 2);
        //joints[n] = new GameObject("Joint_" + n);
        joints[n].transform.parent = transform;
		Rigidbody rigid = joints[n].AddComponent<Rigidbody>();
		SphereCollider col = joints[n].AddComponent<SphereCollider>();
        SpringJoint ph = joints[n].AddComponent<SpringJoint>();
        ph.spring = 20;
        ph.anchor = new Vector3(0, 0, 0);

        //joints[n].layer = LayerMask.GetMask("End");
		joints[n].transform.position = segmentPos[n];
        //GameObject temp=Instantiate(prefab, segmentPos[n], Quaternion.identity, joints[n].transform);
        
 
		rigid.drag = ropeDrag;
		rigid.mass = ropeMass;
		col.radius = ropeColRadius;
 
		if(n==1){		
			ph.connectedBody = transform.GetComponent<Rigidbody>();
		} else
		{
			ph.connectedBody = joints[n-1].GetComponent<Rigidbody>();	
		}
 
	}
 
	public void DestroyRope()
	{
		rope = false;
		for(int dj=0;dj<joints.Length;dj++)
		{
			Destroy(joints[dj]);	
		}
        //Destroy(target);
        Destroy(gameObject);
		segmentPos = new Vector3[0];
		joints = new GameObject[0];
		segments = 0;
	}
}

