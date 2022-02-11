﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LibraryISP17.ClassHelper;

namespace LibraryISP17.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddReaderWindow.xaml
    /// </summary>
    public partial class AddReaderWindow : Window
    {
        EF.Reader editReader = new EF.Reader();

        bool isEdit = true;

        public AddReaderWindow()
        {
            InitializeComponent();
            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";
            cmbGender.SelectedIndex = 0;

            isEdit = false;
        }

        public AddReaderWindow(EF.Reader reader)
        {
            InitializeComponent();

            //заполнение комбобокса
            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";

            // изменение заголовка и текста кнопки
            tbTitle.Text = "Изменение данных читателя";
            btAdd.Content = "Изменить";
            // передача значений в поля

            editReader = reader;

            txtLastName.Text = editReader.LastName;
            txtFirstName.Text = editReader.FirstName;
            txtPhone.Text = editReader.Phone;
            txtEmail.Text = editReader.Email;
            txtAddress.Text = editReader.Address;
            cmbGender.SelectedIndex = editReader.IdGender - 1;

            isEdit = true;
        }

            private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            
            // проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле Адрес не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // проверка на количество символов

            if (txtLastName.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstName.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtEmail.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Length > 12)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtAddress.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                try
                {
                    editReader.LastName = txtLastName.Text;
                    editReader.FirstName = txtFirstName.Text;
                    editReader.Phone = txtPhone.Text;
                    editReader.Email = txtEmail.Text;
                    editReader.Address = txtAddress.Text;
                    editReader.IdGender = cmbGender.SelectedIndex + 1;
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Успех", "Данные читателя успешно изменены", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            else
            {
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтверите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        // Добавление нового читателя
                        EF.Reader newReader = new EF.Reader();
                        newReader.LastName = txtLastName.Text;
                        newReader.FirstName = txtFirstName.Text;
                        newReader.Phone = txtPhone.Text;
                        newReader.Email = txtEmail.Text;
                        newReader.Address = txtAddress.Text;
                        newReader.IdGender = cmbGender.SelectedIndex + 1;

                        AppData.Context.Reader.Add(newReader);

                        AppData.Context.SaveChanges();
                        MessageBox.Show("Успех", "Пользователь успешно добавлен", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }


        }
    }
}
