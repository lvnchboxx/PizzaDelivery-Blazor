## Getting started

Create a new class called `OrderState` in the Client Project root directory - and register it as a scoped service in the DI container. In Blazor WebAssembly applications, services are registered in the `Program` class. Add the service just before the call to `await builder.Build().RunAsync();`.

```csharp
using BlazingPizza.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<OrderState>();

await builder.Build().RunAsync();
```

> Note: the reason why we choose scoped over singleton is for symmetry with a server-side-components application. Singleton usually means *for all users*, where as scoped means *for the current unit-of-work*.

## Updating Index

Now that this type is registered in DI, we can `@inject` it into the `Index` page.

```razor
@page "/"
@inject HttpClient HttpClient
@inject OrderState OrderState
@inject NavigationManager NavigationManager
```

Recall that `@inject` is a convenient shorthand to both retrieve something from DI by type, and define a property of that type.

You can test this now by running the app again. If you try to inject something that isn't found in the DI container, then it will throw an exception and the `Index` page will fail to come up.

Now, let's add properties and methods to this class that will represent and manipulate the state of an `Order` and a `Pizza`.

Move the `configuringPizza`, `showingConfigureDialog` and `order` fields to be properties on the `OrderState` class. Make them `private set` so they can only be manipulated via methods on `OrderState`.

```csharp
public class OrderState
{
    public bool ShowingConfigureDialog { get; private set; }

    public Pizza ConfiguringPizza { get; private set; }

    public Order Order { get; private set; } = new Order();
}
```

Now let's move some of the methods from the `Index` to `OrderState`. We won't move `PlaceOrder` into `OrderState` because that triggers a navigation, so instead we'll just add a `ResetOrder` method.

```csharp
public void ShowConfigurePizzaDialog(PizzaSpecial special)
{
    ConfiguringPizza = new Pizza()
    {
        Special = special,
        SpecialId = special.Id,
        Size = Pizza.DefaultSize,
        Toppings = new List<PizzaTopping>(),
    };

    ShowingConfigureDialog = true;
}

public void CancelConfigurePizzaDialog()
{
    ConfiguringPizza = null;

    ShowingConfigureDialog = false;
}

public void ConfirmConfigurePizzaDialog()
{
    Order.Pizzas.Add(ConfiguringPizza);
    ConfiguringPizza = null;

    ShowingConfigureDialog = false;
}

public void ResetOrder()
{
    Order = new Order();
}

public void RemoveConfiguredPizza(Pizza pizza)
{
    Order.Pizzas.Remove(pizza);
}
```

Remember to remove the corresponding methods from `Index.razor`. You must also remember to remove the `order`, `configuringPizza`, and `showingConfigureDialog` fields entirely from `Index.razor`, since you'll be getting the state data from the injected `OrderState`.

At this point it should be possible to get the `Index` component compiling again by updating references to refer to various bits attached to `OrderState`. For example, the remaining `PlaceOrder` method in `Index.razor` should look like this:

```csharp
async Task PlaceOrder()
{
    var response = await HttpClient.PostAsJsonAsync("orders", OrderState.Order);
    var newOrderId = await response.Content.ReadFromJsonAsync<int>();
    OrderState.ResetOrder();
    NavigationManager.NavigateTo($"myorders/{newOrderId}");
}
```
