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

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] numbers;
        private int[] _array;
        private double[] _negativeArray;
        private Dictionary<string, List<string>> phoneBook = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        private List<string> queries = new List<string>();
        public MainWindow ()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbElement.Text))
            {
                lbList.Items.Add(tbElement.Text);
                tbElement.Clear();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lbList.SelectedItem != null)
            {
                lbList.Items.Remove(lbList.SelectedItem);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int[] numbers = lbList.Items.OfType<string>().Select(int.Parse).ToArray();
            int index = Array.FindIndex(numbers, n => n % 2 == 0);

            if (index >= 0)
            {
                Result.Text = $"Индекс первого четного элемента: {index}";
            }
            else
            {
                Result.Text = "Четные элементы не найдены.";
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbElementQueue.Text, out int n) && n > 0)
            {
                Random rand = new Random();
                _array = new int[n];

                for (int i = 0; i < n; i++)
                {
                    _array[i] = rand.Next(1, 101);
                }

                lbQueue.Items.Clear();
                foreach (var number in _array)
                {
                    lbQueue.Items.Add(number);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите допустимое количество элементов.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (_array == null || _array.Length == 0)
            {
                MessageBox.Show("Сначала создайте массив.");
                return;
            }

            int maxIndex = Array.IndexOf(_array, _array.Max());
            if (maxIndex == 0)
            {
                MessageBox.Show("Первый элемент уже является максимальным.");
                return;
            }

            int temp = _array[0];
            _array[0] = _array[maxIndex];
            _array[maxIndex] = temp;

            lbQueue.Items.Clear();
            foreach (var number in _array)
            {
                lbQueue.Items.Add(number);
            }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbElementFere.Text, out int n) && n > 0)
            {
                Random rand = new Random();
                _negativeArray = new double[n];

                for (int i = 0; i < n; i++)
                {
                    _negativeArray[i] = rand.Next(-100, 101); // Генерация случайного целого числа от -100 до 100
                }

                lbFere.Items.Clear();
                foreach (var number in _negativeArray)
                {
                    lbFere.Items.Add(number);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите допустимое количество элементов.");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (_negativeArray == null || _negativeArray.Length == 0)
            {
                MessageBox.Show("Сначала создайте массив.");
                return;
            }

            int negativeCount = _negativeArray.Count(number => number < 0);
            tbResult.Text = $"Количество отрицательных элементов: {negativeCount}";
        }
        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = tbEntryPhoneNumber.Text.Trim();
            string name = tbEntryName.Text.Trim();

            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Пожалуйста, введите и номер телефона, и имя.");
                return;
            }

            if (!phoneBook.ContainsKey(name))
            {
                phoneBook[name] = new List<string>();
            }
            phoneBook[name].Add(phoneNumber);

            lbEntries.Items.Add($"{phoneNumber} {name}");
        }

        private void AddQuery_Click(object sender, RoutedEventArgs e)
        {
            string queryName = tbQueryName.Text.Trim();

            if (string.IsNullOrEmpty(queryName))
            {
                MessageBox.Show("Пожалуйста, введите имя для поиска.");
                return;
            }

            queries.Add(queryName);
            lbQueries.Items.Add(queryName);
        }

        private void SearchEntries_Click(object sender, RoutedEventArgs e)
        {
            tbResults.Text = string.Empty;

            foreach (string query in queries)
            {
                if (phoneBook.ContainsKey(query))
                {
                    string numbers = string.Join(" ", phoneBook[query]);
                    tbResults.Text += $"{numbers}\n";
                }
                else
                {
                    tbResults.Text += "абонент не найден\n";
                }
            }
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}