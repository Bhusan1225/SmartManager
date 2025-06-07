using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AICharacterModel
{
    private GameObject npcCharacterModel;
    private List<Transform> waypoints = new List<Transform>();
    private bool isMoving = false;
    private int waypointIndex;
    private float moveSpeed = 1f;
    private float rotationSpeed = 5f;
    private bool isLoop = true;

    private ProductSO product; // Assign through trigger or manually
    private Rack nearbyRack;   // Assign through trigger or manually
    private List<ProductSO> cart = new List<ProductSO>();
    private int productCount;

    // Constructor
    public AICharacterModel(GameObject _npcCharacterModel, List<Transform> _waypoints, float _moveSpeed, float _rotationSpeed, ProductSO _product, Rack _nearbyRack, List<ProductSO> _cart)
    {
        this.npcCharacterModel = _npcCharacterModel;
        this.waypoints = _waypoints;
        this.moveSpeed = _moveSpeed;
        this.rotationSpeed = _rotationSpeed;
        this.product = _product;
        this.nearbyRack = _nearbyRack;
        this.cart = _cart;
    }

    // Properties (public access)
    public GameObject NPCCharacterModel
    {
        get => npcCharacterModel;
        set => npcCharacterModel = value;
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

    public List<ProductSO> Cart
    {
        get => cart;
        set => cart = value;
    }

    public int ProductCount
    {
        get => productCount;
        set => productCount = value;
    }
}
