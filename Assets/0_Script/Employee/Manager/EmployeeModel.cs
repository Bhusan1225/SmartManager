using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmployeeModel
{
    // Private fields
    private GameObject employeeCharacterModel;
    private float speed = 5f;
    private int bankBalance;
    private int targetBankBalance;
    private bool isManagerInCashCounter;
    private bool isPaymentTransferred;
    private GameLevel currentLevel;
    private EmployeeType employeeType;

    private ProductSO product; // Assign through trigger or manually
    private Rack nearbyRack; // Assign through trigger or manually
    private List<ProductSO> cart = new List<ProductSO>();
    private int productCount;

    private bool isReadyForNewLoop = true;

    //private EmployeeController employeeController;



    private List<Transform> waypoints = new List<Transform>();
    private bool isMoving;
    private int waypointIndex;
    private float moveSpeed = 1f;
    private float rotationSpeed = 5f;
    private bool isLoop;


    public bool IsReadyForNewLoop
    {
        get => isReadyForNewLoop;
        set => isReadyForNewLoop = value;
    }
    public int ProductCount
    {
        get => productCount;
        set => productCount = value;
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

    public GameObject EmployeeCharacterModel
    {
        get => employeeCharacterModel;
        set => employeeCharacterModel = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public int BankBalance
    {
        get => bankBalance;
        set => bankBalance = value;
    }

    public int TargetBankBalance
    {
        get => targetBankBalance;
        set => targetBankBalance = value;
    }

    public bool IsManagerInCashCounter
    {
        get => isManagerInCashCounter;
        set => isManagerInCashCounter = value;
    }

    public bool IsPaymentTransferred
    {
        get => isPaymentTransferred;
        set => isPaymentTransferred = value;
    }

    public GameLevel CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    public EmployeeType EmployeeType
    {
        get => employeeType;
        set => employeeType = value;
    }

    public EmployeeModel(GameObject _model, float _speed, GameLevel _currentLevel, EmployeeType _employeeType, List<ProductSO> _cart, Rack _nearbyRack, ProductSO _product, List<Transform> _waypoints)
    {
        this.employeeCharacterModel = _model;
        this.speed = _speed;
        this.currentLevel = _currentLevel;
        this.employeeType = _employeeType;
        
        this.cart = _cart;
        this.nearbyRack = _nearbyRack;
        this.product = _product;

        productCount = _cart.Count;
        this.waypoints = _waypoints;
       // this.isMoving = _isMoving;  


    }


    public List<Transform> Waypoints
    {
        get => waypoints;
        set => waypoints = value;
    }

    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }

    public int WaypointIndex
    {
        get => waypointIndex;
        set => waypointIndex = value;
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = value;
    }

    public bool IsLoop
    {
        get => isLoop;
        set => isLoop = value;
    }
}

