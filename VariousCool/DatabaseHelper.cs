using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousCool
{
    public class DatabaseHelper
    {
        //private static string dbPath = Constants.DataBase.DATABASE_FILENAME;
        private static string dbPath = "Data Source =" + Constants.DataBase.DATABASE_FILENAME;
        public static void Init()
        {

            //string dbPath = Constants.DataBase.DATABASE_FILENAME;
            if (File.Exists(Constants.DataBase.DATABASE_FILENAME) == false)
            {
                File.Create(Constants.DataBase.DATABASE_FILENAME);
            }

            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //创建table
            string sql_user =
                @"CREATE TABLE IF NOT EXISTS " + Constants.DataBase.DATABASE_USER_TABLE + @"(
                  uid      INTEGER          NOT NULL     PRIMARY KEY         AUTOINCREMENT, 
                  username VARCHAR(20),
                  name     VARCHAR(20),
                  pwd      VARCHAR(20),
                  icon     VARCHAR(50));";
            SQLiteCommand myCreateTableUser = new SQLiteCommand(sql_user, myCon);
            myCreateTableUser.ExecuteNonQuery();
            string sql_tips =
                @"CREATE TABLE IF NOT EXISTS " + Constants.DataBase.DATABASE_TIPS_TABLE + @" (
                  tipsid   INTEGER          NOT NULL     PRIMARY KEY         AUTOINCREMENT,         
                  uid      INTEGER REFERENCES " + Constants.DataBase.DATABASE_USER_TABLE + @"(uid),
                  tips_text VARCHAR(150), 
                  isread   INTEGER,
                  time     VARCHAR(50));";
            SQLiteCommand myCreateTableTips = new SQLiteCommand(sql_tips, myCon);
            myCreateTableTips.ExecuteNonQuery();

            User fff = new User();
            fff.UserName = "chenfenghuimeng@gmail.com";
            fff.Name = "CFhM_R";
            fff.IconAddress = "D:\\pictures\\aicon\\白底.png";
            if (GetUserFromUserName(fff.UserName) == null)
                InsertUser(fff);

            //释放资源
            myCreateTableUser.Dispose();
            myCreateTableTips.Dispose();
            myCon.Dispose();
        }

        public static IEnumerable<User> GetAllUsers()
        {
            //string dbPath = Constants.DataBase.DATABASE_FILENAME;

            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //查询
            string sql_query =
                @"SELECT * 
                  FROM " + Constants.DataBase.DATABASE_USER_TABLE;

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);

            //执行
            SQLiteDataReader myreader = mycmd.ExecuteReader(System.Data.CommandBehavior.Default);
            while (myreader.Read())
            {
                User myuser = new User();
                myuser.Uid = myreader.GetInt64(0);
                myuser.UserName = myreader.GetString(1);
                myuser.Name = myreader.GetString(2);
                myuser.Password = myreader.GetString(3);
                myuser.IconAddress = myreader.GetString(4);
                yield return myuser;
            }

            //释放
            mycmd.Dispose();
            myCon.Dispose();
            yield break;
        }

        public static IEnumerable<Tips> GetAllTips()
        {
            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //查询
            string sql_query =
                @"SELECT * 
                  FROM " + Constants.DataBase.DATABASE_TIPS_TABLE;

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);

            //执行
            SQLiteDataReader myreader = mycmd.ExecuteReader(System.Data.CommandBehavior.Default);
            while (myreader.Read())
            {
                Tips mytips = new Tips();
                mytips.TipsId = myreader.GetInt64(0);
                mytips.Uid = myreader.GetInt64(1);
                mytips.TipText = myreader.GetString(2);
                mytips.IsRead = myreader.GetInt64(3) != 0;
                mytips.Time = DateTime.Parse(myreader.GetString(4));
                yield return mytips;
            }

            //释放
            mycmd.Dispose();
            myCon.Dispose();
            yield break;
        }

        public static User GetUserFromUid(long uid)
        {
            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //查询
            string sql_query =
                @"SELECT * 
                  FROM " + Constants.DataBase.DATABASE_USER_TABLE + @"
                  WHERE uid=" + uid;

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);
            SQLiteDataReader myreader = mycmd.ExecuteReader();
            if (myreader.Read() == false)
                return null;
            User myuser = new User();
            myuser.Uid = myreader.GetInt64(0);
            myuser.UserName = myreader.GetString(1);
            myuser.Name = myreader.GetString(2);
            myuser.Password = myreader.GetString(3);
            myuser.IconAddress = myreader.GetString(4);

            mycmd.Dispose();
            myCon.Dispose();
            return myuser;
        }

        public static User GetUserFromUserName(string username)
        {
            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //查询
            string sql_query =
                @"SELECT * 
                  FROM " + Constants.DataBase.DATABASE_USER_TABLE + @"
                  WHERE username='" + username + "'";

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);
            SQLiteDataReader myreader = mycmd.ExecuteReader();
            if (myreader.Read() == false)
                return null;
            User myuser = new User();
            myuser.Uid = myreader.GetInt64(0);
            myuser.UserName = myreader.GetString(1);
            myuser.Name = myreader.GetString(2);
            myuser.Password = myreader.GetString(3);
            myuser.IconAddress = myreader.GetString(4);

            mycmd.Dispose();
            myCon.Dispose();
            return myuser;
        }

        public static Tips GetTipsFromTipsId(long tipsid)
        {
            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //查询
            string sql_query =
                @"SELECT * 
                  FROM " + Constants.DataBase.DATABASE_TIPS_TABLE + @"
                  WHERE tipid='" + tipsid + "'";

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);
            SQLiteDataReader myreader = mycmd.ExecuteReader();
            if (myreader.Read() == false)
                return null;
            Tips mytips = new Tips();
            mytips.TipsId = myreader.GetInt64(0);
            mytips.Uid = myreader.GetInt64(1);
            mytips.TipText = myreader.GetString(2);
            mytips.IsRead = myreader.GetInt64(3) != 0;
            mytips.Time = DateTime.Parse(myreader.GetString(4));

            mycmd.Dispose();
            myCon.Dispose();
            return mytips;
        }

        public static void DeleteTipsFromTipsId(long tipsid)
        {
            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //删除
            string sql_query =
                @"DELETE
                  FROM " + Constants.DataBase.DATABASE_TIPS_TABLE + @"
                  WHERE tipsid=" + tipsid;

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);
            SQLiteDataReader myreader = mycmd.ExecuteReader(System.Data.CommandBehavior.Default);

            mycmd.Dispose();
            myCon.Dispose();
        }

        public static void InsertTips(Tips mytips)
        {
            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //添加
            string sql_query =
                @"INSERT INTO " + Constants.DataBase.DATABASE_TIPS_TABLE + @"
                  (uid, tips_text, isread, time) VALUES (" +
                  mytips.Uid + "," +
                  "'" + mytips.TipText + "'," +
                  (mytips.IsRead ? 1 : 0) + "," +
                  "'" + mytips.Time + "');";

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);
            mycmd.ExecuteNonQuery(System.Data.CommandBehavior.Default);

            mycmd.Dispose();
            myCon.Dispose();
        }

        public static void InsertUser(User myuser)
        {
            SQLiteConnection myCon = new SQLiteConnection(dbPath);
            myCon.Open();

            //添加
            string sql_query =
                @"INSERT INTO " + Constants.DataBase.DATABASE_USER_TABLE + @"
                  (username, name, pwd, icon) VALUES (" +
                  "'" + myuser.UserName + "'," +
                  "'" + myuser.Name + "'," +
                  "'" + myuser.Password + "'," +
                  "'" + myuser.IconAddress + "');";

            SQLiteCommand mycmd = new SQLiteCommand(sql_query, myCon);
            mycmd.ExecuteNonQuery(System.Data.CommandBehavior.Default);

            mycmd.Dispose();
            myCon.Dispose();
        }
    }
}
