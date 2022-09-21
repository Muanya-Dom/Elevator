using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ElevatorSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            displayCP.Image = picGroundFloor;
            displayFL.Image = picGroundFloor;
            displayGL.Image = picGroundFloor;
        }

        //lift currently on the ground floor
        int liftCurrentFloor = 0;

        // lift doors currently closed
        bool isFFDoorOpen = false;
        bool isGFDoorOpen = false;


        // lift image and gif variables
        Image gifArrowUp = Image.FromFile(@".\images\up.gif");
        Image gifArrowDown = Image.FromFile(@".\images\down.gif");
        Image picFirstFloor = Image.FromFile(@".\images\1.png");
        Image picGroundFloor = Image.FromFile(@".\images\G.png");



        //
        // Buttons
        //
        private void btnFFCallLift_Click(object sender, EventArgs e)
        {
            // validation
            if (tmrLiftGoingUp.Enabled == true || tmrLiftGoingDown.Enabled == true ||
                tmrLiftDoorsOpen.Enabled == true || tmrLiftMoveAfterDoorClose.Enabled == true)
            {
                return;
            }

            // open first floor doors when current floor is 1
            if (liftCurrentFloor == 1 && isFFDoorOpen == false)
            {
                tmrLiftDoorsOpen.Start();
                tmrDoorsCloseAutomatically.Start();
            }


            // moving lift to the first floor
            if (liftCurrentFloor == 0 && isGFDoorOpen == true)
            {
                tmrLiftDoorsClose.Start();
                tmrLiftMoveAfterDoorClose.Start();

                displayCP.Image = gifArrowUp;
                displayFL.Image = gifArrowUp;
                displayGL.Image = gifArrowUp;
                databaseInsert("Going Up");
            }
            else if (liftCurrentFloor == 0 && isGFDoorOpen == false)
            {
                tmrLiftGoingUp.Start();

                displayCP.Image = gifArrowUp;
                displayFL.Image = gifArrowUp;
                displayGL.Image = gifArrowUp;
                databaseInsert("Going Up");
            }
            btnFFCallLift.ForeColor = Color.Red;

        }

        private void btnGFCallLift_Click(object sender, EventArgs e)
        {
            // validation
            if (tmrLiftGoingUp.Enabled == true || tmrLiftGoingDown.Enabled == true ||
               tmrLiftDoorsOpen.Enabled == true || tmrLiftMoveAfterDoorClose.Enabled == true)
            {
                return;
            }


            // open ground floor doors when current floor is 0
            if (liftCurrentFloor == 0 && isGFDoorOpen == false)
            {
                tmrLiftDoorsOpen.Start();
                tmrDoorsCloseAutomatically.Start();
            }

            // moving lift to the ground floor
            if (liftCurrentFloor == 1 && isFFDoorOpen == true)
            {
                tmrLiftDoorsClose.Start();
                tmrLiftMoveAfterDoorClose.Start();

                displayCP.Image = gifArrowDown;
                displayFL.Image = gifArrowDown;
                displayGL.Image = gifArrowDown;
                databaseInsert("Going Down");

            }
            else if (liftCurrentFloor == 1 && isFFDoorOpen == false)
            {
                tmrLiftGoingDown.Start();

                displayCP.Image = gifArrowDown;
                displayFL.Image = gifArrowDown;
                displayGL.Image = gifArrowDown;
                databaseInsert("Going Down");
            }
            btnGFCallLift.ForeColor = Color.Red;

        }

        private void btnGoingUp_FirstFloor_Click(object sender, EventArgs e)
        {

            // validation
            if (tmrLiftGoingUp.Enabled == true || tmrLiftGoingDown.Enabled == true ||
                tmrLiftDoorsOpen.Enabled == true || tmrLiftMoveAfterDoorClose.Enabled == true)
            {
                return;
            }

            // open first floor doors when current floor is 1
            if (liftCurrentFloor == 1 && isFFDoorOpen == false)
            {
                tmrLiftDoorsOpen.Start();
                tmrDoorsCloseAutomatically.Start();
            }


            // moving lift to the first floor
            if (liftCurrentFloor == 0 && isGFDoorOpen == true)
            {
                tmrLiftDoorsClose.Start();
                tmrLiftMoveAfterDoorClose.Start();

                displayCP.Image = gifArrowUp;
                displayFL.Image = gifArrowUp;
                displayGL.Image = gifArrowUp;
                databaseInsert("Going Up");

            }
            else if (liftCurrentFloor == 0 && isGFDoorOpen == false)
            {
                tmrLiftGoingUp.Start();

                displayCP.Image = gifArrowUp;
                displayFL.Image = gifArrowUp;
                displayGL.Image = gifArrowUp;
                databaseInsert("Going Up");
            }
            btnGoingUp_FirstFloor.ForeColor = Color.Red;

        }

        private void btnGoingDown_GroundFloor_Click(object sender, EventArgs e)
        {

            // validation
            if (tmrLiftGoingUp.Enabled == true || tmrLiftGoingDown.Enabled == true ||
               tmrLiftDoorsOpen.Enabled == true || tmrLiftMoveAfterDoorClose.Enabled == true)
            {
                return;
            }


            // open ground floor doors when current floor is 0
            if (liftCurrentFloor == 0 && isGFDoorOpen == false)
            {
                tmrLiftDoorsClose.Stop();
                tmrLiftDoorsOpen.Start();
                tmrDoorsCloseAutomatically.Start();
            }

            // moving lift to the ground floor
            if (liftCurrentFloor == 1 && isFFDoorOpen == true)
            {
                tmrLiftDoorsClose.Start();

                tmrLiftMoveAfterDoorClose.Start();

                displayCP.Image = gifArrowDown;
                displayFL.Image = gifArrowDown;
                displayGL.Image = gifArrowDown;
                databaseInsert("Going Down");

            }
            else if (liftCurrentFloor == 1 && isFFDoorOpen == false)
            {
                tmrLiftGoingDown.Start();

                displayCP.Image = gifArrowDown;
                displayFL.Image = gifArrowDown;
                displayGL.Image = gifArrowDown;
                databaseInsert("Going Down");
            }
            btnGoingDown_GroundFloor.ForeColor = Color.Red;

        }




        //
        // Timers
        //
        private void tmrLiftGoingUp_Tick(object sender, EventArgs e)
        {
            // lift going up
            if (movingLift.Top != 53)
            {
                movingLift.Top -= 1;

            }
            else
            {
                tmrLiftGoingUp.Stop();
                liftCurrentFloor = 1;

                btnFFCallLift.ForeColor = Color.Black;
                btnGoingUp_FirstFloor.ForeColor = Color.White;

                displayCP.Image = picFirstFloor;
                displayFL.Image = picFirstFloor;
                displayGL.Image = picFirstFloor;

                tmrLiftDoorsOpen.Start();
                tmrDoorsCloseAutomatically.Start();

                tmrLiftMoveAfterDoorClose.Stop();

                databaseInsert("First Floor");
            }


        }

        private void tmrLiftGoingDown_Tick(object sender, EventArgs e)
        {
            // lift going down
            if (movingLift.Top != 374)
            {
                movingLift.Top += 1;
            }
            else
            {
                tmrLiftGoingDown.Stop();
                liftCurrentFloor = 0;

                btnGFCallLift.ForeColor = Color.Black;
                btnGoingDown_GroundFloor.ForeColor = Color.White;

                displayCP.Image = picGroundFloor;
                displayFL.Image = picGroundFloor;
                displayGL.Image = picGroundFloor;

                tmrLiftDoorsOpen.Start();
                tmrDoorsCloseAutomatically.Start();

                tmrLiftMoveAfterDoorClose.Stop();

                databaseInsert("Ground Floor");


            }

        }

        private void tmrLiftDoorsOpen_Tick(object sender, EventArgs e)
        {
            if (liftCurrentFloor == 1)
            {
                // doors open first floor
                if (FLDoorLeft.Left != 605 && FLDoorRight.Left != 818)
                {
                    FLDoorLeft.Left -= 1;
                    FLDoorRight.Left += 1;
                }
                else
                {
                    isFFDoorOpen = true;
                    tmrLiftDoorsOpen.Stop();

                    databaseInsert("Doors Open on First Floor");
                }
            }
            else
            {
                // doors open ground floor
                if (GLDoorLeft.Left != 605 && GLDoorRight.Left != 818)
                {
                    GLDoorLeft.Left -= 1;
                    GLDoorRight.Left += 1;
                }
                else
                {
                    isGFDoorOpen = true;
                    tmrLiftDoorsOpen.Stop();

                    databaseInsert("Doors Open on Ground Floor");
                }
            }
        }

        private void tmrLiftDoorsClose_Tick(object sender, EventArgs e)
        {
            if (liftCurrentFloor == 1)
            {
                // doors close first floor
                if (FLDoorLeft.Left != 676 && FLDoorRight.Left != 748)
                {
                    FLDoorLeft.Left += 1;
                    FLDoorRight.Left -= 1;
                }
                else
                {
                    isFFDoorOpen = false;
                    tmrLiftDoorsClose.Stop();

                    btnFFCallLift.ForeColor = Color.Black;
                    btnGoingUp_FirstFloor.ForeColor = Color.White;

                    tmrDoorsCloseAutomatically.Stop();

                    databaseInsert("Doors Close on First Floor");
                }
            }
            else
            {
                // doors close ground floor
                if (GLDoorLeft.Left != 676 && GLDoorRight.Left != 748)
                {
                    GLDoorLeft.Left += 1;
                    GLDoorRight.Left -= 1;
                }
                else
                {
                    isGFDoorOpen = false;
                    tmrLiftDoorsClose.Stop();

                    btnGFCallLift.ForeColor = Color.Black;
                    btnGoingDown_GroundFloor.ForeColor = Color.White;

                    tmrDoorsCloseAutomatically.Stop();

                    databaseInsert("Doors Close on Ground Floor");

                }
            }
        }

        private void tmrLiftMoveAfterDoorClose_Tick(object sender, EventArgs e)
        {
            // Close doors before lift moves
            if (liftCurrentFloor == 1)
            {
                tmrLiftGoingDown.Start();
            }

            if (liftCurrentFloor == 0)
            {
                tmrLiftGoingUp.Start();
            }
        }

        private void tmrDoorsCloseAutomatically_Tick(object sender, EventArgs e)
        {
            // Close doors automatically
            if (liftCurrentFloor == 1)
            {
                tmrLiftDoorsClose.Start();
            }

            if (liftCurrentFloor == 0)
            {
                tmrLiftDoorsClose.Start();
            }
        }




        // database

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            OleDbConnection con;
            OleDbDataAdapter Adapter;
            DataSet ds = new DataSet();
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=.\ElevatorSystemDatabase.accdb";
            string dbcommand = "Select * from DatabaseLog;";
            con = new OleDbConnection(connectionString);
            OleDbCommand Odbcommand = new OleDbCommand(dbcommand, con);
            Adapter = new OleDbDataAdapter(Odbcommand);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(Adapter);

            try
            {
                // load Database
                Adapter.Fill(ds);
                eleDatabaseLog.Items.Clear();

                //Display in the list box.
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    eleDatabaseLog.Items.Add(row["ActionTime"] + "\t\t" + row["ActionDate"] + "\t\t" + row["ActionExecuted"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void databaseInsert(string status)
        {
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();

            OleDbConnection con;
            OleDbDataAdapter Adapter;
            DataSet ds = new DataSet();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=.\ElevatorSystemDatabase.accdb";
            string dbcommand = "Select * from DatabaseLog;";
            
            con = new OleDbConnection(connectionString);
            OleDbCommand Odbcommand = new OleDbCommand(dbcommand, con);
            Adapter = new OleDbDataAdapter(Odbcommand);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(Adapter);

            Adapter.Fill(ds);

            DataTable dt = ds.Tables[0];
            DataRow newRow = ds.Tables[0].NewRow();

            ds.Tables[0].Rows.Add(newRow);
            //this code add the date/time and the lift events into the database
            newRow["ActionTime"] = date;
            newRow["ActionDate"] = time;
            newRow["ActionExecuted"] = status;
            DataSet dataSetChanges = ds.GetChanges();
            try
            {
                //update DB
                Adapter.Update(dataSetChanges);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            eleDatabaseLog.Items.Add(date + "\t\t" + time + "\t\t" + status);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            eleDatabaseLog.Items.Clear();
        }

    } 
}

