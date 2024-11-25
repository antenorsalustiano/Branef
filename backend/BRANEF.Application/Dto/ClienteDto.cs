namespace BRANEF.Application.Dto
{
	public class ClienteDto
	{
		public int Id { get; }
		public string Nome { get; }
		public int EmpresaPorteId { get; }
		public string EmpresaPorte { get; }

		// Construtor com os parâmetros necessários
		public ClienteDto(int id, string nome, int empresaPorteId, string empresaPorte)
		{
			Id = id;
			Nome = nome;
			EmpresaPorteId = empresaPorteId;
			EmpresaPorte = empresaPorte;
		}
	}
}
