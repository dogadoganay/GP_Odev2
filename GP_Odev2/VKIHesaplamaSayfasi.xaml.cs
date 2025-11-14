namespace GP_Odev2
{
    public partial class VKIHesaplamaSayfasi : ContentPage
    {
        // En az ve En çok slider/entry deðerleri
        const double MIN_KILO = 30;
        const double MAX_KILO = 200;
        const double MIN_BOY = 100;
        const double MAX_BOY = 220;

        public VKIHesaplamaSayfasi()
        {
            InitializeComponent();
            // Baþlangýç deðerlerini Entry'lere atadýk.
            // Bu, ayný zamanda Label'larý da CalculateAndDisplayVKI() çaðýrarak güncelleyecektir.
            EntryKilo.Text = SliderKilo.Value.ToString("N0");
            EntryBoy.Text = SliderBoy.Value.ToString("N0");

            CalculateAndDisplayVKI();
        }

        // --- Slider Deðiþtiðinde Çalýþýr ---
        private void KiloBoy_SliderChanged(object sender, ValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;

            if (slider == SliderKilo)
            {
                // SliderKilo deðiþtiyse, EntryKilo'yu Slider'ýn deðeriyle güncelle.
                EntryKilo.Text = slider.Value.ToString("N0");
            }
            else if (slider == SliderBoy)
            {
                // SliderBoy deðiþtiyse, EntryBoy'u Slider'ýn deðeriyle güncelle.
                EntryBoy.Text = slider.Value.ToString("N0");
            }

            // Deðerler senkronize edildikten sonra hesaplamayý yap.
            CalculateAndDisplayVKI();
        }

        // --- Entry Deðiþtiðinde Çalýþýr ---
        private void KiloBoy_EntryChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;

            // 1. Geçerli bir sayý olup olmadýðýný kontrol et
            if (double.TryParse(entry.Text, out double newValue))
            {
                // 2. Minimum/Maksimum aralýkta olup olmadýðýný kontrol et ve düzelt
                if (entry == EntryKilo)
                {
                    newValue = Math.Clamp(newValue, MIN_KILO, MAX_KILO);
                    // Düzeltilen deðeri Slider'a aktar (Bu, KiloBoy_SliderChanged'i tetikleyecektir)
                    SliderKilo.Value = newValue;
                }
                else if (entry == EntryBoy)
                {
                    newValue = Math.Clamp(newValue, MIN_BOY, MAX_BOY);
                    // Düzeltilen deðeri Slider'a aktar (Bu, KiloBoy_SliderChanged'i tetikleyecektir)
                    SliderBoy.Value = newValue;
                }
            }
            // Sayýsal olmayan veya geçersiz bir giriþ varsa, burasý hesaplamayý tetiklemez.
        }

        // --- Hesaplama ve Görüntüleme Metodu (Ana Metot) ---
        private void CalculateAndDisplayVKI()
        {
            // Kilo ve Boy deðerlerini Entry'lerden al.
            if (!double.TryParse(EntryKilo.Text, out double kilo) || kilo < MIN_KILO)
                return;

            if (!double.TryParse(EntryBoy.Text, out double boy_cm) || boy_cm < MIN_BOY)
                return;

            // Etiketleri güncelle (Artýk XAML'de tanýmlý olduklarý için hata vermeyecekler)
            LabelKiloDegeri.Text = $"{kilo:N0} kg";
            LabelBoyDegeri.Text = $"{boy_cm:N0} cm";

            // Formül için Boy'u metreye çevir
            double boy_m = boy_cm / 100.0;

            if (boy_m > 0)
            {
                double vki = kilo / (boy_m * boy_m);

                // 3. Sonuçlarý Etiketlere Yaz
                LabelVKISonucu.Text = $"{vki:N2}"; // N2: 2 ondalýk basamak

                // 4. VKÝ Deðerlendirmesi
                string degerlendirme = GetVKIDegerlendirmesi(vki);
                LabelVKIDegerlendirme.Text = degerlendirme;
            }
        }

        // VKÝ Deðerlendirme Kriterlerini Uygulayan Metot (Deðiþmedi)
        private string GetVKIDegerlendirmesi(double vki)
        {
            if (vki < 16)
            {
                return "Ýleri Düzeyde Zayýf";
            }
            else if (vki >= 16 && vki <= 16.99)
            {
                return "Orta Düzeyde Zayýf";
            }
            else if (vki >= 17 && vki <= 18.49)
            {
                return "Hafif Düzeyde Zayýf";
            }
            else if (vki >= 18.50 && vki <= 24.9)
            {
                return "Normal Kilolu";
            }
            else if (vki >= 25 && vki <= 29.99)
            {
                return "Hafif Þiþman / Fazla Kilolu";
            }
            else if (vki >= 30 && vki <= 34.99)
            {
                return "1. Derecede Obez";
            }
            else if (vki >= 35 && vki <= 39.99)
            {
                return "2. Derecede Obez";
            }
            else // vki >= 40
            {
                return "3. Derecede Obez / Morbid Obez";
            }
        }
    }
}