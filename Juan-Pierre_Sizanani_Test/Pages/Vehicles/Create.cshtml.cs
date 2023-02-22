using Juan_Pierre_Sizanani_Test.Pages.Contractors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace Juan_Pierre_Sizanani_Test.Pages.Vehicles
{
    public class CreateModel : PageModel
    {

        public Vehicle vehicle = new Vehicle();
        public String errorMessage = "";
        public String succsessMessage = "";

        public void OnGet()
        {
        }


        public void OnPost()
        {
            vehicle.type = Request.Form["type"];
            vehicle.registration = Request.Form["registration"];
            vehicle.model = Request.Form["model"];
            vehicle.weight = Request.Form["weight"];
            vehicle.contractorID = Convert.ToInt32(Request.Form["weight"]);

            if (vehicle.type.Length == 0 || vehicle.registration.Length == 0 || vehicle.model.Length == 0 || vehicle.weight.Equals(0) || vehicle.contractorID.Equals(0))
            {
                errorMessage = "All the fields are required!";
                return;
            }

            //save new contractor

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=dbSizanani;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String query = "INSERT INTO tblVehicle" +
                                    "(type, registration, model, weight, contractorID) VALUES" +
                                    "(@type, @registration, @model, @weight, @contractorID);";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        com.Parameters.AddWithValue("@type", vehicle.type);
                        com.Parameters.AddWithValue("@registration", vehicle.registration);
                        com.Parameters.AddWithValue("@model", vehicle.model);
                        com.Parameters.AddWithValue("@weight", vehicle.weight);
                        com.Parameters.AddWithValue("@contractorID", vehicle.contractorID);

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            vehicle.type = "";
            vehicle.registration = "";
            vehicle.model = "";
            vehicle.weight = "";
            vehicle.contractorID = 0;
            succsessMessage = "New Vehicle Added!";

            Response.Redirect("/Vehicles/Vehicle");

        }
    }
}
