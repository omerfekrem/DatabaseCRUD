using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VerileriTazele();
        }

        private void VerileriTazele()
        {
            //veritabanındaki bilgileri çekip datagrid e yazmamızı sağlayan metod

            //Veritabanına bağlantı kurmamızı sağlar
            SqlConnection connection = VeriTabaninaBaglan(); 

            //Kurulan bağlantı üzerinden SQL komutunu hazırla
            SqlCommand command = new SqlCommand("SELECT * FROM Ogrenci", connection); 

            //SQL komutunu çalıştır ve elde edilen değeri reader değişkenine aktar
            SqlDataReader reader = command.ExecuteReader();

            //Çekilen veriler reader değişkeninde tutuluyor. Buradan dataGrid e aktarmak için gereken hazırlık yapılıyor
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            //reader ve connection kapatılıyor
            reader.Close();
            connection.Close();

            //veriler dataGrid e aktarılıyor
            dataGridView1.DataSource = dataTable;
        }

        private static SqlConnection VeriTabaninaBaglan()
        {
            //Veritabanına bağlanma işlemi select, insert, update vb işlemlerinde yapılması gerekir. Bunun için bağlanma komutlarını tekrar tekrar yazmak yerine metod halina getirmek mantıklı olacaktır.
            
            //Bağlantı için kullanılan bağlantı cümlesinin tanımlanması
            string connectionString = "Data Source=.;Initial Catalog=VT3;Integrated Security=True";

            //bağlantı cümlesi ile bağlantının oluşturulması
            SqlConnection connection = new SqlConnection(connectionString);

            //bağlantı kapalı durumda ise aç
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            //bağlantı nesnesini çarıldığı ihtiyaç olan yere gönder
            return connection;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            //kayıt ekleme metodu

            //VT bağlantısı için gerekli olan nesne
            SqlConnection connection = VeriTabaninaBaglan();

            //SQL komutunun kalıbının ve parametrelerinin hazırlanması
            SqlCommand command = new SqlCommand("INSERT INTO Ogrenci(Numarasi,Adi,Soyadi) VALUES(@Numarasi,@Adi,@Soyadi)", connection);
            command.Parameters.AddWithValue("@Numarasi", txtNumarasi.Text);
            command.Parameters.AddWithValue("@Adi", txtAdi.Text);
            command.Parameters.AddWithValue("@Soyadi", txtSoyadi.Text);

            //SQL komutunun çalıştır
            command.ExecuteNonQuery();

            //Bağlantıyı kapat
            connection.Close();

            //Verileri dataGrid e tekrar çek
            VerileriTazele();
        }
    }
}
