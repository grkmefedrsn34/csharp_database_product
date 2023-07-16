using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ado_Net_DEMO
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;initial catalog=E_TRADE;integrated security=true");
        public List<Product> GetAll()
        {
           
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            
            SqlCommand command = new SqlCommand("Select * from Products", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    ID = Convert.ToInt32(reader["ID"]) ,
                    Name =  reader["Name"].ToString(),
                    Stock_Amount = Convert.ToInt32(reader["Stock_Amount"]),
                    Unite_Price = Convert.ToInt32(reader["Unite_Price"])
                };

                products.Add(product);

            }

            reader.Close();
            _connection.Close();
            return products;   
        }

        public void Add(Product product)
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlCommand command = new SqlCommand("Insert into Products values(@Name,@Unit_Price,@Stock_Amount)",_connection);
            command.Parameters.AddWithValue("@Name",product.Name);
            command.Parameters.AddWithValue("@Unite_Price", product.Unite_Price);
            command.Parameters.AddWithValue("@Stock_Amount", product.Stock_Amount);
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }

    /*
    public DataTable GetAll2()
    {
        SqlConnection connection = new SqlConnection(@"Server=(localdb)\mssqllocaldb;initial catalog=E_TRADE;integrated security=true");
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        SqlCommand command = new SqlCommand("Select * from Products", connection);
        SqlDataReader reader = command.ExecuteReader();
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            connection.Close();
            return dataTable;
        }
    }
    */
}
