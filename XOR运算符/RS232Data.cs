using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseModule;
namespace XOR运算符
{
    public class RS232Data:BaseModule.BaseModel
    {
        public string Description { get; set; }
        public string TextData { get; set; }
        public bool Select { get; set; }
    }
}
