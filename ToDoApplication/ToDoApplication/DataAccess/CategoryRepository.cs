using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoApplication.Models;
using System.Data;
using System.Data.SqlClient;

namespace ToDoApplication.DataAccess
{
    public class CategoryRepository
    {
        private readonly string connectionString;
        public CategoryRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Category> GetAll()
        {
            List<Category> result = new List<Category>();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Categories";

                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.Id = (int)reader["Id"];
                        category.Name = reader["Name"].ToString();

                        result.Add(category);
                    }
                }

            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public void Insert(Category category)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Categories (Name)
VALUES (@Name)";

                SqlParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@Name";
                parameter.Value = category.Name;

                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Category category)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
@"
UPDATE Categories
SET Name = @Name
WHERE Id = @Id
";
            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = category.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = category.Id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
            
        }

        public Category Get(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
@"SELECT * FROM Categories WHERE Id = @Id";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = (int)reader["Id"];
                    category.Name = (string)reader["Name"];

                    return category;
                } 
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
@"DELETE FROM Categories WHERE Id = @Id";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}