using System;
using System.Data;
using DataAccessTier;

namespace ApplicationTier
{
    /// <summary>
    ///     The methods within this class are the sister methods to those found in the DataAccessClass. All methods found in the DataAccessLayer
    ///     have a matching method within this class. Each method here is called from one of the Presentation Tier .xaml.cs classes, these in 
    ///     turn call the sister method within the DataAccessLayer class which contains the query and passes that through to its respective 
    ///     method in the DbConnection class which returns a Data Table to the DataAccessLayer method which in turn passes that Data Table to
    ///     the methods found here, which is how the Data Tables are filled within each method.
    ///     
    ///     The methods following can then be split into two distinct types, ones that use the Data Table to pull specific object data out of 
    ///     (those containing a foreach loop) and ones that don't (those containing no foreach loop). The first type are all Select Query 
    ///     related methods, and the second are any Queries that are used to update the database (insert, delete, update etc.). Both types
    ///     return an Object (which depends on the Database table they are engaging with). There is also a third type of method which is all
    ///     but identical to the second type of method mentioned above, but instead of returning an Object corresponding to the Database,
    ///     they return a Data Table - These are used only to populate the Data Grids found on the UI tables within the application.
    /// 
    ///     The layout of my three-layer architecture was based heavily on The Code Project's 'Three Layer Architecture in C# .NET'
    ///     by Parikshit Patel found here: https://www.codeproject.com/Articles/36847/Three-Layer-Architecture-in-C-NET-2. As such 
    ///     the DbConnection.cs, DataAccessLayer.cs, BusinessLogicLayer, 'X'Object.cs and some of the .xaml.cs classes found 
    ///     throughout the Presentation Tier use either whole or small bits of code from this walk-through. However, many aspects 
    ///     of the code found in the walk-through have either been built upon or modified heavily and will bare very little 
    ///     resemblance, if any, to the original; to suit the nature of my project, and are my own ideas. This paragraph will be 
    ///     found in the DbConnection.cs, DataAccessLayer.cs, BusinessLogicLayer.cs, XObject.cs classes as well as in all .xaml.cs 
    ///     classes found in the Presentation Tier for referencing purposes.
    /// </summary>
    public class BusinessLogicLayer
    {
        private DataAccessLayer dataAccessLayer;

        /// <summary>
        ///     The Constructor called BusinessLogicLayer.
        ///     This instantiates the DataAccessLayer class, creating an object of that type.
        /// </summary>
        public BusinessLogicLayer()
        {
            dataAccessLayer = new DataAccessLayer();
        }


