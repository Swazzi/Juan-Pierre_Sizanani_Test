using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Juan_Pierre_Sizanani_Test.Pages.Contractors
{
    public class ContractorModel : PageModel
    {
        public List<Contractor> listContractors = new List<Contractor>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=dbSizanani;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select * FROM tblContractor";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contractor contractor = new Contractor();
                                contractor.id = reader.GetInt32(0);
                                contractor.name = reader.GetString(1);
                                contractor.email = reader.GetString(2);
                                contractor.phone = reader.GetString(3);

                                listContractors.Add(contractor);
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

    public class Contractor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
}
