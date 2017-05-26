using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;

public class dbConn
{
	private static string strConn = "server=mysql16.unoeuro.com;Database=themcwar_com_db2;Uid=themcwar_com;Pwd=lucky7d0llar;";



    public DataTable GetData(MySqlCommand cmd)
	{
		MySqlConnection objConn = new MySqlConnection(strConn);
		MySqlDataAdapter objDA = new MySqlDataAdapter();
		DataSet objDS = new DataSet();

		cmd.Connection = objConn;
		objDA.SelectCommand = cmd;
		objDA.Fill(objDS);

		return objDS.Tables[0];
	}


    public void ModifyData(MySqlCommand cmd)
	{
		MySqlConnection objConn = new MySqlConnection(strConn);
		cmd.Connection = objConn;

		objConn.Open();
		cmd.ExecuteNonQuery();
        objConn.Close();
	}

    public int InsertDataGetNewID(MySqlCommand cmd)
    {
       MySqlConnection objConn = new MySqlConnection(strConn);

        int newid;

        try
        {

            cmd.Connection = objConn;

            objConn.Open();

            cmd.ExecuteNonQuery();
            newid = (Int32) cmd.LastInsertedId;

        }

        finally
        {

            objConn.Close();

        }

        return newid;

    }
}
