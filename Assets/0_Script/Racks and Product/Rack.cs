using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Rack : MonoBehaviour
{

    [SerializeField] List<ProductSO> StorageRackProducts = new List<ProductSO>();
    [SerializeField] int productCount;
    [SerializeField] TextMeshProUGUI productCountText;


    // this is for the visual representation of the product 
    //[SerializeField] GameObject rackSlotHolder;
    //private GameObject[] rack;

    private void Start()
    {
        
        productCount = StorageRackProducts.Count;
        productCountText.text = productCount.ToString();
    }

    // Remove a specific amount of product (e.g., 2 units)
    public void RemoveProduct(ProductSO targetProduct)
    {
        bool removed = false;

        for (int i = StorageRackProducts.Count - 1; i >= 0; i--)
        {
            if (StorageRackProducts[i] == targetProduct)
            {
                StorageRackProducts.RemoveAt(i);
                productCount--;
                productCountText.text = productCount.ToString();
                removed = true;
                break; // Remove only one occurrence (or remove break to remove all)
            }
        }

        if (!removed)
        {
            Debug.Log("Not enough stock on shelf or product not found.");
        }
    }

    public void AddProduct(ProductSO targetProduct)
    {
        if (StorageRackProducts.Count >= 8)
        {
            Debug.Log("Rack is full");
            return;
        }

        StorageRackProducts.Add(targetProduct);
        productCount++;
        productCountText.text = productCount.ToString();
    }




}




