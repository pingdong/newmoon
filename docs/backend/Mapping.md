[Back](../dotnet-Backend.md)

> ## Mapping 

Using the below codes to define object mapping in its projects, Events service automatically discovers and registers mapping setting.

```
public class AutoMapperProfileConfiguration : Profile
{
    public AutoMapperProfileConfiguration()
    : this("MyProfile")
    {
    }
    protected AutoMapperProfileConfiguration(string profileName)
    : base(profileName)
    {
        CreateMap<UserViewModel, User>();
    }
}
```