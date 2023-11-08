Hi, this is a readme for my full stack Book bulk buy web app using ASP .NET Core Web App and Entity Framework.

This website is a personal project I made to practice and learn ASP .NET Core, Razor Page, Entity Framework concepts with instructions from [Mr. Bhrugen](https://www.dotnetmastery.com/).

Live website link: [https://tanbook.azurewebsites.net/](https://tanbook.azurewebsites.net/)

There are only 3 types of users (for now), and you can register as an admin user with full permissions (please don't delete any data that you didn't add).
Besides the admin and customer, there will be a company customer, the only difference for now is that they need to select a company while registering and it will set a delay payment for 30 days in their order and the bill will be directly send to the company
Also, under normal circumtanse, the company user will not be allowed to register themselves, its the admin user reponsibility to register a new company user

Pages (for everyone):

[[/Login](https://tanbook.azurewebsites.net/Identity/Account/Login)]

[[/Register](https://tanbook.azurewebsites.net/Identity/Account/Register)]

[[/Home](https://tanbook.azurewebsites.net/)]

[[/Book Details](https://tanbook.azurewebsites.net/Customer/Home/Details/7)]

Pages (for logged-in customer users):

[[/Shopping Cart](https://tanbook.azurewebsites.net/Customer/ShoppingCart)]

[[/Checkout](https://tanbook.azurewebsites.net/Customer/ShoppingCart/Summary)] you need to select an item from the [[/Shopping Cart](https://tanbook.azurewebsites.net/Customer/ShoppingCart)] page by clicking on the description or image first

[[/Edit Checkout Detail](https://tanbook.azurewebsites.net/customer/ShoppingCart/ShippingDetails)] you need to access this page from the checkout page

[[/Orders List](https://tanbook.azurewebsites.net/Admin/Order)] Order List for Customer user, customers can see all their orders here

[[/Order Details](https://tanbook.azurewebsites.net/Admin/Order/Details?orderId=5/)] Order Details page, you need to access this page from [[/Orders List](https://tanbook.azurewebsites.net/Admin/Order)] page so it has an id to process, customers can cancel their orders if its not completed yet

Pages (for admin users):
Admin users can access and perform all customer user tasks. Additionally, they can access

[[/All Orders](https://tanbook.azurewebsites.net/Admin/Order)] Order List for Admin user, same page as customer but admin can see all user orders here

[[/Order Details](https://tanbook.azurewebsites.net/Admin/Order/Details?orderId=1)] Order Details page, you need to access this page from [[/All Orders](https://tanbook.azurewebsites.net/Admin/Order/Details?orderId=1)] page so it has an id to process, if the user is admin, they can update the order status right here with Confirm, Ready, Completed or Cancel button

[[/Product List](https://tanbook.azurewebsites.net/Admin/Product)] This is where admin users can add/update/delete product. Since I purposely let you register as an admin user for testing purposes, please don't mess up my preset data. thank you

[[/Upsert](https://tanbook.azurewebsites.net/Admin/Product/UpSert)] This is the upsert page you access from [[/Product List](https://tanbook.azurewebsites.net/Admin/Product)] to update or if not if will be an insert page, i combined Update and Insert logic depend whether there is a an id present on the route

[[/Company List](https://tanbook.azurewebsites.net/Admin/Company)] [[/Company List](https://tanbook.azurewebsites.net/Admin/Company/Upsert)]  This is the same as two pages above, but for Company (not to be confused with company users a user belong to one company like a product belong to a category)

[[/Category List](https://tanbook.azurewebsites.net/Admin/Category)] This is where admin users can add/edit/delete on category of book

