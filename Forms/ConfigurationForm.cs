using HendersonvilleTrafficTest.Configuration;
using System.ComponentModel;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class ConfigurationForm : Form
    {
        private readonly List<ConfigurationProperty> _properties;
        private ConfigurationProperty? _selectedProperty;

        public ConfigurationForm()
        {
            InitializeComponent();
            _properties = ConfigurationPropertyExtractor.ExtractProperties(ConfigurationManager.Current);
            LoadConfigurationProperties();
        }

        private void LoadConfigurationProperties()
        {
            cmbProperties.Items.Clear();
            
            var groupedProperties = _properties.GroupBy(p => p.Category).OrderBy(g => g.Key);
            
            foreach (var group in groupedProperties)
            {
                foreach (var property in group.OrderBy(p => p.DisplayName))
                {
                    var displayText = $"{group.Key} - {property.DisplayName}";
                    cmbProperties.Items.Add(new ComboBoxItem(displayText, property));
                }
            }

            if (cmbProperties.Items.Count > 0)
            {
                cmbProperties.SelectedIndex = 0;
            }
        }

        private void cmbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProperties.SelectedItem is ComboBoxItem item)
            {
                _selectedProperty = item.Property;
                LoadPropertyEditor();
            }
        }

        private void LoadPropertyEditor()
        {
            pnlEditor.Controls.Clear();
            
            if (_selectedProperty == null)
                return;

            lblDescription.Text = _selectedProperty.Description;

            if (_selectedProperty.PropertyType.IsEnum)
            {
                CreateEnumEditor();
            }
            else if (_selectedProperty.PropertyType == typeof(bool))
            {
                CreateBooleanEditor();
            }
            else if (IsNumericType(_selectedProperty.PropertyType))
            {
                CreateNumericEditor();
            }
            else if (_selectedProperty.PropertyType == typeof(string))
            {
                CreateTextEditor();
            }
        }

        private void CreateEnumEditor()
        {
            var comboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };

            var enumValues = Enum.GetValues(_selectedProperty!.PropertyType);
            foreach (var value in enumValues)
            {
                comboBox.Items.Add(value);
            }

            comboBox.SelectedItem = _selectedProperty.Value;
            comboBox.SelectedIndexChanged += (s, e) =>
            {
                _selectedProperty.SetValue(comboBox.SelectedItem);
            };

            pnlEditor.Controls.Add(comboBox);
        }

        private void CreateBooleanEditor()
        {
            var checkBox = new CheckBox
            {
                Dock = DockStyle.Top,
                Text = _selectedProperty!.DisplayName,
                Checked = (bool)(_selectedProperty.Value ?? false),
                Font = new Font("Segoe UI", 10F)
            };

            checkBox.CheckedChanged += (s, e) =>
            {
                _selectedProperty.SetValue(checkBox.Checked);
            };

            pnlEditor.Controls.Add(checkBox);
        }

        private void CreateNumericEditor()
        {
            var numericUpDown = new NumericUpDown
            {
                Dock = DockStyle.Top,
                DecimalPlaces = IsFloatingPointType(_selectedProperty!.PropertyType) ? 2 : 0,
                Minimum = decimal.MinValue,
                Maximum = decimal.MaxValue,
                Font = new Font("Segoe UI", 10F)
            };

            if (_selectedProperty.Value != null)
            {
                numericUpDown.Value = Convert.ToDecimal(_selectedProperty.Value);
            }

            numericUpDown.ValueChanged += (s, e) =>
            {
                var convertedValue = Convert.ChangeType(numericUpDown.Value, _selectedProperty.PropertyType);
                _selectedProperty.SetValue(convertedValue);
            };

            pnlEditor.Controls.Add(numericUpDown);
        }

        private void CreateTextEditor()
        {
            var textBox = new TextBox
            {
                Dock = DockStyle.Top,
                Text = _selectedProperty!.Value?.ToString() ?? "",
                Font = new Font("Segoe UI", 10F)
            };

            textBox.TextChanged += (s, e) =>
            {
                _selectedProperty.SetValue(textBox.Text);
            };

            pnlEditor.Controls.Add(textBox);
        }

        private bool IsNumericType(Type type)
        {
            return type == typeof(int) || type == typeof(double) || type == typeof(float) ||
                   type == typeof(decimal) || type == typeof(long) || type == typeof(short);
        }

        private bool IsFloatingPointType(Type type)
        {
            return type == typeof(double) || type == typeof(float) || type == typeof(decimal);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigurationManager.SaveConfiguration();
                MessageBox.Show("Configuration saved successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save configuration: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ConfigurationManager.ReloadConfiguration();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private class ComboBoxItem
        {
            public string Text { get; }
            public ConfigurationProperty Property { get; }

            public ComboBoxItem(string text, ConfigurationProperty property)
            {
                Text = text;
                Property = property;
            }

            public override string ToString() => Text;
        }
    }
}