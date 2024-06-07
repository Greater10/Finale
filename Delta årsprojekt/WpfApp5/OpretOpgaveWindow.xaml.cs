using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp5.Models;
using WpfApp5.Data;

namespace WpfApp5
{
    public partial class Opgave : Page
    {
        private OpgaveRepository _repository;
        private OpgaveModel _currentOpgave;

        public Opgave()
        {
            InitializeComponent();
            _repository = new OpgaveRepository();
            _currentOpgave = new OpgaveModel();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void Anuller_Click(object sender, RoutedEventArgs e)
        {
            
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void Opret_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                _currentOpgave.CPR = CPRTextBox.Text;
                _currentOpgave.Patientnavn = PatientnavnTextBox.Text;
                _currentOpgave.Stue = StueTextBox.Text;
                _currentOpgave.Isolation = IsolationYesRadioButton.IsChecked ?? false;
                _currentOpgave.Prøver = GetSelectedProver();
                _currentOpgave.SærligeForhold = SærligeForholdTextBox.Text;
                _currentOpgave.Inaktiv = InaktivCheckBox.IsChecked ?? false;
                _currentOpgave.Prioritet = GetSelectedPrioritet(); 
                _currentOpgave.Dato = DatoDatePicker.SelectedDate ?? DateTime.Now;
                _currentOpgave.Kommentar = KommentarTextBox.Text;

                // Midlertidig skal Afdeling være et tal
                if (int.TryParse(AfdelingTextBox.Text, out int afdelingId))
                {
                    _currentOpgave.AfdelingID = afdelingId;
                }
                else
                {
                    MessageBox.Show("AfdelingID must be a number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                _repository.AddOpgave(_currentOpgave);

                MessageBox.Show("Opgave oprettet!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetSelectedPrioritet()
        {
            if (NormalRadioButton.IsChecked == true)
                return "1";  // Normal er 1
            if (HasterRadioButton.IsChecked == true)
                return "2";  // Haster er 2
            if (KritiskRadioButton.IsChecked == true)
                return "3";  // Kritisk er 3
            return string.Empty;
        }

        private string GetSelectedProver()
        {
            var proverList = new List<string>();
            if (BlodprøveCheckBox.IsChecked == true)
                proverList.Add("Blodprøve");
            if (EKGCheckBox.IsChecked == true)
                proverList.Add("EKG");
            if (GlukoseCsvCheckBox.IsChecked == true)
                proverList.Add("Glukose-Csv");
            if (POCTPCRCheckBox.IsChecked == true)
                proverList.Add("POCT-PCR");
            if (SærligeOBSCheckBox.IsChecked == true)
                proverList.Add("Særlige OBS");
            if (MedicinafhengigCheckBox.IsChecked == true)
                proverList.Add("Medicinafhængig prøvetagning");
            return string.Join(", ", proverList);
        }


        // Ryder efter korrekt oprettet opgave
        private void ClearForm()
        {
            CPRTextBox.Clear();
            PatientnavnTextBox.Clear();
            StueTextBox.Clear();
            IsolationYesRadioButton.IsChecked = false;
            IsolationNoRadioButton.IsChecked = false;
            BlodprøveCheckBox.IsChecked = false;
            EKGCheckBox.IsChecked = false;
            GlukoseCsvCheckBox.IsChecked = false;
            POCTPCRCheckBox.IsChecked = false;
            SærligeOBSCheckBox.IsChecked = false;
            MedicinafhengigCheckBox.IsChecked = false;
            InaktivCheckBox.IsChecked = false;
            NormalRadioButton.IsChecked = false;
            HasterRadioButton.IsChecked = false;
            KritiskRadioButton.IsChecked = false;
            DatoDatePicker.SelectedDate = null;
            KommentarTextBox.Clear();
            AfdelingTextBox.Clear();
            SærligeForholdTextBox.Clear();
        }
    }
}






