using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AICharacterView : MonoBehaviour
{

    private AICharacterController controller;
    public  GameObject NPCCharacterModel;
    public  List<Transform> waypoints = new List<Transform>();

    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public float rotationSpeed = 5f;
    public float buyingRadious = 5f;
 

    // Start is called before the first frame update
    void Start()
    {
        controller = new AICharacterController(NPCCharacterModel, waypoints, moveSpeed, rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        controller.Update();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(NPCCharacterModel.transform.position, 0.3f);
    }

}