        // ALL - LOGIN
        /// <summary>
        ///     Type 1 Method called Login, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject Login(string email, string role, string password)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.Login(email, role, password);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                user.email = dataRow["email"].ToString();
                user.role = dataRow["role"].ToString();
                user.password = dataRow["password"].ToString();
            }
            return user;
        }





        // VIEWS

        // Select All Users
        /// <summary>
        ///     Type 3 Method called SelectAllUsers, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllUsers()
        {
            DataTable dataTable = dataAccessLayer.SelectAllUsers();
            return dataTable;
        }

        // Select All Users - Workers
        /// <summary>
        ///     Type 3 Method called SelectAllUsersWorkers, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllUsersWorkers()
        {
            DataTable dataTable = dataAccessLayer.SelectAllUsersWorkers();
            return dataTable;
        }

        // Select All Users - Managers
        /// <summary>
        ///     Type 3 Method called SelectAllUsersManagers, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllUsersManagers()
        {
            DataTable dataTable = dataAccessLayer.SelectAllUsersManagers();
            return dataTable;
        }

        // Select All Users - Admins
        /// <summary>
        ///     Type 3 Method called SelectAllUsersAdmins, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllUsersAdmins()
        {
            DataTable dataTable = dataAccessLayer.SelectAllUsersAdmins();
            return dataTable;
        }



        // Select All Products
        /// <summary>
        ///     Type 3 Method called SelectAllProducts, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllProducts()
        {
            DataTable dataTable = dataAccessLayer.SelectAllProducts();
            return dataTable;
        }

        // Select All Accounts
        /// <summary>
        ///     Type 3 Method called SelectAllAccounts, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllAccounts()
        {
            DataTable dataTable = dataAccessLayer.SelectAllAccounts();
            return dataTable;
        }
        // Select Products Matching Account ID
        /// <summary>
        ///     Type 3 Method called SelectProductsMatchingAccountID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectProductsMatchingAccountID(int account_id)
        {
            DataTable dataTable = dataAccessLayer.SelectProductsMatchingAccountID(account_id);
            return dataTable;
        }



        // Select All Receipts
        /// <summary>
        ///     Type 3 Method called SelectAllReceipts, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllReceipts()
        {
            DataTable dataTable = dataAccessLayer.SelectAllReceipts();
            return dataTable;
        }
        // Select Receipt Lines matching given Order ID
        /// <summary>
        ///     Type 3 Method called SelectReceiptLinesMatchingReceiptID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectReceiptLinesMatchingReceiptID(int receipt_id)
        {
            DataTable dataTable = dataAccessLayer.SelectReceiptLinesMatchingReceiptID(receipt_id);
            return dataTable;
        }

        // Select All Receipt Lines
        /// <summary>
        ///     Type 3 Method called SelectAllReceiptLines, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllReceiptLines()
        {
            DataTable dataTable = dataAccessLayer.SelectAllReceiptLines();
            return dataTable;
        }



        // Select All Stock
        /// <summary>
        ///     Type 3 Method called SelectAllStock, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllStock()
        {
            DataTable dataTable = dataAccessLayer.SelectAllStock();
            return dataTable;
        }

        // Select All Locations
        /// <summary>
        ///     Type 3 Method called SelectAllLocations, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllLocations()
        {
            DataTable dataTable = dataAccessLayer.SelectAllLocations();
            return dataTable;
        }
        // Select All Stock Matching Location ID
        /// <summary>
        ///     Type 3 Method called SelectAllStockMatchingLocationID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllStockMatchingLocationID(int location_id)
        {
            DataTable dataTable = dataAccessLayer.SelectAllStockMatchingLocationID(location_id);
            return dataTable;
        }

        // Select All Warehouses
        /// <summary>
        ///     Type 3 Method called SelectAllWarehouses, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllWarehouses()
        {
            DataTable dataTable = dataAccessLayer.SelectAllWarehouses();
            return dataTable;
        }
        // Select All Stock Matching Warehouse ID
        /// <summary>
        ///     Type 3 Method called SelectAllStockMatchingWarehouseID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllStockMatchingWarehouseID(int warehouse_id)
        {
            DataTable dataTable = dataAccessLayer.SelectAllStockMatchingWarehouseID(warehouse_id);
            return dataTable;
        }
        // Select All Locations Matching Warehouse ID
        /// <summary>
        ///     Type 3 Method called SelectAllLocationsMatchingWarehouseID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllLocationsMatchingWarehouseID(int warehouse_id)
        {
            DataTable dataTable = dataAccessLayer.SelectAllLocationsMatchingWarehouseID(warehouse_id);
            return dataTable;
        }



        // Select All Orders
        /// <summary>
        ///     Type 3 Method called SelectAllOrders, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllOrders()
        {
            DataTable dataTable = dataAccessLayer.SelectAllOrders();
            return dataTable;
        }
        // Select Order Lines Matching Order ID
        /// <summary>
        ///     Type 3 Method called SelectOrderLinesMatchingOrderID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectOrderLinesMatchingOrderID(int order_id)
        {
            DataTable dataTable = dataAccessLayer.SelectOrderLinesMatchingOrderID(order_id);
            return dataTable;
        }

        // Select All Order Lines
        /// <summary>
        ///     Type 3 Method called SelectAllOrderLines, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllOrderLines()
        {
            DataTable dataTable = dataAccessLayer.SelectAllOrderLines();
            return dataTable;
        }

        // Select All Picks
        /// <summary>
        ///     Type 3 Method called SelectAllPicks, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     DataTable containing information of the Select Query found in the DataAccessLayer Class.
        /// </returns>
        public DataTable SelectAllPicks()
        {
            DataTable dataTable = dataAccessLayer.SelectAllPicks();
            return dataTable;
        }





        // ADMIN - USERS

        /// <summary>
        ///     Type 1 Method called CountAllUsers, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject CountAllUsers()
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.CountAllUsers();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                user.user_id = int.Parse(dataRow["user_id"].ToString());
            }
            return user;
        }



        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckUserInsert, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject CheckUserInsert(string email)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.CheckUsersInsert(email);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                user.email = dataRow["email"].ToString();
            }
            return user;
        }

        /// <summary>
        ///     Type 1 Method called CheckUserDelete, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject CheckUserDelete(string first_name, string last_name, long telephone, string email, string role)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.CheckUsersDelete(first_name, last_name, telephone, email, role);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                user.first_name = dataRow["first_name"].ToString();
                user.last_name = dataRow["last_name"].ToString();
                user.telephone = long.Parse(dataRow["telephone"].ToString());
                user.email = dataRow["email"].ToString();
                user.role = dataRow["role"].ToString();
            }
            return user;
        }

        /// <summary>
        ///     Type 1 Method called CheckUsersAllEdit, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject CheckUsersAllEdit(string first_name, string last_name, long telephone, string email, string role, string password)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.CheckUsersAllEdit(first_name, last_name, telephone, email, role, password);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                user.first_name = dataRow["first_name"].ToString();
                user.last_name = dataRow["last_name"].ToString();
                user.telephone = long.Parse(dataRow["telephone"].ToString());
                user.email = dataRow["email"].ToString();
                user.role = dataRow["role"].ToString();
                user.password = dataRow["password"].ToString();
            }
            return user;
        }

        /// <summary>
        ///     Type 1 Method called CheckUsersEmailEdit, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject CheckUsersEmailEdit(string email)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.CheckUsersEmailEdit(email);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                user.email = dataRow["email"].ToString();
            }
            return user;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewUser, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject InsertNewUser(string first_name, string last_name, long telephone, string email, string role, string password)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.InsertNewUser(first_name, last_name, telephone, email, role, password);
            return user;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentUser, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject DeleteCurrentUser(string first_name, string last_name, long telephone, string email, string role)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentUser(first_name, last_name, telephone, email, role);
            return user;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentUser, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_first_name"></param>
        /// <param name="new_last_name"></param>
        /// <param name="new_telephone"></param>
        /// <param name="new_email"></param>
        /// <param name="new_role"></param>
        /// <param name="new_password"></param>
        /// <param name="current_first_name"></param>
        /// <param name="current_last_name"></param>
        /// <param name="current_telephone"></param>
        /// <param name="current_email"></param>
        /// <param name="current_role"></param>
        /// <param name="current_password"></param>
        /// <returns>
        ///     UserObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public UserObject EditCurrentUser(string new_first_name, string new_last_name, long new_telephone, string new_email, string new_role, string new_password,
                                          string current_first_name, string current_last_name, long current_telephone, string current_email, string current_role,
                                          string current_password)
        {
            UserObject user = new UserObject();
            DataTable dataTable = dataAccessLayer.EditCurrentUser(new_first_name, new_last_name, new_telephone, new_email, new_role, new_password,
                                                                  current_first_name, current_last_name, current_telephone, current_email, current_role, current_password);
            return user;
        }





        // ADMIN - ACCOUNTS

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckAccountsByName, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     AccountObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public AccountObject CheckAccountsByName(string name)
        {
            AccountObject account = new AccountObject();
            DataTable dataTable = dataAccessLayer.CheckAccountsByName(name);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                account.name = dataRow["name"].ToString();
            }
            return account;
        }

        /// <summary>
        ///     Type 1 Method called CheckAccountsByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param>
        /// <returns>
        ///     AccountObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public AccountObject CheckAccountsByID(int account_id)
        {
            AccountObject account = new AccountObject();
            DataTable dataTable = dataAccessLayer.CheckAccountsByID(account_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                account.account_id = int.Parse(dataRow["account_id"].ToString());
            }
            return account;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewAccount, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     AccountObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public AccountObject InsertNewAccount(string name)
        {
            AccountObject account = new AccountObject();
            DataTable dataTable = dataAccessLayer.InsertNewAccount(name);
            return account;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentAccount, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     AccountObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public AccountObject DeleteCurrentAccount(string name)
        {
            AccountObject account = new AccountObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentAccount(name);
            return account;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentAccount, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_name"></param>
        /// <param name="current_name"></param>
        /// <returns>
        ///     AccountObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public AccountObject EditCurrentAccount(string new_name, string current_name)
        {
            AccountObject account = new AccountObject();
            DataTable dataTable = dataAccessLayer.EditCurrentAccount(new_name, current_name);
            return account;
        }





        // ADMIN - WAREHOUSES

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckWarehousesByName, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     WarehouseObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public WarehouseObject CheckWarehousesByName(string name)
        {
            WarehouseObject warehouse = new WarehouseObject();
            DataTable dataTable = dataAccessLayer.CheckWarehousesByName(name);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                warehouse.name = dataRow["name"].ToString();
            }
            return warehouse;
        }

        /// <summary>
        ///     Type 1 Method called CheckWarehousesByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     WarehouseObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public WarehouseObject CheckWarehousesByID(int warehouse_id)
        {
            WarehouseObject warehouse = new WarehouseObject();
            DataTable dataTable = dataAccessLayer.CheckWarehousesByID(warehouse_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                warehouse.warehouse_id = int.Parse(dataRow["warehouse_id"].ToString());
            }
            return warehouse;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewWarehouse, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     WarehouseObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public WarehouseObject InsertNewWarehouse(string name)
        {
            WarehouseObject warehouse = new WarehouseObject();
            DataTable dataTable = dataAccessLayer.InsertNewWarehouse(name);
            return warehouse;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentWarehouse, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     WarehouseObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public WarehouseObject DeleteCurrentWarehouse(string name)
        {
            WarehouseObject warehouse = new WarehouseObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentWarehouse(name);
            return warehouse;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentWarehouse, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_name"></param>
        /// <param name="current_name"></param>
        /// <returns>
        ///     WarehouseObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public WarehouseObject EditCurrentWarehouse(string new_name, string current_name)
        {
            WarehouseObject warehouse = new WarehouseObject();
            DataTable dataTable = dataAccessLayer.EditCurrentWarehouse(new_name, current_name);
            return warehouse;
        }





        // MANAGER - PRODUCTS

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckProductsAll, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="title"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     ProductObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ProductObject CheckProductsAll(int account_id, string title, string sku)
        {
            ProductObject product = new ProductObject();
            DataTable dataTable = dataAccessLayer.CheckProductsAll(account_id, title, sku);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                product.account_id = Int32.Parse(dataRow["account_id"].ToString());
                product.title = dataRow["title"].ToString();
                product.sku = dataRow["sku"].ToString();
            }
            return product;
        }

        /// <summary>
        ///     Type 1 Method called CheckProductsByIDAndSku, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     ProductObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ProductObject CheckProductsByIDAndSku(int account_id, string sku)
        {
            ProductObject product = new ProductObject();
            DataTable dataTable = dataAccessLayer.CheckProductsByIDAndSku(account_id, sku);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                product.account_id = Int32.Parse(dataRow["account_id"].ToString());
                product.sku = dataRow["sku"].ToString();
            }
            return product;
        }

        /// <summary>
        ///     Type 1 Method called CheckProductsByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>
        ///     ProductObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ProductObject CheckProductsByID(int product_id)
        {
            ProductObject product = new ProductObject();
            DataTable dataTable = dataAccessLayer.CheckProductsByID(product_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                product.product_id = int.Parse(dataRow["product_id"].ToString());
            }
            return product;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewProduct, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="title"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     ProductObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ProductObject InsertNewProduct(int account_id, string title, string sku)
        {
            ProductObject product = new ProductObject();
            DataTable dataTable = dataAccessLayer.InsertNewProduct(account_id, title, sku);
            return product;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentProduct, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="title"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     ProductObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ProductObject DeleteCurrentProduct(int account_id, string title, string sku)
        {
            ProductObject product = new ProductObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentProduct(account_id, title, sku);
            return product;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentProduct, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_account_id"></param>
        /// <param name="new_title"></param>
        /// <param name="new_sku"></param>
        /// <param name="current_account_id"></param>
        /// <param name="current_title"></param>
        /// <param name="current_sku"></param>
        /// <returns>
        ///     ProductObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ProductObject EditCurrentProduct(int new_account_id, string new_title, string new_sku, int current_account_id, string current_title, string current_sku)
        {
            ProductObject product = new ProductObject();
            DataTable dataTable = dataAccessLayer.EditCurrentProduct(new_account_id, new_title, new_sku, current_account_id, current_title, current_sku);
            return product;
        }





        // MANAGER - STOCK

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckStockAll, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="product_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <param name="quantity"></param>
        /// <param name="allocated_quantity"></param>
        /// <param name="availability_status"></param>
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject CheckStockAll(int account_id, int product_id, int warehouse_id, int location_id, int quantity, int allocated_quantity, bool availability_status)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.CheckStockAll(account_id, product_id, warehouse_id, location_id, quantity, allocated_quantity, availability_status);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.account_id = Int32.Parse(dataRow["account_id"].ToString());
                stock.product_id = Int32.Parse(dataRow["product_id"].ToString());
                stock.warehouse_id = Int32.Parse(dataRow["warehouse_id"].ToString());
                stock.location_id = Int32.Parse(dataRow["location_id"].ToString());
                stock.quantity = Int32.Parse(dataRow["quantity"].ToString());
                stock.allocated_quantity = Int32.Parse(dataRow["allocated_quantity"].ToString());
                stock.availability_status = bool.Parse(dataRow["availability_status"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called CheckStockByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject CheckStockByID(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.CheckStockByID(stock_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.stock_id = Int32.Parse(dataRow["stock_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called CheckStockByIDAndLocation, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject CheckStockByIDAndLocation(int stock_id, int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.CheckStockByIDAndLocation(stock_id, location_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.stock_id = Int32.Parse(dataRow["stock_id"].ToString());
                stock.location_id = Int32.Parse(dataRow["location_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called CheckStockByWarehouseAndLocation, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject CheckStockByWarehouseAndLocation(int warehouse_id, int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.CheckStockByWarehouseAndLocation(warehouse_id, location_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.warehouse_id = Int32.Parse(dataRow["warehouse_id"].ToString());
                stock.location_id = Int32.Parse(dataRow["location_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called CheckLocationByIDAndWarehouse, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject CheckLocationByIDAndWarehouse(int location_id, int warehouse_id)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.CheckLocationByIDAndWarehouse(location_id, warehouse_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                location.location_id = Int32.Parse(dataRow["location_id"].ToString());
            }
            return location;
        }

        /// <summary>
        ///     Type 1 Method called CheckStockByIDAndWarehouseAndLocation, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject CheckStockByIDAndWarehouseAndLocation(int stock_id, int warehouse_id, int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.CheckStockByIDAndWarehouseAndLocation(stock_id, warehouse_id, location_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.stock_id = Int32.Parse(dataRow["stock_id"].ToString());
                stock.warehouse_id = Int32.Parse(dataRow["warehouse_id"].ToString());
                stock.location_id = Int32.Parse(dataRow["location_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called CheckStockIsAllocated, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject CheckStockIsAllocated(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.CheckStockIsAllocated(stock_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.allocated_quantity = Int32.Parse(dataRow["allocated_quantity"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called FindFreeLocation, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject FindFreeLocation(int warehouse_id)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.FindFreeLocation(warehouse_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                location.location_id = Int32.Parse(dataRow["location_id"].ToString());
            }
            return location;
        }

        /// <summary>
        ///     Type 2 Method called MarkLocationAllocated, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stockLocation"></param>
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject MarkLocationAllocated(int stockLocation)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.MarkLocationAllocated(stockLocation);
            return location;
        }

        /// <summary>
        ///     Type 2 Method called MarkLocationUnallocated, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stockLocation"></param>
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject MarkLocationUnallocated(int stockLocation)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.MarkLocationUnallocated(stockLocation);
            return location;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewStock, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param
        /// <param name="product_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject InsertNewStock(int account_id, int product_id, int warehouse_id, int location_id, int quantity)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.InsertNewStock(account_id, product_id, warehouse_id, location_id, quantity);
            return stock;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentStock, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject DeleteCurrentStock(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentStock(stock_id);
            return stock;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentStock, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_account_id"></param
        /// <param name="new_product_id"></param
        /// <param name="new_warehouse_id"></param
        /// <param name="new_location_id"></param
        /// <param name="new_quantity"></param
        /// <param name="new_allocated_quantity"></param
        /// <param name="new_availability_status"></param
        /// <param name="current_stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject EditCurrentStock(int new_account_id, int new_product_id, int new_warehouse_id, int new_location_id, int new_quantity, int new_allocated_quantity,
                                              bool new_availability_status,
                                              int current_stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.EditCurrentStock(new_account_id, new_product_id, new_warehouse_id, new_location_id, new_quantity, new_allocated_quantity,
                                                         new_availability_status,
                                                         current_stock_id);
            return stock;
        }





        // N/A - LOCATIONS

        // CHECK
        /// <summary>
        ///     Type 1 Method called CheckLocationsByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="location_id"></param
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject CheckLocationsByID(int location_id)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.CheckLocationsByID(location_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                location.location_id = int.Parse(dataRow["location_id"].ToString());
            }
            return location;
        }





        // MANAGER - RECEIPTS

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckReceiptsAll, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_reference"></param
        /// <param name="account_id"></param
        /// <param name="warehouse_id"></param
        /// <param name="expected_date"></param
        /// <returns>
        ///     ReceiptObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptObject CheckReceiptsAll(string receipt_reference, int account_id, int warehouse_id, DateTime expected_date)
        {
            ReceiptObject receipt = new ReceiptObject();
            DataTable dataTable = dataAccessLayer.CheckReceiptsAll(receipt_reference, account_id, warehouse_id, expected_date);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                receipt.receipt_reference = dataRow["receipt_reference"].ToString();
                receipt.account_id = int.Parse(dataRow["account_id"].ToString());
                receipt.warehouse_id = int.Parse(dataRow["warehouse_id"].ToString());
                receipt.expected_date = DateTime.Parse(dataRow["expected_date"].ToString());
            }
            return receipt;
        }

        /// <summary>
        ///     Type 1 Method called CheckReceiptsByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_id"></param
        /// <returns>
        ///     ReceiptObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptObject CheckReceiptsByID(int receipt_id)
        {
            ReceiptObject receipt = new ReceiptObject();
            DataTable dataTable = dataAccessLayer.CheckReceiptsByID(receipt_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                receipt.receipt_id = int.Parse(dataRow["receipt_id"].ToString());
            }
            return receipt;
        }

        /// <summary>
        ///     Type 1 Method called CheckReceiptsByRefAndAccountID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_reference"></param
        /// <param name="account_id"></param
        /// <returns>
        ///     ReceiptObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptObject CheckReceiptsByRefAndAccountID(string receipt_reference, int account_id)
        {
            ReceiptObject receipt = new ReceiptObject();
            DataTable dataTable = dataAccessLayer.CheckReceiptsByRefAndAccountID(receipt_reference, account_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                receipt.receipt_reference = dataRow["receipt_reference"].ToString();
                receipt.account_id = int.Parse(dataRow["account_id"].ToString());
            }
            return receipt;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewReceipt, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_reference"></param
        /// <param name="account_id"></param
        /// <param name="warehouse_id"></param
        /// <param name="expected_date"></param
        /// <returns>
        ///     ReceiptObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptObject InsertNewReceipt(string receipt_reference, int account_id, int warehouse_id, DateTime expected_date)
        {
            ReceiptObject receipt = new ReceiptObject();
            DataTable dataTable = dataAccessLayer.InsertNewReceipt(receipt_reference, account_id, warehouse_id, expected_date);
            return receipt;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentReceipt, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_reference"></param
        /// <param name="account_id"></param
        /// <param name="warehouse_id"></param
        /// <param name="expected_date"></param
        /// <returns>
        ///     ReceiptObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptObject DeleteCurrentReceipt(string receipt_reference, int account_id, int warehouse_id, DateTime expected_date)
        {
            ReceiptObject receipt = new ReceiptObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentReceipt(receipt_reference, account_id, warehouse_id, expected_date);
            return receipt;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentReceipt, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_receipt_reference"></param
        /// <param name="new_account_id"></param
        /// <param name="new_warehouse_id"></param
        /// <param name="new_expected_date"></param
        /// <param name="receipted_date"></param
        /// <param name="current_receipt_reference"></param
        /// <param name="current_account_id"></param
        /// <param name="current_warehouse_id"></param
        /// <param name="current_expected_date"></param
        /// <returns>
        ///     ReceiptObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptObject EditCurrentReceipt(string new_receipt_reference, int new_account_id, int new_warehouse_id, DateTime new_expected_date, DateTime receipted_date,
                                                string current_receipt_reference, int current_account_id, int current_warehouse_id, DateTime current_expected_date)
        {
            ReceiptObject receipt = new ReceiptObject();
            DataTable dataTable = dataAccessLayer.EditCurrentReceipt(new_receipt_reference, new_account_id, new_warehouse_id, new_expected_date, receipted_date,
                                                           current_receipt_reference, current_account_id, current_warehouse_id, current_expected_date);
            return receipt;
        }

        /// <summary>
        ///     Type 2 Method called EditCurrentReceiptNoRecDate, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_receipt_reference"></param
        /// <param name="new_account_id"></param
        /// <param name="new_warehouse_id"></param
        /// <param name="new_expected_date"></param
        /// <param name="current_receipt_reference"></param
        /// <param name="current_account_id"></param
        /// <param name="current_warehouse_id"></param
        /// <param name="current_expected_date"></param
        /// <returns>
        ///     ReceiptObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptObject EditCurrentReceiptNoRecDate(string new_receipt_reference, int new_account_id, int new_warehouse_id, DateTime new_expected_date,
                                                         string current_receipt_reference, int current_account_id, int current_warehouse_id, DateTime current_expected_date)
        {
            ReceiptObject receipt = new ReceiptObject();
            DataTable dataTable = dataAccessLayer.EditCurrentReceiptNoRecDate(new_receipt_reference, new_account_id, new_warehouse_id, new_expected_date,
                                                                    current_receipt_reference, current_account_id, current_warehouse_id, current_expected_date);
            return receipt;
        }





        // MANAGER - Receipt Lines

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckReceiptLinesAll, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_id"></param
        /// <param name="product_id"></param
        /// <param name="quantity"></param
        /// <returns>
        ///     ReceiptLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptLineObject CheckReceiptLinesAll(int receipt_id, int product_id, int quantity)
        {
            ReceiptLineObject receiptLine = new ReceiptLineObject();
            DataTable dataTable = dataAccessLayer.CheckReceiptLinesAll(receipt_id, product_id, quantity);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                receiptLine.receipt_id = Int32.Parse(dataRow["receipt_id"].ToString());
                receiptLine.product_id = Int32.Parse(dataRow["product_id"].ToString());
                receiptLine.quantity = Int32.Parse(dataRow["quantity"].ToString());
            }
            return receiptLine;
        }

        /// <summary>
        ///     Type 1 Method called CheckReceiptLinesByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_line_id"></param
        /// <returns>
        ///     ReceiptLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptLineObject CheckReceiptLinesByID(int receipt_line_id)
        {
            ReceiptLineObject receiptLine = new ReceiptLineObject();
            DataTable dataTable = dataAccessLayer.CheckReceiptLinesByID(receipt_line_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                receiptLine.receipt_line_id = Int32.Parse(dataRow["receipt_line_id"].ToString());
            }
            return receiptLine;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewReceiptLine, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_id"></param
        /// <param name="product_id"></param
        /// <param name="quantity"></param
        /// <returns>
        ///     ReceiptLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptLineObject InsertNewReceiptLine(int receipt_id, int product_id, int quantity)
        {
            ReceiptLineObject receiptLine = new ReceiptLineObject();
            DataTable dataTable = dataAccessLayer.InsertNewReceiptLine(receipt_id, product_id, quantity);
            return receiptLine;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentReceiptLine, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="receipt_line_id"></param
        /// <returns>
        ///     ReceiptLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptLineObject DeleteCurrentReceiptLine(int receipt_line_id)
        {
            ReceiptLineObject receiptLine = new ReceiptLineObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentReceiptLine(receipt_line_id);
            return receiptLine;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentReceiptLine, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_receipt_id"></param
        /// <param name="new_product_id"></param
        /// <param name="new_quantity"></param
        /// <param name="current_receipt_line_id"></param
        /// <returns>
        ///     ReceiptLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ReceiptLineObject EditCurrentReceiptLine(int new_receipt_id, int new_product_id, int new_quantity, int current_receipt_line_id)
        {
            ReceiptLineObject receiptLine = new ReceiptLineObject();
            DataTable dataTable = dataAccessLayer.EditCurrentReceiptLine(new_receipt_id, new_product_id, new_quantity, current_receipt_line_id);
            return receiptLine;
        }





        // MANAGER - ORDERS

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckOrdersAll, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_reference"></param
        /// <param name="account_id"></param
        /// <param name="warehouse_id"></param
        /// <param name="dispatch_date"></param
        /// <param name="first_name"></param
        /// <param name="last_name"></param
        /// <param name="address_line_1"></param
        /// <param name="address_line_2"></param
        /// <param name="city"></param
        /// <param name="postcode"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject CheckOrdersAll(string order_reference, int account_id, int warehouse_id, DateTime dispatch_date, string first_name, string last_name,
                                          string address_line_1, string address_line_2, string city, string postcode)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.CheckOrdersAll(order_reference, account_id, warehouse_id, dispatch_date, first_name, last_name, address_line_1, address_line_2,
                                                       city, postcode);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                order.order_reference = dataRow["order_reference"].ToString();
                order.account_id = int.Parse(dataRow["account_id"].ToString());
                order.warehouse_id = int.Parse(dataRow["warehouse_id"].ToString());
                order.dispatch_date = DateTime.Parse(dataRow["dispatch_date"].ToString());
                order.first_name = dataRow["first_name"].ToString();
                order.last_name = dataRow["last_name"].ToString();
                order.address_line_1 = dataRow["address_line_1"].ToString();
                order.address_line_2 = dataRow["address_line_2"].ToString();
                order.city = dataRow["city"].ToString();
                order.postcode = dataRow["postcode"].ToString();
            }
            return order;
        }

        /// <summary>
        ///     Type 1 Method called CheckOrdersByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject CheckOrdersByID(int order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.CheckOrdersByID(order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                order.order_id = int.Parse(dataRow["order_id"].ToString());
            }
            return order;
        }

        /// <summary>
        ///     Type 1 Method called CheckOrdersByRefAndAccountID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_reference"></param
        /// <param name="account_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject CheckOrdersByRefAndAccountID(string order_reference, int account_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.CheckOrdersByRefAndAccountID(order_reference, account_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                order.order_reference = dataRow["order_reference"].ToString();
                order.account_id = int.Parse(dataRow["account_id"].ToString());
            }
            return order;
        }

        /// <summary>
        ///     Type 1 Method called CheckOrdersByIDAndRefAndAccID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <param name="order_reference"></param
        /// <param name="account_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject CheckOrdersByIDAndRefAndAccID(int order_id, string order_reference, int account_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.CheckOrdersByIDAndRefAndAccID(order_id, order_reference, account_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                order.order_id = int.Parse(dataRow["order_id"].ToString());
                order.order_reference = dataRow["order_reference"].ToString();
                order.account_id = int.Parse(dataRow["account_id"].ToString());
            }
            return order;
        }

        /// <summary>
        ///     Type 1 Method called CheckOrderIsAllocated, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject CheckOrderIsAllocated(int order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.CheckOrderIsAllocated(order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                order.status = dataRow["status"].ToString();
            }
            return order;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewOrder, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_reference"></param
        /// <param name="account_id"></param
        /// <param name="warehouse_id"></param
        /// <param name="dispatch_date"></param
        /// <param name="first_name"></param
        /// <param name="last_name"></param
        /// <param name="address_line_1"></param
        /// <param name="address_line_2"></param
        /// <param name="city"></param
        /// <param name="postcode"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject InsertNewOrder(string order_reference, int account_id, int warehouse_id, DateTime dispatch_date, string first_name, string last_name,
                                            string address_line_1, string address_line_2, string city, string postcode)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.InsertNewOrder(order_reference, account_id, warehouse_id, dispatch_date, first_name, last_name, address_line_1, address_line_2,
                                                       city, postcode);
            return order;
        }


        // ALLOCATE
        /// <summary>
        ///     Type 1 Method called GetOrderLineOrderID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_line_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject GetOrderLineOrderID(int order_line_id)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.GetOrderLineOrderID(order_line_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderLine.order_id = int.Parse(dataRow["order_id"].ToString());
            }
            return orderLine;
        }

        /// <summary>
        ///     Type 1 Method called GetOrderLineProductIDAndTitleOLObj, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_line_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject GetOrderLineProductIDAndTitleOLObj(int order_line_id)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.GetOrderLineProductIDAndTitle(order_line_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderLine.product_id = int.Parse(dataRow["product_id"].ToString());
            }
            return orderLine;
        }

        /// <summary>
        ///     Type 1 Method called GetOrderLineProductIDAndTitlePrObj, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_line_id"></param
        /// <returns>
        ///     ProductObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public ProductObject GetOrderLineProductIDAndTitlePrObj(int order_line_id)
        {
            ProductObject product = new ProductObject();
            DataTable dataTable = dataAccessLayer.GetOrderLineProductIDAndTitle(order_line_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                product.title = dataRow["title"].ToString();
            }
            return product;
        }

        /// <summary>
        ///     Type 1 Method called GetStockProductID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="product_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetStockProductID(int product_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetStockProductID(product_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.product_id = int.Parse(dataRow["product_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetOrderAccountID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject GetOrderAccountID(int order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.GetOrderAccountID(order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                order.account_id = int.Parse(dataRow["account_id"].ToString());
            }
            return order;
        }

        /// <summary>
        ///     Type 1 Method called GetOrderWarehouseID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject GetOrderWarehouseID(int order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.GetOrderWarehouseID(order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                order.warehouse_id = int.Parse(dataRow["warehouse_id"].ToString());
            }
            return order;
        }

        /// <summary>
        ///     Type 1 Method called GetStockAccountProductAndWarehouseID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param
        /// <param name="product_id"></param
        /// <param name="warehouse_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetStockAccountProductAndWarehouseID(int account_id, int product_id, int warehouse_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetStockAccountProductAndWarehouseID(account_id, product_id, warehouse_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.account_id = int.Parse(dataRow["account_id"].ToString());
                stock.product_id = int.Parse(dataRow["product_id"].ToString());
                stock.warehouse_id = int.Parse(dataRow["warehouse_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetAvailableStockStObj, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param
        /// <param name="product_id"></param
        /// <param name="warehouse_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetAvailableStockStObj(int product_id, int account_id, int warehouse_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetAvailableStock(product_id, account_id, warehouse_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.stock_id = int.Parse(dataRow["stock_id"].ToString());
                stock.location_id = int.Parse(dataRow["location_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetAvailableStockLoObj, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param
        /// <param name="product_id"></param
        /// <param name="warehouse_id"></param
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject GetAvailableStockLoObj(int product_id, int account_id, int warehouse_id)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.GetAvailableStock(product_id, account_id, warehouse_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                location.location_code = dataRow["location_code"].ToString();
            }
            return location;
        }

        /// <summary>
        ///     Type 2 Method called SetAvailabilityFalse, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject SetAvailabilityFalse(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.SetAvailabilityFalse(stock_id);
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetOrderLineQuantity, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_line_id"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject GetOrderLineQuantity(int order_line_id)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.GetOrderLineQuantity(order_line_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderLine.quantity = int.Parse(dataRow["quantity"].ToString());
            }
            return orderLine;
        }

        /// <summary>
        ///     Type 1 Method called GetPickQuantityByOrderLineID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_line_id"></param
        /// <returns>
        ///     PickObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public PickObject GetPickQuantityByOrderLineID(int order_line_id)
        {
            PickObject pick = new PickObject();
            DataTable dataTable = dataAccessLayer.GetPickQuantityByOrderLineID(order_line_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                pick.quantity = int.Parse(dataRow["quantity"].ToString());
            }
            return pick;
        }

        /// <summary>
        ///     Type 2 Method called GeneratePick, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <param name="order_line_id"></param
        /// <param name="location_id"></param
        /// <param name="location_code"></param
        /// <param name="product_id"></param
        /// <param name="product_title"></param
        /// <param name="quantity"></param
        /// <returns>
        ///     PickObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public PickObject GeneratePick(int order_id, int order_line_id, int location_id, string location_code, int product_id, string product_title, int quantity)
        {
            PickObject pick = new PickObject();
            DataTable dataTable = dataAccessLayer.GeneratePick(order_id, order_line_id, location_id, location_code, product_id, product_title, quantity);
            return pick;
        }

        /// <summary>
        ///     Type 2 Method called UpdateStockQuantity, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="allocated_quantity"></param
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject UpdateStockQuantity(int allocated_quantity, int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.UpdateStockQuantity(allocated_quantity, stock_id);
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called CountOrderLinesInOrderLinesByOrderID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject CountOrderLinesInOrderLinesByOrderID(int order_id)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.CountOrderLinesInOrderLinesByOrderID(order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderLine.order_line_id = int.Parse(dataRow["order_line_id"].ToString());
            }
            return orderLine;
        }

        /// <summary>
        ///     Type 1 Method called CountOrderLinesInOrderLinesByOrderID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     PickObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public PickObject CountOrderLinesInPicksByOrderID(int order_id)
        {
            PickObject pick = new PickObject();
            DataTable dataTable = dataAccessLayer.CountOrderLinesInPicksByOrderID(order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                pick.order_line_id = int.Parse(dataRow["order_line_id"].ToString());
            }
            return pick;
        }

        /// <summary>
        ///     Type 1 Method called GetOrderLineQuantityByOLIDAndOID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_lines_order_id"></param
        /// <param name="orders_order_id"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject GetOrderLineQuantityByOLIDAndOID(int order_lines_order_id, int orders_order_id)
        {
            OrderLineObject orderline = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.GetOrderLineQuantityByOLIDAndOID(order_lines_order_id, orders_order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderline.quantity = int.Parse(dataRow["quantity"].ToString());
            }
            return orderline;
        }

        /// <summary>
        ///     Type 1 Method called GetPickQuantityByOrderID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     PickObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public PickObject GetPickQuantityByOrderID(int order_id)
        {
            PickObject pick = new PickObject();
            DataTable dataTable = dataAccessLayer.GetPickQuantityByOrderID(order_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                pick.quantity = int.Parse(dataRow["quantity"].ToString());
            }
            return pick;
        }

        /// <summary>
        ///     Type 2 Method called SetOrderStatusAllocated, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject SetOrderStatusAllocated(int order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.SetOrderStatusAllocated(order_id);
            return order;
        }

        /// <summary>
        ///     Type 1 Method called GetStockQuantityByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetStockQuantityByID(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetStockQuantityByID(stock_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.quantity = int.Parse(dataRow["quantity"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetStockAccountProductAndWarehouseIDByStockID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetStockAccountProductAndWarehouseIDByStockID(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetStockAccountProductAndWarehouseIDByStockID(stock_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.account_id = int.Parse(dataRow["account_id"].ToString());
                stock.product_id = int.Parse(dataRow["product_id"].ToString());
                stock.warehouse_id = int.Parse(dataRow["warehouse_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetStockSumByProductID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="account_id"></param
        /// <param name="product_id"></param
        /// <param name="warehouse_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetStockSumByProductID(int account_id, int product_id, int warehouse_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetStockSumByProductID(account_id, product_id, warehouse_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.quantity = int.Parse(dataRow["quantity"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetStockAllocatedQuantity, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetStockAllocatedQuantity(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetStockAllocatedQuantity(stock_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.allocated_quantity = int.Parse(dataRow["allocated_quantity"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 2 Method called SetAvailabilityTrue, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject SetAvailabilityTrue(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.SetAvailabilityTrue(stock_id);
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called GetStockAvailabilityStatus, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject GetStockAvailabilityStatus(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.GetStockAvailabilityStatus(stock_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.availability_status = bool.Parse(dataRow["availability_status"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 2 Method called SetAvailabilityTrue, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="location_id"></param
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject SetLocationAllocatedFalse(int location_id)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.SetLocationAllocatedFalse(location_id);
            return location;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called DeleteCurrentOrder, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject DeleteCurrentOrder(int order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentOrder(order_id);
            return order;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentOrder, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_order_reference"></param
        /// <param name="new_account_id"></param
        /// <param name="new_warehouse_id"></param
        /// <param name="new_status"></param
        /// <param name="new_dispatch_date"></param
        /// <param name="new_first_name"></param
        /// <param name="new_last_name"></param
        /// <param name="new_address_line_1"></param
        /// <param name="new_address_line_2"></param
        /// <param name="new_city"></param
        /// <param name="new_postcode"></param
        /// <param name="current_order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject EditCurrentOrder(string new_order_reference, int new_account_id, int new_warehouse_id, string new_status, DateTime new_dispatch_date,
                                            string new_first_name, string new_last_name, string new_address_line_1, string new_address_line_2, string new_city, string new_postcode,
                                            int current_order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.EditCurrentOrder(new_order_reference, new_account_id, new_warehouse_id, new_status, new_dispatch_date,
                                                         new_first_name, new_last_name, new_address_line_1, new_address_line_2, new_city, new_postcode,
                                                         current_order_id);
            return order;
        }





        // MANAGER - Order Lines

        // CHECK

        /// <summary>
        ///     Type 1 Method called CheckOrderLinesAll, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <param name="product_id"></param
        /// <param name="quantity"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject CheckOrderLinesAll(int order_id, int product_id, int quantity)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.CheckOrderLinesAll(order_id, product_id, quantity);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderLine.order_id = Int32.Parse(dataRow["order_id"].ToString());
                orderLine.product_id = Int32.Parse(dataRow["product_id"].ToString());
                orderLine.quantity = Int32.Parse(dataRow["quantity"].ToString());
            }
            return orderLine;
        }

        /// <summary>
        ///     Type 1 Method called CheckOrderLinesByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_line_id"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject CheckOrderLinesByID(int order_line_id)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.CheckOrderLinesByID(order_line_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                orderLine.order_line_id = Int32.Parse(dataRow["order_line_id"].ToString());
            }
            return orderLine;
        }



        // INSERT
        /// <summary>
        ///     Type 2 Method called InsertNewOrderLine, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <param name="product_id"></param
        /// <param name="quantity"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject InsertNewOrderLine(int order_id, int product_id, int quantity)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.InsertNewOrderLine(order_id, product_id, quantity);
            return orderLine;
        }



        // DELETE
        /// <summary>
        ///     Type 2 Method called InsertNewOrderLine, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_line_id"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject DeleteCurrentOrderLine(int order_line_id)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentOrderLine(order_line_id);
            return orderLine;
        }



        // UPDATE
        /// <summary>
        ///     Type 2 Method called EditCurrentOrderLine, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="new_order_id"></param
        /// <param name="new_product_id"></param
        /// <param name="new_quantity"></param
        /// <param name="current_order_line_id"></param
        /// <returns>
        ///     OrderLineObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderLineObject EditCurrentOrderLine(int new_order_id, int new_product_id, int new_quantity, int current_order_line_id)
        {
            OrderLineObject orderLine = new OrderLineObject();
            DataTable dataTable = dataAccessLayer.EditCurrentOrderLine(new_order_id, new_product_id, new_quantity, current_order_line_id);
            return orderLine;
        }





        // MANAGER - PICKS

        // CHECK
        /// <summary>
        ///     Type 1 Method called CheckPicksByPickID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="pick_id"></param
        /// <returns>
        ///     PickObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public PickObject CheckPicksByPickID(int pick_id)
        {
            PickObject pick = new PickObject();
            DataTable dataTable = dataAccessLayer.CheckPicksByPickID(pick_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                pick.pick_id = Int32.Parse(dataRow["pick_id"].ToString());
            }
            return pick;
        }



        // DELETE

        /// <summary>
        ///     Type 1 Method called SelectPicksOrderLineLocationIDAndQuantityByPickID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="pick_id"></param
        /// <returns>
        ///     PickObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public PickObject SelectPicksOrderLineLocationIDAndQuantityByPickID(int pick_id)
        {
            PickObject pick = new PickObject();
            DataTable dataTable = dataAccessLayer.SelectPicksOrderLineLocationIDAndQuantityByPickID(pick_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                pick.order_line_id = Int32.Parse(dataRow["order_line_id"].ToString());
                pick.location_id = Int32.Parse(dataRow["location_id"].ToString());
                pick.quantity = Int32.Parse(dataRow["quantity"].ToString());
            }
            return pick;
        }

        /// <summary>
        ///     Type 1 Method called SelectStockIDByPicksLocationID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="location_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject SelectStockIDByPicksLocationID(int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.SelectStockIDByPicksLocationID(location_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.stock_id = Int32.Parse(dataRow["stock_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called SelectStockLocationIDByID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="stock_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject SelectStockLocationIDByID(int stock_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.SelectStockLocationIDByID(stock_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.location_id = Int32.Parse(dataRow["location_id"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 2 Method called UndoStockQuantity, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="quantity"></param
        /// <param name="location_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject UndoStockQuantity(int quantity, int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.UndoStockQuantity(quantity, location_id);
            return stock;
        }

        /// <summary>
        ///     Type 2 Method called UndoStockAllocatedQuantity, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="allocated_quantity"></param
        /// <param name="location_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject UndoStockAllocatedQuantity(int allocated_quantity, int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.UndoStockAllocatedQuantity(allocated_quantity, location_id);
            return stock;
        }

        /// <summary>
        ///     Type 1 Method called SelectStockQuantityByLocationID, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="location_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject SelectStockQuantityByLocationID(int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.SelectStockQuantityByLocationID(location_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.quantity = Int32.Parse(dataRow["quantity"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 2 Method called UndoStockAvailabilityStatus, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="location_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject UndoStockAvailabilityStatus(int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.UndoStockAvailabilityStatus(location_id);
            return stock;
        }

        /// <summary>
        ///     Type 2 Method called SetOrderStatusCreated, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="order_id"></param
        /// <returns>
        ///     OrderObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public OrderObject SetOrderStatusCreated(int order_id)
        {
            OrderObject order = new OrderObject();
            DataTable dataTable = dataAccessLayer.SetOrderStatusCreated(order_id);
            return order;
        }

        /// <summary>
        ///     Type 1 Method called SelectStockAvailabilityStatusByStockLocation, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="location_id"></param
        /// <returns>
        ///     StockObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public StockObject SelectStockAvailabilityStatusByStockLocation(int location_id)
        {
            StockObject stock = new StockObject();
            DataTable dataTable = dataAccessLayer.SelectStockAvailabilityStatusByStockLocation(location_id);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                stock.availability_status = bool.Parse(dataRow["availability_status"].ToString());
            }
            return stock;
        }

        /// <summary>
        ///     Type 2 Method called SetLocationAllocatedTrue, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="location_id"></param
        /// <returns>
        ///     LocationObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public LocationObject SetLocationAllocatedTrue(int location_id)
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = dataAccessLayer.SetLocationAllocatedTrue(location_id);
            return location;
        }

        /// <summary>
        ///     Type 2 Method called DeleteCurrentPick, matching the method of the same name in the DataAccessLayer class.
        /// </summary>
        /// <param name="pick_id"></param
        /// <returns>
        ///     PickObject - This relates to the type of Database Table being accessed.
        /// </returns>
        public PickObject DeleteCurrentPick(int pick_id)
        {
            PickObject pick = new PickObject();
            DataTable dataTable = dataAccessLayer.DeleteCurrentPick(pick_id);
            return pick;
        }
    }
}


