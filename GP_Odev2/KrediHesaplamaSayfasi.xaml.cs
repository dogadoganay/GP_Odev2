namespace GP_Odev2
{
    public partial class KrediHesaplamaSayfasi : ContentPage
    {
        const double KKDF_IHTIYAC = 0.15; // %15
        const double BSMV_IHTIYAC = 0.10; // %10

        const double KKDF_KONUT = 0.0;
        const double BSMV_KONUT = 0.0;

        const double KKDF_TASIT = 0.15; // %15
        const double BSMV_TASIT = 0.05; // %5

        const double KKDF_TICARI = 0.0; // Ticari Kredi KKDF %0
        const double BSMV_TICARI = 0.05;

        public KrediHesaplamaSayfasi()
        {
            InitializeComponent();
            // Varsayýlan olarak ilk kredi türünü seçili hale getirelim.
            PickerKrediTuru.SelectedIndex = 0;
        }

        private void SliderVade_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            // Vade deðerini tam sayýya yuvarlayalým
            int vade = (int)SliderVade.Value;
            LabelVadeDegeri.Text = $"{vade} Ay";
        }

        private async void ButtonHesapla_Clicked(object sender, EventArgs e)
        {
            if (PickerKrediTuru.SelectedItem == null ||
                !double.TryParse(EntryTutar.Text, out double tutar) || tutar <= 0 ||
                !double.TryParse(EntryFaizOrani.Text, out double yillikFaizOrani) || yillikFaizOrani <= 0)
            {
                await DisplayAlert("Hata", "Lütfen tüm alanlara geçerli pozitif sayýlar giriniz ve kredi türünü seçiniz.", "Tamam");
                return;
            }

            int vade = (int)SliderVade.Value;

            // Yýllýk faizi aylýk faize çevir
            double aylikFaizOrani = yillikFaizOrani / 100.0 / 12.0;

            // Kredi türüne göre vergileri al
            double kkdfOrani = 0.0;
            double bsmvOrani = 0.0;
            string secilenKrediTuru = PickerKrediTuru.SelectedItem.ToString();

            if (secilenKrediTuru == "Ýhtiyaç Kredisi")
            {
                kkdfOrani = KKDF_IHTIYAC;
                bsmvOrani = BSMV_IHTIYAC;
            }
            else if (secilenKrediTuru == "Konut Kredisi")
            {
                kkdfOrani = KKDF_KONUT;
                bsmvOrani = BSMV_KONUT;
            }
            else if (secilenKrediTuru == "Taþýt Kredisi")
            {
                kkdfOrani = KKDF_TASIT;
                bsmvOrani = BSMV_TASIT;
            }
            else if (secilenKrediTuru == "Ticari Kredisi")
            {
                kkdfOrani = KKDF_TICARI;
                bsmvOrani = BSMV_TICARI;
            }

            double brutFaiz = aylikFaizOrani * (1 + kkdfOrani + bsmvOrani);

            double pow = Math.Pow(1 + brutFaiz, vade);
            double taksit = (pow * brutFaiz) / (pow - 1) * tutar;

            double toplamOdeme = taksit * vade;
            double toplamFaiz = toplamOdeme - tutar;

            LabelAylikTaksit.Text = $"{taksit:N2} TL";       // N2: 2 ondalýk basamaklý sayý formatý
            LabelToplamOdeme.Text = $"{toplamOdeme:N2} TL";
            LabelToplamFaiz.Text = $"{toplamFaiz:N2} TL";
        }


        private void PickerKrediTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ýhtiyaç duyarsanýz burada otomatik hesaplama tetikleyebilirsiniz, 
            // ancak ödevde Buton'a basýnca hesapla dendiði için þimdilik boþ býrakýyoruz.
        }
    }
}
