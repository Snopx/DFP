namespace Application.AutoMapper
{
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        public EntityDto()
        {

        }
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
        public TPrimaryKey Id { get; set; }
    }
}
