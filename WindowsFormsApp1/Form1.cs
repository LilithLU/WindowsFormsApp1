using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        TabControl tabControl = new TabControl();

        TextBox tbLastName = new TextBox();
        TextBox tbFirstName = new TextBox();
        DateTimePicker dpBirth = new DateTimePicker();
        ComboBox cbCourse = new ComboBox();
        TextBox tbEmail = new TextBox();
        Button btnAddStudent = new Button();
        ListView lvStudents = new ListView();

        TextBox tbAmount = new TextBox();
        ComboBox cbCategory = new ComboBox();
        DateTimePicker dpExpenseDate = new DateTimePicker();
        Button btnAddExpense = new Button();
        ListView lvExpenses = new ListView();

        Label statusLabel = new Label();
        public Form1()
        {
            InitializeComponent(); 
            this.Load += Form1_Load;

            Text = "18 практическая";
            Width = 900;
            Height = 600;

            tabControl.Dock = DockStyle.Fill;
            Controls.Add(tabControl);

            InitStudentsTab();
            InitExpensesTab();

            statusLabel.Dock = DockStyle.Bottom;
            statusLabel.Height = 25;
            statusLabel.Text = "Готово";
            Controls.Add(statusLabel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lvStudents.Items.Add(new ListViewItem(new string[] { "Иванов", "Иван", "01.01.2000", "2", "ivan@mail.ru" }));
            lvExpenses.Items.Add(new ListViewItem(new string[] { "1000", "Канцелярия", DateTime.Now.ToShortDateString() }));
        }

        void InitStudentsTab()
        {
            var tab = new TabPage("Студенты");

            tbLastName.SetBounds(20, 20, 150, 25);
            tbFirstName.SetBounds(180, 20, 150, 25);
            dpBirth.SetBounds(340, 20, 150, 25);
            cbCourse.SetBounds(500, 20, 80, 25);
            cbCourse.Items.AddRange(new object[] { "1", "2", "3", "4" });
            tbEmail.SetBounds(590, 20, 180, 25);

            btnAddStudent.Text = "Добавить";
            btnAddStudent.SetBounds(780, 20, 80, 25);
            btnAddStudent.Click += AddStudent;

            lvStudents.SetBounds(20, 60, 840, 400);
            lvStudents.UseCompatibleStateImageBehavior = false;
            lvStudents.View = View.Details;
            lvStudents.FullRowSelect = true;
            lvStudents.GridLines = true;
            lvStudents.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            lvStudents.Columns.Add("Фамилия", 120);
            lvStudents.Columns.Add("Имя", 120);
            lvStudents.Columns.Add("Дата рождения", 120);
            lvStudents.Columns.Add("Курс", 60);
            lvStudents.Columns.Add("Email", 200);

            tab.Controls.AddRange(new Control[]
            {
                tbLastName, tbFirstName, dpBirth, cbCourse, tbEmail,
                btnAddStudent, lvStudents
            });

            tabControl.TabPages.Add(tab);
        }

        void AddStudent(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLastName.Text) || string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                statusLabel.Text = "Фамилия и имя обязательны";
                return;
            }

            int age = DateTime.Now.Year - dpBirth.Value.Year;
            if (age < 14)
            {
                statusLabel.Text = "Возраст должен быть не менее 14 лет";
                return;
            }

            if (!tbEmail.Text.Contains("@"))
            {
                statusLabel.Text = "Некорректный email";
                return;
            }

            var item = new ListViewItem(new string[]
            {
                tbLastName.Text,
                tbFirstName.Text,
                dpBirth.Value.ToShortDateString(),
                cbCourse.Text,
                tbEmail.Text
            });

            lvStudents.Items.Add(item);
            statusLabel.Text = "Студент добавлен";
        }

        void InitExpensesTab()
        {
            var tab = new TabPage("Расходы");

            tbAmount.SetBounds(20, 20, 120, 25);
            cbCategory.SetBounds(150, 20, 150, 25);
            cbCategory.Items.AddRange(new object[] { "Канцелярия", "Оборудование", "ПО", "Прочее" });
            dpExpenseDate.SetBounds(310, 20, 150, 25);

            btnAddExpense.Text = "Добавить";
            btnAddExpense.SetBounds(470, 20, 80, 25);
            btnAddExpense.Click += AddExpense;

            lvExpenses.SetBounds(20, 60, 840, 400);
            lvExpenses.UseCompatibleStateImageBehavior = false;
            lvExpenses.View = View.Details;
            lvExpenses.FullRowSelect = true;
            lvExpenses.GridLines = true;
            lvExpenses.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            lvExpenses.Columns.Add("Сумма", 120);
            lvExpenses.Columns.Add("Категория", 200);
            lvExpenses.Columns.Add("Дата", 120);

            tab.Controls.AddRange(new Control[] { tbAmount, cbCategory, dpExpenseDate, btnAddExpense, lvExpenses });

            tabControl.TabPages.Add(tab);
        }

        void AddExpense(object sender, EventArgs e)
        {
            if (!decimal.TryParse(tbAmount.Text, out decimal amount) || amount <= 0)
            {
                statusLabel.Text = "Некорректная сумма";
                return;
            }

            var item = new ListViewItem(new string[]
            {
                amount.ToString(),
                cbCategory.Text,
                dpExpenseDate.Value.ToShortDateString()
            });

            lvExpenses.Items.Add(item);
            statusLabel.Text = "Расход добавлен";
        }
    }
}
