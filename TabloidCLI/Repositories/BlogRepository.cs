﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.Repositories
    //Needed to create reposity of methods to use Blog.cs
    //Tabloid is using an interface found in IRepository that uses IRepository<TEntity> (TEntity in his case being Blog)
    //Class should inherit the connector field and connection methods from DatabaseConnector
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
    //DatabaseConnector connects using a connection string, so first make the connection possible
    public BlogRepository(string connectionString) : base(connectionString) { }

    //interface dictates that all properties and class must be used that are found in IRepository

    //1st requirement is a GetAll method, to gather all blogs in database
    public List<Blog> GetAll()
    {
        //Step one, make your connection(tunnel) to the server
        using (SqlConnection conn = Connection)
        {
            //*Reminder using() automatically closes at the end, but you have to manually open it
            conn.Open();

            //Now that we have a connection, it's time to setup the instructions for the server
            using (SqlCommand cmd = conn.CreateCommand())
            {
                //remember that SQL receives text for directions in out to output information
                cmd.CommandText = "SELECT Title, URL FROM Blog";

                List<Blog> blogs = new List<Blog>();

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Blog blog = new Blog()
                    {
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Url = reader.GetString(reader.GetOrdinal("URL"))
                    };

                    blogs.Add(blog);
                }
                reader.Close();
                return blogs;
            }
        }
    }

}
}