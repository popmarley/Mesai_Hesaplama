using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojistikOtomasyon
{
	public class Program
	{
		static void Main(string[] args)
		{
			TimeZoneInfo almanya = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");// Orta Avrupa Saati'ni (Almanya'nın da dahil olduğu zaman dilimi) elde ediyoruz.
			DateTime simdiAlmanya = TimeZoneInfo.ConvertTime(DateTime.UtcNow, almanya);// Mevcut UTC saati, Orta Avrupa Saatine dönüştürüyoruz.

			Console.WriteLine($"Mevcut Almanya saati: {simdiAlmanya.ToShortTimeString()}");

			TimeSpan girisSaati;
			do //kullanıcı doğru girdi yapana kadar tekrar soruyoruz
			{
				Console.WriteLine("Personelin giriş saati (HH:mm formatında): ");
				girisSaati = TimeSpan.Parse(Console.ReadLine());

				if (girisSaati.TotalHours > 24)
				{
					Console.WriteLine("Lütfen 24 saat formatında bir saat girin.");
				
				}

			} while (girisSaati.TotalHours > 24);

			TimeSpan cikisSaati;
			do
			{
				Console.WriteLine("Personelin çıkış saati (HH:mm formatında): ");
				cikisSaati = TimeSpan.Parse(Console.ReadLine());

				if (cikisSaati < girisSaati)
				{
					Console.WriteLine("Çıkış saati giriş saatinden önce olamaz!");
					
				}

				if (cikisSaati.TotalHours > 24)
				{
					Console.WriteLine("Lütfen 24 saat formatında bir saat girin.");
					
				}

			} while (cikisSaati < girisSaati || cikisSaati.TotalHours > 24);

			int molaSuresi;
			do
			{
				Console.WriteLine("Personelin toplam mola süresi (dakika olarak): ");
				molaSuresi = int.Parse(Console.ReadLine());

				if (molaSuresi > (cikisSaati - girisSaati).TotalMinutes)
				{
					Console.WriteLine("Mola süresi toplam çalışma süresinden fazla olamaz!");
				
				}

			} while (molaSuresi > (cikisSaati - girisSaati).TotalMinutes);

			HesaplaMesai(girisSaati, cikisSaati, molaSuresi); // Mesai hesaplama fonksiyonunu çağırıyoruz.
		}

		public static void HesaplaMesai(TimeSpan giris, TimeSpan cikis, int mola)
		{
			TimeSpan normalCikis = new TimeSpan(17, 0, 0); // Normal çıkış saati olarak 17:00 kabul ediyoruz.
			TimeSpan calismaSuresi = cikis - giris - TimeSpan.FromMinutes(mola);

			if (cikis > normalCikis)
			{
				TimeSpan mesaiSuresi = cikis - normalCikis;
				double odul = mesaiSuresi.TotalHours * 50; // 50 TL/saat

				Console.WriteLine($"Personel {mesaiSuresi.Hours} saat {mesaiSuresi.Minutes} dakika mesai yapmıştır.");
				Console.WriteLine($"Personel mesai için {odul} TL ödüllendirilmiştir.");
				Console.ReadLine();
			}
			else
			{
				Console.WriteLine("Personel mesai yapmamıştır.");
				
			}

			Console.WriteLine($"Personel toplam {calismaSuresi.Hours} saat {calismaSuresi.Minutes} dakika çalışmıştır.");
			Console.ReadLine();
		}
	}
	
}
