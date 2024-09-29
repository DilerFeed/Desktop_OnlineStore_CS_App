using System;
using System.Windows;
using System.Windows.Controls;

namespace ShopApp
{
    public partial class CheckoutWindow : Window
    {
        private readonly DatabaseService _databaseService = new DatabaseService();

        private UIElement originalContent;

        public CheckoutWindow()
        {
            InitializeComponent();
            LoadCartItems();
            originalContent = (UIElement)((ScrollViewer)this.Content).Content;
            PostalDeliveryRadioButton.Checked += OnDeliveryOptionChanged;
            StorePickupRadioButton.Checked += OnDeliveryOptionChanged;
            SpecialOrderCheckBox.Checked += OnSpecialOrderChecked;
            SpecialOrderCheckBox.Unchecked += OnSpecialOrderUnchecked;
        }

        private void LoadCartItems()
        {
            CheckoutListView.ItemsSource = CartService.GetCartItems();
        }

        private void OnDeliveryOptionChanged(object sender, RoutedEventArgs e)
        {
            if (PostalDeliveryRadioButton.IsChecked == true)
            {
                PostalOptionsPanel.Visibility = Visibility.Visible;
                StoreAddressPanel.Visibility = Visibility.Collapsed;
                SpecialOrderCheckBox.Visibility = Visibility.Collapsed;
                SpecialOrderRequestsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                PostalOptionsPanel.Visibility = Visibility.Collapsed;
                StoreAddressPanel.Visibility = Visibility.Visible;
                SpecialOrderCheckBox.Visibility = Visibility.Visible;
                SpecialOrderRequestsPanel.Visibility = SpecialOrderCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void OnSpecialOrderChecked(object sender, RoutedEventArgs e)
        {
            SpecialOrderRequestsPanel.Visibility = Visibility.Visible;
        }

        private void OnSpecialOrderUnchecked(object sender, RoutedEventArgs e)
        {
            SpecialOrderRequestsPanel.Visibility = Visibility.Collapsed;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Please enter your name.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Please enter your phone number.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Please enter your email.");
                return false;
            }

            if (PostalDeliveryRadioButton.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(DeliveryCompanyTextBox.Text))
                {
                    MessageBox.Show("Please enter the delivery company.");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(DeliveryAddressTextBox.Text))
                {
                    MessageBox.Show("Please enter the delivery address.");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(PostOfficeTextBox.Text))
                {
                    MessageBox.Show("Please enter the post office number.");
                    return false;
                }
            }
            else if (StorePickupRadioButton.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(StoreAddressTextBox.Text))
                {
                    MessageBox.Show("Please enter the store address.");
                    return false;
                }

                if (SpecialOrderCheckBox.IsChecked == true && string.IsNullOrWhiteSpace(SpecialRequestsTextBox.Text))
                {
                    MessageBox.Show("Please enter special request for your order.");
                    return false;
                }
            }

            return true;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            ShowFinalOrderView();
        }

