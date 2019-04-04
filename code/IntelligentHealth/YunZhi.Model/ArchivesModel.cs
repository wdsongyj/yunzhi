using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YunZhi.Model
{
    public class ArchivesModel
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _sex;

        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        private DateTime _birthday;

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        private string _idCard;

        public string IdCard
        {
            get { return _idCard; }
            set { _idCard = value; }
        }
        private string _tel;

        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _addressNew;

        public string AddressNew
        {
            get { return _addressNew; }
            set { _addressNew = value; }
        }
    }
}
