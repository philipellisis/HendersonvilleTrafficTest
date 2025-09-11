using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using HendersonvilleTrafficTest.Configuration;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class UserManagementForm : Form
    {
        private BindingList<UserAccount> _userAccounts;
        private bool _isEditing = false;
        private int _editingIndex = -1;

        public UserManagementForm()
        {
            InitializeComponent();
            InitializeUserTypes();
            LoadUserAccounts();
        }

        private void InitializeUserTypes()
        {
            cmbUserType.Items.Clear();
            cmbUserType.Items.AddRange(Enum.GetNames(typeof(UserType)));
        }

        private void LoadUserAccounts()
        {
            var userAccounts = ConfigurationManager.Current.UserAccounts.GetUserAccounts();
            _userAccounts = new BindingList<UserAccount>(userAccounts);
            
            dgvUsers.DataSource = _userAccounts;
            
            // Configure DataGridView columns
            if (dgvUsers.Columns.Count >= 5)
            {
                dgvUsers.Columns[0].HeaderText = "Username";        // Username
                dgvUsers.Columns[1].Visible = false;                // Password - hide for security
                dgvUsers.Columns[2].HeaderText = "Full Name";       // FullName
                dgvUsers.Columns[3].HeaderText = "User Type";       // UserType
                dgvUsers.Columns[4].HeaderText = "Active";          // IsActive
                // dgvUsers.Columns[4].Width = 80;
            }

            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = dgvUsers.SelectedRows.Count > 0;
            bool isEditing = _isEditing;

            btnEdit.Enabled = hasSelection && !isEditing;
            btnDelete.Enabled = hasSelection && !isEditing;
            btnAdd.Enabled = !isEditing;
            
            btnSave.Enabled = isEditing;
            btnCancel.Enabled = isEditing;
            
            grpUserDetails.Enabled = isEditing;
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
            
            if (dgvUsers.SelectedRows.Count > 0 && !_isEditing)
            {
                var selectedUser = (UserAccount)dgvUsers.SelectedRows[0].DataBoundItem;
                DisplayUserDetails(selectedUser);
            }
        }

        private void DisplayUserDetails(UserAccount user)
        {
            txtUsername.Text = user.Username;
            txtPassword.Text = user.Password;
            txtFullName.Text = user.FullName;
            cmbUserType.SelectedItem = user.UserType.ToString();
            chkIsActive.Checked = user.IsActive;
        }

        private void ClearUserDetails()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            cmbUserType.SelectedIndex = 0;
            chkIsActive.Checked = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isEditing = true;
            _editingIndex = -1;
            ClearUserDetails();
            UpdateButtonStates();
            txtUsername.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                _isEditing = true;
                _editingIndex = dgvUsers.SelectedRows[0].Index;
                var selectedUser = (UserAccount)dgvUsers.SelectedRows[0].DataBoundItem;
                DisplayUserDetails(selectedUser);
                UpdateButtonStates();
                txtUsername.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var selectedUser = (UserAccount)dgvUsers.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show(
                    $"Are you sure you want to delete user '{selectedUser.Username}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _userAccounts.RemoveAt(dgvUsers.SelectedRows[0].Index);
                    SaveUserAccounts();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateUserInput())
                return;

            var userAccount = new UserAccount
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                UserType = (UserType)Enum.Parse(typeof(UserType), cmbUserType.SelectedItem.ToString()),
                IsActive = chkIsActive.Checked
            };

            // Check for duplicate username (excluding current user being edited)
            var existingUser = _userAccounts.Where((u, index) => 
                u.Username.Equals(userAccount.Username, StringComparison.OrdinalIgnoreCase) && 
                index != _editingIndex).FirstOrDefault();

            if (existingUser != null)
            {
                MessageBox.Show("A user with this username already exists.", "Duplicate Username", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (_editingIndex >= 0)
            {
                // Edit existing user
                _userAccounts[_editingIndex] = userAccount;
            }
            else
            {
                // Add new user
                _userAccounts.Add(userAccount);
            }

            SaveUserAccounts();
            CancelEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelEdit();
        }

        private void CancelEdit()
        {
            _isEditing = false;
            _editingIndex = -1;
            ClearUserDetails();
            UpdateButtonStates();
        }

        private bool ValidateUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (cmbUserType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a User Type.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUserType.Focus();
                return false;
            }

            // Check for invalid characters in username (no commas or newlines)
            if (txtUsername.Text.Contains(",") || txtUsername.Text.Contains("\n"))
            {
                MessageBox.Show("Username cannot contain commas or newlines.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Check for invalid characters in password (no commas or newlines)
            if (txtPassword.Text.Contains(",") || txtPassword.Text.Contains("\n"))
            {
                MessageBox.Show("Password cannot contain commas or newlines.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Check for invalid characters in full name (no commas or newlines)
            if (txtFullName.Text.Contains(",") || txtFullName.Text.Contains("\n"))
            {
                MessageBox.Show("Full Name cannot contain commas or newlines.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            return true;
        }

        private void SaveUserAccounts()
        {
            try
            {
                ConfigurationManager.Current.UserAccounts.SaveUserAccounts(_userAccounts.ToList());
                ConfigurationManager.SaveConfiguration();
                MessageBox.Show("User accounts saved successfully.", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isEditing)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Do you want to save them before closing?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    btnSave_Click(this, EventArgs.Empty);
                    if (_isEditing) // Save failed
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }
    }
}