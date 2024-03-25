using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public  class Data
    {
        //private static string DuongDan = @"Data Source=Chrysalism\SQLEXPRESS;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        string databaseName = "KLTN";
        //private static SqlConnection TaoKetNoi(string selectedDatabase)
        //{
        //    return new SqlConnection($"Data Source=Chrysalism\\SQLEXPRESS;Initial Catalog={selectedDatabase};Integrated Security=True");
        //}'
        private static SqlConnection TaoKetNoi()
        {
            return new SqlConnection($"Data Source=Chrysalism\\SQLEXPRESS;Initial Catalog=KLTN;Integrated Security=True");
        }
        // Lấy dữ liệu từ database ra 1 table
        public static DataTable GetTable(string sql)
        {
            SqlConnection KetNoi = TaoKetNoi();
            KetNoi.Open();
            SqlDataAdapter ad = new SqlDataAdapter(sql, KetNoi);
            DataTable b = new DataTable();
            ad.Fill(b);
            KetNoi.Close();
            ad.Dispose();
            return b;
        }
        // phương thức thêm sửa xoa
        public static void AddEditDelete(string sql)
        {
            SqlConnection con = TaoKetNoi();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }
        public static List<List<string>> GetDataFromDatabase(string nameTable)
        {
            List<List<string>> dataList = new List<List<string>>();
            SqlConnection KetNoi = TaoKetNoi();
            string query = $"SELECT Transactions FROM {nameTable}";
            KetNoi.Open();
            SqlCommand command = new SqlCommand(query, KetNoi);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string transactions = reader.GetString(0);
                transactions = transactions.Replace(" ", "");
                List<string> transactionList = new List<string>(transactions.Split(','));
                dataList.Add(transactionList);
            }

            reader.Close();
                
            return dataList;
        }
    }
}
