public class Candidatura
{
    public int Id { get; set; }
    public string Vaga { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string CurriculoPath { get; set; }
    public DateTime DataEnvio { get; set; } = DateTime.Now;
}
