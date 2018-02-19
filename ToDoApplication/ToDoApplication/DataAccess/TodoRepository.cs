using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoApplication.Models;

namespace ToDoApplication.DataAccess
{
    public class TodoRepository
    {
        private readonly string connectionString;
        public TodoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Todo> GetAll()
        {
            List<Todo> result = new List<Todo>();

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Todos";

                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Todo todo = new Todo();
                        todo.Id = (int)reader["Id"];
                        todo.CategoryId = (int)reader["CategoryId"];
                        todo.Title = reader["Title"].ToString();
                        todo.IsDone = (bool)reader["IsDone"];

                        result.Add(todo);
                    }
                }

            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public void Insert(Todo todo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
@"INSERT INTO Todos (CategoryId, Title, IsDone)
VALUES (@CategoryId, @Title, @IsDone)";

                SqlParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@CategoryId";
                parameter.Value = todo.CategoryId;

                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@Title";
                parameter.Value = todo.Title;

                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@IsDone";
                parameter.Value = todo.IsDone;

                command.Parameters.Add(parameter);
                
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Todo todo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
@"
UPDATE Todos
SET 
    CategoryId = @CategoryId,
    Title = @Title,
    IsDone = @IsDone
WHERE Id = @Id
";
            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Title";
            parameter.Value = todo.Title;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@IsDone";
            parameter.Value = todo.IsDone;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CategoryId";
            parameter.Value = todo.CategoryId;
            command.Parameters.Add(parameter);
        
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = todo.Id;
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

        public Todo Get(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
@"SELECT * FROM Todos WHERE Id = @Id";

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
                    Todo todo = new Todo();
                    todo.Id = (int)reader["Id"];
                    todo.CategoryId = (int)reader["CategoryId"];
                    todo.Title = reader["Title"].ToString();
                    todo.IsDone = (bool)reader["IsDone"];

                    return todo;
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
@"DELETE FROM Todos WHERE Id = @Id";

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

        public List<Todo> GetByCategoryId(int categoryId)
        {
            List<Todo> result = new List<Todo>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText =
@"SELECT * FROM Todos 
WHERE CategoryId = @CategoryId";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@CategoryId";
            parameter.Value = categoryId;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Todo todo = new Todo();
                    todo.Id = (int)reader["Id"];
                    todo.CategoryId = (int)reader["CategoryId"];
                    todo.Title = reader["Title"].ToString();
                    todo.IsDone = (bool)reader["IsDone"];

                    result.Add(todo);
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        
    }
}