// Old implementations kept just in case.

/*public DataTable SelectAllUsers()
{
    DataTable dataTable = dataAccessLayer.SelectAllUsers();
    return dataTable;

    *//*            string CmdString = string.Empty;
    {
        CmdString = "SELECT emp_id, fname, lname, hire_date FROM Employee";
        //List<UserObject> UsersList = new List<UserObject>();

        //UserObject user = new UserObject();
        SqlCommand cmd = new SqlCommand(DataAccessLayer.SelectAllUsers().query);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dataTable = new DataTable("hello");
        sda.Fill(dataTable);
        return dataTable.DefaultView;

        //return UsersList;
    }*//*

    //List<UserObject> UsersList = new List<UserObject>();
    //UserObject user = new UserObject();
    *//*            foreach (DataRow dataRow in dataTable.Rows)
                {
                    user.user_id = int.Parse(dataRow["user_id"].ToString());
                    user.first_name = dataRow["first_name"].ToString();
                    user.last_name = dataRow["last_name"].ToString();
                    //user.telephone = int.Parse(dataRow["telephone"].ToString());
                    user.email = dataRow["email"].ToString();
                    user.role = dataRow["role"].ToString();
                    user.password = dataRow["password"].ToString();
                    UsersList.Add(user);
                }*/
//}

/*        public LocationObject FindFreeLocation()
        {
            LocationObject location = new LocationObject();
            DataTable dataTable = new DataTable();
            dataTable = dataAccessLayer.FindFreeLocation();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                location.location_id = Int32.Parse(dataRow["location_id"].ToString());
            }
            return location;
        }*/