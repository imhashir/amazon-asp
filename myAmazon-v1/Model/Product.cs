﻿using System.Data.SqlClient;

namespace myAmazon_v1.Model
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public int brandId { get; set; }
        public int catId { get; set; }

        public void fillWithSqlReader(SqlDataReader reader)
        {
            id = (int)reader["id"];
            name = reader["Name"].ToString();
            desc = reader["Desc"].ToString();
            image = reader["Image"].ToString();
            price = (int) reader["Price"];
            name = reader["CategoryId"].ToString();
            desc = reader["BrandId"].ToString();
        }
    }
}