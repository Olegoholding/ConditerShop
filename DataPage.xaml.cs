using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ConditerShop
{
    /// <summary>
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {

        string PageName;
        string Tag;
        string Uid;
        string Command;
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        readonly string connStr = "Server = 95.183.12.18; Port = 3306; Database = konditerskaya; user = oleg";
        public DataPage(string pageName, string tag, string uid, string command)
        {
            PageName = pageName;   
            Tag = tag;
            Uid = uid;
            Command = command;
            InitializeComponent();
            DataGrid.ItemsSource = LoadData(command).DefaultView;
            PageNameText.Text = PageName;
            SrcBtnText.Text = $"Поиск по {Uid}";
        }
        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e) => ((MainWindow)Application.Current.MainWindow).Frame.Content = null;
        private void SrcBtn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = LoadData($"Select * From {Tag}_view where {Uid} = '{TexxtBox.Text}'").DefaultView;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            InsertData($"{Tag}", $"Select * From {Tag}", (DataView)DataGrid.ItemsSource);
            DataGrid.ItemsSource = LoadData(Command).DefaultView;
        }
        public void InsertData(string TableName, string Command, DataView DataView)
        {
            var DataSet = new DataSet();
            var DataTable = DataView.Table;
            DataTable.TableName = TableName;
            DataSet.Tables.Add(DataTable);
            try
            {
                using (conn = new MySqlConnection(connStr))
                {
                    adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Command, conn);
                    var builder = new MySqlCommandBuilder(adapter);

                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.Update(DataSet, TableName);
                }
                DataSet.Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void DltBtn_Click(object sender, RoutedEventArgs e)
        {
            int ID = 0;
            try
            {
                using (conn = new MySqlConnection(connStr))
                {
                    var cellContent = DataGrid.Columns[0].GetCellContent(DataGrid.SelectedItem);
                    if (cellContent is TextBlock textBlock)
                    {
                        ID = int.Parse(textBlock.Text);
                    }
                    string quer = $"DELETE FROM {Tag} WHERE Номер = {ID}";
                    cmd = new MySqlCommand(quer, conn);
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            DataGrid.ItemsSource = LoadData(Command).DefaultView;
        }
        public DataTable LoadData(string Command)
        {
            DataTable DataTable = new DataTable();
            using (conn = new MySqlConnection($"{connStr}"))
            {
                try
                {
                    conn.Open();
                    using (cmd = new MySqlCommand($"{Command}", conn))
                    {
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(DataTable);
                    }
                    conn.Close();
                    return DataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.HResult.ToString());
                    return DataTable;
                }
            }
        }
    }
}
