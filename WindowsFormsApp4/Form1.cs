using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Tải danh sách phông chữ và kích thước
            LoadSystemFonts();
            LoadFontSizes();

            // Liên kết sự kiện cho các ToolStripButton
            toolStripButton1.Click += ToolStripButton1_Click; // In đậm
            toolStripButton2.Click += ToolStripButton2_Click; // In nghiêng
            toolStripButton3.Click += ToolStripButton3_Click; // Gạch chân

            // Liên kết sự kiện thay đổi font chữ và kích thước
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }

        // Tải danh sách phông chữ hệ thống vào comboBox1
        private void LoadSystemFonts()
        {
            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily font in fonts.Families)
            {
                comboBox1.Items.Add(font.Name);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0; // Chọn font mặc định
            }
        }

        // Tải danh sách kích thước font vào comboBox2
        private void LoadFontSizes()
        {
            int[] fontSizes = { 8, 10, 12, 14, 16, 18, 20, 24, 28, 32, 36, 48, 72 };
            foreach (int size in fontSizes)
            {
                comboBox2.Items.Add(size);
            }

            comboBox2.SelectedIndex = 2; // Kích thước mặc định = 12
        }

        // Thay đổi font chữ khi chọn từ comboBox1
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFormatting();
        }

        // Thay đổi kích thước font khi chọn từ comboBox2
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFormatting();
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
                string selectedFont = comboBox1.SelectedItem.ToString(); // Lấy font từ comboBox1
                float selectedSize = float.Parse(comboBox2.SelectedItem.ToString()); // Lấy kích thước từ comboBox2
                FontStyle selectedStyle = richTextBox1.SelectionFont.Style; // Lấy kiểu chữ hiện tại

                // Cập nhật font mới cho văn bản được chọn trong RichTextBox
                richTextBox1.SelectionFont = new Font(selectedFont, selectedSize, selectedStyle);
            }
        }
    }
}
