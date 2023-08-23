using System.CodeDom;
using System.ComponentModel;

namespace CustomerAccounter
{
    enum UsersList
    {
        [Description("Адміністратор")] Admin,
        [Description("Фізична особа")] FO,
        [Description("Фізична особа-підприємець")] FOP,
        [Description("Юридична особа")] UO
    }
    public partial class Form1 : Form
    {
        //List<User> users;
        Admin admin;
        //ReadFromFile fileReader = new ReadFromFile();
        string textBoxLogin;
        string textBoxPassword;
        Label welcome=new Label();
        List<Client> clients= new List<Client>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UsersList[] userValues = (UsersList[])Enum.GetValues(typeof(UsersList));
            foreach (UsersList user in userValues)
            {
                comboBox1.Items.Add(GetDescription(user));
            }
            comboBox1.SelectedIndex = 0;
        }

        private string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBoxLogin = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBoxPassword = textBox5.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) 
            {
                if (admin == null)
                {
                    admin=ReadFromFile.UserAdminRead();
                    if (textBoxLogin == admin.Login && textBoxPassword == admin.Password)
                    {
                        //MessageBox.Show("Вход успешен!");
                        UnvisibleElements();
                        Form1_LoadToAdmin();
                    }
                    else
                    {
                        MessageBox.Show("Невірно введений логін або пароль!");
                    }
                }
            }
        }

        private void UnvisibleElements()
        {
            button1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            checkBox1.Visible = false;
            comboBox1.Visible = false;
            button2.Visible = false;
            label6.Visible = false;
        }
        private void Form1_LoadToAdmin()
        {
            welcome.Parent = this;
            welcome.AutoSize = true;
            welcome.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            welcome.TextAlign = ContentAlignment.MiddleCenter;
            welcome.Location = new Point((ClientSize.Width) / 3, 10); ;
            welcome.Text = "Вітаємо Вас в системі обліку клієнтів! \nВхід успішно здійснений!";
            welcome.Visible = true;
            //Thread.Sleep(5000);
            //welcome.Visible = false;
            Label fiziki = new Label();
            fiziki.Parent = this;
            fiziki.Text = "Фізичні особи:";
            fiziki.Location = new Point(20, 50);
            fiziki.AutoSize = true;
            fiziki.Font = new Font("Arial", 12, FontStyle.Bold);
            CreateListviewFiziks();
            Label fop = new Label();
            fop.Parent = this;
            fop.Text = "Фізичні особи-підприємці:";
            fop.Location = new Point(20, 200);
            fop.AutoSize = true;
            fop.Font = new Font("Arial", 12, FontStyle.Bold);
            CreateListviewFops();
            
        }
        private ListView CreateListviewFiziks()
        {
            ListView lv = new ListView();
            lv.Parent = this;
            lv.Location = new Point(10, 70);
            lv.View = View.Details;
            lv.LabelEdit = true;
            lv.AllowColumnReorder = true;
            lv.CheckBoxes = true;
            lv.FullRowSelect = true;


            //fiziki=fileReader.ReadFiziks();
            List<Fiziki> fiziki = ReadFromFile.ReadFiziks();
            foreach (Fiziki f in fiziki)
            {
                ListViewItem item = new ListViewItem(f.Name/*f.Login*/);
                //item.SubItems.Add(f.Password);
                //item.SubItems.Add(f.Name);
                item.SubItems.Add(f.Surname);
                item.SubItems.Add(f.Code.ToString());
                item.SubItems.Add(f.phoneNumber.ToString());
                item.SubItems.Add(f.Email);
                item.SubItems.Add(f.Adress);
                lv.Items.Add(item);
            }

            //lv.Bounds = new Rectangle(new Point(30, 50), new Size(800, 700));
            //int itemHeight = lv.GetItemRect(0).Height;
            //int itemCount = lv.Items.Count;
            //int headerHeight = (int)(lv.Font.Height * 1.5);
            //int borderHeight = 2;
            //int newHeight = itemHeight * itemCount + headerHeight + borderHeight;
            //lv.Height = newHeight;

            //lv.Columns.Add("Логин", 100, HorizontalAlignment.Left);
            //lv.Columns.Add("Пароль", 80, HorizontalAlignment.Left);
            lv.Columns.Add("Iм'я", 100, HorizontalAlignment.Left);
            lv.Columns.Add("Прізвище", 100, HorizontalAlignment.Left);
            lv.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lv.Columns.Add("Телефон", 100, HorizontalAlignment.Left);
            lv.Columns.Add("Електронна адреса", 150, HorizontalAlignment.Left);
            lv.Columns.Add("Адреса", 190, HorizontalAlignment.Left);

            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            int totalColumnWidth = 0;
            foreach (ColumnHeader column in lv.Columns)
            {
                totalColumnWidth += column.Width;
            }
            int totalRowHeight = lv.Items.Count * lv.GetItemRect(0).Height;

            int extraHeight = SystemInformation.HorizontalScrollBarHeight + 2; // учитываем вертикальную полосу прокрутки
            int extraWidth = SystemInformation.VerticalScrollBarWidth + 2; // учитываем горизонтальную полосу прокрутки

            lv.Width = totalColumnWidth + extraWidth;
            lv.Height = totalRowHeight + lv.Font.Height + extraHeight;
            return lv;
        }
        private ListView CreateListviewFops()
        {
            ListView lv = new ListView();
            lv.Parent = this;
            lv.Location = new Point(10, 220);
            lv.View = View.Details;
            lv.LabelEdit = true;
            lv.AllowColumnReorder = true;
            lv.CheckBoxes = true;
            lv.FullRowSelect = true;

            List<FOP> fops = ReadFromFile.ReadFOPs();
            foreach (Fiziki f in fops)
            {
                ListViewItem item = new ListViewItem(f.Name);
                item.SubItems.Add(f.Surname);
                item.SubItems.Add(f.Code.ToString());
                item.SubItems.Add(f.phoneNumber.ToString());
                item.SubItems.Add(f.Email);
                item.SubItems.Add(f.Adress);
                lv.Items.Add(item);
            }

            lv.Columns.Add("Iм'я", 100, HorizontalAlignment.Left);
            lv.Columns.Add("Прізвище", 100, HorizontalAlignment.Left);
            lv.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lv.Columns.Add("Телефон", 100, HorizontalAlignment.Left);
            lv.Columns.Add("Електронна адреса", 150, HorizontalAlignment.Left);
            lv.Columns.Add("Адреса", 190, HorizontalAlignment.Left);

            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            int totalColumnWidth = 0;
            foreach (ColumnHeader column in lv.Columns)
            {
                totalColumnWidth += column.Width;
            }
            int totalRowHeight = lv.Items.Count * lv.GetItemRect(0).Height;

            int extraHeight = SystemInformation.HorizontalScrollBarHeight + 2; // учитываем вертикальную полосу прокрутки
            int extraWidth = SystemInformation.VerticalScrollBarWidth + 2; // учитываем горизонтальную полосу прокрутки

            lv.Width = totalColumnWidth + extraWidth;
            lv.Height = totalRowHeight + lv.Font.Height + extraHeight;
            return lv;
        }
    }
}