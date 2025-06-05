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

    public EmployeeController employeeController;


    // Start is called before the first frame update
    void Start()
    {
        var npcDepenpencies = new NPCDepenpencies
        {
            employeeController = employeeController
        };

        controller = new AICharacterController(NPCCharacterModel, waypoints, moveSpeed, rotationSpeed, npcDepenpencies);
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(NPCCharacterModel.transform.position, 1.1f);
    }

}
