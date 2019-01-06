using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

using ServerApi.Models;

namespace ServerApi.Database
{
    public class Connection
    {
        static MySqlConnectionStringBuilder builder;

        public Connection()
        {
            builder = new MySqlConnectionStringBuilder
            {
                Server = "sql7.freesqldatabase.com",
                Database = "sql7272543",
                UserID = "sql7272543",
                Password = "QwgImFzEpd",
                SslMode = MySqlSslMode.None,
            };
        }

        public int registerUser(string Email)
        {
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string sql4 = "SELECT id FROM usuarios WHERE email = '" + Email + "'";
                    MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                    MySqlDataReader rdr = cmd4.ExecuteReader();

                    if (rdr.Read())
                    {
                        return Int32.Parse(rdr[0].ToString());
                    }
                    else
                    {
                        using (var conn1 = new MySqlConnection(builder.ConnectionString))
                        {
                            conn1.Open();
                            using (var command1 = conn1.CreateCommand())
                            {
                                string sql = "INSERT INTO usuarios (email) VALUES ('" + Email + "')";
                                MySqlCommand cmd = new MySqlCommand(sql, conn1);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        using (var conn2 = new MySqlConnection(builder.ConnectionString))
                        {
                            conn2.Open();
                            using (var command2 = conn.CreateCommand())
                            {
                                MySqlCommand cmd3 = new MySqlCommand(sql4, conn2);
                                MySqlDataReader rdr1 = cmd3.ExecuteReader();
                                if (rdr1.Read())
                                {
                                    return Int32.Parse(rdr1[0].ToString());
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void insertProduct(string link, int user, string description, string ebayId)
        {
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string sql = "INSERT INTO productos (link, usuarioId, descripcion, ebayId) VALUES ('" + link + "', " + user + " ,'" + description + "', " + ebayId + ")";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<string, string> selectUserRecommendations(int user)
        {
            List<string> descripcionesUser = new List<string>();
            Dictionary<string, string> rec;

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string sql4 = "SELECT descripcion FROM productos WHERE usuarioId = " + user;
                    MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                    MySqlDataReader rdr = cmd4.ExecuteReader();

                    while (rdr.Read())
                    {
                        descripcionesUser.Add(rdr[0].ToString());
                    }
                }
            }

            Dictionary<string, int> color = new Dictionary<string, int>();
            Dictionary<string, int> brand = new Dictionary<string, int>();
            Dictionary<string, int> size = new Dictionary<string, int>();

            foreach (var item in descripcionesUser)
            {
                string[] desc = item.Split(',');

                string colorDe = desc[0].Split(":")[1];

                if (color.TryGetValue(colorDe, out int value))
                {
                    color[colorDe] = value + 1;
                }
                else
                {
                    color.Add(colorDe, 0);
                }

                string brandDe = desc[1].Split(":")[1];

                if (brand.TryGetValue(colorDe, out int value2))
                {
                    brand[brandDe] = value2 + 1;
                }
                else
                {
                    brand.Add(brandDe, 0);
                }

                string sizeDe = desc[2].Split(":")[1];

                if (size.TryGetValue(sizeDe, out int valu3))
                {
                    size[sizeDe] = valu3 + 1;
                }
                else
                {
                    size.Add(sizeDe, 0);
                }
            }

            rec = new Dictionary<string, string>();

            if (color.Count > 0 && brand.Count > 0 && size.Count > 0)
            {
                var maxValueColor = color.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                var maxValueBrand = brand.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                var maxValueSize = size.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

                rec.Add("color", maxValueColor);
                rec.Add("size", maxValueSize);
                rec.Add("brand", maxValueBrand);
            }

            return rec;
        }

        public List<User> getUsers()
        {
            List<User> si = new List<User>();
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string sql4 = "SELECT * FROM usuarios";
                    MySqlCommand cmd4 = new MySqlCommand(sql4, conn);
                    MySqlDataReader rdr = cmd4.ExecuteReader();
                    while (rdr.Read())
                    {
                        si.Add(new User
                        {
                            id = Int32.Parse(rdr[0].ToString()),
                            email = rdr[1].ToString()
                        });
                    }
                    return si;
                }
            }
        }
    }
}