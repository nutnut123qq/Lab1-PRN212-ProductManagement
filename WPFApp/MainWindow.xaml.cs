using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;
using Repositories;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public MainWindow()
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }

        public void LoadCategoryList()
        {
            var categories = categoryRepository.GetCategories(); // Lấy danh mục từ cơ sở dữ liệu hoặc bất kỳ nguồn nào.
            cboCategory.ItemsSource = categories; // Bind dữ liệu vào ComboBox
            cboCategory.DisplayMemberPath = "CategoryName"; // Tên thuộc tính hiển thị
            cboCategory.SelectedValuePath = "CategoryId"; // Tên thuộc tính để bind giá trị
        }

        public void LoadProductList()
        {
            var list = productRepository.GetProducts();
            dgData.ItemsSource = null;
            dgData.ItemsSource = list;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductName.Text) ||
                    string.IsNullOrEmpty(txtUnitsInStock.Text) ||
                    string.IsNullOrEmpty(txtPrice.Text) ||
                    cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }

                Product product = new Product
                {
                    ProductId = productRepository.GetMaxProductId(),
                    ProductName = txtProductName.Text,
                    UnitsInStock = short.Parse(txtUnitsInStock.Text),
                    UnitPrice = Decimal.Parse(txtPrice.Text),
                    CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString()) // Giá trị mặc định nếu null
                };

                productRepository.SaveProduct(product);

                // Sau khi thêm sản phẩm mới, load lại danh sách sản phẩm để hiển thị.
                LoadProductList();

                MessageBox.Show("Tạo sản phẩm thành công!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu nhập vào không đúng định dạng.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }



        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductID.Text))
                {
                    Product product = productRepository.GetProductById(Int32.Parse(txtProductID.Text));
                    if (product != null)
                    {
                        product.ProductName = txtProductName.Text;
                        product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                        product.UnitPrice = Decimal.Parse(txtPrice.Text);
                        product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                        productRepository.UpdateProduct(product);
                        LoadProductList();
                        MessageBox.Show("Sửa thành công!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductID.Text))
                {
                    Product product = productRepository.GetProductById(Int32.Parse(txtProductID.Text));
                    if (product != null)
                    {
                        productRepository.DeleteProduct(product);
                        LoadProductList();
                        MessageBox.Show("Xóa sản phẩm thành công!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally { 
                LoadProductList();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Application.Current.Shutdown();

        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is Product product)
            {
                txtProductID.Text = product.ProductId.ToString();
                txtProductName.Text = product.ProductName;
                txtUnitsInStock.Text = product.UnitsInStock.ToString();
                txtPrice.Text = product.UnitPrice.ToString();
                cboCategory.SelectedValue = product.CategoryId;
            }
            //DataGrid dataGrid = sender as DataGrid;
            //DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgData.SelectedIndex);
            //DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            //string id = ((TextBlock)RowColumn.Content).Text;
            //Product product = productRepository.GetProductById(Int32.Parse(id));
            //if (product != null)
            //{
            //    txtProductID.Text = product.ProductId.ToString();
            //    txtProductName.Text = product.ProductName;
            //    txtUnitsInStock.Text = product.UnitsInStock.ToString();
            //    txtPrice.Text = product.UnitPrice.ToString();
            //    cboCategory.SelectedValue = product.CategoryId;
            //}

        }
    }
}