using Core.Entities;

namespace h2dYatırım.Entities
{
    public class User:IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        //        {
        //  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        //  "firstName": "Hasan Hüseyin",
        //  "lastName": "DURSUN",
        //  "identificationNumber": "20110226442",
        //  "phoneNumber": "+905456704103",
        //  "email": "hasanhuseyindursun70@gmail.com",
        //  "password": "753159hH"
        //}

}

}
