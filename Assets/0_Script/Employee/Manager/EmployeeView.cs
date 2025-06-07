using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class EmployeeView : MonoBehaviour
{

    [Header("Employee View")]
    [SerializeField] GameObject employeeCharacterModel;
    [SerializeField] int speed;
    //[SerializeField] Rigidbody rigidbody;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] TextMeshProUGUI bankBalanceText;
    [SerializeField] TextMeshProUGUI targetBankBalanceText;
    [SerializeField] GameLevel currentLevel;
    [SerializeField] EmployeeType employeeType;

    private EmployeeController employeeController;

    [SerializeField] ProductSO product; 
    [SerializeField] List<ProductSO> cart;
    [SerializeField] Rack nearbyRack = null;
    [SerializeField] TextMeshProUGUI productCountText;



    [SerializeField] List<Transform> waypoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        productCountText.text = "0";
        var model = new EmployeeModel(employeeCharacterModel, speed, currentLevel, employeeType, cart, nearbyRack, product, waypoints);
        employeeController = new EmployeeController(this, model);
    }



    // Update is called once per frame
    void Update()
    {
        employeeController.update();
    }

    private void FixedUpdate()
    {
        employeeController.FixedUpdate();
    }

    private void OnCollisionStay(Collision collision)
    {
        employeeController.OnCollisionStay(collision);
    }

    private void OnCollisionEnter(Collision collision)
    {
        employeeController.OnCollisionEnter(collision);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(employeeCharacterModel.transform.position, 0.4f);
    }

   // public Rigidbody Rigidbody => rigidbody;

    public FixedJoystick Joystick => joystick;

    public TextMeshProUGUI BankBalanceText => bankBalanceText;

    public TextMeshProUGUI TargetBankBalanceText => targetBankBalanceText;

    public GameLevel CurrentLevel => currentLevel;
    public void SetCurrentLevel(GameLevel level)
    {
        currentLevel = level;
        
    }

    public EmployeeType EmployeeType => employeeType;

    public EmployeeController GetEmployeeController => employeeController;

   
    public List<ProductSO> Cart
    {
        get => cart;
        set => cart = value;
    }
    public ProductSO Product
    {
        get => product;
        set => product = value;
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

}
