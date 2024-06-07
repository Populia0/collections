using System.Text;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using System.Numerics;
using System.ComponentModel;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using Azure.Core;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using ClassLibrary;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        ApplicationContext db;
        static Class c1 = new Class { Id = 1, Number = 1, Letter = 'А' };
        static Class c2 = new Class { Id = 2, Number = 1, Letter = 'Б' };
        Student s1 = new Student { Id = 1, LastName = "Попов", Name = "Петр", Patronymic = "Вячеславович", StClass = c1, ClassId = c1.Id };
        Student s2 = new Student { Id = 2, LastName = "Иванов", Name = "Василий", Patronymic = "Владиславович", StClass = c2, ClassId = c2.Id };

        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

            db.Classes.Add(c1);
            db.Classes.Add(c2);
            db.SaveChanges();

            db.Students.AddRange(new List<Student> { s2, s1 });
            db.SaveChanges();

            db.Classes.Load();
            db.Students.Load();

            classesGrid.ItemsSource = db.Classes.Local.ToBindingList();
            studentsGrid.ItemsSource = db.Students.Local.ToBindingList();
            this.Closing += MainWindow_Closing;
        }

        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Class cl = e.Row.Item as Class;
            if (cl != null)
            {
                cl.Id = classesGrid.Items.IndexOf(cl)+1;
            }
        }
        private void studentsGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Student s = e.Row.Item as Student;
            if (s != null)
            {
                s.Id = studentsGrid.Items.IndexOf(s) + 1;
            }
        }
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            db.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            foreach (var x in studentsGrid.Items)
            {
                Student student = x as Student;
                if (student != null)
                {
                    if (db.Classes.Find(student.ClassId) == null)
                    {
                        flag = false;
                        System.Windows.MessageBox.Show($"Ошибка! Текущий Id класса не найден в таблице классов. База данных не обновлена");
                        break;
                    }
                }
            }
            if (flag)
            {
                db.SaveChanges();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (classesGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < classesGrid.SelectedItems.Count; i++)
                {
                    Class elem = classesGrid.SelectedItems[i] as Class;
                    if (elem != null)
                    {
                        db.Classes.Remove(elem);
                    }
                }
            }
            db.SaveChanges();
        }

        private void deleteStButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (studentsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < studentsGrid.SelectedItems.Count; i++)
                {
                    Student elem = studentsGrid.SelectedItems[i] as Student;
                    if (elem != null)
                    {
                        if (db.Classes.Find(elem.ClassId) == null)
                        {
                            flag = false;
                        }
                        db.Students.Remove(elem);
                    }
                }
            }
            if(flag)
            {
                db.SaveChanges();
            }
        }
    }
}