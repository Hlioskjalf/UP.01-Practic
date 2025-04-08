
using Zadanie9.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Zadanie9.Forms;
using Zadanie9;

namespace Zadanie9.Forms
{
    public partial class MainWindow : Form
    {
        private int _itemcount = 0;
        private string _userRole;
        private bool _dataChanged = false;
        private BindingSource _bindingSource = new BindingSource();

        public MainWindow(string userRole, string name)
        {
            InitializeComponent();
            stripStatus.Text += " " + userRole;
            stripName.Text += " " + name;
            _userRole = userRole;

            goodsDGV.DataSource = _bindingSource;
            saveButton.Visible = _userRole == "Admin";
            backButton.Visible = _userRole == "Admin";
            addButton.Visible = _userRole == "Admin";

            var CategoryType = Program.context.Categories.OrderBy(p => p.CategoryName).ToList();
            CategoryType.Insert(0, new Zadanie9.Models.Category
            {
                CategoryName = "All Types"
            }
            );
            categoryComboBox.DataSource = CategoryType;
            categoryComboBox.DisplayMember = "CategoryName";
            categoryComboBox.ValueMember = "CategoryId";

            LoadAndInitData();
        }

        public class GoodViewModel
        {
            public int GoodId { get; set; }
            public string GoodName { get; set; }
            public double Price { get; set; }
            public string Picture { get; set; }
            public string Description { get; set; }
            public string CategoryName { get; set; }
            public int CategoryId { get; set; }
        }
        private List<GoodViewModel> GetCurrentGoods()
        {
            return Program.context.Goods.Join(Program.context.Categories, p => p.CategoryId, t => t.CategoryId, (p, t) => new GoodViewModel
            {
                GoodId = p.GoodId,
                GoodName = p.GoodName,
                Price = (double)p.Price,
                Picture = p.Picture,
                Description = p.Description,
                CategoryName = t.CategoryName,
                CategoryId = (int)p.CategoryId
            }).ToList();
        }
        private void LoadAndInitData()
        {
            _bindingSource.Clear();
            var currentGoods = GetCurrentGoods();

            BindingList<GoodViewModel> bindingList = new BindingList<GoodViewModel>();
            foreach (var item in currentGoods)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", item.Picture);
                if (string.IsNullOrWhiteSpace(item.Picture) || !File.Exists(imagePath))
                {
                    item.Picture = "picture.png";
                }
                bindingList.Add(item);
            }

            _bindingSource.DataSource = bindingList;
            goodsDGV.DataSource = _bindingSource;

            goodsDGV.Columns[6].Visible = false;
            goodsDGV.Columns[0].HeaderText = "Item Code";
            goodsDGV.Columns[1].HeaderText = "Name";
            goodsDGV.Columns[2].HeaderText = "Price";
            goodsDGV.Columns[3].HeaderText = "Image";
            goodsDGV.Columns[4].HeaderText = "Description";
            goodsDGV.Columns[5].HeaderText = "Category";

            _itemcount = Program.context.Goods.Count();
            countGoodLabel.Text = $" Query Result: {currentGoods.Count} records out of {_itemcount}";

            if (goodsDGV.Rows.Count > 0)
                dgvGoods_CellClick(goodsDGV, new DataGridViewCellEventArgs(0, 0));

