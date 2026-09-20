using System;
using System.Data;
//using System.Configuration;
//using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
//using System.Linq;
using System.Text;

public static class Database
{
    // NOTE: connection string is intentionally externalized as a placeholder for this
    // public/research release. Real credentials and server name were removed here.
    // Supply your own SQL Server connection details before running locally.
    public static String ConnectionString = "SERVER=YOUR_SERVER_NAME;User Id=YOUR_DB_USER; Password=YOUR_DB_PASSWORD; DATABASE=MTTest; Encrypt=FALSE;";

    public static SqlConnection GetConnection()
	{
        return new SqlConnection(ConnectionString);
	}

    public static DataTable Fill(DataTable table, String sql, params Object[] parameters)
    {
        SqlCommand command = Database.CreateCommand(sql, parameters);
        new SqlDataAdapter(command).Fill(table);
        command.Connection.Close();

        return table;
    }

    public static DataTable GetData(String sql, params Object[] parameters)
    {
        return Database.Fill(new DataTable(), sql, parameters);
    }

    public static int ExecuteNonQuery(String sql, params Object[] parameters)
    {
        SqlCommand command = Database.CreateCommand(sql, parameters);
        
        command.Connection.Open();
        int rows = command.ExecuteNonQuery();
        command.Connection.Close();
        
        return rows;
    }

    public static object ExecuteScalar(String sql, params Object[] parameters)
    {
        SqlCommand command = Database.CreateCommand(sql, parameters);
        
        command.Connection.Open();
        object value = command.ExecuteScalar();
        command.Connection.Close();

        return value;
    }

    private static SqlCommand CreateCommand(String sql, params Object[] parameters)
    {
        SqlCommand command = new SqlCommand(sql, Database.GetConnection());
        for (int i = 0; i < parameters.Length; i+=2)
        {
            command.Parameters.AddWithValue(parameters[i].ToString(), parameters[i + 1]);
        }
        return command;
    }

    public static string GetMd5Hash(string input)
    {
        MD5 md5Hash = MD5.Create();
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    /// <summary>
    /// Mã hóa chuỗi có mật khẩu
    /// </summary>
    /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
    /// <returns>Chuỗi đã mã hóa</returns>
    private static string GetEncryptionKey()
    {
        string key = Environment.GetEnvironmentVariable("MTTEST_ENCRYPTION_KEY");
        if (String.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("MTTEST_ENCRYPTION_KEY environment variable is required.");
        return key;
    }

    public static string Encrypt(string toEncrypt)
    {
        string key = GetEncryptionKey();
        bool useHashing = true;
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// Giản mã
    /// </summary>
    /// <param name="toDecrypt">Chuỗi đã mã hóa</param>
    /// <returns>Chuỗi giản mã</returns>
    public static string Decrypt(string toDecrypt)
    {
        string key = GetEncryptionKey();
        bool useHashing = true;
        byte[] keyArray;
        byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        return UTF8Encoding.UTF8.GetString(resultArray);
    }
}