using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace WebTracker.ModelAgent
{
    internal class JobAgent
    {
        private IConfiguration Configuration;

        public JobAgent(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        internal List<Model.Job> GetActiveJob()
        {
            var connectString = this.Configuration.GetConnectionString("DefaultConnection");
            List<Model.Job> result = null;
            string cmdText = "sp_get_all_job";
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = mySqlCommand.ExecuteReader(System.Data.CommandBehavior.Default);
                    if (reader != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var job = new Model.Job();
                            job.Id = DbUtils.GetIntValue(reader, "id");

                            result.Add(job);
                        }
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    NewRelicHelper.HandleNewRelicOpsCustomEvent("CRITICAL", "GetLocationsForClearCredentialGrid failed ", "", "Failure in executing Stored Procedure " + mySqlCommand.CommandText);
                    Log.Error(ex, "Exception {@ExceptionMessage} while executing Sql Query {@SqlQuery} while trying to get Clear credential locations for {@BusinessId}.",
                                  ex.Message, mySqlCommand.CommandText, businessId);
                    Log.Debug(ex, "Exception {@ExceptionMessage} while executing SQL Query {@SqlQuery} while trying to get Clear credential locations for {@BusinessId}, with {@Stacktrace}",
                            ex.Message, mySqlCommand.CommandText, businessId, ex.StackTrace);
                }
            }
            return result;
        }
    }
}
