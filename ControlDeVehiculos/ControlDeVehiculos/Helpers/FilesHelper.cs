using System;
using System.Collections.Generic;
using System.Text;

namespace ControlDeVehiculos.Helpers
{
    using System.IO;
    public class FilesHelpers
    {
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
