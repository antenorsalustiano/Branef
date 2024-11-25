namespace BRANEF.Domain.Entities.Base
{
    public class EntidadeBase
    {
        public int Id { get; set; }

        public bool IsNew => this.Id < 1;
    }
}
