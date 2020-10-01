using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessTier
{
    /// <summary>
    ///     The methods within this class are used to pass pre-constructed SQL Queries along with the relevant parameters (if there
    ///     are any) to the DbConnection class. They then return a Data Table containing the information asked of by the query from
    ///     the DbConnection method (select, edit, no parameters) it has called, to the matching methods in the BuisnessLogicLayer.
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
    public class DataAccessLayer
    {
        private DbConnection dbConnection;

        /// <summary>
        ///     The Constructor called DataAccessLayer.
        ///     This instantiates the DbConnection class, creating an object of that type.
        /// </summary>
        public DataAccessLayer()
        {
            dbConnection = new DbConnection();
        }


        // LOGIN 

        /// <summary>
        ///     This method Selects User Email, Role, and Password where they match those of the provided parameters in the database
        ///     and returns a Data Table containing that information.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable Login(string email, string role, string password)
        {
            string query = string.Format("SELECT email, role, password FROM users WHERE email = @email AND role = @role AND password = @password");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@email", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(email);
            sqlParameters[1] = new SqlParameter("@role", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(role);
            sqlParameters[2] = new SqlParameter("@password", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(password);
            return dbConnection.SelectQuery(query, sqlParameters);
        }





        // VIEWS

        // USERS

        /// <summary>
        ///     Selects All Users from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllUsers()
        {
            string query = string.Format("SELECT * FROM users ORDER BY user_id");
            return dbConnection.NoParameterSelectQuery(query);
        }

        /// <summary>
        ///     Selects All Users Where Role = 'worker' from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllUsersWorkers()
        {
            string query = string.Format("SELECT * FROM users WHERE role = 'worker' ORDER BY user_id");
            return dbConnection.NoParameterSelectQuery(query);
        }

        /// <summary>
        ///     Selects All Users Where Role = 'manager' from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllUsersManagers()
        {
            string query = string.Format("SELECT * FROM users WHERE role = 'manager' ORDER BY user_id");
            return dbConnection.NoParameterSelectQuery(query);
        }

        /// <summary>
        ///     Selects All Users Where Role = 'admin' from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllUsersAdmins()
        {
            string query = string.Format("SELECT * FROM users WHERE role = 'admin' ORDER BY user_id");
            return dbConnection.NoParameterSelectQuery(query);
        }



        // Products & Accounts

        /// <summary>
        ///     Selects All Products from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllProducts()
        {
            string query = string.Format("SELECT products.product_id, products.account_id, accounts.name, products.title, products.sku FROM products, accounts WHERE products.account_id = accounts.account_id ORDER BY products.product_id");
            return dbConnection.NoParameterSelectQuery(query);
        }

        /// <summary>
        ///     Selects All Accounts from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllAccounts()
        {
            string query = string.Format("SELECT * FROM accounts ORDER BY account_id");
            return dbConnection.NoParameterSelectQuery(query);
        }
        /// <summary>
        ///     Selects All Products matching account ID from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectProductsMatchingAccountID(int account_id)
        {
            string query = string.Format("SELECT products.product_id, products.account_id, accounts.name, products.title, products.sku FROM products, accounts WHERE products.account_id = @account_id AND products.account_id = accounts.account_id ORDER BY products.product_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // Receipts & Receipt Lines

        /// <summary>
        ///     Selects All Receipts from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllReceipts()
        {
            string query = string.Format("SELECT receipts.receipt_id, receipts.date_added, receipts.receipt_reference, receipts.account_id, accounts.name, receipts.warehouse_id, warehouses.name, receipts.expected_date, receipts.receipted_date FROM receipts, accounts, warehouses WHERE receipts.account_id = accounts.account_id AND receipts.warehouse_id = warehouses.warehouse_id ORDER BY receipts.date_added DESC");
            return dbConnection.NoParameterSelectQuery(query);
        }
        /// <summary>
        ///     Selects All Receipt Lines matching Receipt ID from the Database
        /// </summary>
        /// <param name="receipt_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectReceiptLinesMatchingReceiptID(int receipt_id)
        {
            string query = string.Format("SELECT receipt_lines.receipt_line_id, receipt_lines.receipt_id, receipts.receipt_reference, receipt_lines.product_id, products.title, products.sku, receipt_lines.quantity, receipt_lines.date_added FROM receipt_lines, receipts, products WHERE receipt_lines.receipt_id = @receipt_id AND receipt_lines.receipt_id = receipts.receipt_id AND receipt_lines.product_id = products.product_id ORDER BY receipt_lines.date_added DESC");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@receipt_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(receipt_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects All Receipt Lines from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllReceiptLines()
        {
            string query = string.Format("SELECT receipt_lines.receipt_line_id, receipt_lines.receipt_id, receipts.receipt_reference, receipt_lines.product_id, products.title, products.sku, receipt_lines.quantity, receipt_lines.date_added FROM receipt_lines, receipts, products WHERE receipt_lines.receipt_id = receipts.receipt_id AND receipt_lines.product_id = products.product_id ORDER BY receipt_lines.date_added DESC");
            return dbConnection.NoParameterSelectQuery(query);
        }



        // Stock & Locations

        /// <summary>
        ///     Selects All Stock from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllStock()
        {
            string query = string.Format("SELECT stock.stock_id, stock.account_id, accounts.name, stock.product_id, products.title, products.sku, stock.warehouse_id, warehouses.name, stock.location_id, locations.location_code, stock.quantity, stock.allocated_quantity, stock.availability_status, stock.date_added FROM stock, accounts, products, warehouses, locations WHERE quantity > 0 AND stock.account_id = accounts.account_id AND stock.product_id = products.product_id AND stock.product_id = products.product_id AND stock.warehouse_id = warehouses.warehouse_id AND stock.location_id = locations.location_id ORDER BY stock.date_added DESC");
            return dbConnection.NoParameterSelectQuery(query);
        }

        /// <summary>
        ///     Selects All Locations from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllLocations()
        {
            string query = string.Format("SELECT locations.location_id, locations.warehouse_id, warehouses.name, locations.location_code, locations.allocated, locations.put_sequence, locations.pick_sequence FROM locations, warehouses WHERE locations.warehouse_id = warehouses.warehouse_id ORDER BY location_id");
            return dbConnection.NoParameterSelectQuery(query);
        }
        /// <summary>
        ///     Selects All Stock matching Location ID from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllStockMatchingLocationID(int location_id)
        {
            string query = string.Format("SELECT stock.stock_id, stock.account_id, accounts.name, stock.product_id, products.title, products.sku, stock.warehouse_id, warehouses.name, stock.location_id, locations.location_code, stock.quantity, stock.allocated_quantity, stock.availability_status, stock.date_added FROM stock, accounts, products, warehouses, locations WHERE quantity > 0 AND stock.location_id = @location_id AND stock.account_id = accounts.account_id AND stock.product_id = products.product_id AND stock.product_id = products.product_id AND stock.warehouse_id = warehouses.warehouse_id AND stock.location_id = locations.location_id ORDER BY stock.date_added DESC");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects All Warehouses from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllWarehouses()
        {
            string query = string.Format("SELECT * FROM warehouses ORDER BY warehouse_id");
            return dbConnection.NoParameterSelectQuery(query);
        }
        /// <summary>
        ///     Selects All Stock matching Warehouse ID from the Database
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllStockMatchingWarehouseID(int warehouse_id)
        {
            string query = string.Format("SELECT stock.stock_id, stock.account_id, accounts.name, stock.product_id, products.title, products.sku, stock.warehouse_id, warehouses.name, stock.location_id, locations.location_code, stock.quantity, stock.allocated_quantity, stock.availability_status, stock.date_added FROM stock, accounts, products, warehouses, locations WHERE quantity > 0 AND stock.warehouse_id = @warehouse_id AND stock.account_id = accounts.account_id AND stock.product_id = products.product_id AND stock.product_id = products.product_id AND stock.warehouse_id = warehouses.warehouse_id AND stock.location_id = locations.location_id ORDER BY stock.date_added DESC");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }
        /// <summary>
        ///     Selects All Locations matching Warehouse ID from the Database
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllLocationsMatchingWarehouseID(int warehouse_id)
        {
            string query = string.Format("SELECT locations.location_id, locations.warehouse_id, warehouses.name, locations.location_code, locations.allocated, locations.put_sequence, locations.pick_sequence FROM locations, warehouses WHERE locations.warehouse_id = @warehouse_id AND locations.warehouse_id = warehouses.warehouse_id ORDER BY location_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // Orders, Order Lines & Picks

        /// <summary>
        ///     Selects All Order Lines from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllOrders()
        {
            string query = string.Format("SELECT orders.order_id, orders.date_added, orders.order_reference, orders.account_id, accounts.name, orders.warehouse_id, warehouses.name, orders.status, orders.dispatch_date, orders.first_name, orders.last_name, orders.address_line_1, orders.address_line_2, orders.city, orders.postcode FROM orders, accounts, warehouses WHERE orders.account_id = accounts.account_id AND orders.warehouse_id = warehouses.warehouse_id ORDER BY orders.date_added DESC");
            return dbConnection.NoParameterSelectQuery(query);
        }
        /// <summary>
        ///     Selects All Order Lines matching Order ID from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectOrderLinesMatchingOrderID(int order_id)
        {
            string query = string.Format("SELECT order_lines.order_line_id, order_lines.order_id, orders.order_reference, order_lines.product_id, products.title, products.sku, order_lines.quantity, order_lines.date_added FROM order_lines, orders, products WHERE order_lines.order_id = @order_id AND order_lines.order_id = orders.order_id AND order_lines.product_id = products.product_id ORDER BY order_lines.date_added DESC");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects All Order Lines from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllOrderLines()
        {
            string query = string.Format("SELECT order_lines.order_line_id, order_lines.order_id, orders.order_reference, order_lines.product_id, products.title, products.sku, order_lines.quantity, order_lines.date_added FROM order_lines, orders, products WHERE order_lines.order_id = orders.order_id AND order_lines.product_id = products.product_id ORDER BY order_lines.date_added DESC");
            return dbConnection.NoParameterSelectQuery(query);
        }

        /// <summary>
        ///     Selects All Picks from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectAllPicks()
        {
            string query = string.Format("SELECT picks.pick_id, picks.order_id, orders.order_reference, picks.order_line_id, orders.warehouse_id, warehouses.name, picks.location_id, picks.location_code, picks.product_id, picks.product_title, picks.quantity, picks.date_added FROM picks, orders, warehouses, locations WHERE picks.order_id = orders.order_id AND orders.warehouse_id = warehouses.warehouse_id AND picks.location_id = locations.location_id ORDER BY picks.order_id DESC, pick_sequence, date_added DESC");
            return dbConnection.NoParameterSelectQuery(query);
        }





        // ADMIN - USER

        /// <summary>
        ///     Counts no. of Users from the Database
        /// </summary>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CountAllUsers()
        {
            string query = string.Format("select count(*) as user_id from users");
            return dbConnection.NoParameterSelectQuery(query);
        }



        // CHECK

        /// <summary>
        ///     Selects Email from Users where Email = Email Parameter from the Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckUsersInsert(string email)
        {
            string query = string.Format("SELECT email FROM users WHERE email = @email");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@email", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(email);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects first_name, last_name, telephone, email, role from Users where Parameters match, from the Database
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckUsersDelete(string first_name, string last_name, long telephone, string email, string role)
        {
            string query = string.Format("SELECT first_name, last_name, telephone, email, role FROM users WHERE first_name = @first_name AND last_name = @last_name AND telephone = @telephone AND email = @email AND role = @role");
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@first_name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(first_name);
            sqlParameters[1] = new SqlParameter("@last_name", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(last_name);
            sqlParameters[2] = new SqlParameter("@telephone", SqlDbType.BigInt);
            sqlParameters[2].Value = Convert.ToString(telephone);
            sqlParameters[3] = new SqlParameter("@email", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(email);
            sqlParameters[4] = new SqlParameter("@role", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(role);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects first_name, last_name, telephone, email, role, password from Users where Parameters match, from the Database
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckUsersAllEdit(string first_name, string last_name, long telephone, string email, string role, string password)
        {
            string query = string.Format("SELECT first_name, last_name, telephone, email, role, password FROM users WHERE first_name = @first_name AND last_name = @last_name AND telephone = @telephone AND email = @email AND role = @role AND password = @password");
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@first_name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(first_name);
            sqlParameters[1] = new SqlParameter("@last_name", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(last_name);
            sqlParameters[2] = new SqlParameter("@telephone", SqlDbType.BigInt);
            sqlParameters[2].Value = Convert.ToString(telephone);
            sqlParameters[3] = new SqlParameter("@email", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(email);
            sqlParameters[4] = new SqlParameter("@role", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(role);
            sqlParameters[5] = new SqlParameter("@password", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(password);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects email from Users where Parameters match, from the Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckUsersEmailEdit(string email)
        {
            string query = string.Format("SELECT email FROM users WHERE email = @email");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@email", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(email);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Inserts new user into Users where Parameters match, from the Database
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewUser(string first_name, string last_name, long telephone, string email, string role, string password)
        {
            string query = string.Format("INSERT INTO users (first_name, last_name, telephone, email, role, password) VALUES (@first_name, @last_name, @telephone, @email, @role, @password)");
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@first_name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(first_name);
            sqlParameters[1] = new SqlParameter("@last_name", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(last_name);
            sqlParameters[2] = new SqlParameter("@telephone", SqlDbType.BigInt);
            sqlParameters[2].Value = Convert.ToString(telephone);
            sqlParameters[3] = new SqlParameter("@email", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(email);
            sqlParameters[4] = new SqlParameter("@role", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(role);
            sqlParameters[5] = new SqlParameter("@password", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(password);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes current user from Users where Parameters match, from the Database
        /// </summary>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="telephone"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentUser(string first_name, string last_name, long telephone, string email, string role)
        {
            string query = string.Format("DELETE FROM users WHERE first_name = @first_name AND last_name = @last_name AND telephone = @telephone AND email = @email AND role = @role");
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@first_name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(first_name);
            sqlParameters[1] = new SqlParameter("@last_name", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(last_name);
            sqlParameters[2] = new SqlParameter("@telephone", SqlDbType.BigInt);
            sqlParameters[2].Value = Convert.ToString(telephone);
            sqlParameters[3] = new SqlParameter("@email", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(email);
            sqlParameters[4] = new SqlParameter("@role", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(role);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // UPDATE

        /// <summary>
        ///     Updates current user from Users where Parameters match, from the Database
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
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentUser(string new_first_name, string new_last_name, long new_telephone, string new_email, string new_role, string new_password,
                                         string current_first_name, string current_last_name, long current_telephone, string current_email, string current_role, string current_password)
        {
            string query = string.Format("UPDATE users SET first_name = @new_first_name, last_name = @new_last_name, telephone = @new_telephone, email = @new_email, role = @new_role, password = @new_password WHERE first_name = @current_first_name AND last_name = @current_last_name AND telephone = @current_telephone AND email = @current_email AND role = @current_role AND password = @current_password");
            SqlParameter[] sqlParameters = new SqlParameter[12];

            sqlParameters[0] = new SqlParameter("@new_first_name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(new_first_name);
            sqlParameters[1] = new SqlParameter("@new_last_name", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(new_last_name);
            sqlParameters[2] = new SqlParameter("@new_telephone", SqlDbType.BigInt);
            sqlParameters[2].Value = Convert.ToString(new_telephone);
            sqlParameters[3] = new SqlParameter("@new_email", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(new_email);
            sqlParameters[4] = new SqlParameter("@new_role", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(new_role);
            sqlParameters[5] = new SqlParameter("@new_password", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(new_password);

            sqlParameters[6] = new SqlParameter("@current_first_name", SqlDbType.VarChar);
            sqlParameters[6].Value = Convert.ToString(current_first_name);
            sqlParameters[7] = new SqlParameter("@current_last_name", SqlDbType.VarChar);
            sqlParameters[7].Value = Convert.ToString(current_last_name);
            sqlParameters[8] = new SqlParameter("@current_telephone", SqlDbType.BigInt);
            sqlParameters[8].Value = Convert.ToString(current_telephone);
            sqlParameters[9] = new SqlParameter("@current_email", SqlDbType.VarChar);
            sqlParameters[9].Value = Convert.ToString(current_email);
            sqlParameters[10] = new SqlParameter("@current_role", SqlDbType.VarChar);
            sqlParameters[10].Value = Convert.ToString(current_role);
            sqlParameters[11] = new SqlParameter("@current_password", SqlDbType.VarChar);
            sqlParameters[11].Value = Convert.ToString(current_password);

            return dbConnection.EditQuery(query, sqlParameters);
        }





        // ADMIN - ACCOUNT

        // CHECK

        /// <summary>
        ///     Selects Name from Accounts where Parameters match, from the Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckAccountsByName(string name)
        {
            string query = string.Format("SELECT name FROM accounts WHERE name = @name");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(name);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Account_id from Accounts where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckAccountsByID(int account_id)
        {
            string query = string.Format("SELECT account_id FROM accounts WHERE account_id = @account_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Inserts new Account into Accounts where Parameters match, from the Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewAccount(string name)
        {
            string query = string.Format("INSERT INTO accounts (name) VALUES (@name)");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(name);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        // DELETE

        /// <summary>
        ///     Deletes current Account from Accounts where Parameters match, from the Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentAccount(string name)
        {
            string query = string.Format("DELETE FROM accounts WHERE name = @name");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(name);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // UPDATE

        /// <summary>
        ///     Updates Account from Accounts where Parameters match, from the Database
        /// </summary>
        /// <param name="new_name"></param>
        /// <param name="current_name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentAccount(string new_name, string current_name)
        {
            string query = string.Format("UPDATE accounts SET name = @new_name WHERE name = @current_name");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@new_name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(new_name);

            sqlParameters[1] = new SqlParameter("@current_name", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(current_name);
            return dbConnection.EditQuery(query, sqlParameters);
        }





        // ADMIN - WAREHOUSE

        // CHECK

        /// <summary>
        ///     Selects Name from Warehouses where Parameters match, from the Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckWarehousesByName(string name)
        {
            string query = string.Format("SELECT name FROM warehouses WHERE name = @name");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(name);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Warehouse ID from Warehouses where Parameters match, from the Database
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckWarehousesByID(int warehouse_id)
        {
            string query = string.Format("SELECT warehouse_id FROM warehouses WHERE warehouse_id = @warehouse_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Inserts new Warehouse int Warehouses where Parameters match, from the Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewWarehouse(string name)
        {
            string query = string.Format("INSERT INTO warehouses (name) VALUES (@name)");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(name);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes Warehouse from Warehouses where Parameters match, from the Database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentWarehouse(string name)
        {
            string query = string.Format("DELETE FROM warehouses WHERE name = @name");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(name);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        // UPDATE

        /// <summary>
        ///     Updates Warehouse from Warehouses where Parameters match, from the Database
        /// </summary>
        /// <param name="new_name"></param>
        /// <param name="current_name"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentWarehouse(string new_name, string current_name)
        {
            string query = string.Format("UPDATE warehouses SET name = @new_name WHERE name = @current_name");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@new_name", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(new_name);

            sqlParameters[1] = new SqlParameter("@current_name", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(current_name);
            return dbConnection.EditQuery(query, sqlParameters);
        }





        // MANAGER - PRODUCTS

        // CHECK

        /// <summary>
        ///     Selects Account ID, Title, SKU from Products where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="title"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckProductsAll(int account_id, string title, string sku)
        {
            string query = string.Format("SELECT account_id, title, sku FROM products WHERE account_id = @account_id AND title = @title AND sku = @sku");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@title", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(title);
            sqlParameters[2] = new SqlParameter("@sku", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(sku);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Account ID, SKU from Products where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckProductsByIDAndSku(int account_id, string sku)
        {
            string query = string.Format("SELECT account_id, sku FROM products WHERE account_id = @account_id AND sku = @sku");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@sku", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(sku);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Product ID from Products where Parameters match, from the Database
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckProductsByID(int product_id)
        {
            string query = string.Format("SELECT product_id FROM products WHERE product_id = @product_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(product_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Inserts new Product into Products where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="title"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewProduct(int account_id, string title, string sku)
        {
            string query = string.Format("INSERT INTO products (account_id, title, sku) VALUES (@account_id, @title, @sku)");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@title", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(title);
            sqlParameters[2] = new SqlParameter("@sku", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(sku);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes current Product from Products where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="title"></param>
        /// <param name="sku"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentProduct(int account_id, string title, string sku)
        {
            string query = string.Format("DELETE FROM products WHERE account_id = @account_id AND title = @title AND sku = @sku");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@title", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(title);
            sqlParameters[2] = new SqlParameter("@sku", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(sku);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        // UPDATE

        /// <summary>
        ///     Updates current Product from Products where Parameters match, from the Database
        /// </summary>
        /// <param name="new_account_id"></param>
        /// <param name="new_title"></param>
        /// <param name="new_sku"></param>
        /// <param name="current_account_id"></param>
        /// <param name="current_title"></param>
        /// <param name="current_sku"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentProduct(int new_account_id, string new_title, string new_sku, int current_account_id, string current_title, string current_sku)
        {
            string query = string.Format("UPDATE products SET account_id = @new_account_id, title = @new_title, sku = @new_sku WHERE account_id = @current_account_id AND title = @current_title AND sku = @current_sku");
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@new_account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(new_account_id);
            sqlParameters[1] = new SqlParameter("@new_title", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(new_title);
            sqlParameters[2] = new SqlParameter("@new_sku", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(new_sku);

            sqlParameters[3] = new SqlParameter("@current_account_id", SqlDbType.Int);
            sqlParameters[3].Value = Convert.ToString(current_account_id);
            sqlParameters[4] = new SqlParameter("@current_title", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(current_title);
            sqlParameters[5] = new SqlParameter("@current_sku", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(current_sku);
            return dbConnection.EditQuery(query, sqlParameters);
        }





        // MANAGER - STOCK

        // CHECK

        /// <summary>
        ///     Selects all Stock from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="product_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <param name="quantity"></param>
        /// <param name="allocated_quantity"></param>
        /// <param name="availability_status"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckStockAll(int account_id, int product_id, int warehouse_id, int location_id, int quantity, int allocated_quantity, bool availability_status)
        {
            string query = string.Format("SELECT account_id, product_id, warehouse_id, location_id, quantity, allocated_quantity, availability_status FROM stock WHERE account_id = @account_id AND product_id = @product_id AND warehouse_id = @warehouse_id AND location_id = @location_id AND quantity = @quantity AND allocated_quantity = @allocated_quantity AND availability_status = @availability_status");
            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            sqlParameters[3] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[3].Value = Convert.ToString(location_id);
            sqlParameters[4] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[4].Value = Convert.ToString(quantity);
            sqlParameters[5] = new SqlParameter("@allocated_quantity", SqlDbType.Int);
            sqlParameters[5].Value = Convert.ToString(allocated_quantity);
            sqlParameters[6] = new SqlParameter("@availability_status", SqlDbType.Bit);
            sqlParameters[6].Value = Convert.ToString(availability_status);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Stock ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckStockByID(int stock_id)
        {
            string query = string.Format("SELECT stock_id FROM stock WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Stock ID and Location ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckStockByIDAndLocation(int stock_id, int location_id)
        {
            string query = string.Format("SELECT stock_id, location_id FROM stock WHERE stock_id = @stock_id AND location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            sqlParameters[1] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Warehouse ID and Location ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckStockByWarehouseAndLocation(int warehouse_id, int location_id)
        {
            string query = string.Format("SELECT warehouse_id, location_id FROM stock WHERE warehouse_id = @warehouse_id AND location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(warehouse_id);
            sqlParameters[1] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Location ID from Locations where Parameters match, from the Database
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckLocationByIDAndWarehouse(int location_id, int warehouse_id)
        {
            string query = string.Format("SELECT location_id FROM locations WHERE location_id = @location_id AND warehouse_id = @warehouse_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            sqlParameters[1] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Stock ID, Warehouse ID, Location ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckStockByIDAndWarehouseAndLocation(int stock_id, int warehouse_id, int location_id)
        {
            string query = string.Format("SELECT stock_id, warehouse_id, location_id FROM stock WHERE stock_id = @stock_id AND warehouse_id = @warehouse_id AND location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            sqlParameters[1] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(warehouse_id);
            sqlParameters[2] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Allocated Quantity from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckStockIsAllocated(int stock_id)
        {
            string query = string.Format("SELECT allocated_quantity FROM stock WHERE stock_id = @stock_id AND allocated_quantity > 0");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Top 1 Location ID from Locations where Parameters match, from the Database
        /// </summary>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable FindFreeLocation(int warehouse_id)
        {
            string query = string.Format("SELECT TOP 1 location_id FROM locations WHERE allocated = 0 AND warehouse_id = @warehouse_id ORDER BY put_sequence");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates allocated to 1 in Locations where Parameters match, from the Database
        /// </summary>
        /// <param name="stockLocation"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable MarkLocationAllocated(int stockLocation)
        {
            string query = string.Format("UPDATE locations SET allocated = 1 WHERE location_id = @stockLocation");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stockLocation", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stockLocation);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates allocated to 0 in Locations where Parameters match, from the Database
        /// </summary>
        /// <param name="stockLocation"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable MarkLocationUnallocated(int stockLocation)
        {
            string query = string.Format("UPDATE locations SET allocated = 0 WHERE location_id = @stockLocation");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stockLocation", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stockLocation);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Inserts new Stock in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="product_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="location_id"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewStock(int account_id, int product_id, int warehouse_id, int location_id, int quantity)
        {
            string query = string.Format("INSERT INTO stock (account_id, product_id, warehouse_id, location_id, quantity) VALUES (@account_id, @product_id, @warehouse_id, @location_id, @quantity)");
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            sqlParameters[3] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[3].Value = Convert.ToString(location_id);
            sqlParameters[4] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[4].Value = Convert.ToString(quantity);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes current Stock from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentStock(int stock_id)
        {
            string query = string.Format("DELETE FROM stock WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // UPDATE

        /// <summary>
        ///     Updates current Stock in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="new_account_id"></param>
        /// <param name="new_product_id"></param>
        /// <param name="new_warehouse_id"></param>
        /// <param name="new_location_id"></param>
        /// <param name="new_quantity"></param>
        /// <param name="new_allocated_quantity"></param>
        /// <param name="new_availability_status"></param>
        /// <param name="current_stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentStock(int new_account_id, int new_product_id, int new_warehouse_id, int new_location_id, int new_quantity, int new_allocated_quantity,
                                  bool new_availability_status,
                                  int current_stock_id)
        {
            string query = string.Format("UPDATE stock SET account_id = @new_account_id, product_id = @new_product_id, warehouse_id = @new_warehouse_id, location_id = @new_location_id, quantity = @new_quantity, allocated_quantity = @new_allocated_quantity, availability_status = @new_availability_status WHERE stock_id = @current_stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[8];
            sqlParameters[0] = new SqlParameter("@new_account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(new_account_id);
            sqlParameters[1] = new SqlParameter("@new_product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(new_product_id);
            sqlParameters[2] = new SqlParameter("@new_warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(new_warehouse_id);
            sqlParameters[3] = new SqlParameter("@new_location_id", SqlDbType.Int);
            sqlParameters[3].Value = Convert.ToString(new_location_id);
            sqlParameters[4] = new SqlParameter("@new_quantity", SqlDbType.Int);
            sqlParameters[4].Value = Convert.ToString(new_quantity);
            sqlParameters[5] = new SqlParameter("@new_allocated_quantity", SqlDbType.Int);
            sqlParameters[5].Value = Convert.ToString(new_allocated_quantity);
            sqlParameters[6] = new SqlParameter("@new_availability_status", SqlDbType.Bit);
            sqlParameters[6].Value = Convert.ToString(new_availability_status);

            sqlParameters[7] = new SqlParameter("@current_stock_id", SqlDbType.Int);
            sqlParameters[7].Value = Convert.ToString(current_stock_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }





        // N/A - LOCATIONS

        // CHECK

        /// <summary>
        ///     Selects Location ID from Locations where Parameters match, from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckLocationsByID(int location_id)
        {
            string query = string.Format("SELECT location_id FROM locations WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }





        // MANAGER - RECEIPTS

        // CHECK

        /// <summary>
        ///     Selects receipt_reference, account_id, warehouse_id, expected_date from Receipts where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_reference"></param>
        /// <param name="account_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="expected_date"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckReceiptsAll(string receipt_reference, int account_id, int warehouse_id, DateTime expected_date)
        {
            string query = string.Format("SELECT receipt_reference, account_id, warehouse_id, expected_date FROM receipts WHERE receipt_reference = @receipt_reference AND account_id = @account_id AND warehouse_id = @warehouse_id AND expected_date = @expected_date");
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@receipt_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(receipt_reference);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            sqlParameters[3] = new SqlParameter("@expected_date", SqlDbType.DateTime);
            sqlParameters[3].Value = Convert.ToString(expected_date);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects receipt_id from Receipts where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckReceiptsByID(int receipt_id)
        {
            string query = string.Format("SELECT receipt_id FROM receipts WHERE receipt_id = @receipt_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@receipt_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(receipt_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects receipt_reference, account_id from Receipts where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_reference"></param>
        /// <param name="account_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckReceiptsByRefAndAccountID(string receipt_reference, int account_id)
        {
            string query = string.Format("SELECT receipt_reference, account_id FROM receipts WHERE receipt_reference = @receipt_reference AND account_id = @account_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@receipt_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(receipt_reference);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Inserts new Receipt into Receipts where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_reference"></param>
        /// <param name="account_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="expected_date"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewReceipt(string receipt_reference, int account_id, int warehouse_id, DateTime expected_date)
        {
            string query = string.Format("INSERT INTO receipts (receipt_reference, account_id, warehouse_id, expected_date) VALUES (@receipt_reference, @account_id, @warehouse_id, @expected_date)");
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@receipt_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(receipt_reference);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            sqlParameters[3] = new SqlParameter("@expected_date", SqlDbType.DateTime);
            sqlParameters[3].Value = Convert.ToString(expected_date);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes current Receipt from Receipts where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_reference"></param>
        /// <param name="account_id"></param>
        /// <param name="warehouse_id"></param>
        /// <param name="expected_date"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentReceipt(string receipt_reference, int account_id, int warehouse_id, DateTime expected_date)
        {
            string query = string.Format("DELETE FROM receipts WHERE account_id = @account_id AND account_id = @account_id AND warehouse_id = @warehouse_id AND expected_date = @expected_date");
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@receipt_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(receipt_reference);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            sqlParameters[3] = new SqlParameter("@expected_date", SqlDbType.DateTime);
            sqlParameters[3].Value = Convert.ToString(expected_date);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // UPDATE

        /// <summary>
        ///     Updates current Receipt from Receipts where Parameters match, from the Database
        /// </summary>
        /// <param name="new_receipt_reference"></param>
        /// <param name="new_account_id"></param>
        /// <param name="new_warehouse_id"></param>
        /// <param name="new_expected_date"></param>
        /// <param name="receipted_date"></param>
        /// <param name="current_receipt_reference"></param>
        /// <param name="current_account_id"></param>
        /// <param name="current_warehouse_id"></param>
        /// <param name="current_expected_date"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentReceipt(string new_receipt_reference, int new_account_id, int new_warehouse_id, DateTime new_expected_date, DateTime receipted_date, 
                                            string current_receipt_reference, int current_account_id, int current_warehouse_id, DateTime current_expected_date)
        {
            string query = string.Format("UPDATE receipts SET receipt_reference = @new_receipt_reference, account_id = @new_account_id, warehouse_id = @new_warehouse_id, expected_date = @new_expected_date, receipted_date = @receipted_date WHERE receipt_reference = @current_receipt_reference AND account_id = @current_account_id AND warehouse_id = @current_warehouse_id AND expected_date = @current_expected_date");
            SqlParameter[] sqlParameters = new SqlParameter[9];
            sqlParameters[0] = new SqlParameter("@new_receipt_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(new_receipt_reference);
            sqlParameters[1] = new SqlParameter("@new_account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(new_account_id);
            sqlParameters[2] = new SqlParameter("@new_warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(new_warehouse_id);
            sqlParameters[3] = new SqlParameter("@new_expected_date", SqlDbType.DateTime);
            sqlParameters[3].Value = Convert.ToString(new_expected_date);
            sqlParameters[4] = new SqlParameter("@receipted_date", SqlDbType.DateTime);
            sqlParameters[4].Value = Convert.ToString(receipted_date);

            sqlParameters[5] = new SqlParameter("@current_receipt_reference", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(current_receipt_reference);
            sqlParameters[6] = new SqlParameter("@current_account_id", SqlDbType.Int);
            sqlParameters[6].Value = Convert.ToString(current_account_id);
            sqlParameters[7] = new SqlParameter("@current_warehouse_id", SqlDbType.Int);
            sqlParameters[7].Value = Convert.ToString(current_warehouse_id);
            sqlParameters[8] = new SqlParameter("@current_expected_date", SqlDbType.DateTime);
            sqlParameters[8].Value = Convert.ToString(current_expected_date);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates current Receipt from Receipts where Parameters match, from the Database
        /// </summary>
        /// <param name="new_receipt_reference"></param>
        /// <param name="new_account_id"></param>
        /// <param name="new_warehouse_id"></param>
        /// <param name="new_expected_date"></param>
        /// <param name="current_receipt_reference"></param>
        /// <param name="current_account_id"></param>
        /// <param name="current_warehouse_id"></param>
        /// <param name="current_expected_date"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentReceiptNoRecDate(string new_receipt_reference, int new_account_id, int new_warehouse_id, DateTime new_expected_date,
                                                     string current_receipt_reference, int current_account_id, int current_warehouse_id, DateTime current_expected_date)
        {
            string query = string.Format("UPDATE receipts SET receipt_reference = @new_receipt_reference, account_id = @new_account_id, warehouse_id = @new_warehouse_id, expected_date = @new_expected_date WHERE receipt_reference = @current_receipt_reference AND account_id = @current_account_id AND warehouse_id = @current_warehouse_id AND expected_date = @current_expected_date");
            SqlParameter[] sqlParameters = new SqlParameter[8];
            sqlParameters[0] = new SqlParameter("@new_receipt_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(new_receipt_reference);
            sqlParameters[1] = new SqlParameter("@new_account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(new_account_id);
            sqlParameters[2] = new SqlParameter("@new_warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(new_warehouse_id);
            sqlParameters[3] = new SqlParameter("@new_expected_date", SqlDbType.DateTime);
            sqlParameters[3].Value = Convert.ToString(new_expected_date);

            sqlParameters[4] = new SqlParameter("@current_receipt_reference", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(current_receipt_reference);
            sqlParameters[5] = new SqlParameter("@current_account_id", SqlDbType.Int);
            sqlParameters[5].Value = Convert.ToString(current_account_id);
            sqlParameters[6] = new SqlParameter("@current_warehouse_id", SqlDbType.Int);
            sqlParameters[6].Value = Convert.ToString(current_warehouse_id);
            sqlParameters[7] = new SqlParameter("@current_expected_date", SqlDbType.DateTime);
            sqlParameters[7].Value = Convert.ToString(current_expected_date);
            return dbConnection.EditQuery(query, sqlParameters);
        }





        // MANAGER - Receipt Lines

        // CHECK

        /// <summary>
        ///     Select Receipt ID, Product ID, Quantity from Receipt Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_id"></param>
        /// <param name="product_id"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckReceiptLinesAll(int receipt_id, int product_id, int quantity)
        {
            string query = string.Format("SELECT receipt_id, product_id, quantity FROM receipt_lines WHERE receipt_id = @receipt_id AND product_id = @product_id AND quantity = @quantity");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@receipt_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(receipt_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(quantity);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Select Receipt Line ID from Receipt Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_id"></param>
        /// <param name="product_id"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckReceiptLinesByID(int receipt_line_id)
        {
            string query = string.Format("SELECT receipt_line_id FROM receipt_lines WHERE receipt_line_id = @receipt_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@receipt_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(receipt_line_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Insert new Receipt Line into Receipt Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_id"></param>
        /// <param name="product_id"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewReceiptLine(int receipt_id, int product_id, int quantity)
        {
            string query = string.Format("INSERT INTO receipt_lines (receipt_id, product_id, quantity) VALUES (@receipt_id, @product_id, @quantity)");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@receipt_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(receipt_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(quantity);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes current Receipt Line from Receipt Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="receipt_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentReceiptLine(int receipt_line_id)
        {
            string query = string.Format("DELETE FROM receipt_lines WHERE receipt_line_id = @receipt_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@receipt_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(receipt_line_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // UPDATE

        /// <summary>
        ///     Updates current Receipt Line in Receipt Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="new_receipt_id"></param>
        /// <param name="new_product_id"></param>
        /// <param name="new_quantity"></param>
        /// <param name="current_receipt_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentReceiptLine(int new_receipt_id, int new_product_id, int new_quantity, int current_receipt_line_id)
        {
            string query = string.Format("UPDATE receipt_lines SET receipt_id = @new_receipt_id, product_id = @new_product_id, quantity = @new_quantity WHERE receipt_line_id = @current_receipt_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@new_receipt_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(new_receipt_id);
            sqlParameters[1] = new SqlParameter("@new_product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(new_product_id);
            sqlParameters[2] = new SqlParameter("@new_quantity", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(new_quantity);

            sqlParameters[3] = new SqlParameter("@current_receipt_line_id", SqlDbType.Int);
            sqlParameters[3].Value = Convert.ToString(current_receipt_line_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }




        // MANAGER - Orders

        // CHECK

        /// <summary>
        ///     Selects all from Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_reference"></param>
        /// <param name="account_id"></param>
        /// <param name="dispatch_date"></param>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="address_line_1"></param>
        /// <param name="address_line_2"></param>
        /// <param name="city"></param>
        /// <param name="postcode"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckOrdersAll(string order_reference, int account_id, int warehouse_id, DateTime dispatch_date, string first_name, string last_name, 
                                        string address_line_1, string address_line_2, string city, string postcode)
        {
            string query = string.Format("SELECT order_reference, account_id, warehouse_id, dispatch_date, first_name, last_name, address_line_1, address_line_2, address_line2, city, postcode FROM orders WHERE order_reference = @order_reference AND account_id = @account_id AND warehouse_id = @warehouse_id AND dispatch_date = @dispatch_date AND first_name = @first_name AND last_name = @last_name AND address_line_1 = @address_line_1 AND address_line_2 = @address_line_2 AND city = @city AND postcode = @postcode");
            SqlParameter[] sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@order_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(order_reference);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            sqlParameters[3] = new SqlParameter("@dispatch_date", SqlDbType.DateTime);
            sqlParameters[3].Value = Convert.ToString(dispatch_date);
            sqlParameters[4] = new SqlParameter("@first_name", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(first_name);
            sqlParameters[5] = new SqlParameter("@last_name", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(last_name);
            sqlParameters[6] = new SqlParameter("@address_line_1", SqlDbType.VarChar);
            sqlParameters[6].Value = Convert.ToString(address_line_1);
            sqlParameters[7] = new SqlParameter("@address_line_2", SqlDbType.VarChar);
            sqlParameters[7].Value = Convert.ToString(address_line_2);
            sqlParameters[8] = new SqlParameter("@city", SqlDbType.VarChar);
            sqlParameters[8].Value = Convert.ToString(city);
            sqlParameters[9] = new SqlParameter("@postcode", SqlDbType.VarChar);
            sqlParameters[9].Value = Convert.ToString(postcode);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Order ID from Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckOrdersByID(int order_id)
        {
            string query = string.Format("SELECT order_id FROM orders WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }


        /// <summary>
        ///     Selects Order Reference and Account ID from Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_reference"></param>
        /// <param name="account_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckOrdersByRefAndAccountID(string order_reference, int account_id)
        {
            string query = string.Format("SELECT order_reference, account_id FROM orders WHERE order_reference = @order_reference AND account_id = @account_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@order_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(order_reference);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Order ID, Order Reference and Account ID from Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="order_reference"></param>
        /// <param name="account_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckOrdersByIDAndRefAndAccID(int order_id, string order_reference, int account_id)
        {
            string query = string.Format("SELECT order_id, order_reference, account_id FROM orders WHERE order_id = @order_id AND order_reference = @order_reference AND account_id = @account_id");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            sqlParameters[1] = new SqlParameter("@order_reference", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(order_reference);
            sqlParameters[2] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(account_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Status from Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckOrderIsAllocated(int order_id)
        {
            string query = string.Format("SELECT status FROM orders WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Inserts new Order into Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_reference"></param>
        /// <param name="account_id"></param>
        /// <param name="dispatch_date"></param>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="address_line_1"></param>
        /// <param name="address_line_2"></param>
        /// <param name="city"></param>
        /// <param name="postcode"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewOrder(string order_reference, int account_id, int warehouse_id, DateTime dispatch_date, string first_name, string last_name,
                                        string address_line_1, string address_line_2, string city, string postcode)
        {
            string query = string.Format("INSERT INTO orders (order_reference, account_id, warehouse_id, dispatch_date, first_name, last_name, address_line_1, address_line_2, city, postcode) VALUES (@order_reference, @account_id, @warehouse_id, @dispatch_date, @first_name, @last_name, @address_line_1, @address_line_2, @city, @postcode)");
            SqlParameter[] sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@order_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(order_reference);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            sqlParameters[3] = new SqlParameter("@dispatch_date", SqlDbType.DateTime);
            sqlParameters[3].Value = Convert.ToString(dispatch_date);
            sqlParameters[4] = new SqlParameter("@first_name", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(first_name);
            sqlParameters[5] = new SqlParameter("@last_name", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(last_name);
            sqlParameters[6] = new SqlParameter("@address_line_1", SqlDbType.VarChar);
            sqlParameters[6].Value = Convert.ToString(address_line_1);
            sqlParameters[7] = new SqlParameter("@address_line_2", SqlDbType.VarChar);
            sqlParameters[7].Value = Convert.ToString(address_line_2);
            sqlParameters[8] = new SqlParameter("@city", SqlDbType.VarChar);
            sqlParameters[8].Value = Convert.ToString(city);
            sqlParameters[9] = new SqlParameter("@postcode", SqlDbType.VarChar);
            sqlParameters[9].Value = Convert.ToString(postcode);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // ALLOCATE

        /// <summary>
        ///     Selects Order ID from Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetOrderLineOrderID(int order_line_id)
        {
            string query = string.Format("SELECT order_id FROM order_lines WHERE order_line_id = @order_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_line_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects order_lines.product_id, products.title from Order Lines and Products where Parameters match, from the Database
        /// </summary>
        /// <param name="order_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetOrderLineProductIDAndTitle(int order_line_id)
        {
            string query = string.Format("SELECT order_lines.product_id, products.title FROM order_lines, products WHERE order_line_id = @order_line_id AND order_lines.product_id = products.product_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_line_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Distinct Product ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetStockProductID(int product_id)
        {
            string query = string.Format("SELECT DISTINCT product_id FROM stock WHERE product_id = @product_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(product_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Account ID from Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetOrderAccountID(int order_id)
        {
            string query = string.Format("SELECT account_id FROM orders WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Warehouse ID from Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetOrderWarehouseID(int order_id)
        {
            string query = string.Format("SELECT warehouse_id FROM orders WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Distinct Account ID, Product ID and Warehouse ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="product_id"></param>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetStockAccountProductAndWarehouseID(int account_id, int product_id, int warehouse_id)
        {
            string query = string.Format("SELECT DISTINCT account_id, product_id, warehouse_id FROM stock WHERE account_id = @account_id AND product_id = @product_id AND warehouse_id = @warehouse_id");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects stock.stock_id, stock.location_id, locations.location_code and locations.put_sequence From Stock, Order Lines, Locations where Parameters match,
        ///     from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="product_id"></param>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetAvailableStock(int product_id, int account_id, int warehouse_id)
        {
            string query = string.Format("SELECT DISTINCT TOP 1 stock.stock_id, stock.location_id, locations.location_code, locations.put_sequence FROM stock, order_lines, locations WHERE stock.product_id = @product_id AND stock.account_id = @account_id AND stock.warehouse_id = @warehouse_id AND stock.availability_status = 1 AND stock.quantity > 0 AND stock.location_id = locations.location_id ORDER BY put_sequence");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(product_id);
            sqlParameters[1] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(account_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Availability Status to 0 in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SetAvailabilityFalse(int stock_id)
        {
            string query = string.Format("UPDATE stock SET availability_status = 0 WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Quantity from Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetOrderLineQuantity(int order_line_id)
        {
            string query = string.Format("SELECT quantity FROM order_lines WHERE order_line_id = @order_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_line_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Sum of Given Quantity from Picks where Parameters match, from the Database
        /// </summary>
        /// <param name="order_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetPickQuantityByOrderLineID(int order_line_id)
        {
            string query = string.Format("SELECT SUM(quantity) AS quantity FROM picks WHERE order_line_id = @order_line_id"); 
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_line_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Inserts new Pick in Picks where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="order_line_id"></param>
        /// <param name="location_id"></param>
        /// <param name="location_code"></param>
        /// <param name="product_id"></param>
        /// <param name="product_title"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GeneratePick(int order_id, int order_line_id, int location_id, string location_code, int product_id, string product_title, int quantity)
        {
            string query = string.Format("INSERT into picks (order_id, order_line_id, location_id, location_code, product_id, product_title, quantity) VALUES (@order_id, @order_line_id, @location_id, @location_code, @product_id, @product_title, @quantity)");
            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            sqlParameters[1] = new SqlParameter("@order_line_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(order_line_id);
            sqlParameters[2] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(location_id);
            sqlParameters[3] = new SqlParameter("@location_code", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(location_code);
            sqlParameters[4] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[4].Value = Convert.ToString(product_id);
            sqlParameters[5] = new SqlParameter("@product_title", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(product_title);
            sqlParameters[6] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[6].Value = Convert.ToString(quantity);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Stock Quantity in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="allocated_quantity"></param>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable UpdateStockQuantity(int allocated_quantity, int stock_id)
        {
            string query = string.Format("UPDATE stock SET allocated_quantity = allocated_quantity + @allocated_quantity, quantity = quantity - @allocated_quantity WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@allocated_quantity", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(allocated_quantity);
            sqlParameters[1] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(stock_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Counts Order Lines in Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CountOrderLinesInOrderLinesByOrderID(int order_id)
        {
            string query = string.Format("SELECT count(order_line_id) as order_line_id FROM order_lines WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Counts Order Lines in Picks where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CountOrderLinesInPicksByOrderID(int order_id)
        {
            string query = string.Format("SELECT count(order_line_id) as order_line_id FROM picks WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Sum of Order Lines Quantity from Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_lines_order_id"></param>
        /// <param name="orders_order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetOrderLineQuantityByOLIDAndOID(int order_lines_order_id, int orders_order_id)
        {
            string query = string.Format("SELECT SUM(quantity) AS quantity FROM order_lines, orders WHERE order_lines.order_id = @order_lines_order_id AND orders.order_id = @orders_order_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@order_lines_order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_lines_order_id);
            sqlParameters[1] = new SqlParameter("@orders_order_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(orders_order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Sum of Pick Quantity from Picks where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetPickQuantityByOrderID(int order_id)
        {
            string query = string.Format("SELECT COALESCE(SUM(quantity),0) AS quantity FROM picks WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Status to Allocated in Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SetOrderStatusAllocated(int order_id)
        {
            string query = string.Format("UPDATE orders SET status = 'allocated' WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Quantity from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetStockQuantityByID(int stock_id)
        {
            string query = string.Format("SELECT quantity FROM stock WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Account ID, Product ID and Warehouse ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetStockAccountProductAndWarehouseIDByStockID(int stock_id)
        {
            string query = string.Format("SELECT account_id, product_id, warehouse_id FROM stock WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Stock Sum from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="product_id"></param>
        /// <param name="warehouse_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetStockSumByProductID(int account_id, int product_id, int warehouse_id)
        {
            string query = string.Format("SELECT SUM(quantity) AS quantity FROM stock WHERE account_id = @account_id AND product_id = @product_id AND warehouse_id = @warehouse_id");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@account_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(account_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(warehouse_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Allocated Quantity from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetStockAllocatedQuantity(int stock_id)
        {
            string query = string.Format("SELECT allocated_quantity FROM stock WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Availability Status to 1 in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SetAvailabilityTrue(int stock_id)
        {
            string query = string.Format("UPDATE stock SET availability_status = 1 WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Availability Status from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable GetStockAvailabilityStatus(int stock_id)
        {
            string query = string.Format("SELECT availability_status FROM stock WHERE stock_id = @stock_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Allocated to 0 in Locations where Parameters match, from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SetLocationAllocatedFalse(int location_id)
        {
            string query = string.Format("UPDATE locations SET allocated = 0 WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes current Order in Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentOrder(int order_id)
        {
            string query = string.Format("DELETE FROM orders WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // UPDATE

        /// <summary>
        ///     Updates current Order in Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="new_order_reference"></param>
        /// <param name="new_account_id"></param>
        /// <param name="new_warehouse_id"></param>
        /// <param name="new_status"></param>
        /// <param name="new_dispatch_date"></param>
        /// <param name="new_first_name"></param>
        /// <param name="new_last_name"></param>
        /// <param name="new_address_line_1"></param>
        /// <param name="new_address_line_2"></param>
        /// <param name="new_city"></param>
        /// <param name="new_postcode"></param>
        /// <param name="current_order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentOrder(string new_order_reference, int new_account_id, int new_warehouse_id, string new_status, DateTime new_dispatch_date,
                                          string new_first_name, string new_last_name, string new_address_line_1, string new_address_line_2, string new_city, string new_postcode, 
                                          int current_order_id)
        {

            string query = string.Format("UPDATE orders SET order_reference = @new_order_reference, account_id = @new_account_id, warehouse_id = @new_warehouse_id, status = @new_status, dispatch_date = @new_dispatch_date, first_name = @new_first_name, last_name = @new_last_name, address_line_1 = @new_address_line_1, address_line_2 = @new_address_line_2, city = @new_city, postcode = @new_postcode WHERE order_id = @current_order_id");

            SqlParameter[] sqlParameters = new SqlParameter[12];
            sqlParameters[0] = new SqlParameter("@new_order_reference", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(new_order_reference);
            sqlParameters[1] = new SqlParameter("@new_account_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(new_account_id);
            sqlParameters[2] = new SqlParameter("@new_warehouse_id", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(new_warehouse_id);
            sqlParameters[3] = new SqlParameter("@new_status", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(new_status);
            sqlParameters[4] = new SqlParameter("@new_dispatch_date", SqlDbType.DateTime);
            sqlParameters[4].Value = Convert.ToString(new_dispatch_date);
            sqlParameters[5] = new SqlParameter("@new_first_name", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(new_first_name);
            sqlParameters[6] = new SqlParameter("@new_last_name", SqlDbType.VarChar);
            sqlParameters[6].Value = Convert.ToString(new_last_name);
            sqlParameters[7] = new SqlParameter("@new_address_line_1", SqlDbType.VarChar);
            sqlParameters[7].Value = Convert.ToString(new_address_line_1);
            sqlParameters[8] = new SqlParameter("@new_address_line_2", SqlDbType.VarChar);
            sqlParameters[8].Value = Convert.ToString(new_address_line_2);
            sqlParameters[9] = new SqlParameter("@new_city", SqlDbType.VarChar);
            sqlParameters[9].Value = Convert.ToString(new_city);
            sqlParameters[10] = new SqlParameter("@new_postcode", SqlDbType.VarChar);
            sqlParameters[10].Value = Convert.ToString(new_postcode);

            sqlParameters[11] = new SqlParameter("@current_order_id", SqlDbType.Int);
            sqlParameters[11].Value = Convert.ToString(current_order_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }





        // MANAGER - Order Lines

        // CHECK

        /// <summary>
        ///     Selects all Order Lines from Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="product_id"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckOrderLinesAll(int order_id, int product_id, int quantity)
        {
            string query = string.Format("SELECT order_id, product_id, quantity FROM order_lines WHERE order_id = @order_id AND product_id = @product_id AND quantity = @quantity");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(quantity);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects all Order Line ID from Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckOrderLinesByID(int order_line_id)
        {
            string query = string.Format("SELECT order_line_id FROM order_lines WHERE order_line_id = @order_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_line_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // INSERT

        /// <summary>
        ///     Inserts new Order Line into Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="product_id"></param>
        /// <param name="quantity"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable InsertNewOrderLine(int order_id, int product_id, int quantity)
        {
            string query = string.Format("INSERT INTO order_lines (order_id, product_id, quantity) VALUES (@order_id, @product_id, @quantity)");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            sqlParameters[1] = new SqlParameter("@product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(product_id);
            sqlParameters[2] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(quantity);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Deletes current Order Line into Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="order_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentOrderLine(int order_line_id)
        {
            string query = string.Format("DELETE FROM order_lines WHERE order_line_id = @order_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_line_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_line_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }



        // UPDATE

        /// <summary>
        ///     Updates current Order Line from Order Lines where Parameters match, from the Database
        /// </summary>
        /// <param name="new_order_id"></param>
        /// <param name="new_product_id"></param>
        /// <param name="new_quantity"></param>
        /// <param name="current_order_line_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable EditCurrentOrderLine(int new_order_id, int new_product_id, int new_quantity, int current_order_line_id)
        {
            string query = string.Format("UPDATE order_lines SET order_id = @new_order_id, product_id = @new_product_id, quantity = @new_quantity WHERE order_line_id = @current_order_line_id");
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@new_order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(new_order_id);
            sqlParameters[1] = new SqlParameter("@new_product_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(new_product_id);
            sqlParameters[2] = new SqlParameter("@new_quantity", SqlDbType.Int);
            sqlParameters[2].Value = Convert.ToString(new_quantity);

            sqlParameters[3] = new SqlParameter("@current_order_line_id", SqlDbType.Int);
            sqlParameters[3].Value = Convert.ToString(current_order_line_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }





        // MANAGER - PICKS

        // CHECK

        /// <summary>
        ///     Selects Pick ID from Picks where Parameters match, from the Database
        /// </summary>
        /// <param name="pick_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable CheckPicksByPickID(int pick_id)
        {
            string query = string.Format("SELECT pick_id FROM picks WHERE pick_id = @pick_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@pick_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(pick_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }



        // DELETE

        /// <summary>
        ///     Selects order_line_id, location_id and quantity from Picks where Parameters match, from the Database
        /// </summary>
        /// <param name="pick_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectPicksOrderLineLocationIDAndQuantityByPickID(int pick_id)
        {
            string query = string.Format("SELECT order_line_id, location_id, quantity FROM picks WHERE pick_id = @pick_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@pick_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(pick_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Top 1 Stock ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectStockIDByPicksLocationID(int location_id)
        {
            string query = string.Format("SELECT TOP 1 stock_id FROM stock WHERE location_id = @location_id AND allocated_quantity > 0 ORDER BY date_added DESC");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Location ID from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="stock_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectStockLocationIDByID(int stock_id)
        {
            string query = string.Format("SELECT location_id FROM stock WHERE stock_id = @stock_id");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@stock_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(stock_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Quantity to Quantity + @Quantity in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable UndoStockQuantity(int quantity, int location_id)
        {
            string query = string.Format("UPDATE stock SET quantity = quantity + @quantity WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(quantity);
            sqlParameters[1] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(location_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Allocated Quantity to Allocated Quantity - @Allocated Quantity in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="allocated_quantity"></param>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable UndoStockAllocatedQuantity(int allocated_quantity, int location_id)
        {
            string query = string.Format("UPDATE stock SET allocated_quantity = allocated_quantity - @allocated_quantity WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@allocated_quantity", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(allocated_quantity);
            sqlParameters[1] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[1].Value = Convert.ToString(location_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Quantity from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectStockQuantityByLocationID(int location_id)
        {
            string query = string.Format("SELECT quantity FROM stock WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Availability Status to 1 in Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable UndoStockAvailabilityStatus(int location_id)
        {
            string query = string.Format("UPDATE stock SET availability_status = 1 WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Status to Created in Orders where Parameters match, from the Database
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SetOrderStatusCreated(int order_id)
        {
            string query = string.Format("UPDATE orders SET status = 'created' WHERE order_id = @order_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@order_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(order_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Selects Availability Status from Stock where Parameters match, from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SelectStockAvailabilityStatusByStockLocation(int location_id)
        {
            string query = string.Format("SELECT availability_status FROM stock WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.SelectQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Updates Allocated to 1 in Locations where Parameters match, from the Database
        /// </summary>
        /// <param name="location_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable SetLocationAllocatedTrue(int location_id)
        {
            string query = string.Format("UPDATE locations SET allocated = 1 WHERE location_id = @location_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@location_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(location_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }

        /// <summary>
        ///     Deletes current Pick from Picks where Parameters match, from the Database
        /// </summary>
        /// <param name="pick_id"></param>
        /// <returns>
        ///     A Data Table containing the information asked of by the query, which is processed in the DbConnection class.
        /// </returns>
        public DataTable DeleteCurrentPick(int pick_id)
        {
            string query = string.Format("DELETE FROM picks WHERE pick_id = @pick_id");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@pick_id", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(pick_id);
            return dbConnection.EditQuery(query, sqlParameters);
        }
    }
}