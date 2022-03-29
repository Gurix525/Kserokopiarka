using DevicesGroup;
public class Program
{
    public static void Main()
    {
        Console.ReadLine();

        Copier copier = new Copier();
        IDocument document;

        copier.PowerOn();

        copier.Scan(out document, IDocument.FormatType.JPG);
        copier.Scan(out document, IDocument.FormatType.TXT);
        copier.Scan(out document, IDocument.FormatType.PDF);
    }
}