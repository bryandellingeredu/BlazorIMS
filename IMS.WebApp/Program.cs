using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.Plugins.InMemory;
using IMS.WebApp.Components;
using IMS.UseCases.Products.Interfaces;
using IMS.UseCases.Products;
using IMS.WebApp.Components.Pages.Products;
using IMS.UseCases.Activities;
using IMS.UseCases.Activities.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
builder.Services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();
builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>();
builder.Services.AddTransient<IViewProductByIdUseCase, ViewProductByIdUseCase>();
builder.Services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
builder.Services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
