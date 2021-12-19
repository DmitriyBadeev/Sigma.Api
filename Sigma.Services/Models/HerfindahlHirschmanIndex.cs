namespace Sigma.Services.Models
{
    public record HerfindahlHirschmanIndex(decimal Value, HerfindahlHirschmanIndexInterpretation Interpretation);

    public enum HerfindahlHirschmanIndexInterpretation
    {
        Excellent = 0,
        Good = 1,
        Normal = 2,
        Bad = 3,
        Terrible = 4
    }
}