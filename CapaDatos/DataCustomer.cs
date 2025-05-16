using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CapaDatos
{
    public class DataCustomer
    {
        private readonly string cadena = "Data Source=LAB1502-15; Initial Catalog=Neptuno; Integrated Security=True; TrustServerCertificate=True;";


        public List<Customer> GetAll()
        {
            List<Customer> list = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("USP_listar_customer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Customer
                    {
                        CustomerId = (int)reader["customer_id"],
                        Name = reader["name"].ToString(),
                        Address = reader["address"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Active = (bool)reader["active"]
                    });
                }
            }

            return list;
        }

        public void InsertCustomer(Customer customer)
        {
           
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("USP_insert_customer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.Parameters.AddWithValue("@phone", customer.Phone);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        

        }
        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("USP_delete_customer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customer_id", customerId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("USP_update_customer", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.Parameters.AddWithValue("@phone", customer.Phone);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
