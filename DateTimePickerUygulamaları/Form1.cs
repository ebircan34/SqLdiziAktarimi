using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace DateTimePickerUygulamaları
{
    public partial class Form1 : Form
    {
        string connetionString;
        SqlConnection baglanti;
        public string[] st;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void DB_Connect_CN()
        {
            StreamReader oku = new StreamReader(@"data\Connection_DB.dat");
            connetionString = oku.ReadLine();
            baglanti = new SqlConnection(connetionString);
            baglanti.Open();
            MessageBox.Show("Connection Open  !");
            baglanti.Close();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            
            st = new string[10];
            //comboBox1.Items.Clear();
            string[] lineOfContents = File.ReadAllLines(@"data\CarparkTest.dat");
            int i= 0;
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                // get the 2nd element (the 1st item is always item 0)
                //comboBox1.Items.Add(tokens[0]);
                st[i] = tokens[0].ToString();
                i++;

            }
            int z= i;
            MessageBox.Show(Convert.ToString(z));
            for (i = 0; i < z; i++)
            {
                MessageBox.Show(st[i].ToString());

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DB_Connect_CN();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            string[] dizi;
            dizi = new string[comboBox1.Items.Count];
            for (int i = 0; i < comboBox1.Items.Count;i++)
            {
                baglanti.Open();
                string sql = "Select * from Sales where Carpark='" + st[i].ToString() + "'";

                SqlCommand cmd = new SqlCommand(sql, baglanti);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                
                MessageBox.Show(st[i].ToString());
                baglanti.Close();

            
            
            
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double toplam = 0;
            int i = 0;
            string[] dizi;
            dizi = new string[comboBox1.Items.Count];
            for (i = 0; i < comboBox1.Items.Count; i++)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT sum(TRevenue) FROM Sales where Carpark='" + comboBox1.Items[i].ToString() + "'", baglanti);
                SqlDataReader dr1 = cmd.ExecuteReader();
                
                
                while (dr1.Read())
                {
                    if (dr1[0].ToString() == string.Empty)
                    {

                        toplam = 0;
                        dizi[i] = Convert.ToString(toplam);
                        //textBoxcongreess.Text = string.Format("{0:c}", decimal.Parse(textBoxcongreess.Text));
                    }
                    else
                    {

                        toplam = Convert.ToDouble(dr1[0].ToString());
                        dizi[i] = Convert.ToString(toplam);
                        //TLpara = Convert.ToDouble(textBoxisladet.Text);
                        //textBoxcongreess.Text = string.Format("{0:c}", decimal.Parse(textBoxcongreess.Text));
                    }
                    



                }
                dr1.Close();
                baglanti.Close();
            }
            MessageBox.Show(Convert.ToString(comboBox1.Items.Count));

            for (i = 0; i < comboBox1.Items.Count; i++)
            {
                MessageBox.Show(dizi[i].ToString());
            }


        }
    }
}
