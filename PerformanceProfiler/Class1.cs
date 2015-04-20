using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// CHECK OUT THE FindDuplicateProducts method
namespace OptimizationPractice
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


        static Product FindDuplicateProduct(List<Product> products)
        {
            //foreach (var product1 in products)
            //{
            //    foreach (var product2 in products)
            //    {
            //        if (product1.Id == product2.Id && product1 != product2)
            //        {
            //            return product1;
            //        }
            //    }
            //}

            // *******************************************new code****************
            //for (int i = 0; i < products.Count; i++)
            //{
            //    for (int j = i; j < products.Count; j++)
            //    {
            //        if (products[i] != products[j] && products[i].Id == products[j].Id)
            //        {
            //            return products[i];
            //        }
            //    }

            //}

            //******************************************sorting first*****************
            //  products.Sort(new ProductComparer());

            for (int i = 0; i < products.Count - 1; i++)
            {
                if (products[i] != products[i + 1] && products[i].Id == products[1 + i].Id)
                {
                    return products[i];
                }
            }


            return null;
        }


        static void SortProducts(List<Product> products)
        {
            products.Sort(new ProductComparer());
        }

        static void Main(string[] args)
        {

            var watch = new Stopwatch();
            watch.Start();


            // Create 1 thousand products with Random Ids
            var products = CreateProducts(1000);

            // Sort products
            SortProducts(products);

            // Remove all products with duplicate Ids
            var dup = FindDuplicateProduct(products);
            while (dup != null)
            {
                products.Remove(dup);
                dup = FindDuplicateProduct(products);
            }

            // Show unique list of products
            foreach (var product in products)
            {
                Console.WriteLine(product.Id + " is unique.");
            }

            // Pause
            Console.WriteLine("Elapsed time: {0}", watch.Elapsed);

            Console.ReadLine();

        }
    }
}