using LibraryISP17.ClassHelper;
using LibraryISP17.EF;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryISP17.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReaderWindow.xaml
    /// </summary>
    public partial class ReaderWindow : Window
    {

        List<Reader> readerList = new List<Reader>();

        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии", "По имени", "По адресу" };
        public ReaderWindow()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            readerList = AppData.Context.Reader.ToList();
            readerList = readerList.
                Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower())
                || i.FirstName.ToLower().Contains(txtSearch.Text.ToLower()))
                .ToList();


            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    readerList = readerList.OrderBy(i => i.Id).ToList();
                    break;

                case 1:
                    readerList = readerList.OrderBy(i => i.LastName).ToList();
                    break;

                case 2:
                    readerList = readerList.OrderBy(i => i.FirstName).ToList();
                    break;

                case 3:
                    readerList = readerList.OrderBy(i => i.Address).ToList();
                    break;

                default:
                    readerList = readerList.OrderBy(i => i.Id).ToList();
                    break;
            }

            lvReader.ItemsSource = readerList;
        }

        private void btnAddReader_Click(object sender, RoutedEventArgs e)
        {
            AddReaderWindow addReaderWindow = new AddReaderWindow();
            this.Opacity = 0.2;
            addReaderWindow.ShowDialog();
            lvReader.ItemsSource = AppData.Context.Reader.ToList();
            this.Opacity = 1;

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void lvReader_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete || e.Key == System.Windows.Input.Key.Back)
            {
                if (lvReader.SelectedItem is EF.Reader && lvReader.SelectedIndex != -1) 
                {
                    try
                    {
                        var item = lvReader.SelectedItem as EF.Reader;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтверите Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppData.Context.Reader.Remove(item);
                            AppData.Context.SaveChanges();
                            MessageBox.Show("Успех", "Пользователь успешно удален", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }




            }

        }
    }
}
