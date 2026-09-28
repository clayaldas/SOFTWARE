namespace ClinicApp;

public static class ContactValidator
{
    public static void Validate(string name, string email)
    {
        if (name == null || name == "")
        {
            throw new ArgumentException("El nombre no puede estar vacio.");
        }
        if (email == null || email == "")
        {
            throw new ArgumentException("El email no puede estar vacio.");
        }
        if (!email.Contains("@"))
        {
            throw new ArgumentException("El email debe contener arroba.");
        }
    }
}
