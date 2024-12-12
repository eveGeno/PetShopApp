<h1 align="center">Pet Shop Application</h1>

*The PetShop Application is a modern web application built using ASP.NET MVC. It allows users to explore animals available in a pet shop, manage categories, and handle administration tasks. The app uses Entity Framework to connect to a SQL database, with the schema generated automatically upon first run.*

## **Features**

***Home View***
- Displays the top 2 most commented animals, providing a dynamic and engaging landing page for visitors.
  
***Catalog View***

- Provides a detailed list of all available animals, with options to:

  - Filter by category.
  - Switch between grid view and table view.
  - Use pagination for easier navigation across large datasets.
  - Click on an animal to view its detailed page, including category, description, and comments.

***Admin View***

- Allows the admin to:
  - View all animals in the shop.
  - Add, edit, and delete animals with validation for data integrity.
  - Manage categories, including adding, editing, and deleting categories.
  - Confirm deletions through modals for a safe user experience.
  - Supports file uploads for animal pictures alongside direct URL input.
  
***Architecture***

The application follows the MVC (Model-View-Controller) pattern:

 - Model: Handles data structures for animals, categories, and comments.
 - View: Displays dynamic content with user-friendly interfaces for visitors and administrators.
 - Controller: Manages user requests, updates models, and selects appropriate views to render.
   
## **Database**
- Built with Entity Framework Core for seamless ORM integration.
- Automatically generates the database schema on first run.
- SQL Server is the default provider, and the connection string is configurable via appsettings.json.
  
***Database Structure:***
  
1. Animals: Contains information about pets, including name, age, description, and picture.
2. Categories: Organizes animals into meaningful groups (e.g., Mammals, Reptiles).
3. Comments: Stores user-generated feedback linked to specific animals.

## **Instalation**
1. Clone the repository:
```bash
git clone https://github.com/eveGeno/PetShopApp.git
cd PetShopApp
```
2. Update the connection string:
   
   Modify the appsettings.json file to reflect your SQL Server connection. Example:
```json
"ConnectionStrings": {
    "PetShopConnection": "Server=YOUR_SERVER_NAME;Database=PetShopDb;Trusted_Connection=True;"
}
```
3. Build and run the application:
-  Open the project in Visual Studio.
-  Build the solution.
-  Run the application.
4. Use the application:
-  To access the app navigate to:
>  http://localhost:YOUR_PORT
-  Explore the Home, Catalog, and Admin views.

## Screenshots

<p align="center">
    <img src="https://raw.githubusercontent.com/eveGeno/PetShopApp/refs/heads/PSApp/Images/Screenshot5.png" alt="Main Screenshot" width="80%" />
</p>
<p align="center"><em>Home Page</em></p>

<p align="center">
    <img src="https://raw.githubusercontent.com/eveGeno/PetShopApp/refs/heads/PSApp/Images/Screenshot2.png" alt="Catalog Screenshot" width="32%" style="margin-right: 1%;" />
    <img src="https://raw.githubusercontent.com/eveGeno/PetShopApp/refs/heads/PSApp/Images/Screenshot3.png" alt="Admin Screenshot" width="32%" style="margin-right: 1%;" />
    <img src="https://raw.githubusercontent.com/eveGeno/PetShopApp/refs/heads/PSApp/Images/Screenshot4.png" alt="Dark Mode Screenshot" width="32%" />
</p>
<p align="center">
    <em>Catalog</em> | <em>Admin Page</em> | <em>Dark Mode</em>
</p>




