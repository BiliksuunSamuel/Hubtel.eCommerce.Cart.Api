# Hubtel.eCommerce.Cart.Api<br>

<hr>

## Design an API to be used to unify the e-commerce cart experience for users: <br>

1. Provide an endpoint to Add items to cart, with specified quantity<br>

- Adding similar items (same item ID) should increase the quantity - POST <br>

2. Provide an endpoint to remove an item from cart - DELETE verb<br>
3. Provide an endpoint list all cart items (with filters => phoneNumbers, time, quantity, item - GET <br>
4. Provide endpoint to get single item - GET<br>

<hr>

## Entity Description<br>

1.CartModel:

- ItemID
- ItemName
- Quantity
- UnitPrice

<hr>

## Project Description <br>

1.Solution Name:<br>

- Hubtel.eCommerce.Cart<br>

  2.Project Name:<br>

- Hubtel.eCommerce.Cart.Api<br>

  3.Framework:<br>

- .NET Core 3.1<br>

4. RDMS

- Entity Framework

5. Database

- Microsoft SQL Database
