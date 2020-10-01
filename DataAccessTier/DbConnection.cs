using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccessTier
{
    /// <summary>
    ///     This class is used as the primary database connection class. It is used to connect to the database, as well as perform
    ///     the relevant queries sent from the DataAccessLayer and then return the results as a Data Table. 
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
    public class DbConnection
    {
        /// <summary>
        ///     Declaring the class variables.
        /// </summary>
        private SqlDataAdapter sqlDataAdapter;
        private SqlConnection sqlConnection;

        /// <summary>
        ///     The constructor called DbConnection.
        ///     This creates a new SQL Data Adapter and SQL Connection and then uses the information from app.config to attempt a connection
        ///     to the database.
        /// </summary>
        public DbConnection()
        {
            sqlDataAdapter = new SqlDataAdapter();
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WmsDbConnectionString"].ConnectionString);
        }

        /// <summary>
        ///     Open Database Connection if Closed or Broken.
        /// </summary>
        /// <returns>
        ///     sqlConnection
        /// </returns>
        private SqlConnection OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken)
            {
                sqlConnection.Open();
            }
            return sqlConnection;
        }

        // Select Query
        /// <summary>
        ///     This method is the method used by all methods within the DataAccessLayer class that use a SELECT query with 
        ///     parameters.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlParameter"></param>
        /// <returns>
        ///     A data table containing the information from the provided query.
        /// </returns>
        public DataTable SelectQuery(String query, SqlParameter[] sqlParameter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet dataSet = new DataSet();
            try
            {
                sqlCommand.Connection = OpenConnection();
                sqlCommand.CommandText = query;
                sqlCommand.Parameters.AddRange(sqlParameter);
                sqlCommand.ExecuteNonQuery();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (SqlException e)
            {
                Console.Write("Error - DbConnection.SelectQuery - Query: " + query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            return dataTable;
        }

        /// <summary>
        ///     This method is the method used by all methods within the DataAccessLayer class that use a INSERT, DELETE, UPDATE or
        ///     any other type of SQL query that updates a database with parameters.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlParameter"></param>
        /// <returns>
        ///     A data table containing the information from the provided query.
        /// </returns>
        public DataTable EditQuery(String query, SqlParameter[] sqlParameter)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet dataSet = new DataSet();
            try
            {
                sqlCommand.Connection = OpenConnection();
                sqlCommand.CommandText = query;
                sqlCommand.Parameters.AddRange(sqlParameter);
                sqlDataAdapter.InsertCommand = sqlCommand;
                sqlCommand.ExecuteNonQuery();
                //sqlDataAdapter.SelectCommand = sqlCommand; // NEED TO TEST!!!
                //sqlDataAdapter.Fill(dataSet); // NEED TO TEST!!!
            }
            catch (SqlException e)
            {
                Console.Write("Error - DbConnection.EditQuery - Query: " + query + " \nException: \n" + e.StackTrace.ToString());
            }
            return dataTable;
        }

        /// <summary>
        ///     This method is the method used by all methods within the DataAccessLayer class that use a INSERT, DELETE, UPDATE or
        ///     any other type of SQL query that updates a database without parameters - Primarily used to display data from the 
        ///     database in data grids within the application.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        ///     A data table containing the information from the provided query.
        /// </returns>
        public DataTable NoParameterSelectQuery(String query)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet dataSet = new DataSet();
            try
            {
                sqlCommand.Connection = OpenConnection();
                sqlCommand.CommandText = query;
                sqlCommand.ExecuteNonQuery();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (SqlException e)
            {
                Console.Write("Error - DbConnection.NoParameterSelectQuery - Query: " + query + " \nException: \n" + e.StackTrace.ToString());
            }
            return dataTable;
        }
    }
}