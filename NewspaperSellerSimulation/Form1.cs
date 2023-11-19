using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;

namespace MultiQueueSimulation
{
    public partial class Form1 : Form
    {
        Form2 form2;
        SimulationSystem system;
        Form3 form3;
        Form4 form4;


        public Form1()
        {
            
            InitializeComponent();
            form2 = new Form2( system);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog(); 

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1_path.Text=openFileDialog.FileName;
            }
            if (textBox1_path.Text.Length!=0 && textBox1_path.Text.Contains(".txt"))
            {


                system = new SimulationSystem(textBox1_path.Text);
                dataGridView1.Rows.Clear();
                dataGridView_ID.Rows.Clear();
                dataGridView_SD.Rows.Clear();
                dataGridView1.Rows.Add(system.NumberOfServers, system.StoppingNumber, system.StoppingCriteria.ToString(), system.SelectionMethod.ToString());


                for (int i = 0; i < system.InterarrivalDistribution.Count; i++)
                {

                    dataGridView_ID.Rows.Add(system.InterarrivalDistribution[i].Time, system.InterarrivalDistribution[i].Probability);
                }
                foreach (var item in system.Servers)
                {
                    for (int i = 0; i < item.TimeDistribution.Count; i++)
                    {
                        dataGridView_SD.Rows.Add(item.TimeDistribution[i].Time, item.TimeDistribution[i].Probability);
                    }
                    dataGridView_SD.Rows.Add("--", "--");
                }
            }
            else
            {
                textBox1_path.Text = "";
                MessageBox.Show("Enter file path");
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            form2 = new Form2( system);
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form3 = new Form3(system);
            form3.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            form4 = new Form4(system);
            form4.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_path_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
