using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Juan_Pierre_Sizanani_Test.Pages.Contractors
{
    public class CreateModel : PageModel
    {
        public Contractor contractor = new Contractor();
        public String errorMessage = "";
        public String succsessMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            contractor.name = Request.Form["name"];
            contractor.email = Request.Form["email"];
            contractor.phone = Request.Form["phone"];

            if (contractor.name.Length == 0 || contractor.email.Length == 0 || contractor.phone.Length == 0)
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
                    String query = "INSERT INTO tblContractor" +
                                    "(name, email, phone) VALUES" +
                                    "(@name, @email, @phone);";

                    using (SqlCommand com = new SqlCommand(query, conn))
                    {
                        com.Parameters.AddWithValue("@name", contractor.name);
                        com.Parameters.AddWithValue("@email", contractor.email);
                        com.Parameters.AddWithValue("@phone", contractor.phone);

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            contractor.name = "";
            contractor.email = "";
            contractor.phone = "";
            succsessMessage = "New Client Added!";

            Response.Redirect("/Contractors/Contractor");

        }
    }
}
