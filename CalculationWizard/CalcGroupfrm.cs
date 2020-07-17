using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculationWizard
{
    public partial class CalcGroupfrm : Form
    {
        private ModelHelper desktopModel;
        public CalcGroupfrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadModel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadModel()
        {
           
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if (args.Count() != 2)
                {
                    throw new ApplicationException("Please use this application directly from Power BI.");
                }
                string DesktopConnString = args[1];
                string DesktopModel = args[2];
                ConfigHelper.CalcGroupConfigurationItems Config;

            
                Config = ConfigHelper.readCalculationItemsfromFile(Application.StartupPath + "/CalcGroupConfiguration.json");
                string GroupName = ConfigHelper.ParseGroupName(Config);
                string ColumnName = ConfigHelper.ParseColumnName(Config);
                IList<CalculationGroupItem> CalculationItems = ConfigHelper.ParseCalculatedMembers(Config);

                desktopModel = new ModelHelper(DesktopConnString, DesktopModel, GroupName, ColumnName, CalculationItems);
                richTextBox1.Text = "Connected to Power BI Dekstop: " + DesktopConnString + " with model:" + DesktopModel;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + "\r\n Error: " + ex.Message;
                btnAddCalcGroup.Enabled = false;
                btnRemoveCalcGroup.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = richTextBox1.Text + "\r\n Adding calculation group.";
                desktopModel.AddCalculationGroups();
                richTextBox1.Text = richTextBox1.Text + "\r\n Calculation groups added.";
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + "\r\n Error: " + ex.Message;
            }
        }

        private void btnRemoveCalcGroup_Click(object sender, EventArgs e)
        {
            try
            {
                desktopModel.RemoveCalculationGroup();
                richTextBox1.Text = richTextBox1.Text + "\r\n Calculation groups removed.";

            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1.Text + "\r\n Error: " + ex.Message;
            }
        }

   }
}
