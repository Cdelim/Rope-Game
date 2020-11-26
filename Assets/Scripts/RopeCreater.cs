using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCreater : MonoBehaviour
{
    [SerializeField]GameObject partPrefab,parentObject; 
    [SerializeField]GameObject buttonPrefab; 
    int lenght=5;
    [SerializeField]
    bool spawn=false;
    float partDistance=0.2f;
    void Start(){
        parentObject=new GameObject();
        parentObject.transform.position=new Vector3(0,0,0);
    }

    
    void Update()
    {
        if(spawn){
            Spawn();
            spawn=false;
        }
    }

    public void Spawn(){

        int count=(int)(lenght/partDistance);
        for(int i=0;i<count;i++){
            GameObject temp;
            temp=Instantiate(partPrefab,new Vector3(transform.position.x,transform.position.y+partDistance*(-i),transform.position.z),Quaternion.identity,parentObject.transform);
            temp.transform.eulerAngles=new Vector3(180,0,0);
            temp.name=parentObject.transform.childCount.ToString();
            switch(i){
                case 0:
                    Destroy(temp.GetComponent<CharacterJoint>());
                    temp.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
                    break;
                default:
                    temp.GetComponent<CharacterJoint>().connectedBody=parentObject.transform.GetChild(i-1).GetComponent<Rigidbody>();
                    break;
            }
           /* if(i==0){
                Destroy(temp.GetComponent<CharacterJoint>());
                temp.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
            }
            else{
                temp.GetComponent<CharacterJoint>()
            }*/
        }
        //parentObject.transform.GetChild(count-1).GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll; //Freze last node
                    
        buttonPrefab.GetComponent<CharacterJoint>().connectedBody=parentObject.transform.GetChild(count-1).GetComponent<Rigidbody>();
        GameObject button=Instantiate(buttonPrefab,new Vector3(transform.position.x,parentObject.transform.GetChild(count-1).transform.position.y+partDistance,transform.position.z),Quaternion.identity,parentObject.transform);
        button.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
        button.transform.position=new Vector3(0,1,1);
    }
}
