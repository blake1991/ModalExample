using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModalExample.Model
{
    public class Volunteer
    {

        public int Id { get; set; } = 1;
        public string firstName { get; set; } 
        public string lastName { get; set; }
        public string address { get; set; }

        public void UpdateVolunteer(Volunteer vol)
        {
            this.firstName = vol.firstName;
            this.lastName = vol.lastName;
            this.address = vol.address;
        }
    }

    public class Volunteers
    {
        public static List<Volunteer> vols = new List<Volunteer>()
        {
            new Volunteer
            {
                //intentionally left blank to simulate database. this is index 0
                Id = 0
            },

            new Volunteer
            {
                Id = 1,
                firstName = "Dave",
                lastName = "Jansen",
                address = "12345 Nowhere Dr."
            },
            new Volunteer
            {
                Id = 2,
                firstName = "Namey",
                lastName = "McNameFace",
                address = "23456 Nowhere Dr."
            }

        };
        

    }

}


