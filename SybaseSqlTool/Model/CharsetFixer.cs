using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SybaseSqlTool.Model
{
    class CharsetFixer
    {

        public void FixCharset()
        {
            
            var iso1Encoding = Encoding.GetEncoding("iso_1");
            iso1Encoding.DecoderFallback = new DecoderFallbackIso();
            iso1Encoding.EncoderFallback = new EncoderFallbackIso();


            var dummy = Encoding.GetEncoding("iso_1");

            if (iso1Encoding == dummy)
            {
                
            }

        }

        
    }

    class DecoderFallbackIso : DecoderFallback
    {
        public override DecoderFallbackBuffer CreateFallbackBuffer()
        {
            throw new NotImplementedException();
        }

        public override int MaxCharCount {
            get { return 2; }
        }
    }


    class EncoderFallbackIso : EncoderFallback
    {
        public override EncoderFallbackBuffer CreateFallbackBuffer()
        {
            throw new NotImplementedException();
        }


        public override int MaxCharCount
        {
            get { return 2; }
        }
    }
}
