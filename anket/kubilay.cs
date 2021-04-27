using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace anket
{
    class kubilay
    {
        SqlConnection bag = new SqlConnection(@"Data Source=SEVVALPC\SQLEXPRESS;Initial Catalog=anket;Integrated Security=True");

       public void sorulari_getir(ComboBox cb)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from sorular order by soru", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb.DataSource = dt;
            //cb.DisplayMember = "soru";
            //cb.ValueMember = "soruno";
        }
        public void cevaplari_getir(string soruno,ListBox lb)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from cevaplar where soruno='" + soruno + "'", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lb.DataSource = dt;
            lb.DisplayMember = "cevap";
            lb.ValueMember = "cevapno";
        }
        public void oyver(string cevapno)
        {
            SqlCommand komut = new SqlCommand("update cevaplar set oy=oy+1 where cevapno = @cevapno", bag);
            komut.Parameters.AddWithValue("@cevapno", cevapno);
            bag.Open();
            komut.ExecuteNonQuery();
            bag.Close();
            MessageBox.Show("Oy kullanıldı...");
        }

        public void grafik_ciz(string soruno,Chart c)
        {
            string sql="Select sum(oy) from cevaplar where soruno = @soruno";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            da.SelectCommand.Parameters.AddWithValue("@soruno",soruno);
            DataTable dt = new DataTable(); // sanal tablo oluşturuyoruz
            da.Fill(dt);
            int toplam = Convert.ToInt32(dt.Rows[0][0]); //buraya kadar soruya verilen toplam oyları bulmuş olduk

            //şimdi ise tablodaki barların değerlerini bulup x ve y eksenlerine atama yapıyoruz
            sql = "select cevap,((oy*100)/" + toplam.ToString()+") as yuzde from cevaplar where soruno=@soruno2";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, bag);
            da2.SelectCommand.Parameters.AddWithValue("@soruno2", soruno);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            c.DataSource = dt2;
            c.Series[0].XValueMember = "cevap";
            c.Series[0].YValueMembers = "yuzde";
            c.DataBind();
        }
    }

}
