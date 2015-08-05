//-----------------------------------------------------------------------
// <copyright file="Utilities.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Serialization;
    using log4net;
    using Oracle.DataAccess.Client;

    /// <summary>
    /// Class hosting various helper methods.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Gets the description of enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value.</param>
        /// <returns>
        /// Description of enumeration value, or exception in case there is no description defined.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Throws exception if value is null.</exception>
        /// <exception cref="System.InvalidOperationException">Throws exception if enumeration does not have description.</exception>
        public static string GetEnumDescription(Enum value)
        {
            if (value == null)
            {
                string exceptionMessage = "Can not get description of enum, because it is null.";
                throw new ArgumentNullException("value", exceptionMessage);
            }

            string result = string.Empty;

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                if (!string.IsNullOrEmpty(attributes[0].Description))
                {
                    result = attributes[0].Description;
                }
                else
                {
                    string exceptionMessage = string.Format(CultureInfo.InvariantCulture, "The description of enum {0}, is empty.", value.ToString());
                    throw new InvalidOperationException(exceptionMessage);
                }
            }
            else
            {
                string exceptionMessage = string.Format(CultureInfo.InvariantCulture,  "Can not get description of enum {0}, because it does not have description specified.", value.ToString());
                throw new InvalidOperationException(exceptionMessage);
            }

            return result;
        }

        /// <summary>
        /// Converts OLEDB connection string to MSSQL
        /// </summary>
        /// <param name="oleDbConnectionString">The OLE database connection string.</param>
        /// <returns>MSSQL connection string.</returns>
        public static string ConvertToSqlConnectionString(string oleDbConnectionString)
        {
            OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder(oleDbConnectionString);
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = oleDbConnectionStringBuilder.DataSource;
            sqlConnectionStringBuilder.InitialCatalog = oleDbConnectionStringBuilder["initial catalog"].ToString();
            sqlConnectionStringBuilder.PersistSecurityInfo = oleDbConnectionStringBuilder.PersistSecurityInfo;
            if (oleDbConnectionStringBuilder["Integrated Security"].ToString() == "SSPI")
            {
                sqlConnectionStringBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlConnectionStringBuilder.UserID = oleDbConnectionStringBuilder["user id"].ToString();
                sqlConnectionStringBuilder.Password = oleDbConnectionStringBuilder["password"].ToString();
            }

            return sqlConnectionStringBuilder.ConnectionString;
        }

        /// <summary>
        /// Converts OLEDB connection string to Oracle
        /// </summary>
        /// <param name="oleDbConnectionString">The OLE database connection string.</param>
        /// <returns>Oracle connection string.</returns>
        public static string ConvertToOracleConnectionString(string oleDbConnectionString)
        {
            OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder(oleDbConnectionString);
            OracleConnectionStringBuilder oracleDbConnectionStringBuilder = new OracleConnectionStringBuilder();
            oracleDbConnectionStringBuilder.DataSource = oleDbConnectionStringBuilder.DataSource;
            oracleDbConnectionStringBuilder.PersistSecurityInfo = oleDbConnectionStringBuilder.PersistSecurityInfo;
            oracleDbConnectionStringBuilder.UserID = oleDbConnectionStringBuilder["user id"].ToString();
            oracleDbConnectionStringBuilder.Password = oleDbConnectionStringBuilder["password"].ToString();

            return oracleDbConnectionStringBuilder.ConnectionString;
        }

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            WriteToBinaryFile<T>(filePath, objectToWrite, false);
        }

        /// <summary>
        /// Appends the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a constructor without parameters.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite) where T : new()
        {
            WriteToXmlFile<T>(filePath, objectToWrite, false);
        }

        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a constructor without parameters.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a constructor without parameters.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Trims all string properties, and sets null for empty or white space strings.
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>Sanitized instance</returns>
        public static T Sanitize<T>(T instance)
        {
            if (instance == null)
            {
                return instance;
            }

            var props = instance.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                //// Ignore non-string properties
                .Where(prop => prop.PropertyType == typeof(string))
                //// Ignore indexers
                .Where(prop => prop.GetIndexParameters().Length == 0)
                //// Must be both readable and writable
                .Where(prop => prop.CanWrite && prop.CanRead);

            foreach (PropertyInfo prop in props)
            {
                string value = (string)prop.GetValue(instance, null);
                if (value != null)
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        value = null;
                    }
                    else
                    {
                        value = value.Trim();
                    }

                    prop.SetValue(instance, value, null);
                }
            }

            return instance;
        }

        /// <summary>
        /// Gets the SHA-1 hash.
        /// </summary>
        /// <param name="data">The data that needs to be hashed.</param>
        /// <returns>The has of the data</returns>
        public static string GetHash(string data)
        {
            string result;
            byte[] hash;

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
            }

            result = BitConverter.ToString(hash).Replace("-", string.Empty);
            return result;
        }

        /// <summary>
        /// Appends the line to file.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="file">The file.</param>
        public static void AppendLineToFile(string line, string file)
        {
            string fileLocation = Path.GetDirectoryName(file);
            Directory.CreateDirectory(fileLocation);

            using (StreamWriter writer = File.AppendText(file))
            {
                writer.Write(line);
            }
        }
    }
}
