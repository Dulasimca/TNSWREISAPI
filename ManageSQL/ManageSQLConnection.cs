using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TNSWREISAPI.ManageSQL
{
    public class ManageSQLConnection
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();

        SqlDataAdapter dataAdapter;
        /// <summary>
        /// Gets values from 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public DataSet GetDataSetValues(string procedureName)
        {
            sqlConnection = new SqlConnection(GlobalVariable.ConnectionString);
            DataSet ds = new DataSet();
            sqlCommand = new SqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }

        }

        public DataSet GetDataSetValues(string procedureName, List<KeyValuePair<string, string>> parameterList)
        {
            sqlConnection = new SqlConnection(GlobalVariable.ConnectionString);
            DataSet ds = new DataSet();
            sqlCommand = new SqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
                sqlCommand.CommandTimeout = 360;
                dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool UpdateValues(string procedureName, List<KeyValuePair<string, string>> parameterList)
        {
            sqlConnection = new SqlConnection(GlobalVariable.ConnectionString);
            DataSet ds = new DataSet();
            sqlCommand = new SqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
                sqlCommand.ExecuteNonQuery();
                //  AuditLog.WriteError(affected.ToString());
                return true;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message + " : " + ex.StackTrace);
                return false;

            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public bool InsertData(string procedureName, List<KeyValuePair<string, string>> parameterList)
        {

            sqlConnection = new SqlConnection(GlobalVariable.ConnectionString);
            DataSet ds = new DataSet();
            sqlCommand = new SqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
                sqlCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        /// <summary>
        /// Insert the Data 
        /// </summary>
        /// <param name="procedureName">Procedure Name</param>
        /// <param name="parameterList">Need to add the parameter list</param>
        /// <param name="sFieldName">Output Parameter field name. If should to an Interger field.</param>
        /// <returns></returns>
        public Tuple<bool,int,string> InsertData(string procedureName, List<KeyValuePair<string, string>> parameterList,string sFieldName)
        {

            sqlConnection = new SqlConnection(GlobalVariable.ConnectionString);
            DataSet ds = new DataSet();
            sqlCommand = new SqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
             
                sqlCommand.Parameters.Add(sFieldName, SqlDbType.Int);
                sqlCommand.Parameters[sFieldName].Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();

                int @slno = (int)sqlCommand.Parameters[sFieldName].Value;

                return new Tuple<bool,int, string>(true, @slno, "Save Successfully");
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return null;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

        public DataSet GetDataSetValuesBM(string procedureName, List<KeyValuePair<string, string>> parameterList)
        {
            sqlConnection = new SqlConnection(GlobalVariable.ConnectionStringBiometric);
            DataSet ds = new DataSet();
            sqlCommand = new SqlCommand();
            try
            {
                if (sqlConnection.State == 0)
                {
                    sqlConnection.Open();
                }
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> keyValuePair in parameterList)
                {
                    sqlCommand.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
                sqlCommand.CommandTimeout = 360;
                dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
                ds.Dispose();
                dataAdapter = null;
            }
        }

    }
}
