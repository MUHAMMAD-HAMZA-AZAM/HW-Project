using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HW.Utility
{
    public interface IUnitOfWork
    {
        T ExecuteNonQuery<T>(string commandText, SqlParameter[] param) where T : new();
        Task<T> ExecuteReaderMultipleDS<T>(string commandText, SqlParameter[] param = null, CommandType commandType = CommandType.StoredProcedure) where T : new();
        List<T> ExecuteReaderSingleDS<T>(string commandText, SqlParameter[] param) where T : new();
        Task<List<T>> ExecuteReaderSingleDSNew<T>(string commandText, SqlParameter[] param) where T : new();
        List<T> ExecuteCommand<T>(string commandText) where T : new();
        Task<T> ExecuteScalar<T>(string commandText, SqlParameter[] param = null, string CmdType = "SP") where T : new();
        void Save();
        Task SaveAsync();
        IRepository<T> Repository<T>() where T : class;
        //void Dispose();
        //void Dispose(bool disposing);        

    }
    public class UnitOfWork<U> : IUnitOfWork where U : class
    {
        #region Private
        private DbContext DbContext { get; set; }
        public IConfiguration Configuration { get; }

        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        #endregion

        public UnitOfWork(IConfiguration configuration, U context)
        {
            Configuration = configuration;
            DbContext = context as DbContext;
        }
        public async Task<T> ExecuteScalar<T>(string commandText, SqlParameter[] param = null, string CmdType = "SP") where T : new()
        {
            SqlConnection connection = null;
            T Value;
            try
            {
                CheckForSQLInjection(param);
                connection = (SqlConnection)DbContext.Database.GetDbConnection();
                if (connection != null && connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = commandText;
                if (CmdType == "SP")
                    command.CommandType = CommandType.StoredProcedure;
                else
                    command.CommandType = CommandType.Text;

                if (param != null)
                    command.Parameters.AddRange(param);
                Value = (T)command.ExecuteScalar();
                connection.Close();
            }
            catch (Exception ex)
            {
                Value = default(T);
                throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return Value;
        }
        public List<T> ExecuteCommand<T>(string commandText) where T : new()
        {
            T newObj = new T();
            List<T> res = null;
            var connectionString = DbContext.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            // var connection = (SqlConnection)DbContext.Database.GetDbConnection();
            if (connection != null && connection.State == ConnectionState.Closed)
                connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            //command.Parameters.AddRange(param);
            using (var reader = command.ExecuteReader())
            {

                Type elementType = newObj.GetType();
                MethodInfo method = typeof(Common).GetMethod("MapToList");
                MethodInfo generic = method.MakeGenericMethod(elementType);
                object obj = generic.Invoke(null, new object[] { reader });
                if (obj != null)
                {
                    IList objectList = obj as IList;
                    if (objectList.Count > 0)
                    {
                        res = new List<T>();
                        res = objectList.Cast<T>().ToList();
                    }
                    else
                        res = null;
                }
                else
                    res = null;

            }
            connection.Close();


            return res;
        }
        private static void CheckForSQLInjection(SqlParameter[] rcvdparams = null)

        {


            var paramsToCheck = new List<string>() { "@SortCol", "@SortDir", "@StartRow", "@PageSize" };
            if (rcvdparams == null)
                return;

            string[] sqlCheckList = { "--",";--", ";","/*","*/","@@ ","@ ","char","nchar","varchar","nvarchar","alter","begin",
"cast", "create ","cursor ","declare ","delete ","drop ","end ","exec ","execute ","fetch ","insert ","kill ","select ",
"sys ","sysobjects ","syscolumns ","table ","update ","WAITFOR ","Union ","WAITFOR Delay ","Substring("
                                       };

            for (int i = 0; i <= rcvdparams.Length - 1; i++)
            {

                SqlParameter paramter = rcvdparams[i];


                if (!paramsToCheck.Any(p => p.ToString().ToLower() == paramter.ParameterName.ToString().ToLower()))
                    continue;

                var userInput = paramter.Value.ToString();
                var ab = paramter.ParameterName;
                string CheckString = userInput.Replace("'", "''");

                for (int j = 0; j <= sqlCheckList.Length - 1; j++)

                {

                    if ((CheckString.IndexOf(sqlCheckList[j], StringComparison.OrdinalIgnoreCase) >= 0))

                    {
                        throw new Exception("sql injection detected:(paramter:" + paramter.ParameterName + " (value:" + userInput + ")");
                    }
                }
            }

        }
        #region Stored Procedure Execution for multiple result set              
        public T ExecuteNonQuery<T>(string commandText, SqlParameter[] param) where T : new()
        {
            T newObj = new T();
            var connectionString = DbContext.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            //  var connection = (SqlConnection)DbContext.Database.GetDbConnection();
            if (connection != null && connection.State == ConnectionState.Closed)
                connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(param);
            command.ExecuteNonQuery();

            SqlParameter[] output = param.Where(x => x.Direction == ParameterDirection.Output).ToArray();

            var entity = typeof(T);
            var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var propDict = new Dictionary<string, PropertyInfo>();
            propDict = props.ToDictionary(x => x.Name.ToUpper(), x => x);

            foreach (SqlParameter item in output)
            {
                if (propDict.ContainsKey(item.ParameterName.ToString().ToUpper().Replace("@", "")))
                {
                    var info = propDict[item.ParameterName.ToString().ToUpper().Replace("@", "")];
                    if ((info != null) && info.CanWrite)
                    {
                        var val = command.Parameters[item.ParameterName].Value;
                        info.SetValue(newObj, (val == DBNull.Value ? null : val), null);
                    }
                }
            }

            connection.Close();

            return newObj;
        }

        /// <summary>
        /// Same Sequene Result Set/Properties with DB  Sequence Result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public T ExecuteReaderMultipleDS<T>(string commandText, SqlParameter[] param) where T : new()
        {
            T ResultData = new T();

            var connection = (SqlConnection)DbContext.Database.GetDbConnection();
            if (connection != null && connection.State == ConnectionState.Closed)
                connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(param);

            using (var reader = command.ExecuteReader())
            {
                foreach (PropertyInfo propInfo in ResultData.GetType().GetProperties())
                {
                    Type elementType = propInfo.PropertyType.GetGenericArguments()[0];
                    MethodInfo method = typeof(Common).GetMethod("MapToList");
                    MethodInfo generic = method.MakeGenericMethod(elementType);
                    //Value Set in Current Property 
                    propInfo.SetValue(ResultData, generic.Invoke(null, new object[] { reader }));
                    reader.NextResult();
                }

            }
            connection.Close();

            return ResultData;
        }

        public async Task<T> ExecuteReaderMultipleDS<T>(string commandText, SqlParameter[] param = null, CommandType commandType = CommandType.StoredProcedure) where T : new()
        {
            T ResultData = new T();
            // var connection = (SqlConnection)DbContext.Database.GetDbConnection();
            var connectionString = DbContext.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                CheckForSQLInjection(param);
                if (connection != null && connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;
                if (param != null)
                    command.Parameters.AddRange(param);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    foreach (PropertyInfo propInfo in ResultData.GetType().GetProperties())
                    {
                        Type elementType = propInfo.PropertyType.GetGenericArguments()[0];
                        MethodInfo method = typeof(Common).GetMethod("MapToList");
                        MethodInfo generic = method.MakeGenericMethod(elementType);
                        //Value Set in Current Property
                        propInfo.SetValue(ResultData, generic.Invoke(null, new object[] { reader }));
                        await reader.NextResultAsync();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ResultData = default(T);
                throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return ResultData;
        }

        public List<T> ExecuteReaderSingleDS<T>(string commandText, SqlParameter[] param) where T : new()
        {
            T newObj = new T();
            List<T> res = null;
          //  string connectionString = Configuration.GetConnectionString("DefaultConnection");
            var connectionString = DbContext.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            //var connection = (SqlConnection)DbContext.Database.GetDbConnection();
            if (connection != null && connection.State == ConnectionState.Closed)
                connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(param);
            using (var reader = command.ExecuteReader())
            {

                Type elementType = newObj.GetType();
                MethodInfo method = typeof(Common).GetMethod("MapToList");
                MethodInfo generic = method.MakeGenericMethod(elementType);
                object obj = generic.Invoke(null, new object[] { reader });
                if (obj != null)
                {
                    IList objectList = obj as IList;
                    if (objectList.Count > 0)
                    {
                        res = new List<T>();
                        res = objectList.Cast<T>().ToList();
                    }
                    else
                        res = null;
                }
                else
                    res = null;

            }
            connection.Close();


            return res;
        }


        public async Task<List<T>> ExecuteReaderSingleDSNew<T>(string commandText, SqlParameter[] param) where T : new()
        {
            SqlConnection connection = null;
            T newObj = new T();
            List<T> res = new List<T>();

            try
            {
                connection = (SqlConnection)DbContext.Database.GetDbConnection();
                if (connection != null && connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();
                var command = connection.CreateCommand();

                command.CommandText = commandText;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(param);
                using (var reader = await command.ExecuteReaderAsync())
                {

                    Type elementType = newObj.GetType();
                    MethodInfo method = typeof(Common).GetMethod("MapToList");
                    MethodInfo generic = method.MakeGenericMethod(elementType);
                    object obj = generic.Invoke(null, new object[] { reader });
                    if (obj != null)
                    {
                        IList objectList = obj as IList;
                        if (objectList.Count > 0)
                        {
                            res = new List<T>();
                            res = objectList.Cast<T>().ToList();
                        }
                        else
                            res = null;
                    }
                    else
                        res = null;

                }
                connection.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return res;
        }
        #endregion

       

        #region Public FUN/Properties
        public void Save()
        {
            DbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new Repository<T>(DbContext);
            repositories.Add(typeof(T), repo);
            return repo;
        }
        #endregion

        #region IDisposable

        //public void Dispose()
        //{
        //    //Dispose(true);
        //    //GC.SuppressFinalize(this);
        //}

        //public void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (DbContext != null)
        //        {
        //            DbContext.Dispose();
        //        }
        //    }
        //}

        #endregion             
    }


}
