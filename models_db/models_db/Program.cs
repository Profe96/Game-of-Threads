using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Xml;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Net.Http;
using System.Web;

namespace models_db
{
    class Program
    {
        static string connStr;
        static MySqlConnection conn;
        //description = clave:valor, clave2:valor2
        static void Main(string[] args)
        {
            Console.Read();
        }
        static void connection()
        {
            connStr = "server =localhost;user=juanito; pass=juancito123!;database =got_main_database";
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        static Boolean register_user(string Email)
        {
            try
            {

                connection();
                string sql = "INSERT INTO users (Email, id_group) VALUES ('" + Email + "','')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("ok");
                Console.Read();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        static Boolean insert_user_ingroup(string user, int id_group)
        {
            connection();
            string sql = "UPDATE users SET id_group = " + id_group + " WHERE Email = '" + user + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("ok");
            Console.Read();
            return true;
        }

        static Boolean insert_own_products(string Description, int id_type, string user)
        {
            connection();
            string sql = "INSERT INTO own_products (description, id_type, id_user) VALUES ('" + Description + "', " + id_type + " ,'" + user + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("ok");
            Console.Read();
            return true;
        }
        class return_description
        {
            public string description;
            public int type_of;
        }
        static List<Object> select_own_products(int id_type, string user)
        {
            //Devuelve descripción y tipo en forma de lista si no se envía un tipo de producto, de lo contrario solo envia descripcion
            List<object> rest = new List<object>();

            connection();
            string sql = "";

            if (id_type == 0)
            {
                sql = "SELECT description, id_type FROM own_products WHERE id_user='" + user + "' AND id_type = " + id_type + "";
            }
            else
            {
                sql = "SELECT description FROM own_products WHERE id_user='" + user + "'";
            }

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                return_description return_d = new return_description();
                return_d.description = rdr[0].ToString();

                if (id_type != 0)
                {
                    return_d.type_of = Int32.Parse(rdr[1].ToString());
                }
                rest.Add(return_d);
            }


            conn.Close();
            Console.WriteLine("ok");
            Console.Read();

            return rest;
        }

        static Boolean insert_products(string id, int id_type, string link, string reference, string user, int group, string description)
        {
            try
            {
                Boolean max_found = false;
                connection();
                string sql4 = "SELECT COUNT(*) FROM user_products WHERE id_user='" + user + "'";
                MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                MySqlDataReader rdr = cmd4.ExecuteReader();

                while (rdr.Read())
                {
                   if(Convert.ToInt32(rdr[0].ToString()) == 5)
                    {
                        max_found = true;
                    }
                }

                if (max_found)
                {
                    connection();
                    string sql5 = "DELETE FROM user_products WHERE id_user_products = (SELECT id_user_products FROM user_products WHERE id_user = '" + user + "' LIMIT 1 OFFSET 4)";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                    cmd5.ExecuteNonQuery();
                    conn.Close();
                }

                    connection();
                string sql = "INSERT INTO products (id, id_type, link, reference) VALUES ('" + id + "', " + id_type + " ,'" + link + "', '" + reference + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                try
                {
                    connection();
                    string sql5 = "INSERT INTO ebay (id, description) VALUES ('" + id + "', '" + description + "')";
                    MySqlCommand cmd5 = new MySqlCommand(sql5, conn);
                    cmd5.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                }

                connection();
                string sql2 = "INSERT INTO user_products (id_user, id_product) VALUES ('" + user + "', '" + id + "')";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.ExecuteNonQuery();
                conn.Close();

                if (group != 0)
                {
                    connection();
                    string sql3 = "INSERT INTO group_products (id_group, id_product) VALUES ('" + group + "', '" + id + "')";
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    cmd3.ExecuteNonQuery();
                    conn.Close();
                }

                Console.WriteLine("ok");
                Console.Read();
                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        static Dictionary<string, string> select_recomendations(string user)
        {
            List<string> own_products;
            List<string> ebay_products;
           own_products = select_recommendations_own(user);
            ebay_products = select_recommendations_ebay(user);

            Dictionary<string, int> products = new Dictionary<string, int>();
            Dictionary<string, string> recommendation = new Dictionary<string, string>();

            foreach (string item in own_products)
            {
                string[] broke = item.Split(',');
                foreach (string sub_item in broke)
                {
                    if (!products.ContainsKey(sub_item))
                    {
                        string[] sub_broke = sub_item.Split(':');
                        if (!recommendation.ContainsKey(sub_broke[0]))
                        {
                            recommendation.Add(sub_broke[0], "");
                        }
                        products.Add(sub_item, 1);
                    }
                    else
                    {
                        products[sub_item] = (products[sub_item] + 1);
                    }
                }
            }

            foreach (string item in ebay_products)
            {
                Array broke = item.Split(',');
                foreach (string sub_item in own_products)
                {
                    if (!products.ContainsKey(sub_item))
                    {
                        string[] sub_broke = sub_item.Split(':');
                        if (!recommendation.ContainsKey(sub_broke[0]))
                        {
                            recommendation.Add(sub_broke[0], "");
                        }                  
                        products.Add(sub_item, 1);
                    }
                    else
                    {
                        products[sub_item] = (products[sub_item] + 1);
                    }
                }
            }

            foreach (KeyValuePair<string, string> found in recommendation)
            {
                int max_val =0;
                string product_value = "";
                foreach (KeyValuePair<string, int> entry in products)
                {
                    if (entry.Key.Contains(found.Key))
                    {
                        if(entry.Value > max_val)
                        {
                            max_val = entry.Value;
                            string[] sub_broke = entry.Key.Split(':');
                            product_value = sub_broke[0];

                        }
                    }
                }
                recommendation[found.Key] = product_value;
            }

                return recommendation;
        }
        static List<string> select_recommendations_own(string user)
        {
            List<string> own_products = new List<string>();
            connection();
            string sql4 = "SELECT own_products.description FROM((user_products INNER JOIN products ON user_products.id_product = products.id) INNER JOIN own_products ON products.id = own_products.id_product) WHERE reference = 'Own' AND user_products.id_user = '" + user + "'; ";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            MySqlDataReader rdr = cmd4.ExecuteReader();

            while (rdr.Read())
            {
                own_products.Add(rdr[0].ToString());
            }
            return own_products;
        }

        static List<string> select_recommendations_ebay(string user)
        {
            List<string> ebay_products = new List<string>();

            connection();
            string sql4 = "SELECT ebay.description FROM((user_products INNER JOIN products ON user_products.id_product = products.id) INNER JOIN ebay ON products.id = ebay.id) WHERE reference = 'Ebay' AND id_user = '" + user + "'";
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            MySqlDataReader rdr = cmd4.ExecuteReader();

            while (rdr.Read())
            {
                ebay_products.Add(rdr[0].ToString());
            }
            return ebay_products;
        }

        static List<object> search_recommendations(Dictionary<string, string> products)
        {
            string query = "";
            Dictionary<string, string> searchquery = products;
            foreach (KeyValuePair<string, string> result in searchquery)
            {
                if (!query.Equals(""))
                {
                    query = "%" + result.Key + ":" + result.Value + "%";
                }
                else
                {
                    query += " OR description LIKE %" + result.Key + ":" + result.Value + "%";
                }
            }
            List<object> searching = new List<object>();
            connection();
            string sql4 = "SELECT description FROM own_products WHERE description LIKE " + query;
            MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
            MySqlDataReader rdr = cmd4.ExecuteReader();

            while (rdr.Read())
            {
                return_product value_product = new return_product();
                value_product.id = "";
                searching.Add(rdr[0].ToString());
            }
        
            return searching;
        }

        class return_product
        {
            public string id;
            public string product_type;
            public List<string> description;
        }


        }
}
