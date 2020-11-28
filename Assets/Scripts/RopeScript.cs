using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
 
// Require a Rigidbody and LineRenderer object for easier assembly
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (LineRenderer))]
 
public class RopeScript : MonoBehaviour {
	
 
	public Transform target;
	public float resolution = 0.5F;							  
	public float ropeDrag = 0.1F;								 
	public float ropeMass = 0.1F;							
	public float ropeColRadius = 0.3F;					
	//public float ropeBreakForce = 25.0F;					 
	private Vector3[] segmentPos;			
	private GameObject[] joints;			
	private LineRenderer line;							
	private int segments = 0;					
	private bool rope = false;						 
    
	//Joint Settings
	/*public Vector3 swingAxis = new Vector3(1,1,1);				 
	public float lowTwistLimit = -100.0F;					
	public float highTwistLimit = 100.0F;					
	public float swing1Limit  = 20.0F;		*/			

    [SerializeField] GameObject prefab;
    [SerializeField] Material matOfPrefab;

    void Awake()
	{
        BuildRope();
	}
 
	void LateUpdate()
	{
		/*if(rope) {
			for(int i=0;i<segments;i++) {
				if(i == 0) {
					line.SetPosition(i,transform.position);
				} else
				if(i == segments-1) {
					line.SetPosition(i,target.transform.position);	
				} else {
					line.SetPosition(i,joints[i].transform.position);
				}
			}
			line.enabled = true;
		} else {
			line.enabled = false;	
		}*/
	}
 
 
 
	void BuildRope()
	{
		//line = gameObject.GetComponent<LineRenderer>();
        segments = (int)(Vector3.Distance(transform.position,target.position)*resolution);
		//line.SetVertexCount(segments);
		//line.SetVertexCount(segments);
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
            /*
            GameObject temp;
            temp=Instantiate(partPrefab,new Vector3(parentObject.position.x,vector,parentObject.position.z),Quaternion.identity,parentObject);
            temp.transform.eulerAngles=new Vector3(180,0,0);
            temp.name=parentObject.transform.childCount.ToString();
            switch(i){
                case 0:
                    Destroy(temp.GetComponent<CharacterJoint>());
                    temp.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
                    break;
                default:
                    temp.GetComponent<CharacterJoint>().connectedBody=parentObject.transform.GetChild(s-1).GetComponent<Rigidbody>();
                    break;
           
            
            }*/
            AddJointPhysics(s);
		}
 
		SpringJoint end = target.gameObject.AddComponent<SpringJoint>();
		end.connectedBody = joints[joints.Length-1].transform.GetComponent<Rigidbody>();
		/*end.swingAxis = swingAxis;
		SoftJointLimit limit_setter = end.lowTwistLimit;
		limit_setter.limit = lowTwistLimit;
		end.lowTwistLimit = limit_setter;
		limit_setter = end.highTwistLimit;
		limit_setter.limit = highTwistLimit;
		end.highTwistLimit = limit_setter;
		limit_setter = end.swing1Limit;
		limit_setter.limit = swing1Limit;
		end.swing1Limit = limit_setter;*/
		//target.parent = transform;
 
		// Rope = true, The rope now exists in the scene!
		rope = true;
	}
 
	void AddJointPhysics(int n)
	{
		joints[n] = Instantiate(prefab, segmentPos[n], prefab.transform.rotation);
        joints[n].GetComponent<MeshRenderer>().material = matOfPrefab;
        //joints[n] = new GameObject("Joint_" + n);
        joints[n].transform.parent = transform;
		Rigidbody rigid = joints[n].AddComponent<Rigidbody>();
		SphereCollider col = joints[n].AddComponent<SphereCollider>();
        SpringJoint ph = joints[n].AddComponent<SpringJoint>();
        ph.spring = 20;
        ph.anchor = new Vector3(0, 0, 0);
		//ph.swingAxis = swingAxis;
		/*SoftJointLimit limit_setter = ph.lowTwistLimit;
		limit_setter.limit = lowTwistLimit;
		ph.lowTwistLimit = limit_setter;
		limit_setter = ph.highTwistLimit;
		limit_setter.limit = highTwistLimit;
		ph.highTwistLimit = limit_setter;
		limit_setter = ph.swing1Limit;
		limit_setter.limit = swing1Limit;
		ph.swing1Limit = limit_setter;*/

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
		for(int dj=0;dj<joints.Length-1;dj++)
		{
			Destroy(joints[dj]);	
		}
 
		segmentPos = new Vector3[0];
		joints = new GameObject[0];
		segments = 0;
	}
}

