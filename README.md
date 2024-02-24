# PassionProject_YejunSon
I have designed the system to allow users to create and manage their own restaurant list. Through this, users can register restaurant folders and individually add restaurants that belong within them. This way, users can easily manage their own restaurant list and quickly find the restaurants they want. This system is designed with a focus on user convenience, providing effective support for managing and utilizing restaurant information
## Features

### User Management
- Create User
- List Users
- Update User
- Delete User

### Restaurant Management
- Create Restaurant
- List Restaurants
- Update Restaurant
- Delete Restaurant

### RestaurantsFolder Management
- Create RestaurantsFolder
- List RestaurantsFolders
- Update RestaurantsFolder
- Delete RestaurantsFolder
- Connect multiple restaurants to a folder at once
- Disconnect multiple restaurants from a folder at once

---
<br/>

## Database

### Tables

#### Users
- UserId
- FirstName
- LastName

#### Restaurants
- RestaurantId
- RestaurantName
- Location
- Rate
- Description

#### RestaurantsFolders
- RestaurantsFolderId
- FolderName

#### Bridge Table: RestaurantsFolderRestaurants
- Establishes a many-to-many relationship between restaurants and RestaurantsFolders

### Table Relationships
- Users to Restaurants: 1 to Many
- Users to RestaurantsFolders: 1 to Many
- Restaurants to RestaurantsFolders: Many to Many
