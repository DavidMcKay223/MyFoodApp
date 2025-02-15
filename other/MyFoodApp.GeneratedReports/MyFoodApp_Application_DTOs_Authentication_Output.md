# Directory: DTOs\Authentication

## File: ForgotPasswordDto.cs

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs.Authentication
{
    public class ForgotPasswordDto
    {
        public string? Email { get; set; }
    }
}

```

## File: LoginUserDto.cs

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs.Authentication
{
    public class LoginUserDto
    {
        public string? UsernameOrEmail { get; set; }
        public string? Password { get; set; }
    }
}

```

## File: RegisterUserDto.cs

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs.Authentication
{
    public class RegisterUserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}

```

## File: UserDto.cs

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.DTOs.Authentication
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

```

