using ModalExample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModalExample.Models
{
    public class IndexVM
    {
        public List<Volunteer> vols { get; set; }

        [DataType(DataType.Time)]
        public DateTime dateBox { get; set; }
    }
}