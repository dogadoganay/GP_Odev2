namespace GP_Odev2
{
    public partial class RenkSeciciSayfasi : ContentPage
    {
        public RenkSeciciSayfasi()
        {
            InitializeComponent();
            // Baþlangýç renklerini ayarla ve ilk hesaplamayý yap
            Slider_ValueChanged(null, null);
        }

        // Kýrmýzý, Yeþil veya Mavi Slider deðiþtiðinde çalýþýr
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            // 1. Slider deðerlerini al (0-255 arasý)
            int kirmizi = (int)SliderKirmizi.Value;
            int yesil = (int)SliderYesil.Value;
            int mavi = (int)SliderMavi.Value;

            // 2. Etiketleri güncelle
            LabelKirmiziDegeri.Text = kirmizi.ToString();
            LabelYesilDegeri.Text = yesil.ToString();
            LabelMaviDegeri.Text = mavi.ToString();

            // 3. Rengi oluþtur ve arka planý dinamik olarak güncelle 
            Color yeniRenk = Color.FromRgb(kirmizi, yesil, mavi);
            this.BackgroundColor = yeniRenk;

            // 4. Renk kodunu #RRGGBB formatýnda oluþtur ve göster 
            string hexKirmizi = kirmizi.ToString("X2"); // X2: 2 haneli Hex
            string hexYesil = yesil.ToString("X2");
            string hexMavi = mavi.ToString("X2");

            string renk_kodu = $"#{hexKirmizi}{hexYesil}{hexMavi}";
            EntryRenkKodu.Text = renk_kodu;
        }

        // Kopyala Butonu: Renk kodunu panoya kopyalar 
        private async void ButtonKopyala_Clicked(object sender, EventArgs e)
        {
            string renk_kodu = EntryRenkKodu.Text;

            // Panoya kopyalama
            await Clipboard.SetTextAsync(renk_kodu);

            // Kullanýcýya bildirim yapmak için, dinamik olarak kopyaladýðýmýz renk_kodu deðiþkenini gösteriyoruz
            await DisplayAlert("Kopyalandý", $"Renk kodu panoya kopyalandý: {renk_kodu}", "Tamam");
        }

        // Rastgele Renk Butonu: Slider'larý rastgele deðerlere ayarlayarak çalýþýyor
        private void ButtonRastgeleRenk_Clicked(object sender, EventArgs e)
        {
            Random rastgele = new Random();

            // Slider deðerlerini rastgele ayarla (0-255)
            // Slider deðerinin deðiþmesi otomatik olarak ValueChanged'i tetikler.
            SliderKirmizi.Value = rastgele.Next(256);
            SliderYesil.Value = rastgele.Next(256);
            SliderMavi.Value = rastgele.Next(256);

            // Slider'larý ayarladýðýmýz için Slider_ValueChanged metodu otomatik çalýþacak ve 
            // rengi, etiketleri ve kodu güncelleyecektir.
        }
    }
}
