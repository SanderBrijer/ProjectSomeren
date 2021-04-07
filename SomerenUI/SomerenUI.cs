using SomerenLogic;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SomerenUI
{
    public partial class SomerenUI : Form
    {

        List<Student> studentList = new List<Student>();
        List<Log> logList = new List<Log>();
        List<Guide> guides;
        List<Activity> activityList = new List<Activity>();
        bool reedsGeklikt0 = false;
        bool reedsGeklikt1 = false;
        bool reedsGeklikt2 = false;
        bool reedsGeklikt3 = false;
        bool reedsGekliktActivity0 = false;
        bool reedsGekliktActivity1 = false;
        bool reedsGekliktActivity2 = false;
        bool reedsGekliktActivity3 = false;


        public SomerenUI()
        {
            InitializeComponent();
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void showPanel(string panelName)
        {
            try
            {
                if (panelName == "Dashboard")
                {
                    hideAllPanels();

                    // show dashboard
                    pnl_Dashboard.Show();
                    img_Dashboard.Show();
                }
                else if (panelName == "Revenue")
                {
                    hideAllPanels();
                    pnl_Revenue.Show();

                    //pnl_Revenue.Show();
                    //lstvw_Revenue.Clear();
                    pnl_Revenue.Show();

                    mntclndr_Revenue.MaxDate = DateTime.Today;

                }
                else if (panelName == "Rooms")
                {
                    hideAllPanels();
                    pnl_Rooms.Show();
                    
                    // fill the rooms listview within the students panel with a list of students
                    SomerenLogic.Room_Service rmService = new SomerenLogic.Room_Service();
                    List<Room> roomList = rmService.GetRooms();

                    // clear the listview before filling it again
                    listViewRooms.Items.Clear();
                    foreach (SomerenModel.Room r in roomList)
                    {
                        ListViewItem li = new ListViewItem(r.Number.ToString());
                        li.SubItems.Add(r.Capacity.ToString());
                        li.SubItems.Add(r.Type);
                        listViewRooms.Items.Add(li);
                    }
                }
                else if (panelName == "Students")
                {
                    // hide all other panels
                    hideAllPanels();
                    pnl_Students.Show();



                    // fill the students listview within the students panel with a list of students
                    SomerenLogic.Student_Service studService = new SomerenLogic.Student_Service();
                    studentList = studService.GetStudents();

                    // clear the listview before filling it again
                    listViewStudents.Items.Clear();
                    fillListViewStudents(studentList);
                }
                else if (panelName == "Teachers")
                {
                    hideAllPanels();
                    pnl_Teachers.Show();


                    Teacher_Service lecService = new Teacher_Service();
                    List<Teacher> lectList = lecService.GetTeachers();

                    listViewTeachers.Items.Clear();
                    foreach (Teacher t in lectList)
                    {
                        ListViewItem item = new ListViewItem(t.Number.ToString());
                        item.SubItems.Add(t.Name);
                        listViewTeachers.Items.Add(item);
                    }
                }
                else if (panelName == "Activities")
                {
                    hideAllPanels();
                    pnl_Activities.Show();

                    ResetModifyActivity();
                }
                else if (panelName == "RoomLayout")
                {
                    hideAllPanels();
                    pnl_kamerindeling.Show();

                    // fill the Combobox within the kamerindeling panel with a list of the roomnumbers
                    SomerenLogic.Student_Service indelingService = new SomerenLogic.Student_Service();
                    List<Student> kamers = indelingService.GetKamers();
                    foreach (Student kamer in kamers)
                    {
                        comboBoxKamerindeling.Items.Add(kamer.RoomNumber);
                    }
                }
                else if (panelName == "Stock")
                {
                    hideAllPanels();
                    pnl_Stock.Show();

                    Stock_Service stockService = new Stock_Service();
                    List<Stock> stockList = stockService.GetStocksCondition();
                    stockList = stockList.OrderBy(stockpile => stockpile.Name).ToList();
                    listViewStock.Items.Clear();
                    foreach (Stock stock in stockList)
                    {
                        ListViewItem item = new ListViewItem(stock.Name.ToString());

                        // ENOUGHT IN STOCK?
                        string icon;
                        if (stock.Stockpile >= 10)
                        {
                            icon = "✔ ";
                        }
                        else
                        {
                            icon = "❌ ";
                        }

                        item.SubItems.Add(icon + stock.Stockpile.ToString());
                        item.SubItems.Add(stock.Sellprice.ToString());
                        listViewStock.Items.Add(item);
                    }
                }
                else if (panelName == "ModifyStock")
                {
                    hideAllPanels();
                    pnl_ModifyStock.Show();


                    ResetModifyStock();
                }
                else if (panelName == "Log")
                {
                    hideAllPanels();
                    pnl_Log.Show();

                    listViewLogs.Items.Clear();
                    foreach (Log log in logList)
                    {
                        ListViewItem item = new ListViewItem(log.Date.ToString());
                        item.SubItems.Add(log.Message);
                        item.SubItems.Add(log.Source);
                        item.SubItems.Add(log.Method);
                        item.SubItems.Add(log.Fullname);
                        listViewLogs.Items.Add(item);
                    }
                }

                else if (panelName == "Cash Register")
                {
                    hideAllPanels();
                    pnl_CashRegister.Show();


                    lvDrink.View = View.Details;
                    lvDrink.Columns.Add("ID", 30);
                    lvDrink.Columns.Add("Name");
                    lvDrink.Columns.Add("Kind", 100);
                    lvDrink.Columns.Add("Price");
                    lvDrink.Columns.Add("Amount Left", 75);
                    lvStudent.View = View.Details;
                    lvStudent.Columns.Add("ID", 30);
                    lvStudent.Columns.Add("Name", 100);

                    lvDrink.Items.Clear();
                    Stock_Service stockservice = new Stock_Service();
                    Student_Service studentService = new Student_Service();
                    foreach (Stock stock in stockservice.GetStocks())
                    {
                        //de lijst vullen met alle data in de juiste volgorde.
                        ListViewItem item = new ListViewItem(stock.Number.ToString());
                        item.SubItems.Add(stock.Name);
                        item.SubItems.Add(stock.Kind);
                        item.SubItems.Add((stock.Sellprice * ((decimal)stock.Tax / 100 + 1)).ToString("0.00"));
                        item.SubItems.Add(stock.SellAmounts.ToString());
                        lvDrink.Items.Add(item);
                        //"\u20AC " + 
                    }
                    lvDrink.Select();

                    lvStudent.Items.Clear();
                    foreach (Student student in studentService.GetStudents())
                    {
                        ListViewItem item = new ListViewItem(student.Number.ToString());
                        item.SubItems.Add(student.Name);
                        lvStudent.Items.Add(item);
                    }
                    lvStudent.Select();
                }
                else if (panelName == "VAT Calculation")
                {
                    hideAllPanels();
                    pnl_BTWBerekenen.Show();
                }
            }
            catch (Exception exception)
            {
                Catch(exception);
            }


        }

        private void hideAllPanels()
        {
            pnl_Activities.Hide();
            pnl_BTWBerekenen.Hide();
            pnl_CashRegister.Hide();
            pnl_Dashboard.Hide();
            pnl_kamerindeling.Hide();
            pnl_Log.Hide();
            pnl_ModifyStock.Hide();
            pnl_Revenue.Hide();
            pnl_Rooms.Hide();
            pnl_Stock.Hide();
            pnl_Students.Hide();
            pnl_Teachers.Hide();
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void img_Dashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Students");
        }

        private void teachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Teachers");
        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Rooms");
        }

        private void RoomsLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("RoomLayout");
        }

        //DRINKS
        private void StockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Stock");
        }

        private void modifyStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("ModifyStock");
        }

        private void LogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Log");
        }

        private void cashRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Cash Register");
        }

        private void revenueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Revenue");
        }


        private void ListViewStudents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (e.Column)
            {
                case 0:
                    {
                        if (reedsGeklikt0 == false)
                        {
                            studentList = studentList.OrderBy(student => student.Number).ToList();
                        }
                        else
                        {
                            studentList.Reverse(0, studentList.Count);
                        }
                        reedsGeklikt0 = !reedsGeklikt0;
                        break;
                    }
                case 1:
                    {
                        if (reedsGeklikt1 == false)
                        {
                            studentList = studentList.OrderBy(student => student.Name).ToList();
                        }
                        else
                        {
                            studentList.Reverse(0, studentList.Count);
                        }
                        reedsGeklikt1 = !reedsGeklikt1;
                        break;
                    }
                case 2:
                    {
                        if (reedsGeklikt2 == false)
                        {
                            studentList = studentList.OrderBy(student => student.StudentNationality).ToList();
                        }
                        else
                        {
                            studentList.Reverse(0, studentList.Count);
                        }
                        reedsGeklikt2 = !reedsGeklikt2;
                        break;
                    }
                case 3:
                    {
                        if (reedsGeklikt3 == false)
                        {
                            studentList = studentList.OrderBy(student => student.RoomNumber).ToList();
                        }
                        else
                        {
                            studentList.Reverse(0, studentList.Count);
                        }
                        reedsGeklikt3 = !reedsGeklikt3;
                        break;
                    }
            }

            fillListViewStudents(studentList);
        }

        private void fillListViewStudents(List<Student> studentList)
        {
            // clear the listview before filling it again
            listViewStudents.Items.Clear();
            foreach (SomerenModel.Student s in studentList)
            {
                ListViewItem item = new ListViewItem(s.Number.ToString());
                item.SubItems.Add(s.Name);
                item.SubItems.Add(s.StudentNationality);
                item.SubItems.Add(s.RoomNumber.ToString());
                listViewStudents.Items.Add(item);
            }
        }

        private void LinkSomeren_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkSomeren.LinkVisited = true;

            // Navigate to the given url.
            System.Diagnostics.Process.Start("https://www.someren.nl");
        }

        private void comboBoxKamerindeling_SelectedIndexChanged(object sender, EventArgs e)
        {
            SomerenLogic.Student_Service student_Service = new Student_Service();
            Student kamernummer = new Student();
            //Get roomnumber from combobox
            kamernummer.RoomNumber = int.Parse(comboBoxKamerindeling.SelectedItem.ToString());
            //Fill dataGridView with Studentnames
            List<Student> students = student_Service.GetStudentsByRoom(kamernummer.RoomNumber);
            dataGridViewKamerindeling.DataSource = students;
            //Hide columns except StudentName
            dataGridViewKamerindeling.Columns[1].Visible = false;
            dataGridViewKamerindeling.Columns[2].Visible = false;
            dataGridViewKamerindeling.Columns[3].Visible = false;
            dataGridViewKamerindeling.Columns[4].Visible = false;
            dataGridViewKamerindeling.Columns[5].Visible = false;
            dataGridViewKamerindeling.Columns[6].Visible = false;
        }

        // ADD DRINKS
        private void BntAddDrinks_Click(object sender, EventArgs e)
        {
            try
            {
                lblModifyStockLogOutput.Text = string.Empty;
                Stock_Service stock_Service = new Stock_Service();
                int VoorraadAantal = int.Parse(txtModifyDrinksStock.Text);
                string DrankNaam = txtModifyDrinksName.Text;
                if (DrankNaam == string.Empty)
                {
                    throw new Exception("The name may not be empty");
                }
                string DrankSoort;
                if (cbModifyDrinksAlcohol.Checked)
                    DrankSoort = "Alcoholisch";
                else
                    DrankSoort = "Non-Alcoholisch";
                int DrankPrijs = tbModifyDrinksSellprice.Value;
                int DrankBTW = int.Parse(cbModifyDrinksBTW.Text);
                int VerkoopAantal = int.Parse(txtModifyDrinksSoled.Text);
                // CHECK IF NAME EXCISTS
                List<Stock> stockpiles = stock_Service.GetStocks();
                stockpiles = stockpiles.OrderBy(stockpile => stockpile.Name).ToList();
                foreach (Stock stockpile in stockpiles)
                {
                    if (stockpile.Name == DrankNaam)
                    {
                        throw new Exception("The name of the drink already excists.");
                    }
                }
                stock_Service.AddToStock(VoorraadAantal, DrankNaam, DrankSoort, DrankPrijs, DrankBTW, VerkoopAantal);
                lblModifyStockLogOutput.Text = $"{DrankNaam} has been added to the database";
                ResetModifyStock();
            }
            catch (Exception exception)
            {
                Catch(exception);
                lblModifyStockLogOutput.Text += exception.Message;
            }
        }

        // CHANGE DRINKS
        private void BtnChangeDrinks_Click(object sender, EventArgs e)
        {
            try
            {
                lblModifyStockLogOutput.Text = string.Empty;
                Stock_Service stock_Service = new Stock_Service();
                string DrinkName = cbSelectDrinksName.SelectedItem.ToString();
                int VoorraadAantal = int.Parse(txtModifyDrinksStock.Text);
                string DrankNaam = txtModifyDrinksName.Text;
                if (DrankNaam == string.Empty)
                {
                    throw new Exception($"The name may not be empty");
                }
                string DrankSoort;
                if (cbModifyDrinksAlcohol.Checked == true)
                    DrankSoort = "Alcoholisch";
                else
                    DrankSoort = "Non-Alcoholisch";
                int DrankPrijs = tbModifyDrinksSellprice.Value;
                int VerkoopAantal = int.Parse(txtModifyDrinksSoled.Text);

                stock_Service.ChangeStock(VoorraadAantal, DrankNaam, DrankSoort, DrankPrijs, VerkoopAantal, DrinkName);
                lblModifyStockLogOutput.Text = $"{DrankNaam} has been updated in the database";
                ResetModifyStock();
            }
            catch (Exception exception)
            {
                Catch(exception);
                lblModifyStockLogOutput.Text += exception.Message;
            }
        }

        private void BtnDeleteDrinks_Click(object sender, EventArgs e)
        {
            try
            {
                lblModifyStockLogOutput.Text = string.Empty;
                if (cbSelectDrinksName.SelectedIndex == -1)
                {
                    throw new Exception($"You have not selected a drink ");
                }
                if (cbIAmSure.Checked)
                {
                    Stock_Service stock_Service = new Stock_Service();
                    List<Stock> stockpiles = stock_Service.GetStocks();
                    stockpiles = stockpiles.OrderBy(stockpile => stockpile.Name).ToList();
                    string DrinkName = cbSelectDrinksName.Text;
                    int Id = stock_Service.GetIdByName(DrinkName);
                    stock_Service.DeleteStock(DrinkName, Id);
                    lblModifyStockLogOutput.Text = $"{DrinkName} has been deleted in the database ";
                    ResetModifyStock();
                }
                else
                {
                    throw new Exception($"You must agree to delete this drink ");
                }
            }
            catch (Exception exception)
            {
                Catch(exception);
                lblModifyStockLogOutput.Text += exception.Message;
            }
        }

        public void ResetModifyStock()
        {
            try
            {
                //ClEAR ALL INFO
                txtModifyDrinksName.Clear();
                txtModifyDrinksSoled.Clear();
                txtModifyDrinksStock.Clear();
                cbModifyDrinksAlcohol.Checked = false;
                cbSelectDrinksName.Items.Clear();
                tbModifyDrinksSellprice.Value = 1;

                cbModifyDrinksBTW.Items.Clear();
                cbModifyDrinksBTW.DropDownStyle = ComboBoxStyle.DropDownList;
                cbModifyDrinksBTW.Items.Add(9);
                cbModifyDrinksBTW.Items.Add(21);

                cbSelectDrinksName.DropDownStyle = ComboBoxStyle.DropDownList;
                SomerenLogic.Stock_Service stock_Service = new SomerenLogic.Stock_Service();
                List<Stock> stockpiles = stock_Service.GetStocks();
                stockpiles = stockpiles.OrderBy(stockpile => stockpile.Name).ToList();
                foreach (Stock stockpile in stockpiles)
                {
                    cbSelectDrinksName.Items.Add(stockpile.Name);
                }
                cbIAmSure.Checked = false;
            }
            catch (Exception exception)
            {
                Catch(exception);
                lblModifyStockLogOutput.Text += exception.Message;
            }
        }
        private void CbSelectDrinksName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedStockValue = cbSelectDrinksName.SelectedIndex;
            // STANDARD
            Stock_Service stock_Service = new Stock_Service();
            List<Stock> stockpiles = stock_Service.GetStocks();
            stockpiles = stockpiles.OrderBy(stockpile => stockpile.Name).ToList();
            txtModifyDrinksName.Text = stockpiles[selectedStockValue].Name; //name
            if (stockpiles[selectedStockValue].Kind == "Alcoholisch") //kind
                cbModifyDrinksAlcohol.Checked = true;
            else
                cbModifyDrinksAlcohol.Checked = false;
            tbModifyDrinksSellprice.Value = stockpiles[selectedStockValue].Sellprice; //sellprice
            if (stockpiles[selectedStockValue].Tax == 21) //tax
                cbModifyDrinksBTW.SelectedIndex = 1;
            else
                cbModifyDrinksBTW.SelectedIndex = 0;
            txtModifyDrinksSoled.Text = stockpiles[selectedStockValue].SellAmounts.ToString(); // amounts solled
            txtModifyDrinksStock.Text = stockpiles[selectedStockValue].Stockpile.ToString(); // stock
        }
        public void Catch(Exception exception)
        {
            Log log = new Log()
            {
                Date = DateTime.Now,
                Message = exception.Message,
                Source = exception.Source,
                Method = exception.TargetSite.Name,
                Fullname = exception.TargetSite.DeclaringType.FullName.ToString(),
            };
            logList.Add(log);
        }

        private void btnCashRegister_Click(object sender, EventArgs e)
        {
            try
            {
                int Hoeveelheid = Convert.ToInt32(txtbAmount.Text);
                string DrankNummer = lvDrink.SelectedItems[0].SubItems[0].Text;
                string DrankNaam = lvDrink.SelectedItems[0].SubItems[1].Text;
                string DrankSoort = lvDrink.SelectedItems[0].SubItems[2].Text;
                string DrankPrijs = lvDrink.SelectedItems[0].SubItems[3].Text;
                string StudentNummer = lvStudent.SelectedItems[0].SubItems[0].Text;
                decimal TotaalPrijs = Convert.ToDecimal(DrankPrijs) * Hoeveelheid;

                var askConfirm = MessageBox.Show("Total Price: " + TotaalPrijs + "\n \n Are you sure?", "Confirmation", MessageBoxButtons.YesNo);

                if (askConfirm == DialogResult.Yes)
                {
                    SqlConnection sc = new SqlConnection("Data Source=den1.mssql7.gear.host; Initial Catalog = pdb1920nl6;User ID = pdb1920nl6;Password = Bp8mM_OGZg?Z");
                    sc.Open();

                    string query = "INSERT INTO Bestelling (BestelDatum, StudentNummer, DrankNummer, DrankAantal, TotaalPrijs) VALUES (@BestelDatum, @StudentNummer, @DrankNummer, @DrankAantal, @TotaalPrijs)";

                    using (SqlCommand command = new SqlCommand(query, sc))
                    {
                        command.Parameters.AddWithValue("@BestelDatum", SqlDbType.DateTime).Value = DateTime.Today;
                        command.Parameters.AddWithValue("@StudentNummer", SqlDbType.Int).Value = StudentNummer;
                        command.Parameters.AddWithValue("@DrankNummer", SqlDbType.Int).Value = DrankNummer;
                        command.Parameters.AddWithValue("@DrankAantal", SqlDbType.Int).Value = Hoeveelheid;
                        command.Parameters.AddWithValue("@TotaalPrijs", SqlDbType.Decimal).Value = TotaalPrijs;

                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Success!");
                    sc.Close();
                    txtbAmount.Text = string.Empty;
                }
            }
            catch (Exception exception)
            {
                Catch(exception);
            }
        }

        private void vATCalculationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("VAT Calculation");
        }

        private void rBTN_Q1_CheckedChanged(object sender, EventArgs e)
        {
            if (rBTN_Q1.Checked)
            {
                SomerenLogic.VAT_Service vat_Service = new SomerenLogic.VAT_Service();
                List<VAT> allVAT = vat_Service.GetVAT();
                lbl_KwartLooptVanUitvoer.Text = "1 January, 2020";
                lbl_KwartLooptTotUitvoer.Text = "31 March, 2020";
                double totalLowVAT = 0;
                double totalHighVAT = 0;
                foreach (VAT vat in allVAT)
                {
                    if (vat.Besteldatum >= DateTime.Parse("1 January, 2020") && vat.Besteldatum <= DateTime.Parse("31 March, 2020"))
                    {
                        BerekenBTW(vat, ref totalLowVAT, ref totalHighVAT);
                    }
                }
                ToonBTW(totalLowVAT, totalHighVAT);
            }
        }

        private void rBTN_Q2_CheckedChanged(object sender, EventArgs e)
        {
            if (rBTN_Q2.Checked)
            {
                SomerenLogic.VAT_Service vat_Service = new SomerenLogic.VAT_Service();
                List<VAT> allVAT = vat_Service.GetVAT();
                lbl_KwartLooptVanUitvoer.Text = "1 April, 2020";
                lbl_KwartLooptTotUitvoer.Text = "30 June, 2020";
                double totalLowVAT = 0;
                double totalHighVAT = 0;
                foreach (VAT vat in allVAT)
                {
                    if (vat.Besteldatum >= DateTime.Parse("1 April, 2020") && vat.Besteldatum <= DateTime.Parse("30 June, 2020"))
                    {
                        BerekenBTW(vat, ref totalLowVAT, ref totalHighVAT);
                    }
                }
                ToonBTW(totalLowVAT, totalHighVAT);
            }
        }

        private void rBTN_Q3_CheckedChanged(object sender, EventArgs e)
        {
            if (rBTN_Q3.Checked)
            {
                SomerenLogic.VAT_Service vat_Service = new SomerenLogic.VAT_Service();
                List<VAT> allVAT = vat_Service.GetVAT();
                lbl_KwartLooptVanUitvoer.Text = "1 July, 2020";
                lbl_KwartLooptTotUitvoer.Text = "30 September, 2020";
                double totalLowVAT = 0;
                double totalHighVAT = 0;
                foreach (VAT vat in allVAT)
                {
                    if (vat.Besteldatum >= DateTime.Parse("1 July, 2020") && vat.Besteldatum <= DateTime.Parse("30 September, 2020"))
                    {
                        BerekenBTW(vat, ref totalLowVAT, ref totalHighVAT);
                    }
                }
                ToonBTW(totalLowVAT, totalHighVAT);
            }
        }

        private void rBTN_Q4_CheckedChanged(object sender, EventArgs e)
        {
            if (rBTN_Q4.Checked)
            {
                SomerenLogic.VAT_Service vat_Service = new SomerenLogic.VAT_Service();
                List<VAT> allVAT = vat_Service.GetVAT();
                lbl_KwartLooptVanUitvoer.Text = "1 October, 2020";
                lbl_KwartLooptTotUitvoer.Text = "31 December, 2020";
                double totalLowVAT = 0;
                double totalHighVAT = 0;
                foreach (VAT vat in allVAT)
                {
                    if (vat.Besteldatum >= DateTime.Parse("1 October, 2020") && vat.Besteldatum <= DateTime.Parse("30 December, 2020"))
                    {
                        BerekenBTW(vat, ref totalLowVAT, ref totalHighVAT);
                    }
                }
                ToonBTW(totalLowVAT, totalHighVAT);
            }
        }
        private void BerekenBTW(VAT vat, ref double totalLowVAT, ref double totalHighVAT)
        {
            if (vat.drankBTW == 6)
            {
                totalLowVAT += vat.BTW;
            }
            else
            {
                totalHighVAT += vat.BTW;
            }
        }
        private void ToonBTW(double totalLowVAT, double totalHighVAT)
        {
            lbl_AfdrachtLaagTariefUitvoer.Text = totalLowVAT.ToString("0.00");
            lbl_AfdrachtHoogTariefUitvoer.Text = totalHighVAT.ToString("0.00");
            lbl_TotaleAfdrachtUitvoer.Text = (totalHighVAT + totalLowVAT).ToString("0.00");
        }

        private void mntclndr_Revenue_DateChanged(object sender, DateRangeEventArgs e)
        {
            lstvw_Revenue.Items.Clear();
            SelectionRange selectie = mntclndr_Revenue.SelectionRange;


            DateTime begindatum = selectie.Start;
            DateTime einddatum = selectie.End;

            List<Revenue> info = null;

            if (begindatum > DateTime.Today || einddatum > DateTime.Today)
            {
                ListViewItem item = new ListViewItem("NO");
                item.SubItems.Add("RECORDS");
                item.SubItems.Add("AVAILABLE");
                lstvw_Revenue.Items.Add(item);
            }
            else
            {
                string begindatumstring = begindatum.ToString("yyyy-MM-dd");
                string einddatumstring = einddatum.ToString("yyyy-MM-dd");

                string[] datumrange = new string[2];
                datumrange[0] = begindatumstring;
                datumrange[1] = einddatumstring;

                Revenue_Service revenue_Service = new Revenue_Service();

                info = revenue_Service.GetRevenueSellingCount(datumrange);

                foreach (SomerenModel.Revenue r in info)
                {
                    ListViewItem item = new ListViewItem(r.Afzet.ToString() + " x");
                    item.SubItems.Add(r.Omzet.ToString("C"));
                    item.SubItems.Add(r.AantalBestellingen.ToString() + " x");
                    lstvw_Revenue.Items.Add(item);
                }
            }
        }
        //HIER BEGINT SEGMENT A Week 4
        private void ActivitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Activities");
        }

        private void ChangeAndAddActivity(string changeoradd)
        {
            //NEW GUIDES
            Activity_Service activity_Service = new Activity_Service();
            List<Guide> guideNew = new List<Guide>();
            List<string> guideNamesNew = new List<string>();

            if (cbSelectGuideName1.Text != string.Empty)
            {
                Guide guide = new Guide()
                {
                    GuideName = cbSelectGuideName1.Text,
                    GuideNumber = int.Parse(lblGuideIdOutput1.Text),
                };
                guideNew.Add(guide);
            }
            if (cbSelectGuideName2.Text != string.Empty)
            {
                Guide guide = new Guide()
                {
                    GuideName = cbSelectGuideName2.Text,
                    GuideNumber = int.Parse(lblGuideIdOutput2.Text),
                };
                guideNew.Add(guide);
            }
            if (cbSelectGuideName3.Text != string.Empty)
            {
                Guide guide = new Guide()
                {
                    GuideName = cbSelectGuideName3.Text,
                    GuideNumber = int.Parse(lblGuideIdOutput3.Text),
                };
                guideNew.Add(guide);
            }
            if (cbSelectGuideName4.Text != string.Empty)
            {
                Guide guide = new Guide()
                {
                    GuideName = cbSelectGuideName4.Text,
                    GuideNumber = int.Parse(lblGuideIdOutput4.Text),
                };
                guideNew.Add(guide);
            }

            foreach (Guide guideNameNew in guideNew)
            {
                guideNamesNew.Add(guideNameNew.GuideName);
            }

            bool isUnique = guideNamesNew.Distinct().Count() == guideNamesNew.Count();
            if (isUnique == false)
            {
                throw new Exception($"Not all guidenames are unique ");
            }
            TimeSpan startTime = dtpActivityStartDate.Value.TimeOfDay;
            TimeSpan endTime = dtpActivityEndDate.Value.TimeOfDay;
            DateTime date = dtpActivityDate.Value.Date;
            string activityName = txtActivityName.Text;
            int numberOfStudents = int.Parse(txtNumberOfStudents.Text);
            List<Guide> guideNew2 = new List<Guide>(guideNew);
            List<Guide> guideNew4 = new List<Guide>(guideNew);
            string output = "";

            // check of de naam al wordt gebruikt
            activityList = activity_Service.GetActivities();
            foreach (Activity activityL in activityList)
            {
                if (activityL.ActivityName == activityName)
                {
                    string exc = $"There is already an activity with name {activityName}";
                    lblModifyActivityLogOutput.Text = exc;
                    throw new Exception($"There is already an activity with name {activityName}");
                }
            }


            if (changeoradd == "change")
            {
                guides = GetAllGuidesById(int.Parse(lblActivityIdOutput.Text));
                string activityNameOld = cbSelectActivity.Text;
                int activityId = int.Parse(lblActivityIdOutput.Text);
                //CHANGE ACTIVITY
                //CHECK OLD WANS
                for (int i = 0; i < guides.Count; i++)
                {
                    //als de nieuwe lijst een persoon uit de oude lijst bevat: schrijf de nieuwe info over de oude persoon heen
                    //if (guideNew.Contains(guides[i]))
                    if (guideNew.Any(guiden => guiden.GuideNumber == guides[i].GuideNumber))
                    {
                        activity_Service.ChangeSchedule(activityId, guides[i].GuideNumber, date, startTime, endTime);
                        //Verwijder uit de nieuwe begeleiderlijst de nog te doorlopen begeleider, je hebt hem hier immers al gecheckt.

                        //CHECK WHO THIS IS AND REMOVE FROM NEW
                        Guide guideSelect = guides[i];
                        List<Guide> guideNew3 = new List<Guide>(guideNew2);
                        foreach (Guide guideN in guideNew3)
                        {
                            if (guideN.GuideNumber == guideSelect.GuideNumber)
                                guideNew2.Remove(guideN);
                        }
                    } //als de nieuwe lijst de persoon uit de oude lijst niet bevat: verwijder de persoon uit de oude lijst
                    else
                    {
                        activity_Service.DeleteFromSchedule(guides[i].GuideNumber);
                    }
                }
                for (int i = 0; i < guideNew2.Count; i++)
                {
                    //Hier zitten nu alleen unieke nieuwe waarden in.
                    activity_Service.AddToSchedule(activityId, guideNew2[i].GuideNumber, date, startTime, endTime);
                }

                if (guideNew4.Count == 0)
                {
                    try
                    {
                        activity_Service.DeleteFromSchedule(0);
                    }
                    catch { };
                    activity_Service.AddToSchedule(activityId, 0, date, startTime, endTime);
                }

                activity_Service.ChangeActivity(activityName, numberOfStudents, guideNamesNew.Count, activityNameOld);

                output = $"{activityName} has been changed in the database ";
                ResetModifyActivity();
                lblModifyActivityLogOutput.Text = output;
            }
            else if (changeoradd == "add")
            {
                //ADD ACTIVITY
                activity_Service.AddToActivities(activityName, numberOfStudents, guideNamesNew.Count);
                //ADD GUIDES
                Activity activity = activity_Service.GetActivityByName(activityName);
                if (guideNew2.Count == 0)
                {
                    for (int i = 0; i < guideNew2.Count; i++)
                    {
                        activity_Service.AddToSchedule(activity.ActivityNumber, guideNew2[i].GuideNumber, date, startTime, endTime);
                    }
                }
                else
                {
                    activity_Service.AddToSchedule(activity.ActivityNumber, 0, date, startTime, endTime);
                }

                lblModifyActivityLogOutput.Text = $"{activityName} has been changed in the database ";
                output = $"{activityName} has been added in the database ";
            }
            ResetModifyActivity();
            lblModifyActivityLogOutput.Text = output;
        }

        public List<Guide> GetAllGuidesById(int ActivityNumber)
        {
            Guide_Service guide_Service = new Guide_Service();
            List<Guide> guideList = guide_Service.GetGuidesByActivityId(ActivityNumber);
            guideList = guideList.OrderBy(guide => guide.GuideName).ToList();
            return guideList;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ResetModifyActivity();
        }

        private void fillListViewActivities(List<Activity> activityList)
        {
            // clear the listview before filling it again
            listViewActivities.Items.Clear();
            foreach (SomerenModel.Activity a in activityList)
            {
                ListViewItem item = new ListViewItem(a.ActivityNumber.ToString());
                item.SubItems.Add(a.ActivityName.ToString());
                item.SubItems.Add(a.NumberOfStudents.ToString());
                item.SubItems.Add(a.NumberOfGuides.ToString());
                listViewActivities.Items.Add(item);
            }
        }
        private List<Activity> GetActivities()
        {
            Activity_Service activityService = new Activity_Service();
            List<Activity> activityList = activityService.GetActivities();
            activityList = activityList.OrderBy(activity => activity.ActivityName).ToList();
            return activityList;

        }
        private List<Guide> GetGuideList()
        {
            Guide_Service guide_Service = new Guide_Service();
            List<Guide> guideList = guide_Service.GetGuides();
            guideList = guideList.OrderBy(guide => guide.GuideName).ToList();
            return guideList;
        }

        public void ResetModifyActivity()
        {
            try
            {
                //CLEAR ALL INPUT
                dtpActivityStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                dtpActivityStartDate.CustomFormat = "HH:mm tt";
                dtpActivityStartDate.ShowUpDown = true;

                dtpActivityEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                dtpActivityEndDate.CustomFormat = "HH:mm tt";
                dtpActivityEndDate.ShowUpDown = true;

                cbSelectActivity.Items.Clear();
                cbSelectGuideName1.Items.Clear();
                cbSelectGuideName2.Items.Clear();
                cbSelectGuideName3.Items.Clear();
                cbSelectGuideName4.Items.Clear();

                lblGuideIdOutput1.Text = string.Empty;
                lblGuideIdOutput2.Text = string.Empty;
                lblGuideIdOutput3.Text = string.Empty;
                lblGuideIdOutput4.Text = string.Empty;

                lblActivityIdOutput.Text = string.Empty;
                lblModifyActivityLogOutput.Text = string.Empty;

                cbSelectActivity.DropDownStyle = ComboBoxStyle.DropDownList;
                cbSelectGuideName1.DropDownStyle = ComboBoxStyle.DropDownList;
                cbSelectGuideName2.DropDownStyle = ComboBoxStyle.DropDownList;
                cbSelectGuideName3.DropDownStyle = ComboBoxStyle.DropDownList;
                cbSelectGuideName4.DropDownStyle = ComboBoxStyle.DropDownList;

                txtNumberOfStudents.Text = string.Empty;
                txtActivityName.Text = string.Empty;

                // show all activities
                List<Activity> activityList = GetActivities();
                listViewActivities.Items.Clear();
                foreach (Activity activity in activityList)
                {
                    ListViewItem item = new ListViewItem(activity.ActivityNumber.ToString());
                    item.SubItems.Add(activity.ActivityName);
                    item.SubItems.Add(activity.NumberOfStudents.ToString());
                    item.SubItems.Add(activity.NumberOfGuides.ToString());
                    listViewActivities.Items.Add(item);

                    // cb select activity fill
                    cbSelectActivity.Items.Add(activity.ActivityName);
                }

                List<Guide> guideList = GetGuideList();
                foreach (Guide guide in guideList)
                {
                    cbSelectGuideName1.Items.Add(guide.GuideName);
                    cbSelectGuideName2.Items.Add(guide.GuideName);
                    cbSelectGuideName3.Items.Add(guide.GuideName);
                    cbSelectGuideName4.Items.Add(guide.GuideName);
                }
            }
            catch (Exception exception)
            {
                Catch(exception);
                lblModifyActivityLogOutput.Text = exception.Message;
            }
        }

        private void BtnRefresh_Click_1(object sender, EventArgs e)
        {
            ResetModifyActivity();
        }
    }
}
