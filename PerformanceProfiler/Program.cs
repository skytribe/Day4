using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp
{

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class ProductComparer : IComparer<Product>
    {

        public int Compare(Product product1, Product product2)
        {
            return product1.Id - product2.Id;
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            // Create 1 thousand products with Random Ids
            var products = CreateProducts(1000);

            List<Product> newproductList;

           // newproductList = RemoveDuplicatesOriginal(products);
            newproductList = RemoveDuplicateProductsFaster(products);

            // Sort products
            SortProducts(newproductList);

            Console.WriteLine("\nFINAL OUTPUT");
            DisplayProducts(newproductList);

            // Show unique list of products
            foreach (var product in newproductList)
            {
                Console.WriteLine(product.Id + " is unique.");
            }

            // Pause
            Console.ReadLine();
        }

        static List<Product> CreateProducts(int count)
        {
            var products = new List<Product>();
            var rnd = new Random();
            for (var i = 0; i < 1000; i++)
            {
                products.Add(new Product
                {
                    Id = rnd.Next(100),
                    Name = "Product " + i
                });
            }
            return products;
        }

        static void SortProducts(List<Product> products)
        {
            products.Sort(new ProductComparer());
        }

        static void DisplayProducts(List<Product> p)
        {
            foreach (var item in p)
            {
                Console.WriteLine("{0} {1}", item.Id, item.Name);
            }
        }

        #region "Original Code Refactored"
        /// <summary>
        /// Original Code to remove the products.
        /// Its slow and we know it.
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        private static List<Product> RemoveDuplicatesOriginal(List<Product> products)
        {
            //// Remove all products with duplicate Ids
            var dup = FindDuplicateProduct(products);
            while (dup != null)
            {
                products.Remove(dup);
                dup = FindDuplicateProduct(products);
            }
            return products;
        }

        static Product FindDuplicateProduct(List<Product> products)
        {
            foreach (var product1 in products)
            {
                foreach (var product2 in products)
                {
                    if (product1 != product2 && product1.Id == product2.Id)
                    {
                        return product1;
                    }
                }
            }
            return null;
        }
        #endregion

        #region "Rewritten Code"

        static Dictionary<int, Product> uniqueProductDictionary;



        /// <summary>
        /// New Code to remove duplicates and return back a new collection of non duplicate products
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        private static List<Product> RemoveDuplicateProductsFaster(List<Product> products)
        {
            uniqueProductDictionary = new Dictionary<int, Product>();
            foreach (var item in products)
            {
                if (!uniqueProductDictionary.ContainsKey(item.Id))
                {
                    uniqueProductDictionary.Add(item.Id, item);
                }
            }
            //  Create a new list from the dictionary as no direct conversion.
            var newlist = new List<Product>();
            foreach (var item in uniqueProductDictionary)
            {
                newlist.Add(item.Value);
            }
            return newlist;

        }

        #endregion


    }
}
