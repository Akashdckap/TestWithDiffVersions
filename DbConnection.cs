using Microsoft.Extensions.Configuration;
using System.Collections;
using Assess2.Models;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace Assess2
{
    public class DbConnection
    {
        private string ConnectionString;
        public DbConnection(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Directors> GetDirectors()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = "Select * From Directors";
                connection.Open();
                return connection.Query<Directors>(query);
            }
        }

        public void AddStudents(Directors director)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = "Insert into Directors" +
                    "(director_name,date_of_birth,no_of_movies,response)" +
                    "Values(@directorName,@dob,@NoOfMovies,@response)";
                connection.Open();
                connection.Execute(query, new
                {
                    directorName = director.director_name,
                    dob = director.date_of_birth,
                    NoOfMovies = director.no_of_movies,
                    response = director.response
                });
            }
        }

        public void UpdateDirector(int id, Directors director)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = "Update Directors Set director_name=@DirectorName," +
                    "date_of_birth=@DOB,no_of_movies=@NoOfMovies,response=@response " +
                    "Where director_id=@Id";
                connection.Open();
                connection.Execute(query, new
                {
                    Id = id,
                    DirectorName = director.director_name,
                    DOB = director.date_of_birth,
                    NoOfMovies = director.no_of_movies,
                    response = director.response
                });
            }
        }

        public void DeleteDirector(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = "Delete from directors where director_id = @Id";
                connection.Open();
                connection.Execute(query, new
                {
                    Id = id
                });
            }
        }
    }
}
