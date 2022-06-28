using CommanLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{

    public class ToDoListRL : IToDoListRL
    {

        private SqlConnection sqlConnection;

        public ToDoListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public string AddItem(ToDoListModel add)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Assignment"]);
                SqlCommand cmd = new SqlCommand("AddItem", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Date", add.Date);
                cmd.Parameters.AddWithValue("@Title", add.Title);
                cmd.Parameters.AddWithValue("@Description", add.Description);
                cmd.Parameters.AddWithValue("@PriorityId", add.PriorityId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 2)
                {
                    return "Enter Valid PriorityId For Adding ToDo List";
                }
                else
                {
                    return "Task Added Successfully";
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public List<ToDoListModel> GetList()
        {
            try
            {
                List<ToDoListModel> book = new List<ToDoListModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Assignment"]);
                SqlCommand cmd = new SqlCommand("GetList", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new ToDoListModel
                        {
                            Date = Convert.ToDateTime(reader["Date"]),
                            // Date = reader["Date"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            PriorityId = Convert.ToInt32(reader["PriorityId"])
                        });
                    }

                    this.sqlConnection.Close();
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public UpdateModel UpdateTask(UpdateModel update)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Assignment"]);
                SqlCommand com = new SqlCommand("UpdateTask", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Date", update.Date);
                com.Parameters.AddWithValue("@Title", update.Title);
                com.Parameters.AddWithValue("@Description", update.Description);
                com.Parameters.AddWithValue("@PriorityId", update.PriorityId);

                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return update;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public bool DeleteTask(int ToDoListId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Assignment"]);
                SqlCommand cmd = new SqlCommand("DeleteTask", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ToDoListId", ToDoListId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
        
