# Amazon

This is our private repository to manage our database project myAmazon.

#### Platforms Used
 - Visual Studio 2015
 - SQL Server 2012

#### Working Functionality
* Admin can add a Brand/Category/Product.
* View/Delete/Edit All Brands, Categories and Product.
* Admin can Approve/Reject a Credit Request From User
* Admin can Approve/Reject a Product request from customer and response will be sent as an email to that user
* Users can signin/signup
* Users can request for required amount of credit
* User can browse through all categories/products/brands
* User can Buy/Add to wishlist a product
* After buying a product, user can leave a Rating/Comment on that product

### How to get it to Work on Your side
* Create a database named myAmazonV2 with SQL Server
* Execute SQL Files. (Schema, Procedures, Views, UDF)
* Clone the repository and in inside your repo make these folders
/myAmazon-v1/BrandsData
/myAmazon-v1/CategoriesData
/myAmazon-v1/ProductsData
/myAmazon-v1/ProductsData
/myAmazon-v1/FeaturedData
/myAmazon-v1/UserData
/myAmazon-v1/Reviews
* You have to create a CS class file as:
/myAmazon-v1/DAL/Credentials.cs
and inside this class, write this code:
>public static string email = "youremail@xyzmail.com";
>public static string password = "yourpassword";

Now you should be good to go.
