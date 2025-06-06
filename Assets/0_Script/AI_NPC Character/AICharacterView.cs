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

   [SerializeField] EmployeeView employeeView;



    // Start is called before the first frame update
    void Start()
    {
        var npcDepenpencies = new NPCDepenpencies
        {
            GetEmployeeView = employeeView
        };

        //controller = new AICharacterController(NPCCharacterModel, waypoints, moveSpeed, rotationSpeed, npcDepenpencies);

        var model = new AICharacterModel(NPCCharacterModel, waypoints, moveSpeed, rotationSpeed);
        controller = new AICharacterController(this, model, npcDepenpencies);
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
