using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
// we gaan hier sqlite gebruiken als databank
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectDevOps
{
    public partial class Voetbalstatistieken : Form
    {
        //hier gaan we overerving gebruiken voor de database connection uit onze databank.cs file
        private Databank databank;
        public Voetbalstatistieken()
        {
            //hier gaan we alle nodige componenten initiliseren
            InitializeComponent();
            //initializeren we de databank
            databank = new Databank();
            //dit is voor onze text boxes voor wanneer ze focus hebben en wnr ze geen focus hebben is voor alle txt boxes
            txtNaamThuisploeg.GotFocus += txtNaamThuisploeg_GotFocus;
            txtNaamThuisploeg.LostFocus += txtNaamThuisploeg_LostFocus;
            txtScoreThuisploeg.GotFocus += txtScoreThuisploeg_GotFocus;
            txtScoreThuisploeg.LostFocus += txtScoreThuisploeg_LostFocus;
            txtCoachThuisploeg.GotFocus += txtCoachThuisploeg_GotFocus;
            txtCoachThuisploeg.LostFocus += txtCoachThuisploeg_LostFocus;
            txtBalbezitThuisploeg.GotFocus += txtBalbezitThuisploeg_GotFocus;
            txtBalbezitThuisploeg.LostFocus += txtBalbezitThuisploeg_LostFocus;
            txtGeleThuisploeg.GotFocus += txtGeleThuisploeg_GotFocus;
            txtGeleThuisploeg.LostFocus += txtGeleThuisploeg_LostFocus;
            txtRodeKaartenThuisploeg.GotFocus += txtRodeKaartenThuisploeg_GotFocus;
            txtRodeKaartenThuisploeg.LostFocus += txtRodeKaartenThuisploeg_LostFocus;
            txtNaamBezoekers.GotFocus += txtNaamBezoekers_GotFocus;
            txtNaamBezoekers.LostFocus += txtNaamBezoekers_LostFocus;
            txtScoreBezoekers.GotFocus += txtScoreBezoekers_GotFocus;
            txtScoreBezoekers.LostFocus += txtScoreBezoekers_LostFocus;
            txtCoachBezoekers.GotFocus += txtCoachBezoekers_GotFocus;
            txtCoachBezoekers.LostFocus += txtCoachBezoekers_LostFocus;
            txtBalbezitBezoekers.GotFocus += txtBalbezitBezoekers_GotFocus;
            txtBalbezitBezoekers.LostFocus += txtBalbezitBezoekers_LostFocus;
            txtGeleKaartenBezoekers.GotFocus += txtGeleKaartenBezoekers_GotFocus;
            txtGeleKaartenBezoekers.LostFocus += txtGeleKaartenBezoekers_LostFocus;
            txtRodeKaartenBezoekers.GotFocus += txtRodeKaartenBezoekers_GotFocus;
            txtRodeKaartenBezoekers.LostFocus += txtRodeKaartenBezoekers_LostFocus;

      
        }

      
        //hier maken we een tabel in onze sql
        protected void CreateTable(SQLiteConnection conn)
        {
            //voor het uitvoeren van sql commandos
            SQLiteCommand cmd = conn.CreateCommand();
            //wordt er een tabel aangemaakt als deze nog niet bestaat, ik noem de mijne voetbal
            //er worden allemaal velden meegegeven met een string eigenschap, dit omdat we niet gaan rekenen met deze gegevens dan kan je deze beter een varchar van houden
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS Voetbal" +
                " (thuisploeg VARCHAR(50), score VARCHAR(50), coach VARCHAR(50)," +
                "balbezit VARCHAR(50), geleKaarten VARCHAR(50), rodeKaarten VARCHAR(50), bezoekers VARCHAR(50), scoreB VARCHAR(50), coachB VARCHAR(50), " +
                "balbezitB VARCHAR(50), geleKaartenB VARCHAR(50), rodeKaartenB VARCHAR(50))";
            //het resultaat uitvoeren van hierboven
            cmd.ExecuteNonQuery();
        }
        //we moeten ook de values in onze tabel krijgen
        protected void InsertData(SQLiteConnection conn, string thuisploeg, string score, string coach, string balbezit,
            string geleKaarten, string rodeKaarten, string bezoekers, string scoreB, string coachB, string balbezitB,
            string geleKaartenB, string rodeKaartenB){ 

            //hier wordt een sql opdracht gemaakt
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Voetbal (thuisploeg, score, coach, balbezit, geleKaarten, rodeKaarten, bezoekers, scoreB, coachB, balbezitB, geleKaartenB, rodeKaartenB) VALUES('{thuisploeg}', '{score}', '{coach}', '{balbezit}', '{geleKaarten}', '{rodeKaarten}', '{bezoekers}', '{scoreB}', '{coachB}', '{balbezitB}', '{geleKaartenB}', '{rodeKaartenB}')";
            
            cmd.ExecuteNonQuery();

        }

        //ik toonde eerst het resultaat in een label maar dat was niet zo geweldig, maar ik laat het erbij in commentaar
        /*
        protected void ReadData(SQLiteConnection conn) {

            SQLiteDataReader reader;
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Voetbal";
            reader = cmd.ExecuteReader();

            StringBuilder resultText=new StringBuilder();

            while (reader.Read())
            {
                string thuisploeg=reader["thuisploeg"].ToString();
                string score = reader["score"].ToString();
                string coach = reader["coach"].ToString();
                string balbezit = reader["balbezit"].ToString();
                string geleKaarten = reader["geleKaarten"].ToString();
                string rodeKaarten = reader["rodeKaarten"].ToString();
                string bezoekers = reader["bezoekers"].ToString();
                string scoreB = reader["scoreB"].ToString();
                string coachB = reader["coachB"].ToString();
                string balbezitB = reader["balbezitB"].ToString();
                string geleKaartenB = reader["geleKaartenB"].ToString();
                string rodeKaartenB = reader["rodeKaartenB"].ToString();

                resultText.AppendLine($"{thuisploeg} tegen {bezoekers}");

            }

            lblShow.Text=resultText.ToString();
        }*/
        //hier wordt onze data gelezen, worden uit de databank gehaald
        protected void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader reader;
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Voetbal";
            reader = cmd.ExecuteReader();
            //dingen die in de listbox staan worden eruit verwijderd als het programme start
            lstTest.Items.Clear(); 

            while (reader.Read())
            {
                //gegevens uit de rij ophalen die we hierboven hebben benoemd
                string thuisploeg = reader["thuisploeg"].ToString();
                string score = reader["score"].ToString();
                string coach = reader["coach"].ToString();
                string balbezit = reader["balbezit"].ToString();
                string geleKaarten = reader["geleKaarten"].ToString();
                string rodeKaarten = reader["rodeKaarten"].ToString();
                string bezoekers = reader["bezoekers"].ToString();
                string scoreB = reader["scoreB"].ToString();
                string coachB = reader["coachB"].ToString();
                string balbezitB = reader["balbezitB"].ToString();
                string geleKaartenB = reader["geleKaartenB"].ToString();
                string rodeKaartenB = reader["rodeKaartenB"].ToString();

                //zo gaat ons eindresultaat eruit zien in onze listbox 
                lstTest.Items.Add($"{thuisploeg}: {score} - {bezoekers}: {scoreB}");
                lstTest.Items.Add($"Coach van {thuisploeg}: {coach} en van {bezoekers}: {coachB}");
                lstTest.Items.Add($"Statistieken: Balbezit: {balbezit}% - {balbezitB}% ");
                lstTest.Items.Add($"Statistieken: Gele kaarten: {geleKaarten} - {geleKaartenB} ");
                lstTest.Items.Add($"Statistieken: Rode kaarten: {rodeKaarten} - {rodeKaartenB} ");
                lstTest.Items.Add($"*****************************************************************");



            }
        }

        //hiermee kan je alle gegevens uit de databank verwijderen
        protected void ClearTable(SQLiteConnection conn)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            //worden all gegevens uit Voetbal verwijderd
            cmd.CommandText = "DELETE FROM Voetbal";
            cmd.ExecuteNonQuery();
        }

       


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NaamBezoekers_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
      
        public void button1_Click(object sender, EventArgs e)
        {
            //Als er een veld niet is ingevuld 
            if (txtNaamThuisploeg.Text == "Naam" || txtScoreThuisploeg.Text=="Score" || txtCoachThuisploeg.Text=="Coach" ||
                txtGeleThuisploeg.Text=="Gele kaarten" || txtRodeKaartenThuisploeg.Text=="Rode kaarten" || txtNaamBezoekers.Text == "Naam" || 
                txtScoreBezoekers.Text == "Score" || txtCoachBezoekers.Text == "Coach" ||
                txtGeleKaartenBezoekers.Text == "Gele kaarten" || txtRodeKaartenBezoekers.Text == "Rode kaarten")

            {
                //dan komt er een message box met de melding dat je alle velden moet invullen
                MessageBox.Show("Vul aub alle velden", "De info is niet compleet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            //word er verbinding gemaakt met de databank
            SQLiteConnection connectionSQL = databank.InitializeDatabase();
            CreateTable(connectionSQL);
            //nu wordt alle informatie van in onze textboxen naar de databank gestuurd
            InsertData(connectionSQL, txtNaamThuisploeg.Text, txtScoreThuisploeg.Text, txtCoachThuisploeg.Text, txtBalbezitThuisploeg.Text, txtGeleThuisploeg.Text, txtRodeKaartenThuisploeg.Text, txtNaamBezoekers.Text, txtScoreBezoekers.Text, txtCoachBezoekers.Text, txtBalbezitBezoekers.Text, txtGeleKaartenBezoekers.Text, txtRodeKaartenBezoekers.Text);

            connectionSQL.Close();
             
        }

        private void txtCoachThuisploeg_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtScoreBezoekers_TextChanged(object sender, EventArgs e)
        {

        }
        //als we op onze button indienen drukken gaat er dit gebeuren:
        private void button1_Click_1(object sender, EventArgs e)
        {
            //wordt de verbinding met de databank aangeroepen, ds je gaat gegevens naar de databank sturen
            SQLiteConnection connectionSQL =databank.InitializeDatabase();

            ClearTable(connectionSQL);
            connectionSQL.Close();
        }
        //dit is om de vorige matchen te bekijken in de databank: dus je gaat gegevens uit de databank halen
        private void btnVorige_Click(object sender, EventArgs e)
        {
            SQLiteConnection connectionSQL = databank.InitializeDatabase();

            ReadData(connectionSQL);

            connectionSQL.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //dit is wanneer we focus hebben op een text field
          private void txtNaamThuisploeg_GotFocus(object sender, EventArgs e)
          {
            //als er in de textbox nog het woord naam staat en we drukken hierop dan 
               if (txtNaamThuisploeg.Text == "Naam")
            {
                //moet dit worden leegemaakt
                txtNaamThuisploeg.Text = string.Empty;
            }
          }

        //  als we de focus verliezeb dus als we uit de textbox gaan dan
          private void txtNaamThuisploeg_LostFocus(object sender, EventArgs e)
          {
              // moeten we nakijken of deze leeg is
              if (string.IsNullOrWhiteSpace(txtNaamThuisploeg.Text))
              {
                  // als deze leeg is en wij hebben gaan value ingevuld dan moet de originele tekst weer terugkomen, Naam dus
                  // als we wel een value hebben ingevuld dan blijft die value er staan
                  txtNaamThuisploeg.Text = "Naam";
              }

          }
        //dit is hetzelfde voor alle velden
        private void txtScoreThuisploeg_GotFocus(object sender, EventArgs e)
        {
            if (txtScoreThuisploeg.Text == "Score")
            {
                txtScoreThuisploeg.Text = string.Empty;
            }
        }

        private void txtScoreThuisploeg_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtScoreThuisploeg.Text))
            {
                txtScoreThuisploeg.Text = "Score";
            }
        }

        private void txtCoachThuisploeg_GotFocus(object sender, EventArgs e)
        {
            if (txtCoachThuisploeg.Text == "Coach")
            {
                txtCoachThuisploeg.Text = string.Empty;
            }
        }

        private void txtCoachThuisploeg_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCoachThuisploeg.Text))
            {
                txtCoachThuisploeg.Text = "Coach";
            }
        }

        private void txtBalbezitThuisploeg_GotFocus(object sender, EventArgs e)
        {
            if (txtBalbezitThuisploeg.Text == "Balbezit %")
            {
                txtBalbezitThuisploeg.Text = string.Empty;
            }
        }

        private void txtBalbezitThuisploeg_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBalbezitThuisploeg.Text))
            {
                txtBalbezitThuisploeg.Text = "Balbezit %";
            }
        }

        private void txtGeleThuisploeg_GotFocus(object sender, EventArgs e)
        {
            if (txtGeleThuisploeg.Text == "Gele kaarten")
            {
                txtGeleThuisploeg.Text = string.Empty;
            }
        }

        private void txtGeleThuisploeg_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGeleThuisploeg.Text))
            {
                txtGeleThuisploeg.Text = "Gele kaarten";
            }
        }

        private void txtRodeKaartenThuisploeg_GotFocus(object sender, EventArgs e)
        {
            if (txtRodeKaartenThuisploeg.Text == "Rode kaarten")
            {
                txtRodeKaartenThuisploeg.Text = string.Empty;
            }
        }

        private void txtRodeKaartenThuisploeg_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRodeKaartenThuisploeg.Text))
            {
                txtRodeKaartenThuisploeg.Text = "Rode kaarten";
            }
        }

        private void txtNaamBezoekers_GotFocus(object sender, EventArgs e)
        {
            if (txtNaamBezoekers.Text == "Naam")
            {
                txtNaamBezoekers.Text = string.Empty;
            }
        }

        private void txtNaamBezoekers_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNaamBezoekers.Text))
            {
                txtNaamBezoekers.Text = "Naam";
            }
        }

        private void txtScoreBezoekers_GotFocus(object sender, EventArgs e)
        {
            if (txtScoreBezoekers.Text == "Score")
            {
                txtScoreBezoekers.Text = string.Empty;
            }
        }

        private void txtScoreBezoekers_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtScoreBezoekers.Text))
            {
                txtScoreBezoekers.Text = "Score";
            }
        }

        private void txtCoachBezoekers_GotFocus(object sender, EventArgs e)
        {
            if (txtCoachBezoekers.Text == "Coach")
            {
                txtCoachBezoekers.Text = string.Empty;
            }
        }

        private void txtCoachBezoekers_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCoachBezoekers.Text))
            {
                txtCoachBezoekers.Text = "Coach";
            }
        }

        private void txtBalbezitBezoekers_GotFocus(object sender, EventArgs e)
        {
            if (txtBalbezitBezoekers.Text == "Balbezit %")
            {
                txtBalbezitBezoekers.Text = string.Empty;
            }
        }

        private void txtBalbezitBezoekers_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBalbezitBezoekers.Text))
            {
                txtBalbezitBezoekers.Text = "Balbezit %";
            }
        }

        private void txtGeleKaartenBezoekers_GotFocus(object sender, EventArgs e)
        {
            if (txtGeleKaartenBezoekers.Text == "Gele kaarten")
            {
                txtGeleKaartenBezoekers.Text = string.Empty;
            }
        }

        private void txtGeleKaartenBezoekers_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGeleKaartenBezoekers.Text))
            {
                txtGeleKaartenBezoekers.Text = "Gele kaarten";
            }
        }

        private void txtRodeKaartenBezoekers_GotFocus(object sender, EventArgs e)
        {
            if (txtRodeKaartenBezoekers.Text == "Rode kaarten")
            {
                txtRodeKaartenBezoekers.Text = string.Empty;
            }
        }

        private void txtRodeKaartenBezoekers_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRodeKaartenBezoekers.Text))
            {
                txtRodeKaartenBezoekers.Text = "Rode kaarten";
            }
        }

        private void txtNaamThuisploeg_TextChanged(object sender, EventArgs e)
        {

        }

        private void lsttest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstTest_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
