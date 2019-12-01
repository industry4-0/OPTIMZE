using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimachem.Hackathon
{
    public static class RecipeManager
    {
        private static List<RecipeParameter> recipe;
        private static string customerName;
        private static string recipeName;
        private static string batchID;

        public static void SetRecipe(List<RecipeParameter> userRecipe)
        {
            recipe = userRecipe;
        }
        public static List<RecipeParameter> GetRecipe()
        {
            return recipe;
        }
        public static void SetRecipeName(string userRecipe)
        {
            recipeName = userRecipe;
        }
        public static string GetRecipeName()
        {
            return recipeName;
        }

        public static void SetRecipeBatch(string batch)
        {
            batchID = batch;
        }
        public static string GetRecipeBatch()
        {
            return batchID;
        }
        public static void SetCustomerName(string customer)
        {
            customerName = customer;
        }
        public static string GetCustomerName()
        {
            return customerName;
        }
    }
}
