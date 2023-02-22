using Juan_Pierre_Sizanani_Test.Pages.Contractors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Juan_Pierre_Sizanani_Test.Pages.Vehicles
{
    public class VehicleModel : PageModel
    {

        public List<Vehicle> listVehicles = new List<Vehicle>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=dbSizanani;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select * FROM tblVehicle";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Vehicle vehicle = new Vehicle();
                                vehicle.id = reader.GetInt32(0);
                                vehicle.type = reader.GetString(1);
                                vehicle.registration = reader.GetString(2);
                                vehicle.model = reader.GetString(3);
                                vehicle.weight = reader.GetString(4);
                                vehicle.contractorID = reader.GetInt32(4);

                                listVehicles.Add(vehicle);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class Vehicle
    {
        public int id { get; set; }
        public string type { get; set; }
        public string registration { get; set; }
        public string model { get; set; }
        public string weight { get; set; }
        public int contractorID { get; set; }
    }
}
