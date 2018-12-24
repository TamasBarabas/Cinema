namespace Model.Specifications
{
    public abstract class AbstractRangeSpecification<T> : Specification<T>
    {
        protected readonly int minimum;
        protected readonly int maximum;

        public AbstractRangeSpecification(int? minimum, int? maximum)
        {
            this.minimum = minimum.HasValue ? minimum.Value : int.MinValue;
            this.maximum = maximum.HasValue ? maximum.Value : int.MaxValue;
        }
    }
}
