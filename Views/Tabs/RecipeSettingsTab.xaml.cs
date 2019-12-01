using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vimachem.Hackathon.Views.Tabs
{

    /// <summary>
    /// Interaction logic for Recipes.xaml
    /// </summary>
    public partial class RecipeSettingsTab : UserControl
    {
        public RecipeSettingsTab()
        {
            InitializeComponent();


            ClientTextBox.Content = "Industry";
            RecipeManager.SetCustomerName(ClientTextBox.Content.ToString());

            OrderIDTextBox.Content = "Product A";
            RecipeManager.SetRecipeName(OrderIDTextBox.Content.ToString());

            LotTextBox.Content = "0528A";
            RecipeManager.SetRecipeBatch(LotTextBox.Content.ToString());
            
            Recipe = new List<RecipeParameter>();
            CreateRecipe();
            RecipeManager.SetRecipe(Recipe);

            Settings = new List<MachineParameter>();
            CreateSettings();
            RecipeDataGrid.ItemsSource = Recipe;
            SettingsDataGrid.ItemsSource = Settings;
            //this.Background = (Brush)new BrushConverter().ConvertFrom("#0C4DA1");

        }
        public List<RecipeParameter> Recipe { get; set; }
        public List<MachineParameter> Settings { get; set; }

        
        private void CreateSettings()
        {
            Settings.Add(new MachineParameter() { ID = 1, Description = "Roller Speed", Value = "50 rpm" });
            Settings.Add(new MachineParameter() { ID = 2, Description = "Furnice Temperature", Value = "200 Celcius" });
            Settings.Add(new MachineParameter() { ID = 3, Description = "Furnice Voltage", Value = "380 V" });
            Settings.Add(new MachineParameter() { ID = 4, Description = "Roller Delay", Value = "15 sec" });
        }

        private void CreateRecipe()
        {
            Recipe.Add(new RecipeParameter() { ID = 1, Description = "Outer Diameter (mm)", Value = "100", Tolerance = "1" });
            Recipe.Add(new RecipeParameter() { ID = 2, Description = "Thickness (mm)", Value = "10", Tolerance = "1" });
            Recipe.Add(new RecipeParameter() { ID = 3, Description = "Distortion (%)", Value = "1", Tolerance = "1" });
        }

    }
}
