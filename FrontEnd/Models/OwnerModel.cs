namespace FrontEnd.Models
{
    public class OwnerModel
    {
        private string _firstname;
        private string _lastname;
        private string? _fullName = string.Empty;
        public int Id { get; set; }
        public string fornavn 
        { 
            get
            {
                return _firstname;
            }
            set 
            {
                _firstname = value;
                _fullName = value + _fullName;
            }
        }
        public string efternavn 
        { 
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                _fullName = _fullName + " " + value;
            }
        }
        public string fuldenavn 
        { 
            get { return _fullName; }
            //private set; 
        }
        public string adresse { get; set; }
        public string postnr { get; set; }
        public string by { get; set; }
        
    }
}
