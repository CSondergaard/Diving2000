using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows.Forms;

public class dbConn
{
    private static string strConn = "server=mysql16.unoeuro.com;Database=themcwar_com_db2;Uid=themcwar_com;Pwd=lucky7d0llar;";



    public DataTable GetData(MySqlCommand cmd)
    {
        DataTable dtreturn = null;
        MySqlConnection objConn = new MySqlConnection(strConn);
        MySqlDataAdapter objDA = new MySqlDataAdapter();
        DataSet objDS = new DataSet();

        try
        {
            cmd.Connection = objConn;
            objDA.SelectCommand = cmd;
            objDA.Fill(objDS);
            dtreturn = objDS.Tables[0];
        }
        catch(MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }

        return dtreturn;

    }


    public void ModifyData(MySqlCommand cmd)
    {
        MySqlConnection objConn = new MySqlConnection(strConn);

        try
        {
            cmd.Connection = objConn;

            objConn.Open();
            cmd.ExecuteNonQuery();
        }
        catch(MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            objConn.Close();

        }
    }

    public int InsertDataGetNewID(MySqlCommand cmd)
    {
        MySqlConnection objConn = new MySqlConnection(strConn);

        int newid = 0;

        try
        {

            cmd.Connection = objConn;

            objConn.Open();

            cmd.ExecuteNonQuery();
            newid = (Int32)cmd.LastInsertedId;

        }
        catch (MySqlException ex)
        {
            MessageBox.Show("Et problem er opstået: " + ex.Message);
        }
        finally
        {

            objConn.Close();

        }

        return newid;

    }
}
