namespace AutenticacionNetCore.Types
{
    public class ADType
    {
        public bool isValid { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string WIW { get; set; }
    }
    public class KronosType
    {

        public string Nomina { get; set; }

        public string Name { get; set; }

        public string WorkPlace { get; set; }

        public string Ou_Description { get; set; }
    }
    public class DataAutenticate
    {
        public ADType active { get; set; }
        public KronosType kronos { get; set; }
        public IEnumerable<RolesType> Roles { get; set; }
    }
    public class Input
    {
        public string user { get; set; }
        public string pwd { get; set; }

    }
    public class RolesType
    {
        public string Program { get; set; }
        public IEnumerable<string> Roles { get; set; }

    }
}