        private void ShowFinalOrderView()
        {
            // Clearing ScrollViewer content
            ScrollViewer scrollViewer = (ScrollViewer)this.Content;
            scrollViewer.Content = null;

            StackPanel finalOrderPanel = new StackPanel { Margin = new Thickness(10) };

            TextBlock finalOrderText = new TextBlock { Text = "Your Order:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 10) };
            finalOrderPanel.Children.Add(finalOrderText);

            foreach (var item in CartService.GetCartItems())
            {
                TextBlock itemText = new TextBlock { Text = $"{item.Name} - {item.Price.ToString("C", new System.Globalization.CultureInfo("en-US"))}" };
                finalOrderPanel.Children.Add(itemText);
            }

            finalOrderPanel.Children.Add(new TextBlock { Text = $"Name: {NameTextBox.Text}" });
            finalOrderPanel.Children.Add(new TextBlock { Text = $"Phone: {PhoneTextBox.Text}" });
            finalOrderPanel.Children.Add(new TextBlock { Text = $"Email: {EmailTextBox.Text}" });

            if (PostalDeliveryRadioButton.IsChecked == true)
            {
                finalOrderPanel.Children.Add(new TextBlock { Text = $"Delivery Company: {DeliveryCompanyTextBox.Text}" });
                finalOrderPanel.Children.Add(new TextBlock { Text = $"Delivery Address: {DeliveryAddressTextBox.Text}" });
                finalOrderPanel.Children.Add(new TextBlock { Text = $"Post Office Number: {PostOfficeTextBox.Text}" });
            }
            else if (StorePickupRadioButton.IsChecked == true)
            {
                finalOrderPanel.Children.Add(new TextBlock { Text = $"Store Address: {StoreAddressTextBox.Text}" });
                if (SpecialOrderCheckBox.IsChecked == true)
                {
                    finalOrderPanel.Children.Add(new TextBlock { Text = $"Special Request: {SpecialRequestsTextBox.Text}" });
                }
            }

            // Create StackPanel for the buttons
            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };

            // 'Back' button
            Button backButton = new Button
            {
                Content = "Back",
                Style = (Style)FindResource("CustomButtonStyle"),
                Width = 200,
                Height = 40,
                Margin = new Thickness(0, 0, 10, 0)
            };
            backButton.Click += BackButton_Click;

            // Final 'Place Order' or 'Pay and Place Order' button
            Button finalButton = new Button
            {
                Content = PaymentMethodComboBox.SelectedIndex == 0 ? "Place Order" : "Pay and Place Order",
                Style = (Style)FindResource("CustomButtonStyle"),
                Width = 200,
                Height = 40,
                Margin = new Thickness(10, 0, 0, 0)
            };
            finalButton.Click += FinalizeOrder_Click;

            buttonPanel.Children.Add(backButton);
            buttonPanel.Children.Add(finalButton);

            finalOrderPanel.Children.Add(buttonPanel);

            scrollViewer.Content = finalOrderPanel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Recovering initial content of the window
            ScrollViewer scrollViewer = (ScrollViewer)this.Content;
            scrollViewer.Content = originalContent;
        }

        private void FinalizeOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order;
            var cartItems = CartService.GetCartItems();

            if (PostalDeliveryRadioButton.IsChecked == true)
            {
                order = new OnlineOrder
                {
                    CustomerName = NameTextBox.Text,
                    CustomerPhone = PhoneTextBox.Text,
                    CustomerEmail = EmailTextBox.Text,
                    Products = cartItems,
                    DeliveryAddress = DeliveryAddressTextBox.Text,
                    DeliveryCompany = DeliveryCompanyTextBox.Text,
                    PostOfficeNumber = PostOfficeTextBox.Text
                };
            }
            else if (StorePickupRadioButton.IsChecked == true)
            {
                if (SpecialOrderCheckBox.IsChecked == true)
                {
                    order = new SpecialOrder
                    {
                        CustomerName = NameTextBox.Text,
                        CustomerPhone = PhoneTextBox.Text,
                        CustomerEmail = EmailTextBox.Text,
                        Products = cartItems,
                        StoreAddress = StoreAddressTextBox.Text,
                        SpecialRequest = SpecialRequestsTextBox.Text,
                        IsSpecialOrder = true
                    };
                }
                else
                {
                    order = new InStoreOrder
                    {
                        CustomerName = NameTextBox.Text,
                        CustomerPhone = PhoneTextBox.Text,
                        CustomerEmail = EmailTextBox.Text,
                        Products = cartItems,
                        StoreAddress = StoreAddressTextBox.Text,
                        IsSpecialOrder = false
                    };
                }
            }
            else
            {
                MessageBox.Show("Please select a valid delivery method.");
                return;
            }

            try
            {
                _databaseService.AddOrder(order);
                MessageBox.Show("Order placed successfully!");
                CartService.ClearCart();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to place the order: {ex.Message}");
            }
        }
    }
}