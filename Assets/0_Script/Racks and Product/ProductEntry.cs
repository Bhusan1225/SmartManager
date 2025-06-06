using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductEntry  
{
    
   private ProductSO product;

    public ProductSO Product
    {
        get { return product; }
        set { product = value; }
    }


}
