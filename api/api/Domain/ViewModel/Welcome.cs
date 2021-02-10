using System;

namespace api.Domain.ViewModel
{
    public record Welcome
    {
        public string Message
        { 
          get
          { 
            return "Bem vindo a API";
          }
        }

        public string Documentation
        { 
          get
          { 
            return "https://localhost:5001/swagger";
          }
        }

        
    }
}
