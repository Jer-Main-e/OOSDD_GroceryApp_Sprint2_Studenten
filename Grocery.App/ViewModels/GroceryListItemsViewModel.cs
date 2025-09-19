using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(GroceryList), nameof(GroceryList))]
    public partial class GroceryListItemsViewModel : BaseViewModel
    {
        private readonly IGroceryListItemsService _groceryListItemsService;
        private readonly IProductService _productService;
        public ObservableCollection<GroceryListItem> MyGroceryListItems { get; set; } = [];
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        [ObservableProperty]
        GroceryList groceryList = new(0, "None", DateOnly.MinValue, "", 0);

        public GroceryListItemsViewModel(IGroceryListItemsService groceryListItemsService, IProductService productService)
        {
            _groceryListItemsService = groceryListItemsService;
            _productService = productService;
            Load(groceryList.Id);
        }

        private void Load(int id)
        {
            MyGroceryListItems.Clear();
            foreach (var item in _groceryListItemsService.GetAllOnGroceryListId(id)) MyGroceryListItems.Add(item);
            GetAvailableProducts();
        }

        private void GetAvailableProducts()
        {
            //We maken de lijst eerst leeg, zodat er geen conflicten staan met oude Product objecten.
            AvailableProducts.Clear();
            List<Product> producten = _productService.GetAll();
            List<GroceryListItem> boodschappen = _groceryListItemsService.GetAll();
            foreach (var product in producten)
            {
                //De onderstaande vergelijking kijkt of één product vanuit de voorraad zich al op het boodschappenlijstje bevind 
                //en of de voorraad groter is dan 0. Wanneer dit het geval is, 
                //dan zal het product aan de beschikbare producten lijst toegevoegd worden.
                if (!MyGroceryListItems.Any(boodschappenProduct => boodschappenProduct.ProductId == product.Id)
                    && product.Stock > 0)
                {
                    AvailableProducts.Add(product);
                }
            }           
        }

        partial void OnGroceryListChanged(GroceryList value)
        {
            Load(value.Id);
        }

        [RelayCommand]
        public async Task ChangeColor()
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), GroceryList } };
            await Shell.Current.GoToAsync($"{nameof(ChangeColorView)}?Name={GroceryList.Name}", true, paramater);
        }
        [RelayCommand]
        public void AddProduct(Product product)
        {
            //Wanneer het product geen waarde heeft of het heeft een voorraad kleiner 
            //of gelijk aan nul dan zullen we het niet toevoegen aan de boodschappen lijst.
            if (product == null || product.Id <= 0) return;
            //hier wordt voor de tweede keer de lijst met producten opgeroepen. 
            //Dit doen we omdat er op deze manier geen conflicten ontstaan met verouderde lijsten.
            List<Product> producten = _productService.GetAll();


            //We voegen het product toe aan de boodschappen lijst 
            //en we verwijderen het oude Product object om vervolgens een nieuwe aan te maken met de vernieuwde stock. 
            //Op deze manier verkomen we View conflicten waardoor de stock af en toe niet goed weergeven wordt.
            GroceryListItem newItem = new GroceryListItem(0, groceryList.Id, product.Id, 1);
            _groceryListItemsService.Add(newItem);
            product.Stock--;
            int tmp_index = producten.IndexOf(_productService.Get(product.Id));
            producten.RemoveAt(tmp_index);
            producten.Insert(tmp_index, product);
            GetAvailableProducts();
            OnGroceryListChanged(GroceryList);
        }
    }
}
