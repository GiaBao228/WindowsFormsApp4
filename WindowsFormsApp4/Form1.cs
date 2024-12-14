using System;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Liên kết sự kiện cho các ToolStripMenuItem
            tạoVănBảnMớiToolStripMenuItem1.Click += TạoVănBảnMớiToolStripMenuItem1_Click; // Tạo văn bản mới
            mởTậpTinToolStripMenuItem.Click += MởTậpTinToolStripMenuItem_Click; // Mở tập tin
            lưuNộiDungVănBảnToolStripMenuItem.Click += LưuNộiDungVănBảnToolStripMenuItem_Click; // Lưu nội dung văn bản
            thoátToolStripMenuItem.Click += ThoátToolStripMenuItem_Click; // Thoát ứng dụng

            // Liên kết sự kiện thay đổi font chữ và kích thước
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;

            // Liên kết các sự kiện cho các ToolStripButton (In đậm, In nghiêng, Gạch chân)
            toolStripButton1.Click += ToolStripButton1_Click; // In đậm
            toolStripButton2.Click += ToolStripButton2_Click; // In nghiêng
            toolStripButton3.Click += ToolStripButton3_Click; // Gạch chân
            toolStripButton5.Click += ToolStripButton5_Click; // Lưu văn bản
            toolStripButton4.Click += ToolStripButton4_Click; // Mở văn bản mới (Button này)

            // Tải font và kích thước font
            LoadSystemFonts(); // Tải font chữ hệ thống
            LoadFontSizes();   // Tải kích thước font
        }

        // Tạo văn bản mới
        private void TạoVănBảnMớiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); // Xóa tất cả văn bản trong RichTextBox
        }

        // Mở tập tin
        private void MởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        // Lưu nội dung văn bản
        private void LưuNộiDungVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
            }
        }

        // Thoát ứng dụng
        private void ThoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Đóng ứng dụng
        }

        // Tải danh sách phông chữ hệ thống vào comboBox1
        private void LoadSystemFonts()
        {
            comboBox1.Items.Clear();  // Làm sạch danh sách trước khi thêm vào
            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily font in fonts.Families)
            {
                comboBox1.Items.Add(font.Name);  // Thêm font vào comboBox1
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0; // Chọn font mặc định
            }
        }

        // Tải danh sách kích thước font vào comboBox2
        private void LoadFontSizes()
        {
            comboBox2.Items.Clear();  // Làm sạch danh sách trước khi thêm vào
            int[] fontSizes = { 8, 10, 12, 14, 16, 18, 20, 24, 28, 32, 36, 48, 72 };
            foreach (int size in fontSizes)
            {
                comboBox2.Items.Add(size);  // Thêm kích thước vào comboBox2
            }

            comboBox2.SelectedIndex = 2; // Kích thước mặc định = 12
        }

        // Thay đổi font chữ khi chọn từ comboBox1
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFormatting();  // Cập nhật định dạng văn bản
        }

        // Thay đổi kích thước font khi chọn từ comboBox2
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFormatting();  // Cập nhật định dạng văn bản
        }

        // In đậm (FontStyle.Bold)
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        // In nghiêng (FontStyle.Italic)
        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        // Gạch chân (FontStyle.Underline)
        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        // Lưu văn bản
        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
            }
        }

        // Hàm để thay đổi các kiểu chữ (Bold, Italic, Underline)
        private void ToggleFontStyle(FontStyle style)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newStyle;

                // Kiểm tra nếu đã có kiểu chữ này, thì tắt nó đi
                if (currentFont.Style.HasFlag(style))
                {
                    newStyle = currentFont.Style & ~style; // Tắt kiểu
                }
                else
                {
                    newStyle = currentFont.Style | style; // Bật kiểu
                }

                // Áp dụng font mới
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        // Hàm cập nhật tất cả các định dạng văn bản (font, kích thước, kiểu chữ)
        private void UpdateTextFormatting()
        {
            if (richTextBox1.SelectionFont != null)
            {
                string selectedFont = comboBox1.SelectedItem?.ToString(); // Lấy font từ comboBox1
                float selectedSize = float.Parse(comboBox2.SelectedItem?.ToString() ?? "12"); // Lấy kích thước từ comboBox2

                if (selectedFont != null)
                {
                    FontStyle selectedStyle = richTextBox1.SelectionFont.Style; // Lấy kiểu chữ hiện tại

                    // Cập nhật font mới cho văn bản được chọn trong RichTextBox
                    richTextBox1.SelectionFont = new Font(selectedFont, selectedSize, selectedStyle);
                }
            }
        }

        // Hàm xử lý khi nhấn ToolStripButton4 (Mở văn bản mới)
        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            // Xóa toàn bộ nội dung trong RichTextBox để bắt đầu một văn bản mới
            richTextBox1.Clear();
        }
    }
}
