namespace MiniORM
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Attributes;

    public class EntityManager : IDbContext
    {
        private SqlConnection connection;
        private string connectionString;
        private bool isCodeFirst;

        public EntityManager(string connectionString, bool isCodeFirst)
        {
            this.connectionString = connectionString;
            this.isCodeFirst = isCodeFirst;
        }

        public bool Persist(object entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (isCodeFirst && !this.CheckIfTableExists(entity.GetType()))
            {
                this.CreateTable(entity.GetType());
            }

            Type entityType = entity.GetType();
            FieldInfo idInfo = this.GetId(entityType);
            int id = (int)idInfo.GetValue(entity);

            if (id <= 0)
            {
                return this.Insert(entity, idInfo);
            }

            return this.Update(entity, idInfo);
        }

        public T FindById<T>(int id)
        {
            T wantedObject = default(T);
            string findByIdSql = $"SELECT * FROM {this.GetTableName(typeof(T))} WHERE [Id] = @id";

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(findByIdSql, this.connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new InvalidOperationException($"No entity was found with ID: {id}");
                }

                wantedObject = CreateEntity<T>(reader);
            }

            return wantedObject;
        }

        public IEnumerable<T> FindAll<T>()
        {
            IEnumerable<T> wantedObjects = this.FindAll<T>(null);

            return wantedObjects;
        }

        public IEnumerable<T> FindAll<T>(string @where)
        {
            IEnumerable<T> wantedObjects = new List<T>();

            if (@where != null)
            {
                string findAllWithWhereSql = $"SELECT * FROM {this.GetTableName(typeof(T))} " + where;
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    this.connection.Open();
                    SqlCommand findFirstCommand = new SqlCommand(findAllWithWhereSql, this.connection);
                    SqlDataReader reader = findFirstCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException("No entities were found with given where clause!");
                    }

                    wantedObjects = CreateEntities<T>(reader);
                }
            }
            else
            {
                string findAllWithoutWhereSql = $"SELECT * FROM {this.GetTableName(typeof(T))}";
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    this.connection.Open();
                    SqlCommand findFirstCommand = new SqlCommand(findAllWithoutWhereSql, this.connection);
                    SqlDataReader reader = findFirstCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException($"No entities were found of type {typeof(T)}!");
                    }

                    wantedObjects = CreateEntities<T>(reader);
                }
            }
      
            return wantedObjects;
        }

        public T FindFirst<T>()
        {
            T wantedObject = this.FindFirst<T>(null);

            return wantedObject;
        }

        public T FindFirst<T>(string @where)
        {
            T wantedObject = default(T);
            if (@where != null)
            {
                string findFirstWithWhereSql = $"SELECT TOP 1 * FROM {this.GetTableName(typeof(T))} " + where;
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    this.connection.Open();
                    SqlCommand findFirstCommand = new SqlCommand(findFirstWithWhereSql, this.connection);
                    SqlDataReader reader = findFirstCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException("No entity was found with given where clause!");
                    }

                    wantedObject = CreateEntity<T>(reader);
                }
            }
            else
            {
                string findFirstWithoutWhereSql = $"SELECT TOP 1 * FROM {this.GetTableName(typeof(T))}";
                using (this.connection = new SqlConnection(this.connectionString))
                {
                    this.connection.Open();
                    SqlCommand findFirstCommand = new SqlCommand(findFirstWithoutWhereSql, this.connection);
                    SqlDataReader reader = findFirstCommand.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        throw new InvalidOperationException($"No entity was found of type {typeof(T)}");
                    }

                    wantedObject = CreateEntity<T>(reader);
                }
            }

            return wantedObject;
        }

        public void Delete<T>(object entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Cannot delete undefined entity!");
            }

            Type entityType = entity.GetType();
            FieldInfo idInfo = this.GetId(entityType);

            int id = (int)idInfo.GetValue(entity);

            if (id <= 0)
            {
                throw new InvalidOperationException("The entity is unexistent in the database!");
            }

            this.DeleteById<T>(id);
        }

        public void DeleteById<T>(int id)
        {
            string deleteSql = $"DELETE FROM {this.GetTableName(typeof(T))} WHERE [Id] = @id";

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand deleteCommand = new SqlCommand(deleteSql, this.connection);
                deleteCommand.Parameters.AddWithValue("@id", id);
                deleteCommand.ExecuteNonQuery();
            }
        }

        private T CreateEntity<T>(SqlDataReader reader)
        {
            reader.Read();
            object[] columns = new object[reader.FieldCount];
            reader.GetValues(columns);

            Type[] columnTypes = new Type[columns.Length - 1];
            object[] columnValues = new object[columns.Length - 1];

            for (int i = 1; i < columns.Length; i++)
            {
                columnTypes[i - 1] = columns[i].GetType();
                columnValues[i - 1] = columns[i];
            }

            T createdObject = (T) typeof(T)
                .GetConstructor(columnTypes)
                .Invoke(columnValues);
            FieldInfo idInfo = createdObject.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.IsDefined(typeof(IdAttribute)));
            idInfo.SetValue(createdObject, columns[0]);

            return createdObject;
        }

        private IEnumerable<T> CreateEntities<T>(SqlDataReader reader)
        {
            List<T> wantedObjects = new List<T>();
            while (reader.Read())
            {
                object[] columns = new object[reader.FieldCount];
                reader.GetValues(columns);

                Type[] columnTypes = new Type[columns.Length - 1];
                object[] columnValues = new object[columns.Length - 1];

                for (int i = 1; i < columns.Length; i++)
                {
                    columnTypes[i - 1] = columns[i].GetType();
                    columnValues[i - 1] = columns[i];
                }

                T createdObject = (T)typeof(T)
                    .GetConstructor(columnTypes)
                    .Invoke(columnValues);
                FieldInfo idInfo = createdObject.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .FirstOrDefault(f => f.IsDefined(typeof(IdAttribute)));
                idInfo.SetValue(createdObject, columns[0]);
                wantedObjects.Add(createdObject);
            }

            IEnumerable<T> wantedObjectsAsEnumerable = wantedObjects;

            return wantedObjectsAsEnumerable;
        }


        private bool Update(object entity, FieldInfo primaryKey)
        {
            int numberOfUpdatedRows = 0;

            string updateSql = this.PrepareEntityUpdateString(entity, primaryKey);

            using (this.connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand updateCommand = new SqlCommand(updateSql, this.connection);
                numberOfUpdatedRows = updateCommand.ExecuteNonQuery();
            }

            return numberOfUpdatedRows > 0;
        }

        private string PrepareEntityUpdateString(object entity, FieldInfo primaryKey)
        {
            StringBuilder updateString = new StringBuilder();
            updateString.Append($"UPDATE {this.GetTableName(entity.GetType())} SET ");

            FieldInfo[] columnFields = entity.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.IsDefined(typeof(ColumnAttribute))).ToArray();
            StringBuilder columnsToSet = new StringBuilder();

            foreach (FieldInfo columnField in columnFields)
            {
                columnsToSet.Append($"{this.GetColumnName(columnField)} = '{columnField.GetValue(entity)}', ");
            }

            columnsToSet.Remove(columnsToSet.Length - 2, 2);
            updateString.Append(columnsToSet);

            updateString.Append($" WHERE Id = {primaryKey.GetValue(entity)}");

            return updateString.ToString();
        }

        private bool Insert(object entity, FieldInfo primaryKey)
        {
            int numberOfAffectedRows = 0;

            string insertSql = this.PrepareEntityInsertionString(entity);

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand insertCommand = new SqlCommand(insertSql, this.connection);
                numberOfAffectedRows = insertCommand.ExecuteNonQuery();

                string getLastIdQuery = $"SELECT MAX([Id]) FROM {this.GetTableName(entity.GetType())}";
                SqlCommand getLastIdInDatabaseCommand = new SqlCommand(getLastIdQuery, this.connection);
                int id = (int) getLastIdInDatabaseCommand.ExecuteScalar();
                primaryKey.SetValue(entity, id);
            }

            return numberOfAffectedRows > 0;
        }

        private string PrepareEntityInsertionString(object entity)
        {
            StringBuilder insertionString = new StringBuilder();
            StringBuilder columnsString = new StringBuilder();
            StringBuilder valuesString = new StringBuilder();

            insertionString.Append($"INSERT INTO {this.GetTableName(entity.GetType())} (");
            FieldInfo[] columnFields = entity.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.IsDefined(typeof(ColumnAttribute)))
                .ToArray();

            foreach (var columnField in columnFields)
            {
                string value = columnField.GetValue(entity).ToString();
                columnsString.Append($"{this.GetColumnName(columnField)}, ");
                valuesString.Append($"'{value}', ");
            }

            columnsString = columnsString.Remove(columnsString.Length - 2, 2);
            valuesString = valuesString.Remove(valuesString.Length - 2, 2);

            insertionString.Append(columnsString);
            insertionString.Append(") VALUES (");
            insertionString.Append(valuesString);
            insertionString.Append(")");

            return insertionString.ToString();
        }

        private void CreateTable(Type entity)
        {
            string tableCreationString = this.PrepareTableCreationString(entity);

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(tableCreationString, this.connection);
                command.ExecuteNonQuery();
            }
        }

        private string PrepareTableCreationString(Type entity)
        {
            StringBuilder createSqlCommand = new StringBuilder();
            createSqlCommand.Append($"CREATE TABLE {GetTableName(entity)} (");
            createSqlCommand.Append($"[Id] INT PRIMARY KEY IDENTITY(1, 1), ");

            FieldInfo[] columns = entity
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.IsDefined(typeof(ColumnAttribute)))
                .ToArray();

            foreach (var column in columns)
            {
                createSqlCommand.Append($"[{this.GetColumnName(column)}] {this.GetTypeToDB(column)}, ");
            }

            createSqlCommand = createSqlCommand.Remove(createSqlCommand.Length - 2, 2);
            createSqlCommand.Append(")");

            return createSqlCommand.ToString();
        }

        private string GetTypeToDB(FieldInfo field)
        {
            switch (field.FieldType.Name)
            {
                case "Int32":
                    return "INT";
                case "String":
                    return "VARCHAR(100)";
                case "Boolean":
                    return "BIT";
                case "DateTime":
                    return "DATETIME";
                default:
                    throw new InvalidOperationException("No such type exists. Try extending the framework!");
            }
        }

        private bool CheckIfTableExists(Type entity)
        {
            string query =
              $"SELECT COUNT(name) " +
              $"FROM sys.sysobjects " +
              $"WHERE [Name] = '{this.GetTableName(entity)}' AND [xtype] = 'U'";

            int numberOfTables = 0;
            using (connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(query, this.connection);
                numberOfTables = (int)command.ExecuteScalar();
            }

            return numberOfTables > 0;
        }

        // Helper methods
        private FieldInfo GetId(Type entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot get id from null type");
            }

            FieldInfo idField = entity
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.IsDefined(typeof(IdAttribute)));

            if (idField == null)
            {
                throw new InvalidOperationException("Cannot opearate with entity without primary key");
            }

            return idField;
        }

        private string GetTableName(Type entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Cannot get table name from null type!");
            }

            if (!entity.IsDefined(typeof(EntityAttribute)))
            {
                throw new ArgumentException("Cannot get table name of entity!");
            }

            string tableName = entity.GetCustomAttribute<EntityAttribute>().TableName;

            if (tableName == null)
            {
                throw new ArgumentException("Table name cannot be null!");
            }

            return tableName;
        }

        private string GetColumnName(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("Cannot get name from undefined field");
            }

            if (!field.IsDefined(typeof(ColumnAttribute)))
            {
                throw new ArgumentException("Cannot get field name!");
            }

            string fieldName = field.GetCustomAttribute<ColumnAttribute>().ColumnName;

            if (fieldName == null)
            {
                throw new ArgumentException("Column name cannot be null!");
            }

            return fieldName;
        }
    }
}
