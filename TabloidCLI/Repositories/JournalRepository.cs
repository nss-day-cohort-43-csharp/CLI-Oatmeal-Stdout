﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

    //Get all the journals to display a list
        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               CreateDateTime,
                                               Content
                                          FROM Journal";

                    List<Journal> journals = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Journal journal = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                        };
                        journals.Add(journal);
                    }

                    reader.Close();

                    return journals;
                }
              
                }
            }
    //Be able to insert a new journal
        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content )
                                                     VALUES (@Title, @Content)";
                    cmd.Parameters.AddWithValue("@Title", journal.Title);
                    cmd.Parameters.AddWithValue("@CreateDateTime", journal.CreateDateTime);
                    cmd.Parameters.AddWithValue("@Content", journal.Content);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    }