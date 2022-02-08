namespace Nagad.Models
{
    public abstract class BaseModel
    {
        public IRequester Requester { get; internal set; }
        protected BaseModel() { }
        public string Object { get; set; }
        public string Id { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var another = (BaseModel)obj;

            return this.Object == another.Object && this.Id == another.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Object.GetHashCode();
                hash = hash * 23 + Id.GetHashCode();
                return hash;
            }
        }
    }
}
