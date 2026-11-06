using System;
using System.Collections.Generic;

namespace MBGyey;

public class Sekolah
{
    public string IdSekolah { get; private set; }
    public string NamaSekolah { get; private set; }
    public int JumlahPaketDiterima { get; private set; }
    public Sekolah(string idSekolah, string namaSekolah)
    {
        IdSekolah = idSekolah;
        NamaSekolah = namaSekolah;
        JumlahPaketDiterima = 0;
    }
    public void TampilInfo()
    {
        Console.WriteLine($"ID Sekolah             : {IdSekolah}");
        Console.WriteLine($"Nama Sekolah           : {NamaSekolah}");
        Console.WriteLine($"Jumlah Paket Diterima  : {JumlahPaketDiterima}");
    }
    public void TambahPaketDiterima(int jumlah)
    {
        if (jumlah > 0)
            JumlahPaketDiterima += jumlah;
    }
}
public abstract class PaketMBG
{
    private string idPaket;
    private string namaPaket;
    private int stokDapur;
    public string IdPaket
    {
        get => idPaket;
        set => idPaket = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("ID paket tidak boleh kosong.") : value;
    }
    public string NamaPaket
    {
        get => namaPaket;
        set => namaPaket = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Nama paket tidak boleh kosong.") : value;
    }

    public int StokDapur => stokDapur;
    protected PaketMBG(string idPaket, string namaPaket, int stokDapur)
    {
        if (stokDapur < 0)
            throw new ArgumentOutOfRangeException(nameof(stokDapur), "Stok tidak boleh Negatif.");

        this.idPaket = idPaket;
        this.namaPaket = namaPaket;
        this.stokDapur = stokDapur;
    }
    public bool KurangiStok(int jumlah)
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("Gagal: jumlah pengulangan stok harus lebih dari 0.");
            return false;
        }
        if (jumlah > stokDapur)
        {
            Console.WriteLine($"Gagal: stok tidak cukup. stok tersedia: {stokDapur}.");
            return false;
        }

        stokDapur -= jumlah;
        return true;
    }
    public void TampilkanInfoPaket()
    {
        Console.WriteLine($"ID Paket   : {IdPaket}");
        Console.WriteLine($"Nama Paket : {NamaPaket}");
        Console.WriteLine($"Stok Dapur : {StokDapur}");
    }
    public abstract int HitungBiaya(int jumlah);
}
public class PaketReguler : PaketMBG
{
    public const int HargaPerPaket = 15000;
    public PaketReguler(string id, string nama, int stok)
        : base(id, nama, stok) { }
    public override int HitungBiaya(int jumlah)
    {
        if (jumlah < 0) throw new ArgumentOutOfRangeException(nameof(jumlah));
        return jumlah * HargaPerPaket;
    }
    public void TampilkanIsi()
    {
        Console.WriteLine("Isi paket: nasi, ayam, sayur, dan buah.");
    }
}
public class PaketKhusus : PaketMBG
{
    public const int HargaPerPaket = 20000;
    public string JenisDiet { get; set; }
    public PaketKhusus(string id, string nama, int stok, string jenisLain)
        : base(id, nama, stok)
    {
        jenisLain = jenisLain;
    }
    public override int HitungBiaya(int jumlah)
    {
        if (jumlah < 0) throw new ArgumentOutOfRangeException(nameof(jumlah));
        return jumlah * HargaPerPaket;
    }
}
public interface DapatDidistribusikan
{
    void KirimPaket();
}
public class PengelolaMBG
{
    private readonly List<Sekolah> daftarSekolah = new();
    public void TambahSekolah(Sekolah sekolah) => daftarSekolah.Add(sekolah);
    public void TampilkanDataMBG()
    {
        Console.WriteLine("\nData Sekolah");
        foreach (Sekolah sekolah in daftarSekolah) ;
    }
}
public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(" Sistem Distribusi Paket MBG");

        Sekolah sekolah1 = new Sekolah("S001", "SMA Negeri 1 Lamongan");
        PaketMBG reguler = new PaketReguler("P001", "Paket Reguler", 100); ;

        Console.WriteLine("\nProgram selesai. Tekan Enter untuk keluar.");
    }
}