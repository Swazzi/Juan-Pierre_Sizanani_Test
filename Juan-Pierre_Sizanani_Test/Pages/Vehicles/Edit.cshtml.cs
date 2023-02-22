using Juan_Pierre_Sizanani_Test.Pages.Contractors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Juan_Pierre_Sizanani_Test.Pages.Vehicles
{
    public class EditModel : PageModel
    {

        public Vehicle vehicle = new Vehicle();
        public String errorMessage = "";
        public String succsessMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=dbSizanani;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select * FROM tblVehicle WHERE id=@id";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        com.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                vehicle.id = reader.GetInt32(0);
                                vehicle.type = reader.GetString(1);
                                vehicle.registration = reader.GetString(2);
                                vehicle.model = reader.GetString(3);
                                vehicle.weight = reader.GetString(4);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            vehicle.type = Request.Form["type"];
            vehicle.registration = Request.Form["registration"];
            vehicle.model = Request.Form["model"];
            vehicle.weight = Request.Form["weight"];
            vehicle.contractorID = Convert.ToInt32(Request.Form["weight"]);

            if (vehicle.type.Length == 0 || vehicle.registration.Length == 0 || vehicle.model.Length == 0 || vehicle.weight.Length == 0 || vehicle.contractorID.Equals(0))
            {
                errorMessage = "All the fields are required!";
                return;
            }

            //Edit new contractor

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=dbSizanani;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String query = "UPDATE tblVehicle SET (type = @type, registration = @registration, model = @model, weight = @weight, contractorID = @contractorID) WHERE id=@id";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        com.Parameters.AddWithValue("@type", vehicle.type);
                        com.Parameters.AddWithValue("@registration", vehicle.registration);
                        com.Parameters.AddWithValue("@model", vehicle.model);
                        com.Parameters.AddWithValue("@weight", vehicle.weight);
                        com.Parameters.AddWithValue("@contractorID", vehicle.contractorID);
                        com.Parameters.AddWithValue("@id", vehicle.id);

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
            vehicle.contractorID= 0;
            succsessMessage = "Vehicle Changed!";

            Response.Redirect("/Vehicles/Vehicle");
        }
    }
}
