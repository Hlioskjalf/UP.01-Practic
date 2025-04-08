using System;
using Zadanie9.Models;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Zadanie9.Forms
{
    public partial class GoodDetailsForm : Form
    {
        private Good _good;
        private List<Category> _categories;

        private BindingSource _bindingSource;
        private int _rowIndex;

        public GoodDetailsForm(List<Category> categories, BindingSource bindingSource)
        {
            InitializeComponent();
            _good = new Good();
            _categories = categories;
            _bindingSource = bindingSource;
            this.Text = "Adding Item";

            InitializeControls();
        }

        public GoodDetailsForm(Good good, List<Category> categories, BindingSource bindingSource, int rowIndex)
        {
            InitializeComponent();
            _good = good;
            _categories = categories;
            _bindingSource = bindingSource;
            _rowIndex = rowIndex;
            this.Text = "Editing Item";

            InitializeControls();
        }

        private void InitializeControls()
        {
            categoryComboBox.DataSource = _categories;
            categoryComboBox.DisplayMember = "CategoryName";
            categoryComboBox.ValueMember = "CategoryId";

            if (_good != null)
            {
                nameTextBox.Text = "";
                priceTextBox.Text = "";
                photoTextBox.Text = "";
                descriptionTextBox.Text = "";
                UpdateGoodPhoto();
            }
        }

        private void UpdateGoodPhoto()
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", photoTextBox.Text);

            if (string.IsNullOrWhiteSpace(photoTextBox.Text) || !File.Exists(imagePath))
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
            }

            try
            {
                picture.Image = System.Drawing.Image.FromFile(imagePath);
            }
            catch (Exception)
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
                picture.Image = System.Drawing.Image.FromFile(imagePath);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(priceTextBox.Text))
                {
                    MessageBox.Show("Fill the required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(priceTextBox.Text, out double price))
                {
                    MessageBox.Show("Input correct price", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _good.GoodName = nameTextBox.Text;
                _good.Price = double.Parse(priceTextBox.Text);
                _good.Picture = photoTextBox.Text;
                _good.Description = descriptionTextBox.Text;
                _good.CategoryId = (int)categoryComboBox.SelectedValue;

                if (_rowIndex >= 0)
                {
                    dynamic item = _bindingSource[_rowIndex];
                    item.GoodName = _good.GoodName;
                    item.Price = (double)_good.Price;
                    item.Picture = _good.Picture;
                    item.Description = _good.Description;
                    item.CategoryName = ((Category)categoryComboBox.SelectedItem).CategoryName;
                    item.CategoryId = (int)_good.CategoryId;
                    _bindingSource.ResetBindings(false);
                }
                else
                {
                    Category selectedCategory = (Category)categoryComboBox.SelectedItem;

                    var newGood = new
                    {
                        GoodId = 0,
                        GoodName = _good.GoodName,
                        Price = _good.Price,
                        Picture = _good.Picture,
                        Description = _good.Description,
                        CategoryName = selectedCategory.CategoryName,
                        CategoryId = selectedCategory.CategoryId
                    };
                    _bindingSource.Add(newGood);
                }

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPhoto_TextChanged(object sender, EventArgs e)
        {
            UpdateGoodPhoto();
        }
    }
}