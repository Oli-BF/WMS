# Overview
* A Warehouse Management System for small logistics companies - Completed as part of my dissertation for MSc Computer Science at the University of Birmingham.
* This application was implemented using a three-tier architecture, the .NET Framework, Windows Presentation Foundation and a centralised database using Microsoft's SQL Server hosted on Azure.
* Several UI examples can be found below.

---

**Login Page**
![alt text](https://github.com/Oli-BF/Assets/blob/master/Login.png?raw=true)

---

**Homepage**
![alt text](https://github.com/Oli-BF/Assets/blob/master/Homepage.png?raw=true)

---

**Datatable Example**
![alt text](https://github.com/Oli-BF/Assets/blob/master/Datatable.png?raw=true)

---

**Add to Datatable Example**
![alt text](https://github.com/Oli-BF/Assets/blob/master/Add.png?raw=true)

---

**Database Design**
![alt text](https://github.com/Oli-BF/Assets/blob/master/Database.png?raw=true)

---

# BitByBit Warehouse Management System - Tutorial
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
