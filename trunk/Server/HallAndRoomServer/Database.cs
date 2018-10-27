using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
namespace HallAndRoomServer
{
    class Database : Singleton<Database>
    {
        MySqlConnection mycon;
        public void Connect()
        {
            string constr = "server=localhost;User Id=root;Database=NewFrame";
            mycon = new MySqlConnection(constr);
            mycon.Open();
            Console.WriteLine("Database Opened！");


            //Console.ReadLine();            
        }


        public void Insert()
        {
            MySqlCommand mycmd = new MySqlCommand("INSERT INTO `user`(`UserName`, `Password`, `Age`, `Sex`) VALUES ('test3','test3',10,1)", mycon);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("数据插入成功！");
            }
        }

        public void ExecNonQuery(string cmd)
        {
            MySqlCommand mycmd = new MySqlCommand(cmd, mycon);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("数据插入成功！");
            }
        }

        public MySqlDataReader ExecQuery(string cmd)
        {
            MySqlCommand mycmd = new MySqlCommand(cmd, mycon);
            MySqlDataReader reader = mycmd.ExecuteReader();
            return reader;
        }

        public void DisConnect()
        {
            mycon.Close();
        }

    }
}
