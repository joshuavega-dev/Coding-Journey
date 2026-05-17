using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Terminal_Assessment
{
    public partial class Form1 : Form
    {
        // Line 12: Fixed! TrustServerCertificate has no spaces now so SQL won't crash
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ContactDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
        private int selectedContactID = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // =================================================================
        // 1. INITIALIZATION (Runs right when the window opens)
        // =================================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContactData();
        }

        // =================================================================
        // 2. DATABASE READ OPERATION (Loads data into the DataGridView)
        // =================================================================
        private void LoadContactData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Contacts";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvContacts.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =================================================================
        // 3. DATABASE INSERT OPERATION (Add Button Click Event)
        // =================================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a contact name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Contacts (ContactName, Email, Phone) VALUES (@Name, @Email, @Phone)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Contact saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadContactData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =================================================================
        // 4. DATABASE UPDATE OPERATION (Update Button Click Event)
        // =================================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedContactID == 0)
            {
                MessageBox.Show("Please select a record from the grid to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Contacts SET ContactName = @Name, Email = @Email, Phone = @Phone WHERE ContactID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", selectedContactID);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Contact updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadContactData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =================================================================
        // 5. DATABASE DELETE OPERATION (Delete Button Click Event)
        // =================================================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedContactID == 0)
            {
                MessageBox.Show("Please select a record from the grid to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this contact?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Contacts WHERE ContactID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", selectedContactID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Contact deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadContactData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =================================================================
        // 6. UI SELECTION OPERATION (Populates fields when you click a grid item)
        // =================================================================
        private void dgvContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvContacts.Rows[e.RowIndex];

                if (row.Cells["ContactID"].Value != DBNull.Value && row.Cells["ContactID"].Value != null)
                {
                    selectedContactID = Convert.ToInt32(row.Cells["ContactID"].Value);
                    txtName.Text = row.Cells["ContactName"].Value?.ToString();
                    txtEmail.Text = row.Cells["Email"].Value?.ToString();
                    txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                }
            }
        }

        // =================================================================
        // 7. RESET FUNCTION (Clears your form text values)
        // =================================================================
        private void ClearInputs()
        {
            selectedContactID = 0;
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
    }
}