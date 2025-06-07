using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] ProductSO product;
    [SerializeField] List<ProductSO> cart;
    [SerializeField] Rack nearbyRack ;
    [SerializeField] TextMeshProUGUI productCountText;
    [SerializeField] Image productImage;

    // Start is called before the first frame update
    void Start()
    {
        var npcDepenpencies = new NPCDepenpencies
        {
            GetEmployeeView = employeeView
        };

       

        var model = new AICharacterModel(NPCCharacterModel, waypoints, moveSpeed, rotationSpeed, product, nearbyRack, cart);
        controller = new AICharacterController(this, model, npcDepenpencies);
    }

    // Update is called once per frame
    void Update()
    {
        controller.Update();
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("NPC collision happened");
        controller.OnCollisionStay(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        controller.OnTriggerEnter(other);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(NPCCharacterModel.transform.position, 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(NPCCharacterModel.transform.position, 1.1f);
    }


    public ProductSO Product
    {
        get => product;
        set => product = value;
    }

    public List<ProductSO> Cart
    {
        get => cart;
        set => cart = value;
    }

    public Rack NearbyRack
    {
        get => nearbyRack;
        set => nearbyRack = value;
    }

    public TextMeshProUGUI ProductCountText
    {
        get => productCountText;
        set => productCountText = value;
    }

    public Image ProductImage
    {
        get => productImage;
        set => productImage = value;
    }

}
