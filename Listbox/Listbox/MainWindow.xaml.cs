using System.Collections.Generic;
using System.Linq;
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

namespace Listbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> names = new List<string>() {"Peter","Silvia","Cleopatra","Louis","Cesar","Paul", "Luc D", "Luc C","Aleida","Dirk","Liesbeth","Linda","Zorro"};

        public MainWindow()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            simpleListBox.Items.Clear();
            names.Clear();

            FillListBox();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (simpleListBox.SelectedIndex > -1)
            {
                simpleListBox.Items.Remove(simpleListBox.SelectedItem);

                names.Clear();

                foreach (var item in simpleListBox.Items)
                {
                    names.Add(item.ToString());
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een item", "Geen item geselecteerd!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void sortButton_Click(object sender, RoutedEventArgs e)
        {
            names.Sort();

            FillListBox();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(addTextBox.Text.Length > 0)
            {
                names.Add(addTextBox.Text);
                
                FillListBox();

                addTextBox.Text = "";
                addTextBox.Focus();
            }
        }

        private void replaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (replaceTextBox.Text.Length > 0)
            {
                names[simpleListBox.SelectedIndex] = replaceTextBox.Text;

                FillListBox();

                replaceTextBox.Text = "";
            }
        }

        private void FillListBox()
        {
            simpleListBox.Items.Clear();
            multipleListBox.Items.Clear();

            foreach (var item in names)
            {
                simpleListBox.Items.Add(item.ToString());
                multipleListBox.Items.Add(item.ToString());
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(searchTextBox.Text.Length > 0)
            {
                names.Sort();
                FillListBox();

                string temp;
                List<string> list = new List<string>();
                List<int> listIndex = new List<int>();

                temp = searchTextBox.Text;
                temp = names.Find(element => element.ToUpper().StartsWith(temp.ToUpper()));

                simpleListBox.SelectedIndex = names.IndexOf(temp);
                simpleListBox.ScrollIntoView(simpleListBox.SelectedItem);

                list = names.FindAll(element => element.ToUpper().StartsWith(searchTextBox.Text.ToUpper()));

                int first = -1;

                foreach (var item in list)
                {
                    multipleListBox.SelectedIndex = names.IndexOf(item);
                    if (first == -1)
                    {
                        searchLabel.Content = item.ToString();
                        first = multipleListBox.SelectedIndex;
                    }
                    else
                    {
                        searchLabel.Content += ", " + item.ToString();
                    }
                }

                multipleListBox.SelectedIndex = first;
                multipleListBox.ScrollIntoView(first);
                simpleListBox.ScrollIntoView(first);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(sender is TextBox txt)
            {
                switch (txt.Name)
                {
                    case "addTextBox":
                        addButton.IsEnabled = true;
                        addButton.IsDefault = true;

                        replaceButton.IsEnabled = false;
                        searchButton.IsEnabled = false;

                        replaceTextBox.Text = "";
                        searchTextBox.Text = "";
                        searchLabel.Content = "Zoekresultaat...";
                        break;
                    case "replaceTextBox":
                        addButton.IsEnabled = false;
                        if (simpleListBox.SelectedIndex > -1)
                        {
                            replaceButton.IsEnabled = true;
                            replaceButton.IsDefault = true;
                        }
                        else
                        {
                            MessageBox.Show("Selecteer eerst een item","Geen item geselecteerd!",MessageBoxButton.OK,MessageBoxImage.Warning);
                        }
                        searchButton.IsEnabled = false;

                        addTextBox.Text = "";
                        searchTextBox.Text = "";
                        searchLabel.Content = "Zoekresultaat...";
                        break;
                    case "searchTextBox":
                        addButton.IsEnabled = false;
                        replaceButton.IsEnabled = false;
                        searchButton.IsEnabled = true;
                        searchButton.IsDefault = true;

                        addTextBox.Text = "";
                        replaceTextBox.Text = "";
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillListBox();
        }
    }
}