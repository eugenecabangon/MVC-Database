using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDatabase.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;


namespace MVCDatabase.Controllers
{
    public class jobController : Controller
    {
        

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(jobModel job)
        {
            {
                string connectionString = @"server=localhost;user id=root;database=mvcdb";
                MySqlConnection connection = null;


                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO `job info`(`Task Name`, `Description`, `Date Started`, `Date Finished`, `Status`) VALUES (@taskName, @description, @dateStarted, @dateFinished, @status)";

                cmd.Parameters.AddWithValue("@taskName", job.taskName);
                cmd.Parameters.AddWithValue("@description", job.description);
                cmd.Parameters.AddWithValue("@dateStarted", job.dateStarted);
                cmd.Parameters.AddWithValue("@dateFinished", job.dateFinished);
                cmd.Parameters.AddWithValue("@status", job.status);

                cmd.ExecuteNonQuery();

                return View(job);
            }
        }

        public ActionResult List()
        {
            string connectionString = @"server=localhost;user id=root;database=mvcdb";
            string select = "SELECT * FROM `job info`";

            DataTable dtblProduct = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(select, connection);
                da.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }

        //
        [HttpGet]
        public ActionResult Edit(int id)
        {
            string connectionString = @"server=localhost;user id=root;database=mvcdb";
            jobModel job = new jobModel();
            DataTable dtblProduct = new DataTable();


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string select = "SELECT * FROM  `job info` WHERE JobID = @jobID";
                MySqlDataAdapter da = new MySqlDataAdapter(select, connection);
                da.SelectCommand.Parameters.AddWithValue("@jobID", id);
                da.Fill(dtblProduct);

            }

            if (dtblProduct.Rows.Count == 1)
            {
                job.jobID = Convert.ToInt32(dtblProduct.Rows[0][0]);
                job.taskName = dtblProduct.Rows[0][1].ToString();
                job.description = dtblProduct.Rows[0][2].ToString();
                job.dateStarted = dtblProduct.Rows[0][3].ToString();
                job.dateFinished = dtblProduct.Rows[0][4].ToString();
                job.status = dtblProduct.Rows[0][5].ToString();


                System.Diagnostics.Debug.WriteLine(job.jobID);
                System.Diagnostics.Debug.WriteLine(job.taskName);
                System.Diagnostics.Debug.WriteLine(job.description);
                System.Diagnostics.Debug.WriteLine(job.dateStarted);
                System.Diagnostics.Debug.WriteLine(job.dateFinished);
                System.Diagnostics.Debug.WriteLine(job.status);
                return View(job);

            }
            else
            {
                return RedirectToAction("List");
            }




        }

        [HttpPost]
        public ActionResult Edit(jobModel job)
        {
            string connectionString = @"server=localhost;user id=root;database=mvcdb";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string update = "UPDATE `job info` SET `Task Name` =@taskName, Description=@description, `Date Started`=@dateStarted, `Date Finished`=@dateFinished, `Status`=@status WHERE `JobID` = @jobID";
                MySqlCommand cmd = new MySqlCommand(update, connection);
                cmd.Parameters.AddWithValue("@jobID", job.jobID);
                cmd.Parameters.AddWithValue("@taskName", job.taskName);
                cmd.Parameters.AddWithValue("@description", job.description);
                cmd.Parameters.AddWithValue("@dateStarted", job.dateStarted);
                cmd.Parameters.AddWithValue("@dateFinished", job.dateFinished);
                cmd.Parameters.AddWithValue("@status", job.status);


                cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine(job.jobID);
                System.Diagnostics.Debug.WriteLine(job.taskName);
                System.Diagnostics.Debug.WriteLine(job.description);
                System.Diagnostics.Debug.WriteLine(job.dateStarted);
                System.Diagnostics.Debug.WriteLine(job.dateFinished);
                System.Diagnostics.Debug.WriteLine(job.status);



            }
            return RedirectToAction("List");
          

        }

        public ActionResult Delete(int id)
        {
            string connectionString = @"server=localhost;user id=root;database=mvcdb";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string delete = "DELETE FROM `job info` WHere JobID = @jobID";
                MySqlCommand cmd = new MySqlCommand(delete, connection);
                cmd.Parameters.AddWithValue("@jobID", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("List");
        }



    }
}