            _dataChanged = false;
            UpdateButtonsState();
        }

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            CloseMainWindowAndOpenFmLogin();
        }

        private void CloseMainWindowAndOpenFmLogin()
        {
            fmLogin formLogin = new fmLogin();
            formLogin.Owner = this;
            this.Hide();
            formLogin.Show();
        }

        private void UpdateData()
        {
            var currentGoods = GetCurrentGoods();

            if (categoryComboBox.SelectedIndex > 0)
            {
                var selectedCategory = categoryComboBox.SelectedItem as Category;
                if (selectedCategory != null)
                    currentGoods = currentGoods.Where(y => y.CategoryId == selectedCategory.CategoryId).ToList();
            }

            currentGoods = currentGoods.Where(p => p.GoodName.ToLower().Contains(goodsName.Text.ToLower())).ToList();

            if (sortComboBox.SelectedIndex >= 0)
            {
                if (sortComboBox.SelectedIndex == 0)
                    currentGoods = currentGoods.OrderBy(p => p.Price).ToList();
                if (sortComboBox.SelectedIndex == 1)
                    currentGoods = currentGoods.OrderByDescending(p => p.Price).ToList();
                if (sortComboBox.SelectedIndex == 2)
                    currentGoods = currentGoods.OrderBy(p => p.GoodName).ToList();
            }
            else
                currentGoods = currentGoods.OrderBy(p => p.GoodName).ToList();

            BindingList<GoodViewModel> bindingList = new BindingList<GoodViewModel>();
            foreach (var item in currentGoods)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", item.Picture);
                if (string.IsNullOrWhiteSpace(item.Picture) || !File.Exists(imagePath))
                {
                    item.Picture = "picture.png";
                }
                bindingList.Add(item);
            }

            _bindingSource.DataSource = bindingList;

            countGoodLabel.Text = $"Query Result: {currentGoods.Count} Records out of {_itemcount}";
        }
        private void comboSort_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateData();
        }
        private void comboCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateData();
        }
        private void txtNameGood_TextChanged(object sender, System.EventArgs e)
        {
            UpdateData();
        }
        private void dgvGoods_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < goodsDGV.Rows.Count)
            {
                GoodViewModel selectedRow = (GoodViewModel)_bindingSource[e.RowIndex];

                goodNameLabel2.Text = selectedRow.GoodName.ToString();
                priceLabel.Text = selectedRow.Price.ToString();

                string imageName = selectedRow.Picture.ToString();
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", imageName);

                if (string.IsNullOrWhiteSpace(imageName) || !File.Exists(imagePath))
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
                }

                try
                {
                    pictureBox2.Image = Image.FromFile(imagePath);
                }
                catch (Exception)
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "picture.png");
                    pictureBox2.Image = Image.FromFile(imagePath);
                }
            }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            CloseMainWindowAndOpenFmLogin();
        }
        private void dgvGoods_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_userRole == "Admin")
            {
                _dataChanged = true;
                UpdateButtonsState();
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                goodsDGV.EndEdit();

                List<int> existingGoodIds = Program.context.Goods.Select(g => g.GoodId).ToList();

                BindingList<GoodViewModel> changedData = (BindingList<GoodViewModel>)_bindingSource.DataSource;

                using (var transaction = Program.context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (GoodViewModel row in changedData)
                        {
                            if (row.GoodId == 0)
                            {
                                int maxGoodId = Program.context.Goods.Any() ? Program.context.Goods.Max(g => g.GoodId) : 0;
                                int newGoodId = maxGoodId + 1;

                                Good newGood = new Good
                                {
                                    GoodId = newGoodId,
                                    GoodName = row.GoodName,
                                    Price = row.Price,
                                    Picture = row.Picture,
                                    Description = row.Description,
                                    CategoryId = row.CategoryId
                                };

                                Program.context.Goods.Add(newGood);
                                row.GoodId = newGoodId;
                            }
                            else
                            {
                                int goodId = row.GoodId;
                                Good existingGood = Program.context.Goods.FirstOrDefault(g => g.GoodId == goodId);

                                if (existingGood != null)
                                {
                                    existingGood.GoodName = row.GoodName;
                                    existingGood.Price = row.Price;
                                    existingGood.Picture = row.Picture;
                                    existingGood.Description = row.Description;
                                    existingGood.CategoryId = row.CategoryId;
                                }
                            }
                        }

                        Program.context.SaveChanges();

                        transaction.Commit();

                        MessageBox.Show("Changes saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _dataChanged = false;
                        UpdateButtonsState();
                        LoadAndInitData();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error saving changes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            LoadAndInitData();
            _dataChanged = false;
            UpdateButtonsState();
        }
        private void pictureGood_Click(object sender, EventArgs e)
        {
            ImageForm imageForm = new ImageForm(pictureBox2.Image);
            imageForm.Show();
        }
        private void UpdateButtonsState()
        {
            saveButton.Enabled = _dataChanged && _userRole == "Admin";
            undoButton.Enabled = _dataChanged && _userRole == "Admin";
        }
        private void OpenGoodDetailsForm(int rowIndex)
        {
            GoodDetailsForm detailsForm;

            if (rowIndex == -1)
            {
                detailsForm = new GoodDetailsForm(Program.context.Categories.ToList(), _bindingSource);
            }
            else
            {
                GoodViewModel selectedRow = (GoodViewModel)_bindingSource[rowIndex];

                int goodId = (int)goodsDGV.Rows[rowIndex].Cells[0].Value;
                Good good = Program.context.Goods.FirstOrDefault(g => g.GoodId == goodId);
                detailsForm = new GoodDetailsForm(good, Program.context.Categories.ToList(), _bindingSource, rowIndex);
            }

            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                _dataChanged = true;
                UpdateButtonsState();
            }
        }
        private void dgvGoods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _userRole == "Admin")
            {
                OpenGoodDetailsForm(e.RowIndex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenGoodDetailsForm(-1);
        }
    }
}