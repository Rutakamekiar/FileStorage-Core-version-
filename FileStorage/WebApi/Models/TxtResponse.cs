using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TxtResponse
    {
        public TxtResponse(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}