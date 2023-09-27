using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Processor.S3
{
    public interface IS3Operations 
    {
         void PutObject();
    }
}
