using System.Windows;
using System.Windows.Controls;

namespace MarcosSoft
{
    /// <summary>
    /// Lógica de interacción para UCTssCalculador.xaml
    /// </summary>
    public partial class UCTssCalculador : UserControl
    {
        public UCTssCalculador()
        {
            InitializeComponent();
        }


      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var calculator = new TssCalculator();
            txtresult.Text = calculator.GetCalculation(txtpulsacionesmedias.Text, txtfthr.Text, txtminutos.Text);
            
        }
    }
}
