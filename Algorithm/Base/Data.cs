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
        private static SqlConnection TaoKetNoi()
        {
            return new SqlConnection($"Data Source=CHRYSALISM\\SQLEXPRESS;Initial Catalog=KLTN_DATA;Integrated Security=True");
        }
        //private static SqlConnection TaoKetNoi()
        //{
        //    return new SqlConnection($"Data Source=LAPTOP-9L1Q50HJ\\SQLEXPRESS;Initial Catalog=KLTN;Integrated Security=True");
        //}
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
        public static DataTable DataHeader(string tablename)
        {
            SqlConnection con = TaoKetNoi();
            con.Open();
            SqlCommand cmd = new SqlCommand($"select top 1 * from {tablename}", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            adapter.Dispose();
            return dt;
        }
        public static List<List<string>> GetDataFromDatabase(string nameTable)
        {
            List<List<string>> dataList = new List<List<string>>();
            SqlConnection KetNoi = TaoKetNoi();
            string query = $"SELECT * FROM {nameTable}";
            KetNoi.Open();
            SqlCommand command = new SqlCommand(query, KetNoi);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                List<string> transactionList;
                if (reader.FieldCount > 2)
                {
                    transactionList = new List<string>();
                    if (nameTable == "Du_Lieu_Weka_Breast_Cancer" )
                    {
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            // Đọc dữ liệu từ cột cụ thể
                            string transaction = reader.GetName(i) + " = " + reader.GetString(i);

                            // Xóa khoảng trắng trong dữ liệu
                            transaction = transaction.Replace(" ", "");

                            // Thêm dữ liệu vào danh sách
                            transactionList.Add(transaction);
                        }
                    }
                    else
                    {
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            string transaction = "";
                            string value = reader.GetString(i);
                            value = value.Trim();
                            if (value.Equals("yes"))
                            {
                                transaction = reader.GetName(i);
                                transactionList.Add(transaction);
                            }
                        }
                        
                    }
                }
                else
                {
                    string transactions = reader.GetString(1);
                    transactions = transactions.Replace(" ", "");
                    transactionList = new List<string>(transactions.Split(','));
                }
                dataList.Add(transactionList);
            }

            reader.Close();

            return dataList;
        }
    }
}
