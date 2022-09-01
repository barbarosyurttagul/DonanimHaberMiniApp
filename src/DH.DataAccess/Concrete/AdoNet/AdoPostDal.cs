using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.DataAccess.Abstract;
using DH.Entities.Concrete;

namespace DH.DataAccess.Concrete.AdoNet
{
    public class AdoPostDal : IPostDal
    {
        AppConfiguration configuration = new AppConfiguration();

        public List<Post> GetAll()
        {
            string connStr = configuration.SqlConnectionString;
            using (var connection = new SqlConnection(connStr))
            {
                using (var command = new SqlCommand("SELECT * from Posts  WHERE RootId = 0 ORDER BY DatePublished DESC", connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    var posts = new List<Post>();

                    while (reader.Read())
                    {
                        posts.Add(new Post
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RootId = Convert.ToInt32(reader["RootId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),                           
                            PostTitle = reader["PostTitle"].ToString(),
                            Content = reader["Content"].ToString(),
                            DatePublished = Convert.ToDateTime(reader["DatePublished"])
                        });
                    }
                    reader.Close();
                    return posts;
                }
            }
        }
        public Post Insert(Post entity)
        {
            string connStr = configuration.SqlConnectionString;
            
            using (var connection = new SqlConnection(connStr))
            {
                string commandText = "INSERT INTO Posts(RootId, FirstName, LastName, Email, PostTitle, Content, DatePublished) VALUES (@RootId, @FirstName, @LastName, @Email, @PostTitle, @Content, @DatePublished)";

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@RootId", 0);
                    command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                    command.Parameters.AddWithValue("@LastName", entity.LastName);
                    command.Parameters.AddWithValue("@Email", entity.Email);
                    command.Parameters.AddWithValue("@PostTitle", entity.PostTitle);
                    command.Parameters.AddWithValue("@Content", entity.Content);
                    command.Parameters.AddWithValue("@DatePublished", DateTime.Now);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return entity;
                }
            }
        }    
        public Post InsertReply(Post post)
        {
            string connStr = configuration.SqlConnectionString;

            using (var connection = new SqlConnection(connStr))
            {
                string commandText = "INSERT INTO Posts(RootId, FirstName, LastName, Email, PostTitle, Content, DatePublished) VALUES (@RootId, @FirstName, @LastName, @Email, @PostTitle, @Content, @DatePublished)";

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@RootId", post.RootId);
                    command.Parameters.AddWithValue("@FirstName", post.FirstName);
                    command.Parameters.AddWithValue("@LastName", post.LastName);
                    command.Parameters.AddWithValue("@Email", post.Email);
                    command.Parameters.AddWithValue("@PostTitle", "Re:" + post.PostTitle);
                    command.Parameters.AddWithValue("@Content", post.Content);
                    command.Parameters.AddWithValue("@DatePublished", DateTime.Now);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return post;
                }
            }
        }
        public Post GetById(int id)
        {
            Post post = GetAll().FirstOrDefault(t => t.Id == id);

            return post;
        }

        public void Reply(Post entity)
        {
            string connStr = configuration.SqlConnectionString;

            using (var connection = new SqlConnection(connStr))
            {
                string commandText = "SELECT Id FROM Posts WHERE Id = @Id";

                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();                    
                }
            }
        }
        public List<Post> GetReplies(int id)
        {
            string connStr = configuration.SqlConnectionString;
            using (var connection = new SqlConnection(connStr))
            {
                using (var command = new SqlCommand("SELECT * from Posts WHERE RootId = @id ORDER BY DatePublished ASC", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    var posts = new List<Post>();

                    while (reader.Read())
                    {
                        posts.Add(new Post
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RootId = Convert.ToInt32(reader["RootId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            PostTitle = reader["PostTitle"].ToString(),
                            Content = reader["Content"].ToString(),
                            DatePublished = Convert.ToDateTime(reader["DatePublished"])
                        });
                    }
                    reader.Close();
                    return posts;
                }
            }
        }

    }
}