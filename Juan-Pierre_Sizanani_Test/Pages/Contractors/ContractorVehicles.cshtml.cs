using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Juan_Pierre_Sizanani_Test.Pages.Contractors
{
    public class ContractorVehiclesModel : PageModel
    {
        public List<ContractorVehicles> listContractorsVehicles = new List<ContractorVehicles>();
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=dbSizanani;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select * FROM tblVehicle WHERE contractorID = @id";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContractorVehicles cv = new ContractorVehicles();
                                cv.type = reader.GetInt32(0);
                                cv.registration = reader.GetString(1);
                                cv.model = reader.GetString(2);
                                cv.weight = reader.GetString(3);

                                listContractorsVehicles.Add(cv);
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

    public class ContractorVehicles
    {
        public int type { get; set; }
        public string registration { get; set; }
        public string model { get; set; }
        public string weight { get; set; }

        public int contractorID { get; set; }
    }
}
