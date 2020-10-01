# BitByBit Warehouse Management System - Installation
* To run the application please use the following steps:

## Step 1
* Clone Repository

## Step 2
* Install Visual Studio

## Step 3
* Open WMS.sln (this will open the raw application in Visual Studio)

## Step 4
* Add any VS Dependencies required (if any are required it should state this at the top of the 'Solution Explorer' automatically)
  * If not - 1st - Try and 'Build' (Shortcut: F5) the solution - if no errors move to Step 5
  * If the above fails or the following steps do not work - open VS Installer - Click 'Modify' on your current installation - Add the '.NET Desktop Development' package and try again from Step 3

## Step 5
* You can now close VS if the 'Build' was successful (Should Show "========== Build: 5 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========" in the VS Output)

## Step 6
* Move to "YourRepositoryLocation"\WMS\PresentationTier\bin\Debug
  * Double click 'PresentationTier.exe' (This will only appear if the Build was successful in Step 4/5)
    * The application will start and you are ready to use it

## Step 7
* You will be presented with three separate possible login options: 'Worker', 'Manager' and 'Admin'; Please use the following login details to access each part of the application:
  * Worker 
    * Email: 'tw@bitbybit.co' - Without the Apostrophes
    * Password: 'password123' - Without the Apostrophes
  * Manager 
    * Email: 'tm@bitbybit.co' - Without the Apostrophes
    * Password: 'password123' - Without the Apostrophes
  * Admin 
    * Email: 'ta@bitbybit.co' - Without the Apostrophes
    * Password: 'password123' - Without the Apostrophes

---

# BitByBit Warehouse Management System - Tutorial
* Test data has already been provided but you are encouraged to add/delete/edit your own!
  * The only exception here is the Locations, which are view only in the Admin section (these are completely down to warehouse design and outside the scope of a WMS however, two warehouses with 500 (10 aisles across with each aisle having 10 locations, with each location being 5 pallets high) locations each, are provided from a warehouse design I created to simulate a real world setup complete with location correct put/pick sequences. These have been added via an SQL Script (See DataAccessTier\WMSTestDataScript.sql and DataAccessTier\WarehouseLayout.pdf for more information))


* The system works via the following steps:

## Pre-Conditions
* Pre-Conditions to Products coming in & out of the Warehouse:
  * Admin - Users are created 
  * Admin - Warehouse is Added 
  * Admin - Warehouse Locations are added (See above)
  * Manager - An Account (Customer) is added
  * Manager - Products are added to the Account (The customer will notify you of their product range, in advance of any physical receipt of a product)

## Step 1 - Receipts
* Account Sends Receipt

## Step 2
* Manager - Adds Receipt 
  * Manager - Adds Receipt Lines 

## Step 3
* Receipt Arrives at Warehouse
  * Receipt taken out of Container & put on Pallet

## Step 4
* Worker/Manager - Adds Stock (Pallet) (These are products stacked together from the Receipt Lines received)
  * Put Location is auto-generated in the Stock table
  * Worker - Stock (Pallet) is moved to auto-generated location

## Step 5 - Orders
* Account Sends Order

## Step 6
* Manager - Adds Order 
  * Manager - Adds Order Lines 

## Step 7
* Manager - Allocates all Order Lines belonging to an Order
  * Each allocated Order Line generates a single or multiple Picks (depending on quantity in stock) in the Picks Table
    * Pick Location is auto-generated for each ‘Pick’ in the Picks Table
  * Worker - Picks all Stock at each given location for all Picks relating to the allocated Order