﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Repository;
using MySql.Data.MySqlClient;
using System.Data;

namespace Logic.Data
{
    public class EquipmentData
    {

        EquipmentRepo rep = new EquipmentRepo();
        PropertyRepo proprep = new PropertyRepo();
        dbConn db = new dbConn();

        public Equipment Add(Equipment obj)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Equipment (catId, service) VALUES (@catid, @service)");
            cmd.Parameters.AddWithValue("@catid", obj._catId);
            cmd.Parameters.AddWithValue("@service", obj._service);

            int id = db.InsertDataGetNewID(cmd);


            foreach (KeyValuePair<string, string> item in obj._values)
            {
                Property prop = proprep.GetByName(item.Key);

                MySqlCommand cmdTwo = new MySqlCommand("INSERT INTO EquipmentValues (Property, Equipment, Value) VALUES(@prop, @eq, @val)");
                cmdTwo.Parameters.AddWithValue("@prop", prop._id);
                cmdTwo.Parameters.AddWithValue("@eq", id);
                cmdTwo.Parameters.AddWithValue("val", item.Value);

                db.ModifyData(cmdTwo);

            }

            obj._id = id;

            return obj;
        }

        public void DeleteById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Equipment WHERE id = @id");
            cmd.Parameters.AddWithValue("@id", id);
            db.ModifyData(cmd);

            MySqlCommand cmdTwo = new MySqlCommand("DELETE FROM EquipmentValues WHERE Equipment = @id");
            cmdTwo.Parameters.AddWithValue("@id", id);

            db.ModifyData(cmdTwo);
        }

        public void Edit(Equipment obj)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE Equipment SET Service = @ser, catId = @cat WHERE Id = @id");
            cmd.Parameters.AddWithValue("@ser", obj._service);
            cmd.Parameters.AddWithValue("@cat", obj._catId);
            cmd.Parameters.AddWithValue("@id", obj._id);
            db.ModifyData(cmd);

            foreach (KeyValuePair<string, string> item in obj._values)
            {
                Property prop = proprep.GetByName(item.Key);

                DeleteEquipmentValue(item.Value, obj._id, prop._id);

                MySqlCommand cmdTwo = new MySqlCommand("INSERT INTO EquipmentValues (Value, Property, Equipment) VALUES(@val, @prop, @eq)");
                cmdTwo.Parameters.AddWithValue("@val", item.Value);
                cmdTwo.Parameters.AddWithValue("@prop", prop._id);
                cmdTwo.Parameters.AddWithValue("@eq", obj._id);

                db.ModifyData(cmdTwo);
            }

        }

        public void DeleteEquipmentValue(string ValueName, int id, int prop)
        {
            MySqlCommand cmdTwo = new MySqlCommand("DELETE FROM EquipmentValues WHERE Equipment = @id AND Value = @val AND Property = @prop");
            cmdTwo.Parameters.AddWithValue("@id", id);
            cmdTwo.Parameters.AddWithValue("@val", ValueName);
            cmdTwo.Parameters.AddWithValue("@prop", prop);

            db.ModifyData(cmdTwo);

        }

        public void GetAll()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Id, Service, CatId FROM Equipment");
            DataTable dt = db.GetData(cmd);

            if (dt != null)
            {
                foreach (DataRow rw in dt.Rows)
                {

                    Dictionary<string, string> dic = GetValuesForEquipment(Convert.ToInt32(rw["id"]));

                    DateTime? date;
                    if (rw["service"] == DBNull.Value)
                        date = null;
                    else
                        date = Convert.ToDateTime(rw["service"]);

                    rep.Add(new Equipment(
                    Convert.ToInt32(rw["id"]),
                    Convert.ToInt32(rw["catId"]),
                    date,
                    dic
                    ));

                }
            }
        }

        public Dictionary<string, string> GetValuesForEquipment(int id)
        {
            MySqlCommand cmdTwo = new MySqlCommand(@"
            SELECT Property, Equipment, Value, Name
            FROM EquipmentValues
            JOIN Property ON EquipmentValues.Property = Property.Id
            WHERE EquipmentValues.Equipment = @id
            ");

            cmdTwo.Parameters.AddWithValue("@id", id);

            DataTable prop = db.GetData(cmdTwo);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (DataRow item in prop.Rows)
            {
                dic.Add(item["Name"].ToString(), item["Value"].ToString());
            }

            return dic;

        }






    }
}
